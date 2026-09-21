using System;

namespace YoshisIsland_BizHawkTool
{
    [Flags]
    internal enum BlockedStatus : uint
    {
        /* 
        2 bytes

            TL   TR
            ___  ___
        LH |        | RH

        LB |__ __ __| RB
            BL BC BR
        */

        BottomRight = 0b000000001,
        BottomCenter = 0b000000010,
        BottomLeft = 0b000000100,

        TopRight = 0b000001000,
        TopLeft = 0b000010000,

        RightBody = 0b000100000,
        RightHead = 0b001000000,

        LeftBody = 0b010000000,
        LeftHead = 0b100000000,

        FullyBlocked = 0b111111111
    }
}
