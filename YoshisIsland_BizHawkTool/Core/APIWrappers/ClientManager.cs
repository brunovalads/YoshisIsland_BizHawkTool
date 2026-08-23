using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizHawk.Client.Common;

namespace YoshisIsland_BizHawkTool
{
    internal static class ClientManager
    {
        private static IEmuClientApi _clientAPI;

        internal static void Init(IEmuClientApi clientAPI)
        {
            _clientAPI = clientAPI;
            SetGaps();
        }

        internal static void SetGaps()
        {
            _clientAPI.SetGameExtraPadding(
                ToolOptions.Instance.LeftGap,
                ToolOptions.Instance.TopGap,
                ToolOptions.Instance.RightGap,
                ToolOptions.Instance.BottomGap
            );
        }

        internal static void ClearGaps()
        {
            _clientAPI.SetGameExtraPadding(0, 0, 0, 0);
        }
    }
}
