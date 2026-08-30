using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizHawk.Client.Common;

namespace YoshisIsland_BizHawkTool
{
    internal static class GuiManager
    {
        internal const int SNES_SCREEN_WIDTH = 256;
        internal const int SNES_SCREEN_HEIGHT = 224;

        private static IGuiApi _guiAPI;
        private static ToolOptions _options;
        private static readonly Color _darkFilterBaseColor = Color.Black;

        internal static void Init(IGuiApi guiAPI)
        {
            _guiAPI = guiAPI;
            _options = ToolOptions.Instance;
        }

        internal static void DrawEverything()
        {
            _guiAPI.ClearGraphics(DisplaySurfaceID.EmuCore);
            _guiAPI.ClearGraphics(DisplaySurfaceID.Client);

            DrawDarkFilter();

#if DEBUG
            //_options.Debug(_guiAPI);
            ClientManager.Debug(_guiAPI);
#endif
        }

        private static void DrawDarkFilter()
        {
            int darkFilterOpacity = Math.Max(Math.Min((int)(0xFF * _options.DarkFilterOpacity / 100.0), 0xFF), 0x00);
            Color darkFilterColor = Color.FromArgb(darkFilterOpacity, _darkFilterBaseColor);
            _guiAPI.DrawRectangle(_options.LeftGap, _options.TopGap,
                                  SNES_SCREEN_WIDTH - 1, SNES_SCREEN_HEIGHT - 1,
                                  darkFilterColor, darkFilterColor, DisplaySurfaceID.EmuCore);
        }
    }
}
