using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace YoshisIsland_BizHawkTool
{
    [ExternalTool("Yoshi's Island Utilities", Description = "Yoshi's Island (SNES) Utilities for BizHawk 2.10+")] // this appears in the Tools > External Tools submenu in EmuHawk
    [ExternalToolEmbeddedIcon("YoshisIsland_BizHawkTool.Resources.Images.yoshi_icon_16px.png")]
    public class ToolForm : ToolFormBase, IExternalToolForm
    {
        // CONSTANTS
        public const string TOOL_NAME = "YoshisIsland_BizHawkTool";
        public const string WINDOW_TITLE = "Yoshi's Island Utilities Tool";


        // PROPERTIES
        protected override string WindowTitleStatic => WINDOW_TITLE;
        public ApiContainer? _maybeAPIContainer { get; set; }
        private ApiContainer APIs => _maybeAPIContainer!;


        // CONSTRUCTOR
        public ToolForm()
        {
            ClientSize = new Size(480, 320);
            SuspendLayout();
            Controls.Add(new Label { AutoSize = true, Text = "Yoshi!!" });
            Button wpfTestButton = new Button { AutoSize = true, Text = "Test WPF", Width = 200 };
            wpfTestButton.Click += WpfTestButton_Click;
            Controls.Add(wpfTestButton);
            ResumeLayout(performLayout: false);
            PerformLayout();
            this.Icon = Properties.Resources.yoshi_icon_16px;
        }

        private void WpfTestButton_Click(object sender, EventArgs e)
        {
            TestWPFWindow testWPFWindow = new TestWPFWindow(APIs);
            testWPFWindow.Show();
        }

        // METHODS
        public override void Restart()
        {
            base.Restart();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolForm));
            this.SuspendLayout();
            // 
            // ToolForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ToolForm";
            this.ResumeLayout(false);

        }
    }
}
