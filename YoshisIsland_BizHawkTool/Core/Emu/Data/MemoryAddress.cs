using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizHawk.Client.Common;

namespace YoshisIsland_BizHawkTool
{
    internal class MemoryAddress
    {
        internal int Address { get; }
        internal int Size { get; }
        internal MemoryDomain Domain { get; }

        internal MemoryAddress(int address, int size, MemoryDomain domain)
        {
            Address = address;
            Size = size;
            Domain = domain;
        }
    }
}
