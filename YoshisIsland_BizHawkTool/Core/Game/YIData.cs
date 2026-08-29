using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoshisIsland_BizHawkTool
{
    internal static class YIData
    {
        internal const string YOSHIS_ISLAND_NAME = "Yoshi's Island";

        internal const int SNES_GAME_NAME_ROM_ADDRESS = 0x7FC0;
        internal const int SNES_REGION_ROM_ADDRESS = 0x7FD9;

        private static readonly GameVersionData _originalJ1_0 = GameVersionData.CreateOriginalData(GameRegion.J, "1.0", "5A9F00411B9175A938C823C578E2B9F1256B73C546A50FEC144698F56859D64F");
        private static readonly GameVersionData _originalJ1_1 = GameVersionData.CreateOriginalData(GameRegion.J, "1.1", "C27E73EA19B6C421BCA7640D2ED89C75CD9D3BAEF968EBCD984606402ED93424");
        private static readonly GameVersionData _originalJ1_2 = GameVersionData.CreateOriginalData(GameRegion.J, "1.2", "D54A3EAAB7CE4D250F8EF2CB86FA5AFEBB4712F95CADC65C85A6E5A7355D8B81");
        private static readonly GameVersionData _originalU1_0 = GameVersionData.CreateOriginalData(GameRegion.U, "1.0", "9B4957466798BBDB5B43A450BBB60B2591AE81D95B891430F62D53CA62E8BC7B");
        private static readonly GameVersionData _originalU1_1 = GameVersionData.CreateOriginalData(GameRegion.U, "1.1", "BD763C1A56365C244BE92E6CFFEFD318780A2A19EDA7D5BAF1C6D5BD6C1B3E06");
        private static readonly GameVersionData _originalE1_0 = GameVersionData.CreateOriginalData(GameRegion.E, "1.0", "91A4DC481C54B620CB3BCCAFFE5FA3F69DB955AE600309414D18BB59307CBA90");
        private static readonly GameVersionData _originalE1_1 = GameVersionData.CreateOriginalData(GameRegion.E, "1.1", "824F07E93C9AD38FE408AF561E8979E3C0211F0C6C98AEB6E6BC85CD6F9EDC91");

        internal static readonly Dictionary<GameVersionData, byte[]> OriginalHeaderDatas = new Dictionary<GameVersionData, byte[]>()
        {
            { _originalJ1_0, new byte[]{ 0x59, 0x4F, 0x53, 0x53, 0x59, 0x27, 0x53, 0x20, 0x49, 0x53, 0x4C, 0x41, 0x4E, 0x44, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x15, 0x0B, 0x00, 0x00, 0x33, 0x00 } },
            { _originalJ1_1, new byte[]{ 0x59, 0x4F, 0x53, 0x53, 0x59, 0x27, 0x53, 0x20, 0x49, 0x53, 0x4C, 0x41, 0x4E, 0x44, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x15, 0x0B, 0x00, 0x00, 0x33, 0x01 } },
            { _originalJ1_2, new byte[]{ 0x59, 0x4F, 0x53, 0x53, 0x59, 0x27, 0x53, 0x20, 0x49, 0x53, 0x4C, 0x41, 0x4E, 0x44, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x15, 0x0B, 0x00, 0x00, 0x33, 0x02 } },
            
            { _originalU1_0, new byte[]{ 0x59, 0x4F, 0x53, 0x48, 0x49, 0x27, 0x53, 0x20, 0x49, 0x53, 0x4C, 0x41, 0x4E, 0x44, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x15, 0x0B, 0x00, 0x01, 0x33, 0x00 } },
            { _originalU1_1, new byte[]{ 0x59, 0x4F, 0x53, 0x48, 0x49, 0x27, 0x53, 0x20, 0x49, 0x53, 0x4C, 0x41, 0x4E, 0x44, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x15, 0x0B, 0x00, 0x01, 0x33, 0x01 } },
            
            { _originalE1_0, new byte[]{ 0x59, 0x4F, 0x53, 0x48, 0x49, 0x27, 0x53, 0x20, 0x49, 0x53, 0x4C, 0x41, 0x4E, 0x44, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x15, 0x0B, 0x00, 0x02, 0x33, 0x00 } },
            { _originalE1_1, new byte[]{ 0x59, 0x4F, 0x53, 0x48, 0x49, 0x27, 0x53, 0x20, 0x49, 0x53, 0x4C, 0x41, 0x4E, 0x44, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x15, 0x0B, 0x00, 0x02, 0x33, 0x01 } },
        };

        internal static readonly List<GameVersionData> OriginalVersionDatas = new List<GameVersionData>()
        {
            _originalJ1_0,
            _originalJ1_1,
            _originalJ1_2,
            _originalU1_0,
            _originalU1_1,
            _originalE1_0,
            _originalE1_1
        };

        internal static readonly byte[] PracticeHackSegmentSignature = new byte[]
        {
            0x40, 0x00,
            0x41, 0x00,
            0x42, 0x00,
        };

        internal static readonly byte[] PracticeHackSignature = new byte[]
        {
            PracticeHackSegmentSignature[0x00], PracticeHackSegmentSignature[0x01],
            PracticeHackSegmentSignature[0x02], PracticeHackSegmentSignature[0x03],
            PracticeHackSegmentSignature[0x04], PracticeHackSegmentSignature[0x05],

            0x19, 0x00, // P
            0x1B, 0x00, // R
            0x0A, 0x00, // A
            0x0C, 0x00, // C
            0x1D, 0x00, // T
            0x12, 0x00, // I
            0x0C, 0x00, // C
            0x0E, 0x00, // E
            0x3F, 0x00, //  
            0x11, 0x00, // H
            0x0A, 0x00, // A
            0x0C, 0x00, // C
            0x14, 0x00, // K
            //0x3F, 0x00, //  
            //0x01, 0x00, // 1
            //0x24, 0x00, // .
            //0x00, 0x00, // 0
            //0x24, 0x00, // .
            //0x02, 0x00, // 2
            
            //PracticeHackSegmentSignature[0x00], PracticeHackSegmentSignature[0x01],
            //PracticeHackSegmentSignature[0x02], PracticeHackSegmentSignature[0x03],
            //PracticeHackSegmentSignature[0x04], PracticeHackSegmentSignature[0x05],
        };

        internal static readonly Dictionary<byte, char> PracticeHackStringFontMap = new Dictionary<byte, char>()
        {
            {0x00, '0'},
            {0x01, '1'},
            {0x02, '2'},
            {0x03, '3'},
            {0x04, '4'},
            {0x05, '5'},
            {0x06, '6'},
            {0x07, '7'},
            {0x08, '8'},
            {0x09, '9'},
            {0x0A, 'A'},
            {0x0B, 'B'},
            {0x0C, 'C'},
            {0x0D, 'D'},
            {0x0E, 'E'},
            {0x0F, 'F'},
            {0x10, 'G'},
            {0x11, 'H'},
            {0x12, 'I'},
            {0x13, 'J'},
            {0x14, 'K'},
            {0x15, 'L'},
            {0x16, 'M'},
            {0x17, 'N'},
            {0x18, 'O'},
            {0x19, 'P'},
            {0x1A, 'Q'},
            {0x1B, 'R'},
            {0x1C, 'S'},
            {0x1D, 'T'},
            {0x1E, 'U'},
            {0x1F, 'V'},
            {0x20, 'W'},
            {0x21, 'X'},
            {0x22, 'Y'},
            {0x23, 'Z'},
            {0x24, '.'},
            {0x25, ','},
            {0x27, '-'},
            {0x28, '!'},
            {0x29, '='},
            {0x2A, ':'},
            {0x2B, '\''},
            {0x2C, '"'},
            {0x2D, '/'},
            {0x30, '^'},
            {0x31, 'v'},
            {0x32, '<'},
            {0x33, '>'},
            {0x36, '*'},
            {0x3F, ' '},
            {0x46, '+'},
            {0x47, 's'},
            {0x49, '('},
            {0x4A, ')'},
            {0x4D, 'p'},
        };
    }
}
