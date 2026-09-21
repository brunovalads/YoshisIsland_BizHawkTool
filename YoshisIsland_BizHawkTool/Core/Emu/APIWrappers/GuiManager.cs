using BizHawk.Client.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace YoshisIsland_BizHawkTool
{
    internal static class GuiManager
    {
        internal const int SNES_SCREEN_WIDTH = 256;
        internal const int SNES_SCREEN_HEIGHT = 224;
        internal const int BIZHAWK_FONT_WIDTH = 10;
        internal const int BIZHAWK_FONT_HEIGHT = 18;

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
            Clear();

            DrawDarkFilter();

            Yoshi.Instance.DrawInfo(_guiAPI);

#if DEBUG
            //_options.Debug(_guiAPI);
            //ClientManager.Debug(_guiAPI);
            //DebugPerformance(true);
#endif
        }

        internal static void Clear()
        {
            _guiAPI?.ClearGraphics(DisplaySurfaceID.EmuCore);
            _guiAPI?.ClearGraphics(DisplaySurfaceID.Client);
        }

        private static void DrawDarkFilter()
        {
            int darkFilterOpacity = Math.Max(Math.Min((int)(0xFF * _options.DarkFilterOpacity / 100.0), 0xFF), 0x00);
            Color darkFilterColor = Color.FromArgb(darkFilterOpacity, _darkFilterBaseColor);
            _guiAPI?.DrawRectangle(_options.LeftGap, _options.TopGap,
                                   SNES_SCREEN_WIDTH - 1, SNES_SCREEN_HEIGHT - 1,
                                   darkFilterColor, darkFilterColor, DisplaySurfaceID.EmuCore);
        }

        private static void DebugPerformance(bool singleDrawCall)
        {
            if (_guiAPI == null)
                return;

            Random random = new Random();
            int maxIter = 5000;
            int minWidth = 2;
            int maxWidth = 24;
            int minHeight = 2;
            int maxHeight = 24;

            if (singleDrawCall) // 21fps
            {
                _guiAPI.ClearImageCache();

                using (Bitmap bitmap = new Bitmap(SNES_SCREEN_WIDTH, SNES_SCREEN_HEIGHT, PixelFormat.Format32bppArgb))
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.Transparent);

                    for (int i = 0; i < maxIter; i++)
                    {
                        int x = random.Next(0, SNES_SCREEN_WIDTH) - maxWidth;
                        int y = random.Next(0, SNES_SCREEN_HEIGHT) - maxHeight;
                        int width = random.Next(minWidth, maxWidth);
                        int height = random.Next(minHeight, maxHeight);
                        Color lineColor = Color.FromArgb(0x80, random.Next(0xFF), random.Next(0xFF), random.Next(0xFF));
                        Color fillColor = Color.FromArgb(0x40, lineColor);

                        using (SolidBrush brush = new SolidBrush(fillColor))
                        using (Pen pen = new Pen(lineColor, 1))
                        {
                            graphics.FillRectangle(brush, x, y, width, height);
                            graphics.DrawRectangle(pen, x, y, width, height);
                        }
                    }

                    _guiAPI.DrawImage(bitmap, 0, 0, surfaceID: DisplaySurfaceID.EmuCore);
                }
            }
            else // 24fps
            {
                for (int i = 0; i < maxIter; i++)
                {
                    int x = random.Next(0, SNES_SCREEN_WIDTH) - maxWidth;
                    int y = random.Next(0, SNES_SCREEN_HEIGHT) - maxHeight;
                    int width = random.Next(minWidth, maxWidth);
                    int height = random.Next(minHeight, maxHeight);
                    Color lineColor = Color.FromArgb(0x80, random.Next(0xFF), random.Next(0xFF), random.Next(0xFF));
                    Color fillColor = Color.FromArgb(0x40, lineColor);
                    _guiAPI.DrawRectangle(x, y, width, height, lineColor, fillColor, DisplaySurfaceID.EmuCore);
                }
            }
        }
    }
}
