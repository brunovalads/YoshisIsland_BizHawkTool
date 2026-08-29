using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace YoshisIsland_BizHawkTool
{
    internal class EnvironmentException : Exception
    {
        public EnvironmentException()
        {
        }

        public EnvironmentException(string message) : base(message)
        {
        }

        public EnvironmentException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
