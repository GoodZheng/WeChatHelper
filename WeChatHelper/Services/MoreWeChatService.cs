using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatHelper.Services
{
    public class MoreWeChatService
    {
        public string? FindWeChatPath()
        {
            try
            {
                // 打开注册表项 
                using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"Software\Tencent\Weixin"))
                {
                    if (key != null)
                    {
                        // 查询 InstallPath 的值 
                        object? installPath = key.GetValue("InstallPath");
                        if (installPath != null)
                        {
                            return installPath.ToString();
                        }
                    }
                }
                // 如果未找到注册表项或 InstallPath 值，抛出异常 
                throw new System.IO.FileNotFoundException();
            }
            catch (System.IO.FileNotFoundException)
            {
                // 输出错误信息 
                Console.WriteLine("[ERR] WX 4.0 注册表未找到，无法自动检测路径");
                // 暂停程序，等待用户输入 
                Console.WriteLine("按任意键退出...");
                Console.ReadKey();
                // 退出程序 
                Environment.Exit(1);
                return null;
            }
        }
    }
}
