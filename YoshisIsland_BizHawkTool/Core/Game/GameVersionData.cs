using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoshisIsland_BizHawkTool
{
    internal class GameVersionData
    {
        internal GameRegion Region { get; }
        internal string Version { get; }
        internal bool IsHack { get; }
        internal bool IsPracticeHack { get; }
        internal string RomHash { get; }
        internal string GameName { get; }

        private GameVersionData(GameRegion region, string version, bool isHack, bool isPracticeHack, string romHash, string gameName)
        {
            Region = region;
            Version = version;
            IsHack = isHack;
            IsPracticeHack = isPracticeHack;
            RomHash = romHash;
            GameName = gameName;
        }

        internal static GameVersionData CreateOriginalData(GameRegion region, string version, string romHash)
        {
            return new GameVersionData(region, version, false, false, romHash, YIData.YOSHIS_ISLAND_NAME);
        }

        internal static GameVersionData CreatePracticeHackData(GameRegion region, string version, string romHash)
        {
            return new GameVersionData(region, version, true, true, romHash, $"{YIData.YOSHIS_ISLAND_NAME} Practice Hack");
        }

        internal static GameVersionData CreateGeneralHackData(GameRegion region, string version, string romHash, string gameName)
        {
            return new GameVersionData(region, version, true, false, romHash, gameName);
        }


        // OVERRIDES ==========
        public override bool Equals(object? obj)
        {
            return Equals(obj as GameVersionData);
        }

        public bool Equals(GameVersionData? other)
        {
            if (other is null) 
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Region == other.Region &&
                   Version == other.Version &&
                   IsHack == other.IsHack &&
                   IsPracticeHack == other.IsPracticeHack &&
                   RomHash == other.RomHash;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Region, Version, IsHack, IsPracticeHack, RomHash);
        }

        public static bool operator ==(GameVersionData? left, GameVersionData? right) => Equals(left, right);
        public static bool operator !=(GameVersionData? left, GameVersionData? right) => !Equals(left, right);

        public override string ToString()
        {
            if (IsPracticeHack)
                return $"{GameName} {Version} ({Region})";
            else if (IsHack)
                return $"{GameName} (hack of {Region} {Version})";
            else
                return $"{GameName} ({Region} {Version})";
        }
    }
}
