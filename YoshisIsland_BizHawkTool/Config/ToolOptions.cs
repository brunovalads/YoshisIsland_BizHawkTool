using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoshisIsland_BizHawkTool
{
    internal class ToolOptions
    {
        // SINGLETON ==========
        private static ToolOptions _instance;
        internal static ToolOptions Instance => GetInstance();
        private ToolOptions() { }


        // OPTIONS PROPERTIES ==========
        // TODO: Maybe each segment could be its own class, for instance: ToolOptions.Instance.Player.DisplayInfo, with Player being an instance of PlayerOptions
        // Player
        public bool DisplayPlayerInfo { get; set; } = true;
        public bool DisplayPlayerHitbox { get; set; } = true;
        public bool DisplayInteractionPoints { get; set; } = true;
        public bool DisplayBlockedStatus { get; set; } = true;
        public bool DisplayThrowInfo { get; set; } = true;
        public bool DisplayEggInfo { get; set; } = true;
        public bool DisplayTongueHitbox { get; set; } = true;
        // Sprites
        public bool DisplaySpriteInfo { get; set; } = true;
        public bool DisplaySpriteTable { get; set; } = true;
        public bool DisplaySpriteHitbox { get; set; } = true;
        public bool DisplaySpriteSlotInScreen { get; set; } = true; // TODO: Decide if will be really used
        public bool DisplaySpriteSpecialInfo { get; set; } = true;
        public bool DisplaySpriteSpawningAreas { get; set; } = true;
        // Level
        public bool DisplayLevelInfo { get; set; } = true;
        public bool DisplaySpriteData { get; set; } = true;
        public bool DisplayLevelExtra { get; set; } = true;
        public bool DrawTileMapGrid { get; set; } = true;
        public bool DrawTileMapType { get; set; } = true;
        public bool DrawTileMapScreen { get; set; } = true;
        public bool DisplayLevelLayout { get; set; } = true;
        // General
        public bool DisplayOverworldInfo { get; set; } = true;
        public bool DisplayMiscInfo { get; set; } = true;
        public bool DisplayCounters { get; set; } = true;
        public bool DisplayMovieInfo { get; set; } = true;
        public bool DisplayCreditsWarpHelper { get; set; } = true;
        // Ambient sprites
        public bool DisplayAmbientSpriteInfo { get; set; } = true;
        public bool DisplayAmbientSpriteTable { get; set; } = true;
        public bool DisplayAmbientSpriteSlotInScreen { get; set; } = true;
        // Debug
        public bool DisplayDebugPlayerExtra { get; set; } = true;
        public bool DisplayDebugSpriteExtra { get; set; } = true;
        public bool DisplayDebugAmbientSprite { get; set; } = true;
        public bool DisplaySpriteLoadStatus { get; set; } = true;
        public bool DisplayDebugControllerData { get; set; } = true;
        public bool DisplayLagmeter { get; set; } = true;
        public bool DisplayDebugInfo { get; set; } = true;
        // Settings
        public double WindowsDisplayScale { get; set; } = 1.0;
        public bool DrawTilesWithClick { get; set; } = true;
        public int MaxTilesDrawn { get; set; } = 50;
        public bool DisplayMouseCoordinates { get; set; } = true;
        public bool DrawDarkFilter { get; set; } = true;
        public int DarkFilterOpacity { get; set; } = 8;
        public int LeftGap { get; set; } = 150;
        public int RightGap { get; set; } = 200;
        public int TopGap { get; set; } = 55;
        public int BottomGap { get; set; } = 154;


        // METHODS ==========
        private static ToolOptions GetInstance()
        {
            if (_instance == null)
                _instance = new ToolOptions();

            return _instance;
        }
    }
}
