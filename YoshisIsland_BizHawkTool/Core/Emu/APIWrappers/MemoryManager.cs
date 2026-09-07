using BizHawk.Client.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static BizHawk.Emulation.Common.MemoryDomain;

namespace YoshisIsland_BizHawkTool
{
    internal static class MemoryManager
    {
        private const string ROM_KEYWORD = "ROM";
        private const string SRAM_KEYWORD = "CARTRAM";
        private const string WRAM_KEYWORD = "WRAM";

        private static IMemoryApi _memoryAPI;
        private static ToolOptions _options;
        private static string _romDomainName;
        private static string _sramDomainName;
        private static string _wramDomainName;
        private static Dictionary<MemoryDomain, string> _memoryDomainNameMap;
        private static Dictionary<MemoryDomain, uint> _memoryDomainSizeMap;

        internal static void Init(IMemoryApi memoryAPI)
        {
            _memoryAPI = memoryAPI;
            _options = ToolOptions.Instance;
            CheckMandatoryDomains();
        }

        private static void CheckMandatoryDomains()
        {
            IReadOnlyCollection<string> memoryDomainNameList;
            try
            {
                memoryDomainNameList = _memoryAPI.GetMemoryDomainList();
            }
            catch (Exception ex)
            {
                throw EnvironmentException.CoreNotLoaded(ex);
            }

            _romDomainName = memoryDomainNameList.FirstOrDefault(domainName => domainName.Contains(ROM_KEYWORD));
            if (string.IsNullOrEmpty(_romDomainName))
                throw EnvironmentException.MissingMemoryDomain(MemoryDomain.ROM);

            _sramDomainName = memoryDomainNameList.FirstOrDefault(domainName => domainName.Contains(SRAM_KEYWORD));
            if (string.IsNullOrEmpty(_sramDomainName))
                throw EnvironmentException.MissingMemoryDomain(MemoryDomain.SRAM);

            _wramDomainName = memoryDomainNameList.FirstOrDefault(domainName => domainName.Contains(WRAM_KEYWORD));
            if (string.IsNullOrEmpty(_wramDomainName))
                throw EnvironmentException.MissingMemoryDomain(MemoryDomain.WRAM);

            _memoryDomainNameMap = new Dictionary<MemoryDomain, string>()
            {
                { MemoryDomain.ROM, _romDomainName },
                { MemoryDomain.SRAM, _sramDomainName },
                { MemoryDomain.WRAM, _wramDomainName },
            };

            _memoryDomainSizeMap = new Dictionary<MemoryDomain, uint>()
            {
                { MemoryDomain.ROM, _memoryAPI.GetMemoryDomainSize(_romDomainName) },
                { MemoryDomain.SRAM, _memoryAPI.GetMemoryDomainSize(_sramDomainName) },
                { MemoryDomain.WRAM, _memoryAPI.GetMemoryDomainSize(_wramDomainName) },
            };
        }

        internal static bool FindBytes(MemoryDomain memoryDomain, byte[] bytes, int startOffset, out int? resultAddress)
        {
            uint memoryDomainSize = _memoryDomainSizeMap[memoryDomain];
            string memoryDomainName = _memoryDomainNameMap[memoryDomain];

            IReadOnlyList<byte> domainBytes = _memoryAPI.ReadByteRange(startOffset, (int)memoryDomainSize - startOffset, memoryDomainName);

            resultAddress = ((ReadOnlySpan<byte>)domainBytes.ToArray()).IndexOf(bytes);

            return resultAddress != -1;
        }

        internal static string GetDomainHash(MemoryDomain memoryDomain)
        {
            return _memoryAPI.HashRegion(0x0, (int)_memoryDomainSizeMap[memoryDomain], _memoryDomainNameMap[memoryDomain]);
        }

        internal static uint ReadByte(int address, MemoryDomain memoryDomain)
        {
            return _memoryAPI.ReadByte(address, _memoryDomainNameMap[memoryDomain]);
        }

        internal static long[] ReadByteRange(int address, MemoryDomain memoryDomain, int dataCount, int dataSize = 1, Endian endian = Endian.Little)
        {
            int totalBytes = dataCount * dataSize;

            byte[] rawBytes = _memoryAPI.ReadByteRange(address, totalBytes, _memoryDomainNameMap[memoryDomain]).ToArray();

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
