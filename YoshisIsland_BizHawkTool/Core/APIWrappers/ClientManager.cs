using BizHawk.Client.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YoshisIsland_BizHawkTool
{
    internal static class ClientManager
    {
        private static IEmuClientApi _clientAPI;

        internal static double ScaleX;
        internal static double ScaleY;
        internal static ScreenInfo HawkScreenInfo;
        internal static ScreenInfo GameScreenInfo;

        internal static void Init(IEmuClientApi clientAPI)
        {
            _clientAPI = clientAPI;
            SetGaps();
            HawkScreenInfo = new ScreenInfo();
            GameScreenInfo = new ScreenInfo();
            UpdateScreenInfos();
        }

        internal static void SetGaps()
        {
            _clientAPI.SetGameExtraPadding(
                ToolOptions.Instance.LeftGap,
                ToolOptions.Instance.TopGap,
                ToolOptions.Instance.RightGap,
                ToolOptions.Instance.BottomGap
            );
        }

        internal static void ClearGaps()
        {
            _clientAPI.SetGameExtraPadding(0, 0, 0, 0);
        }

        internal static void UpdateScreenInfos()
        {
            int bufferWidth = _clientAPI.BufferWidth();
            int bufferHeight = _clientAPI.BufferHeight();
            Point coreMinPointInWindow = _clientAPI.TransformPoint(new Point(0, 0));
            Point coreMaxPointInWindow = _clientAPI.TransformPoint(new Point(bufferWidth, bufferHeight));
            int bufferWidthInWindow = coreMaxPointInWindow.X - coreMinPointInWindow.X;
            int bufferHeightInWindow = coreMaxPointInWindow.Y - coreMinPointInWindow.Y;
            ScaleX = (1.0 * bufferWidthInWindow) / bufferWidth;
            ScaleY = (1.0 * bufferHeightInWindow) / bufferHeight;
            int screenWidth = _clientAPI.ScreenWidth();
            int screenHeight = _clientAPI.ScreenHeight();

            HawkScreenInfo.Update(
                leftGap: coreMinPointInWindow.X,
                rightGap: screenWidth - coreMaxPointInWindow.X,
                topGap: coreMinPointInWindow.Y,
                bottomGap: screenHeight - coreMaxPointInWindow.Y,
                windowWidth: screenWidth,
                windowHeight: screenHeight,
                windowCenterX: screenWidth / 2,
                windowCenterY: screenHeight / 2,
                coreWidth: bufferWidthInWindow,
                coreHeight: bufferHeightInWindow,
                coreCenterX: (coreMinPointInWindow.X + coreMaxPointInWindow.X) / 2,
                coreCenterY: (coreMinPointInWindow.Y + coreMaxPointInWindow.Y) / 2,
                coreRightBorder: coreMaxPointInWindow.X,
                coreBottomBorder: coreMaxPointInWindow.Y
            );

            GameScreenInfo.Update(
                leftGap: (int)(HawkScreenInfo.LeftGap / ScaleX),
                rightGap: (int)(HawkScreenInfo.RightGap / ScaleX),
                topGap: (int)(HawkScreenInfo.TopGap / ScaleY),
                bottomGap: (int)(HawkScreenInfo.BottomGap / ScaleY),
                windowWidth: (int)(HawkScreenInfo.WindowWidth / ScaleX),
                windowHeight: (int)(HawkScreenInfo.WindowHeight / ScaleY),
                windowCenterX: (int)(HawkScreenInfo.WindowCenterX / ScaleX),
                windowCenterY: (int)(HawkScreenInfo.WindowCenterY / ScaleY),
                coreWidth: bufferWidth,
                coreHeight: bufferHeight,
                coreCenterX: (int)(HawkScreenInfo.CoreCenterX / ScaleX),
                coreCenterY: (int)(HawkScreenInfo.CoreCenterY / ScaleY),
                coreRightBorder: (int)(HawkScreenInfo.CoreRightBorder / ScaleX),
                coreBottomBorder: (int)(HawkScreenInfo.CoreBottomBorder / ScaleY)
            );
        }

        internal static void Debug(IGuiApi guiApi)
        {
            bool showTextualDebug = false;
            bool showVisualDebug = true;

            // Textual debug
            if (showTextualDebug)
            {
                int i = 2;
                Color valueColor = Color.FromArgb(0xFF, 0x00, 0xFF, 0xFF);

                string propertyNameStr = $"{nameof(ScaleX)}: ";
                object propertyValue = ScaleX;
                guiApi.Text(2, i * 16, propertyNameStr);
                guiApi.Text(2 + propertyNameStr.Length * 10, i * 16, $"{propertyValue}", valueColor);
                i++;

                propertyNameStr = $"{nameof(ScaleY)}: ";
                propertyValue = ScaleY;
                guiApi.Text(2, i * 16, propertyNameStr);
                guiApi.Text(2 + propertyNameStr.Length * 10, i * 16, $"{propertyValue}", valueColor);
                i++;

                // HawkScreenInfo
                i++;
                guiApi.Text(2, i * 16, nameof(HawkScreenInfo));
                i++;
                PropertyInfo[] hawkScreenInfoProperties = HawkScreenInfo.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (PropertyInfo hawkScreenInfoProperty in hawkScreenInfoProperties)
                {
                    string hawkScreenInfoPropertyName = hawkScreenInfoProperty.Name;
                    object hawkScreenInfoPropertyValue = hawkScreenInfoProperty.GetValue(HawkScreenInfo);

                    string hawkScreenInfoPropertyNameStr = $"- {hawkScreenInfoPropertyName}: ";
                    guiApi.Text(2, i * 16, hawkScreenInfoPropertyNameStr);
                    guiApi.Text(2 + hawkScreenInfoPropertyNameStr.Length * 10, i * 16, $"{hawkScreenInfoPropertyValue}", valueColor);
                    i++;
                }

                // GameScreenInfo
                i++;
                guiApi.Text(2, i * 16, nameof(GameScreenInfo));
                i++;
                PropertyInfo[] gameScreenInfoProperties = GameScreenInfo.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (PropertyInfo gameScreenInfoProperty in gameScreenInfoProperties)
                {
                    string gameScreenInfoPropertyName = gameScreenInfoProperty.Name;
                    object gameScreenInfoPropertyValue = gameScreenInfoProperty.GetValue(GameScreenInfo);

                    string gameScreenInfoPropertyNameStr = $"- {gameScreenInfoPropertyName}: ";
                    guiApi.Text(2, i * 16, gameScreenInfoPropertyNameStr);
                    guiApi.Text(2 + gameScreenInfoPropertyNameStr.Length * 10, i * 16, $"{gameScreenInfoPropertyValue}", valueColor);
                    i++;
                }
            }

            // Visual debug
            if (showVisualDebug)
            {
                Color hawkColor = Color.FromArgb(0xFF, 0xFF, 0x00, 0x7F);
                Color gameColor = Color.FromArgb(0xFF, 0x00, 0xFF, 0x10);
                Color transparent = Color.Transparent;
                int pixelFontWidth = 4;
                int pixelFontHeight = 8;

                // GameScreenInfo
                guiApi.DrawRectangle(0, 0, GameScreenInfo.WindowWidth - 1, GameScreenInfo.WindowHeight - 1, gameColor, transparent, DisplaySurfaceID.EmuCore);
                guiApi.PixelText(0 + 2, 0 + 2, $"Window: {GameScreenInfo.WindowWidth}x{GameScreenInfo.WindowHeight}", gameColor, surfaceID: DisplaySurfaceID.EmuCore);

                guiApi.DrawRectangle(GameScreenInfo.LeftGap, GameScreenInfo.TopGap, GameScreenInfo.CoreWidth, GameScreenInfo.CoreHeight, gameColor, transparent, DisplaySurfaceID.EmuCore);
                guiApi.PixelText(GameScreenInfo.LeftGap + 2, GameScreenInfo.TopGap + 2, $"Core: {GameScreenInfo.CoreWidth}x{GameScreenInfo.CoreHeight}", gameColor, surfaceID: DisplaySurfaceID.EmuCore);

                guiApi.DrawLine(0, GameScreenInfo.CoreCenterY, GameScreenInfo.LeftGap, GameScreenInfo.CoreCenterY, gameColor, DisplaySurfaceID.EmuCore);
                guiApi.PixelText(0 + 2, GameScreenInfo.CoreCenterY + 2, $"LeftGap: {GameScreenInfo.LeftGap}", gameColor, surfaceID: DisplaySurfaceID.EmuCore);

                guiApi.DrawLine(GameScreenInfo.CoreRightBorder, GameScreenInfo.CoreCenterY, GameScreenInfo.WindowWidth, GameScreenInfo.CoreCenterY, gameColor, DisplaySurfaceID.EmuCore);
                string gameRightGapStr = $"RightGap: {GameScreenInfo.RightGap}";
                guiApi.PixelText(GameScreenInfo.WindowWidth - (gameRightGapStr.Length * pixelFontWidth) - 2, GameScreenInfo.CoreCenterY + 2, gameRightGapStr, gameColor, surfaceID: DisplaySurfaceID.EmuCore);

                guiApi.DrawLine(GameScreenInfo.CoreCenterX, 0, GameScreenInfo.CoreCenterX, GameScreenInfo.TopGap, gameColor, DisplaySurfaceID.EmuCore);
                guiApi.PixelText(GameScreenInfo.CoreCenterX + 2, 0 + 2, $"TopGap: {GameScreenInfo.TopGap}", gameColor, surfaceID: DisplaySurfaceID.EmuCore);

                guiApi.DrawLine(GameScreenInfo.CoreCenterX, GameScreenInfo.CoreBottomBorder, GameScreenInfo.CoreCenterX, GameScreenInfo.WindowHeight, gameColor, DisplaySurfaceID.EmuCore);
                guiApi.PixelText(GameScreenInfo.CoreCenterX + 2, GameScreenInfo.WindowHeight - pixelFontHeight, $"BottomGap: {GameScreenInfo.BottomGap}", gameColor, surfaceID: DisplaySurfaceID.EmuCore);

                guiApi.DrawAxis(GameScreenInfo.CoreCenterX, GameScreenInfo.CoreCenterY, 5, gameColor, DisplaySurfaceID.EmuCore);
                guiApi.PixelText(GameScreenInfo.CoreCenterX + 2, GameScreenInfo.CoreCenterY + 2, $"CoreCenter: {GameScreenInfo.CoreCenterX}x{GameScreenInfo.CoreCenterY}", gameColor, surfaceID: DisplaySurfaceID.EmuCore);

                guiApi.DrawAxis(GameScreenInfo.WindowCenterX, GameScreenInfo.WindowCenterY, 5, gameColor, DisplaySurfaceID.EmuCore);
                guiApi.PixelText(GameScreenInfo.WindowCenterX + 2, GameScreenInfo.WindowCenterY + 2, $"WindowCenter: {GameScreenInfo.WindowCenterX}x{GameScreenInfo.WindowCenterY}", gameColor, surfaceID: DisplaySurfaceID.EmuCore);

                // HawkScreenInfo
                guiApi.DrawRectangle(0, 0, HawkScreenInfo.WindowWidth - 1, HawkScreenInfo.WindowHeight - 1, hawkColor, transparent, DisplaySurfaceID.Client);
                guiApi.Text(0 + 4, 0 + 2, $"Window: {HawkScreenInfo.WindowWidth}x{HawkScreenInfo.WindowHeight}", hawkColor, anchor: "bottomright");

                guiApi.DrawRectangle(HawkScreenInfo.LeftGap, HawkScreenInfo.TopGap, HawkScreenInfo.CoreWidth - 1, HawkScreenInfo.CoreHeight - 1, hawkColor, transparent, DisplaySurfaceID.Client);
                guiApi.Text(HawkScreenInfo.RightGap + 4, HawkScreenInfo.BottomGap + 2, $"Core: {HawkScreenInfo.CoreWidth}x{HawkScreenInfo.CoreHeight}", hawkColor, anchor: "bottomright");

                guiApi.DrawLine(0, HawkScreenInfo.CoreCenterY, HawkScreenInfo.LeftGap, HawkScreenInfo.CoreCenterY, hawkColor, DisplaySurfaceID.Client);
                guiApi.Text(0 + 4, HawkScreenInfo.WindowHeight - HawkScreenInfo.CoreCenterY + 2, $"LeftGap: {HawkScreenInfo.LeftGap}", hawkColor, anchor: "bottomleft");

                guiApi.DrawLine(HawkScreenInfo.CoreRightBorder, HawkScreenInfo.CoreCenterY, HawkScreenInfo.WindowWidth, HawkScreenInfo.CoreCenterY, hawkColor, DisplaySurfaceID.Client);
                guiApi.Text(0 + 4, HawkScreenInfo.WindowHeight - HawkScreenInfo.CoreCenterY + 2, $"RightGap: {HawkScreenInfo.RightGap}", hawkColor, anchor: "bottomright");

                guiApi.DrawLine(HawkScreenInfo.CoreCenterX, 0, HawkScreenInfo.CoreCenterX, HawkScreenInfo.TopGap, hawkColor, DisplaySurfaceID.Client);
                guiApi.Text(HawkScreenInfo.WindowWidth - HawkScreenInfo.CoreCenterX + 4, 0 + 2, $"TopGap: {HawkScreenInfo.TopGap}", hawkColor, anchor: "topright");

                guiApi.DrawLine(HawkScreenInfo.CoreCenterX, HawkScreenInfo.CoreBottomBorder, HawkScreenInfo.CoreCenterX, HawkScreenInfo.WindowHeight, hawkColor, DisplaySurfaceID.Client);
                guiApi.Text(HawkScreenInfo.WindowWidth - HawkScreenInfo.CoreCenterX + 4, 0 + 2, $"BottomGap: {HawkScreenInfo.BottomGap}", hawkColor, anchor: "bottomright");

                guiApi.DrawAxis(HawkScreenInfo.CoreCenterX, HawkScreenInfo.CoreCenterY, 5, hawkColor, DisplaySurfaceID.Client);
                guiApi.Text(HawkScreenInfo.WindowWidth - HawkScreenInfo.CoreCenterX + 4, HawkScreenInfo.WindowHeight - HawkScreenInfo.CoreCenterY + 2, $"CoreCenter: {HawkScreenInfo.CoreCenterX}x{HawkScreenInfo.CoreCenterY}", hawkColor, anchor: "bottomright");

                guiApi.DrawAxis(HawkScreenInfo.WindowCenterX, HawkScreenInfo.WindowCenterY, 5, hawkColor, DisplaySurfaceID.Client);
                guiApi.Text(HawkScreenInfo.WindowWidth - HawkScreenInfo.WindowCenterX + 4, HawkScreenInfo.WindowHeight - HawkScreenInfo.WindowCenterY + 2, $"WindowCenter: {HawkScreenInfo.WindowCenterX}x{HawkScreenInfo.WindowCenterY}", hawkColor, anchor: "bottomright");
            }
        }
    }
}
