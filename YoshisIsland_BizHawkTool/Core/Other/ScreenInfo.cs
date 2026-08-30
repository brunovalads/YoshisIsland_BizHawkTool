namespace YoshisIsland_BizHawkTool
{
    internal class ScreenInfo
    {
        internal int LeftGap { get; private set; }
        internal int RightGap { get; private set; }
        internal int TopGap { get; private set; }
        internal int BottomGap { get; private set; }
        internal int WindowWidth { get; private set; }
        internal int WindowHeight { get; private set; }
        internal int WindowCenterX { get; private set; }
        internal int WindowCenterY { get; private set; }
        internal int CoreWidth { get; private set; }
        internal int CoreHeight { get; private set; }
        internal int CoreCenterX { get; private set; }
        internal int CoreCenterY { get; private set; }
        internal int CoreRightBorder { get; private set; }
        internal int CoreBottomBorder { get; private set; }

        internal void Update(
            int leftGap,
            int rightGap,
            int topGap,
            int bottomGap,
            int windowWidth,
            int windowHeight,
            int windowCenterX,
            int windowCenterY,
            int coreWidth,
            int coreHeight,
            int coreCenterX,
            int coreCenterY,
            int coreRightBorder,
            int coreBottomBorder)
        {
            LeftGap = leftGap;
            RightGap = rightGap;
            TopGap = topGap;
            BottomGap = bottomGap;
            WindowWidth = windowWidth;
            WindowHeight = windowHeight;
            WindowCenterX = windowCenterX;
            WindowCenterY = windowCenterY;
            CoreWidth = coreWidth;
            CoreHeight = coreHeight;
            CoreCenterX = coreCenterX;
            CoreCenterY = coreCenterY;
            CoreRightBorder = coreRightBorder;
            CoreBottomBorder = coreBottomBorder;
        }
    }
}
