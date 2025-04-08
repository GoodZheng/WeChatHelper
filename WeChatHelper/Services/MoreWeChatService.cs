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
                LogService.Info("开始从注册表中获取Weixin地址.");
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
                LogService.Error("未在注册表中找到Weixin的地址。");
                return null;
            }
            catch (Exception ex)
            {
                LogService.Error("访问注册表失败，原因是：" + ex.Message);
                return null;
            }
        }
    }
}
