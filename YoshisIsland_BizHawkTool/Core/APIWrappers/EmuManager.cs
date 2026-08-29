using BizHawk.Client.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoshisIsland_BizHawkTool
{
    internal static class EmuManager
    {
        private const string NULL_SYSTEM_ID = "NULL";
        private const string CORE_NOT_LOADED_EXCEPTION_MESSAGE = "This external tool should be used with a core loaded. Open the game ROM and run the tool again.";
        private const string YI_NOT_LOADED_EXCEPTION_MESSAGE = "This external tool should only be used for Yoshi's Island (SNES, any version or hack)."; // TODO: Remove "SNES" if I make it work with GBA too

        private static IEmulationApi _emuAPI;
        private static ToolOptions _options;
        private static string _gameName;
        public static GameVersionData CurrentGameVersion;

        internal static void Init(IEmulationApi emuAPI)
        {
            _emuAPI = emuAPI;
            _options = ToolOptions.Instance;
            CheckIfCoreLoaded();
            _gameName = _emuAPI.GetGameInfo().Name;
            CheckIfYoshisIslandLoaded();
        }

        private static void CheckIfCoreLoaded()
        {
            if (_emuAPI.GetSystemId() == NULL_SYSTEM_ID)
                throw new EnvironmentException(CORE_NOT_LOADED_EXCEPTION_MESSAGE);
        }

        private static void CheckIfYoshisIslandLoaded()
        {
            CurrentGameVersion = CheckIfPracticeCartLoaded();

            CurrentGameVersion ??= CheckIfOriginalGameLoaded();

            CurrentGameVersion ??= CheckIfAnyHackLoaded();

            if (CurrentGameVersion == null)
                throw new EnvironmentException(YI_NOT_LOADED_EXCEPTION_MESSAGE);
        }

        private static GameVersionData CheckIfPracticeCartLoaded()
        {
            // TODO: Leave to MemoryManager the responsability of defining the ROM domain
            if (!MemoryManager.FindBytes("CARTROM", YIData.PracticeHackSignature, 0x00, out int? foundAddress))
                return null;

            GameRegion region = (GameRegion)MemoryManager.ReadByteRom(YIData.SNES_REGION_ROM_ADDRESS);

            MemoryManager.FindBytes(
                "CARTROM", YIData.PracticeHackSegmentSignature, foundAddress.Value + YIData.PracticeHackSegmentSignature.Length, out int? segmentEndAddressRel);
            int practiceVersionBytesAdress = foundAddress.Value + (17 * 2);
            int segmentEndAddress = foundAddress.Value + YIData.PracticeHackSegmentSignature.Length + segmentEndAddressRel.Value;
            long[] practiceVersionBytes = MemoryManager.ReadBytesRom(practiceVersionBytesAdress, (segmentEndAddress - practiceVersionBytesAdress) / 2, 2);
            string version = string.Join("", practiceVersionBytes.Select(b => YIData.PracticeHackStringFontMap[(byte)b]));

            GameVersionData practiceCartVersionData = GameVersionData.CreatePracticeHackData(region, version, MemoryManager.GetRomHash());

            return practiceCartVersionData;
        }

        private static GameVersionData CheckIfOriginalGameLoaded()
        {
            string romHash = MemoryManager.GetRomHash();

            foreach (GameVersionData versionData in YIData.OriginalVersionDatas)
            {
                if (versionData.RomHash == romHash)
                    return versionData;
            }

            return null;
        }

        private static GameVersionData CheckIfAnyHackLoaded()
        {
            byte[] gameHeaderInfo = MemoryManager.ReadBytesRom(YIData.SNES_GAME_NAME_ROM_ADDRESS, 28).Select(b => (byte)b).ToArray();

            foreach (KeyValuePair<GameVersionData, byte[]> originalHeaderDataMap in YIData.OriginalHeaderDatas)
            {
                GameVersionData originalVersionData = originalHeaderDataMap.Key;
                byte[] originalHeaderData = originalHeaderDataMap.Value;
                if (((ReadOnlySpan<byte>)gameHeaderInfo).IndexOf(originalHeaderData) != -1)
                    return GameVersionData.CreateGeneralHackData(originalVersionData.Region, originalVersionData.Version, MemoryManager.GetRomHash(), _gameName);
            }

            return null;
        }
    }
}
