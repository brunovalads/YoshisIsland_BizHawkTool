using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoshisIsland_BizHawkTool
{
    internal static class YIMemoryMap
    {
        // SRAM ==========
        internal static readonly MemoryAddress YoshiXPos = new MemoryAddress(0x008C, 2, MemoryDomain.SRAM);
        internal static readonly MemoryAddress YoshiYPos = new MemoryAddress(0x0090, 2, MemoryDomain.SRAM);
        internal static readonly MemoryAddress YoshiXSubPos = new MemoryAddress(0x008A, 1, MemoryDomain.SRAM);
        internal static readonly MemoryAddress YoshiYSubPos = new MemoryAddress(0x008E, 1, MemoryDomain.SRAM);
        internal static readonly MemoryAddress YoshiXSpeed = new MemoryAddress(0x00B5, 1, MemoryDomain.SRAM);
        internal static readonly MemoryAddress YoshiYSpeed = new MemoryAddress(0x00AB, 1, MemoryDomain.SRAM);
        internal static readonly MemoryAddress YoshiXSubSpeed = new MemoryAddress(0x00B4, 1, MemoryDomain.SRAM);
        internal static readonly MemoryAddress YoshiYSubSpeed = new MemoryAddress(0x00AA, 1, MemoryDomain.SRAM);
        internal static readonly MemoryAddress YoshiDirection = new MemoryAddress(0x00C4, 1, MemoryDomain.SRAM);
        internal static readonly MemoryAddress YoshiState = new MemoryAddress(0x00AC, 1, MemoryDomain.SRAM);
        internal static readonly MemoryAddress YoshiBlockedStatus = new MemoryAddress(0x00FC, 2, MemoryDomain.SRAM);
        internal static readonly MemoryAddress YoshiOnSpritePlatform = new MemoryAddress(0x01B4, 2, MemoryDomain.SRAM);
        internal static readonly MemoryAddress YoshiGroundPoundState = new MemoryAddress(0x00D4, 1, MemoryDomain.SRAM);
        internal static readonly MemoryAddress YoshiGroundPoundTimer = new MemoryAddress(0x00D6, 1, MemoryDomain.SRAM);

        internal static readonly MemoryAddress EggThrowState = new MemoryAddress(0x00DE, 1, MemoryDomain.SRAM);
        internal static readonly MemoryAddress EggTargetX = new MemoryAddress(0x00E4, 2, MemoryDomain.SRAM);
        internal static readonly MemoryAddress EggTargetY = new MemoryAddress(0x00E6, 2, MemoryDomain.SRAM);

        internal static readonly MemoryAddress YoshiTongueXOffset = new MemoryAddress(0x0152, 2, MemoryDomain.SRAM);
        internal static readonly MemoryAddress YoshiTongueYOffset = new MemoryAddress(0x0154, 2, MemoryDomain.SRAM);
        internal static readonly MemoryAddress YoshiTonguedSlotOffset = new MemoryAddress(0x0168, 2, MemoryDomain.SRAM);

        internal static readonly MemoryAddress SpriteType = new MemoryAddress(0x1360, 2, MemoryDomain.SRAM);
    }
}
