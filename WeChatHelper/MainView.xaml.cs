using System;
using System.Collections.Generic;
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
using WeChatHelper.Services;

namespace WeChatHelper
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        private readonly MoreWeChatService _moreWeChatService;

        public MainView(MoreWeChatService moreWeChatService)
        {
            InitializeComponent();

            _moreWeChatService = moreWeChatService;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            string? wxExePath = _moreWeChatService.GetExePath();
            if (string.IsNullOrEmpty(wxExePath))
            {
                MessageBox.Show("未能找到微信的 Weixin.exe 路径，请手动指定！");
            }
            else
            {
                MessageBox.Show($"找到微信的路径：{wxExePath}");
            }
        }
    }
}
