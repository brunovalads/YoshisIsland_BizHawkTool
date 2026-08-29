using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using BizHawk.Emulation.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BizHawk.Emulation.Common.MemoryDomain;

namespace YoshisIsland_BizHawkTool
{
    internal static class MemoryManager
    {
        private static IMemoryApi _memoryAPI;
        private static ToolOptions _options;

        internal static void Init(IMemoryApi memoryAPI)
        {
            _memoryAPI = memoryAPI;
            _options = ToolOptions.Instance;
        }

        internal static bool FindBytes(string memoryDomain, byte[] bytes, int startOffset, out int? resultAddress)
        {
            IReadOnlyList<byte> domainBytes = _memoryAPI.ReadByteRange(startOffset, (int)_memoryAPI.GetMemoryDomainSize(memoryDomain) - startOffset, memoryDomain);

            resultAddress = ((ReadOnlySpan<byte>)domainBytes.ToArray()).IndexOf(bytes);

            return resultAddress != -1;
        }

        internal static string GetRomHash()
        {
            // TODO: Get rom domain automatically
            return _memoryAPI.HashRegion(0x0, (int)_memoryAPI.GetMemoryDomainSize("CARTROM"), "CARTROM");
        }

        internal static uint ReadByteRom(int address)
        {
            // TODO: Get rom domain automatically
            return _memoryAPI.ReadByte(address, "CARTROM");
        }

        internal static long[] ReadBytesRom(int address, int dataCount, int dataSize = 1, Endian endian = Endian.Little)
        {
            int totalBytes = dataCount * dataSize;

            // TODO: Get rom domain automatically
            byte[] rawBytes = _memoryAPI.ReadByteRange(address, totalBytes, "CARTROM").ToArray();

            long[] result = new long[dataCount];

            for (int i = 0; i < dataCount; i++)
            {
                int offset = i * dataSize;
                long value = 0;

                for (int b = 0; b < dataSize; b++)
                {
                    if (endian == Endian.Little)
                        value |= ((long)rawBytes[offset + b]) << (8 * b);
                    else // Endian.Big
                        value = (value << 8) | rawBytes[offset + b];
                }

                result[i] = value;
            }

            return result;
        }
    }
}
