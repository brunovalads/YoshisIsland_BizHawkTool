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
        private const string CORE_NOT_LOADED_EXCEPTION_MESSAGE = "This external tool should be used with a core loaded. Open the game ROM and run the tool again.";
        private const string YI_NOT_LOADED_EXCEPTION_MESSAGE = "This external tool should only be used for Yoshi's Island (SNES, any version or hack)."; // TODO: Remove "SNES" if I make it work with GBA too
        private const string MISSING_MEMORY_DOMAIN_EXCEPTION_MESSAGE = "domain is not available in this core, please change to another one.";

        private EnvironmentException()
        {
        }

        private EnvironmentException(string message) : base(message)
        {
        }

        private EnvironmentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        internal static EnvironmentException CoreNotLoaded(Exception innerException = null)
        {
            if (innerException == null)
                return new EnvironmentException(CORE_NOT_LOADED_EXCEPTION_MESSAGE);
            else
                return new EnvironmentException(CORE_NOT_LOADED_EXCEPTION_MESSAGE, innerException);
        }

        internal static EnvironmentException YINotLoaded(Exception innerException = null)
        {
            if (innerException == null)
                return new EnvironmentException(YI_NOT_LOADED_EXCEPTION_MESSAGE);
            else
                return new EnvironmentException(YI_NOT_LOADED_EXCEPTION_MESSAGE, innerException);
        }

        internal static EnvironmentException MissingMemoryDomain(MemoryDomain memoryDomain, Exception innerException = null)
        {
            string message = $"{memoryDomain} {MISSING_MEMORY_DOMAIN_EXCEPTION_MESSAGE}";
            if (innerException == null)
                return new EnvironmentException(message);
            else
                return new EnvironmentException(message, innerException);
        }
    }
}
