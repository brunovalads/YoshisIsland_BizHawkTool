using BizHawk.Client.Common;
using BizHawk.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace YoshisIsland_BizHawkTool
{
    internal class Yoshi
    {
        // SINGLETON ==========
        private static Yoshi _instance;
        internal static Yoshi Instance => GetInstance();
        private Yoshi() {}
        private static Yoshi GetInstance()
        {
            if (_instance == null)
                _instance = new Yoshi();

            return _instance;
        }


        // FIELDS ==========
        private string _xPosText;
        private string _yPosText;
        private string _xSpeedText;
        private string _ySpeedText;
        private string _eggTargetXText;
        private string _eggTargetYText;
        private string _canJumpText;
        private string _tongueXPosText;
        private string _tongueYPosText;
        private string _tongueXOffsetText;
        private string _tongueYOffsetText;


        // PROPERTIES ==========
        internal int XPos { get; private set; }
        internal int YPos { get; private set; }
        internal uint XSubPos { get; private set; }
        internal uint YSubPos { get; private set; }
        internal int XSpeed { get; private set; }
        internal int YSpeed { get; private set; }
        internal uint XSubSpeed { get; private set; }
        internal uint YSubSpeed { get; private set; }
        internal double XPosFull { get; private set; }
        internal double YPosFull { get; private set; }
        internal double XSpeedFull { get; private set; }
        internal double YSpeedFull { get; private set; }
        internal Direction Direction { get; private set; }
        internal uint State { get; private set; }
        internal uint EggThrowState { get; private set; }
        internal int EggTargetX { get; private set; }
        internal int EggTargetY { get; private set; }
        internal BlockedStatus BlockedStatus { get; private set; }
        internal bool OnSpritePlatform { get; private set; }
        internal bool CanJump { get; private set; }
        internal int TongueXOffset { get; private set; }
        internal int TongueYOffset { get; private set; }
        internal int TongueXPos { get; private set; }
        internal int TongueYPos { get; private set; }
        internal bool IsTongueGlitched { get; private set; }
        internal int TonguedSlot { get; private set; }
        internal uint TonguedSpriteType { get; private set; }
        internal uint GroundPoundState { get; private set; }
        internal uint GroundPoundTimer { get; private set; }


        // METHODS ==========
        internal void Update()
        {
            // Read memory
            XPos = MemoryManager.ReadS(YIMemoryMap.YoshiXPos);
            YPos = MemoryManager.ReadS(YIMemoryMap.YoshiYPos);
            XSubPos = MemoryManager.ReadU(YIMemoryMap.YoshiXSubPos); // TODO: Perhaps these should be signed too
            YSubPos = MemoryManager.ReadU(YIMemoryMap.YoshiYSubPos);
            XSpeed = MemoryManager.ReadS(YIMemoryMap.YoshiXSpeed);
            YSpeed = MemoryManager.ReadS(YIMemoryMap.YoshiYSpeed);
            XSubSpeed = MemoryManager.ReadU(YIMemoryMap.YoshiXSubSpeed);
            YSubSpeed = MemoryManager.ReadU(YIMemoryMap.YoshiYSubSpeed);
            uint rawDirection = MemoryManager.ReadU(YIMemoryMap.YoshiDirection) / 2;
            State = MemoryManager.ReadU(YIMemoryMap.YoshiState);
            EggThrowState = MemoryManager.ReadU(YIMemoryMap.EggThrowState);
            EggTargetX = MemoryManager.ReadS(YIMemoryMap.EggTargetX);
            EggTargetY = MemoryManager.ReadS(YIMemoryMap.EggTargetY);
            uint rawBlockedStatus = MemoryManager.ReadU(YIMemoryMap.YoshiBlockedStatus);
            uint rawOnSpritePlatform = MemoryManager.ReadU(YIMemoryMap.YoshiOnSpritePlatform);
            TongueXOffset = MemoryManager.ReadS(YIMemoryMap.YoshiTongueXOffset);
            TongueYOffset = MemoryManager.ReadS(YIMemoryMap.YoshiTongueYOffset);
            int rawTonguedSlotOffset = MemoryManager.ReadS(YIMemoryMap.YoshiTonguedSlotOffset);
            GroundPoundState = MemoryManager.ReadU(YIMemoryMap.YoshiGroundPoundState);
            GroundPoundTimer = MemoryManager.ReadU(YIMemoryMap.YoshiGroundPoundTimer);

            // Calculate stuff from memory
            XPosFull = XPos + (XSubPos / 256.0);
            YPosFull = YPos + (YSubPos / 256.0);
            XSpeedFull = XSpeed + (XSubSpeed / 256.0);
            YSpeedFull = YSpeed + (YSubSpeed / 256.0);
            int xSpeedFullPadded = (int)Math.Floor(Math.Abs(XSpeedFull * 256));
            int ySpeedFullPadded = (int)Math.Floor(Math.Abs(YSpeedFull * 256));
            Direction = (Direction)rawDirection;
            BlockedStatus = (BlockedStatus)rawBlockedStatus;
            OnSpritePlatform = rawOnSpritePlatform != 0;
            CanJump = BlockedStatus.HasFlag(BlockedStatus.BottomRight)
                   || BlockedStatus.HasFlag(BlockedStatus.BottomCenter)
                   || BlockedStatus.HasFlag(BlockedStatus.BottomLeft)
                   || OnSpritePlatform;
            TongueXPos = XPos + TongueXOffset;
            TongueYPos = YPos + TongueYOffset;
            IsTongueGlitched = (Direction == Direction.Right && TongueXOffset < 0) || (Direction == Direction.Left && TongueXOffset > 0) || (TongueYOffset > 0);
            TonguedSlot = (rawTonguedSlotOffset - 1) / 4;
            TonguedSpriteType = TonguedSlot != 0 ? MemoryManager.ReadU(YIMemoryMap.SpriteType, rawTonguedSlotOffset - 1) : 0xFFFF;

            // Compose texts for display
            _xPosText = $"{XPos & 0xFFFF:X4}.{XSubPos:X2}";
            _yPosText = $"{YPos & 0xFFFF:X4}.{YSubPos:X2}";
            _xSpeedText = XSpeedFull < 0 ? $"-{xSpeedFullPadded / 256:X2}.{xSpeedFullPadded % 256:X2}" : $"+{XSpeed:X2}.{XSubSpeed:X2}";
            _ySpeedText = YSpeedFull < 0 ? $"-{ySpeedFullPadded / 256:X2}.{ySpeedFullPadded % 256:X2}" : $"+{YSpeed:X2}.{YSubSpeed:X2}";
            _eggTargetXText = EggThrowState != 0 ? $"{EggTargetX & 0xFFFF:X4}" : "-";
            _eggTargetYText = EggThrowState != 0 ? $"{EggTargetY & 0xFFFF:X4}" : "-";
            _canJumpText = CanJump ? "yes" : "no";
            _tongueXPosText = $"{TongueXPos & 0xFFFF:X4}";
            _tongueYPosText = $"{TongueYPos & 0xFFFF:X4}";
            _tongueXOffsetText = $"{(TongueXOffset < 0 ? "-" : "+")}{Math.Abs(TongueXOffset):X2}";
            _tongueYOffsetText = $"{(TongueYOffset < 0 ? "-" : "+")}{Math.Abs(TongueYOffset):X2}";
        }

        internal void DrawInfo(IGuiApi guiApi)
        {
            if (!ToolOptions.Instance.DisplayPlayerInfo)
                return;

            int i = 0;
            int delta_x = GuiManager.BIZHAWK_FONT_WIDTH;
            int delta_y = GuiManager.BIZHAWK_FONT_HEIGHT;
            int table_x = 2;
            int table_y = ClientManager.HawkScreenInfo.TopGap;

            guiApi.Text(table_x, table_y + (i++ * delta_y), $"Pos: ({_xPosText}, {_yPosText}) {(Direction == Direction.Right ? "->" : "<-")} ({XPosFull:0.000}, {YPosFull:0.000})");
            
            guiApi.Text(table_x, table_y + (i++ * delta_y), $"Speed: ({_xSpeedText}, {_ySpeedText}) ({XSpeedFull:0.000}, {YSpeedFull:0.000})");
            
            guiApi.Text(table_x, table_y + (i++ * delta_y), $"State: {State:X2}");
            
            guiApi.Text(table_x, table_y + (i++ * delta_y), $"Target: ({_eggTargetXText}, {_eggTargetYText})");
            
            guiApi.Text(table_x, table_y + (i * delta_y), $"Can jump: ");
            
            guiApi.Text(table_x + "Can jump: ".Length * delta_x, table_y + (i++ * delta_y), _canJumpText,
                CanJump ? Color.FromArgb(0x00, 0xFF, 0x00) : Color.FromArgb(0xFF, 0x00, 0x00));

            DrawBlockedStatus(guiApi, table_x, table_y + i * delta_y + 2);
            i += 3;

            Color tongueColor = Color.FromArgb(unchecked((int)0xffD5293D)); // TODO: Move to the ColorManager or whatever that end up being

            guiApi.Text(table_x, table_y + (i++ * delta_y), $"Tongue: ({_tongueXPosText}, {_tongueYPosText})", tongueColor);

            guiApi.Text(table_x, table_y + (i++ * delta_y), $"Offset: ({_tongueXOffsetText}, {_tongueYOffsetText}) {(IsTongueGlitched ? "GLITCH!" : "")}", tongueColor);

            guiApi.Text(table_x, table_y + (i++ * delta_y), $"Slot: <{(TonguedSlot != 0 ? TonguedSlot.ToString("00") : "--")}> (ID {(TonguedSlot != 0 ? TonguedSpriteType.ToString("X3") : "---")})", tongueColor);
        }

        private void DrawBlockedStatus(IGuiApi guiApi, int blockedStatusX, int blockedStatusY)
        {
            if (!ToolOptions.Instance.DisplayBlockedStatus)
                return;

            guiApi.Text(blockedStatusX, blockedStatusY, "Blocked:");

            blockedStatusX += "Blocked:".Length * GuiManager.BIZHAWK_FONT_WIDTH;

            guiApi.DrawImage(Properties.Resources.yoshi_blocked_status, blockedStatusX, blockedStatusY, surfaceID: DisplaySurfaceID.Client);

            int bitmapWidth = Properties.Resources.yoshi_blocked_status.Width;
            int bitmapHeight = Properties.Resources.yoshi_blocked_status.Height;

            Color flagColor = Color.Red;

            if (BlockedStatus.HasFlag(BlockedStatus.BottomRight))
                guiApi.DrawRectangle(blockedStatusX + 21, blockedStatusY + bitmapHeight - 3, 6, 1, flagColor, surfaceID: DisplaySurfaceID.Client);

            if (BlockedStatus.HasFlag(BlockedStatus.BottomCenter))
                guiApi.DrawRectangle(blockedStatusX + 13, blockedStatusY + bitmapHeight - 3, 6, 1, flagColor, surfaceID: DisplaySurfaceID.Client);

            if (BlockedStatus.HasFlag(BlockedStatus.BottomLeft))
                guiApi.DrawRectangle(blockedStatusX + 5, blockedStatusY + bitmapHeight - 3, 6, 1, flagColor, surfaceID: DisplaySurfaceID.Client);

            if (BlockedStatus.HasFlag(BlockedStatus.TopRight))
                guiApi.DrawRectangle(blockedStatusX + 17, blockedStatusY + 1, 10, 1, flagColor, surfaceID: DisplaySurfaceID.Client);

            if (BlockedStatus.HasFlag(BlockedStatus.TopLeft))
                guiApi.DrawRectangle(blockedStatusX + 5, blockedStatusY + 1, 10, 1, flagColor, surfaceID: DisplaySurfaceID.Client);

            if (BlockedStatus.HasFlag(BlockedStatus.RightBody))
                guiApi.DrawRectangle(blockedStatusX + bitmapWidth - 3, blockedStatusY + 19, 1, 13, flagColor, surfaceID: DisplaySurfaceID.Client);

            if (BlockedStatus.HasFlag(BlockedStatus.RightHead))
                guiApi.DrawRectangle(blockedStatusX + bitmapWidth - 3, blockedStatusY + 5, 1, 12, flagColor, surfaceID: DisplaySurfaceID.Client);

            if (BlockedStatus.HasFlag(BlockedStatus.LeftBody))
                guiApi.DrawRectangle(blockedStatusX + 1, blockedStatusY + 19, 1, 13, flagColor, surfaceID: DisplaySurfaceID.Client);

            if (BlockedStatus.HasFlag(BlockedStatus.LeftHead))
                guiApi.DrawRectangle(blockedStatusX + 1, blockedStatusY + 5, 1, 12, flagColor, surfaceID: DisplaySurfaceID.Client);

            if (BlockedStatus == BlockedStatus.FullyBlocked)
            {
                guiApi.DrawLine(blockedStatusX + 4, blockedStatusY + (bitmapHeight / 2) - 1, blockedStatusX + bitmapWidth - 5, blockedStatusY + (bitmapHeight / 2) - 1, flagColor, surfaceID: DisplaySurfaceID.Client);
                guiApi.DrawLine(blockedStatusX + (bitmapWidth / 2), blockedStatusY + 4, blockedStatusX + (bitmapWidth / 2), blockedStatusY + bitmapHeight - 5, flagColor, surfaceID: DisplaySurfaceID.Client);
            }
        }
    }
}
