using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace YoshisIsland_BizHawkTool
{
    internal class NoCoreLoadedException : Exception
    {
        public NoCoreLoadedException()
        {
        }

        public NoCoreLoadedException(string message) : base(message)
        {
        }

        public NoCoreLoadedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
