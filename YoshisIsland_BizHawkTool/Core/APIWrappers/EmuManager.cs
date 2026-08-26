using BizHawk.Client.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoshisIsland_BizHawkTool
{
    internal static class EmuManager
    {
        private const string NULL_SYSTEM_ID = "NULL";
        private const string CORE_EXCEPTION_MESSAGE = "This external tool should be used with a core loaded. Open the game ROM and run the tool again.";

        private static IEmulationApi _emuAPI;
        private static ToolOptions _options;

        internal static void Init(IEmulationApi emuAPI)
        {
            _emuAPI = emuAPI;
            _options = ToolOptions.Instance;
            CheckIfCoreLoaded();   
        }

        private static void CheckIfCoreLoaded()
        {
            if (_emuAPI.GetSystemId() == NULL_SYSTEM_ID)
                throw new NoCoreLoadedException(CORE_EXCEPTION_MESSAGE);
        }
    }
}
