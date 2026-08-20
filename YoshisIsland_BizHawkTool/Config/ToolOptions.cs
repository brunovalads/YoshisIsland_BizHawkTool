using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoshisIsland_BizHawkTool
{
    internal class ToolOptions
    {
        private static ToolOptions _instance;
        public static ToolOptions Instance => _instance ??= new ToolOptions();

        public bool DisplayPlayerInfo { get; set; } = false;
    }
}
