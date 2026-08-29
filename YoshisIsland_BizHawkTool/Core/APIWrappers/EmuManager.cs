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
                throw EnvironmentException.CoreNotLoaded();
        }

        private static void CheckIfYoshisIslandLoaded()
        {
            CurrentGameVersion = CheckIfPracticeCartLoaded();

            CurrentGameVersion ??= CheckIfOriginalGameLoaded();

            CurrentGameVersion ??= CheckIfAnyHackLoaded();

            if (CurrentGameVersion == null)
                throw EnvironmentException.YINotLoaded();
        }

        private static GameVersionData CheckIfPracticeCartLoaded()
        {
            if (!MemoryManager.FindBytes(MemoryDomain.ROM, YIData.PracticeHackSignature, 0x00, out int? foundAddress))
                return null;

            GameRegion region = (GameRegion)MemoryManager.ReadByte(YIData.SNES_REGION_ROM_ADDRESS, MemoryDomain.ROM);

            MemoryManager.FindBytes(
                MemoryDomain.ROM,
                YIData.PracticeHackSegmentSignature,
                foundAddress.Value + YIData.PracticeHackSegmentSignature.Length,
                out int? segmentEndAddressRel);
            int practiceVersionBytesAdress = foundAddress.Value + (17 * 2);
            int segmentEndAddress = foundAddress.Value + YIData.PracticeHackSegmentSignature.Length + segmentEndAddressRel.Value;
            long[] practiceVersionBytes = MemoryManager.ReadByteRange(
                practiceVersionBytesAdress, MemoryDomain.ROM, (segmentEndAddress - practiceVersionBytesAdress) / 2, 2);
            string version = string.Join("", practiceVersionBytes.Select(b => YIData.PracticeHackStringFontMap[(byte)b]));

            string romHash = MemoryManager.GetDomainHash(MemoryDomain.ROM);

            GameVersionData practiceCartVersionData = GameVersionData.CreatePracticeHackData(region, version, romHash);

            return practiceCartVersionData;
        }

        private static GameVersionData CheckIfOriginalGameLoaded()
        {
            string romHash = MemoryManager.GetDomainHash(MemoryDomain.ROM);

            foreach (GameVersionData versionData in YIData.OriginalVersionDatas)
            {
                if (versionData.RomHash == romHash)
                    return versionData;
            }

            return null;
        }

        private static GameVersionData CheckIfAnyHackLoaded()
        {
            byte[] gameHeaderInfo = MemoryManager.ReadByteRange(
                YIData.SNES_GAME_NAME_ROM_ADDRESS, MemoryDomain.ROM, 28).Select(b => (byte)b).ToArray();

            foreach (KeyValuePair<GameVersionData, byte[]> originalHeaderDataMap in YIData.OriginalHeaderDatas)
            {
                GameVersionData originalVersionData = originalHeaderDataMap.Key;
                byte[] originalHeaderData = originalHeaderDataMap.Value;
                if (((ReadOnlySpan<byte>)gameHeaderInfo).IndexOf(originalHeaderData) != -1)
                {
                    return GameVersionData.CreateGeneralHackData(
                        originalVersionData.Region,
                        originalVersionData.Version,
                        MemoryManager.GetDomainHash(MemoryDomain.ROM),
                        _gameName);
                }
            }

            return null;
        }
    }
}
