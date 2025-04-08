using Microsoft.Extensions.DependencyInjection;
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
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceProvider = ConfigureServices();

            // 4. 获取服务并调用
            serviceProvider.GetRequiredService<MainView>().Show();
        }

        private static IServiceProvider ConfigureServices()
        {
            // 1. 创建服务集合对象
            var serviceCollection = new ServiceCollection();

            // 2. 添加服务
            serviceCollection.AddSingleton<MainView>();
            serviceCollection.AddSingleton<MoreWeChatService>();

            // 3. 用服务集合创建服务提供器
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
