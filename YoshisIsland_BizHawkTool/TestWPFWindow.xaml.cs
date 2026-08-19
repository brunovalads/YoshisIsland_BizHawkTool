using BizHawk.Client.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YoshisIsland_BizHawkTool
{
    /// <summary>
    /// Interaction logic for TestWPFWindow.xaml
    /// </summary>
    public partial class TestWPFWindow : Window
    {
        private ApiContainer APIs;

        public TestWPFWindow(ApiContainer apis)
        {
            InitializeComponent();
            APIs = apis;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("TEST button clicked! (Debug)");
            Console.WriteLine("TEST button clicked! (Console)");
            APIs?.Gui.AddMessage("TEST button clicked! (Gui)", 10);
        }
    }
}
