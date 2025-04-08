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
using WeChatHelper.Logs;
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
            LogService.Info("开始从注册表中获取Weixin地址.");

            string? weixinPath = _moreWeChatService.FindWeChatPath();
            if (string.IsNullOrEmpty(weixinPath))
            {
                MessageBox.Show("未能找到微信的路径，请手动指定！");
            }
            else
            {
                MessageBox.Show($"{weixinPath}");
            }
        }
    }
}
