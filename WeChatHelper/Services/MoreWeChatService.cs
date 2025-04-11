using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatHelper.Services
{
    public class MoreWeChatService
    {
        private string? FindWeChatPath()
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


        public string GetExePath()
        {
            string? basePath = FindWeChatPath();
            string fullPath = string.Empty;
            if (basePath != null)
            {
                fullPath = Path.Combine(basePath, "Weixin.exe");
            }
            return fullPath;
        }


        public string GetDllPath()
        {
            string? basePath = FindWeChatPath();
            string dllFullPath = string.Empty;

            if (Directory.Exists(basePath))
            {
                string[] directories = Directory.GetDirectories(basePath);
                foreach (string version in directories)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(version);
                    if (dirInfo.Name.StartsWith("4."))
                    {
                        dllFullPath = Path.Combine(version, "Weixin.dll");
                        if (File.Exists(dllFullPath))
                        {
                            LogService.Info($"在{basePath}中找到版本号为4的微信.dll地址:{dllFullPath}。");
                            return dllFullPath;
                        }
                    }
                }
                LogService.Error($"未在{basePath}中找到版本号为4的微信.dll地址。");
            }
            return dllFullPath;
        }

        // 将字节数据转换为十六进制字符串表示
        public string b2hex(byte[] data, int? max_len = 32)
        {
            StringBuilder hexBuilder = new StringBuilder();
            foreach (byte b in data)
            {
                hexBuilder.AppendFormat("{0:X2}", b);
            }
            string hexStr = hexBuilder.ToString();

            if (max_len.HasValue && hexStr.Length > max_len.Value)
            {
                hexStr = hexStr.Substring(0, max_len.Value) + "...";
            }
            return hexStr;
        }

        // 从文件中加载二进制数据
        public byte[]? load(string file_path)
        {
            try
            {
                return File.ReadAllBytes(file_path);
            }
            catch (Exception ex)
            {
                // 这里可以根据实际需求对异常进行更详细的处理 
                LogService.Error($"读取文件时发生错误: {ex.Message}");
                return null;
            }
        }

        // 将二进制数据保存到文件
        public bool save(string file_path, byte[] data)
        {
            LogService.Info($"\n> 保存 {file_path}");
            try
            {
                File.WriteAllBytes(file_path, data);
                LogService.Info("[√] 文件已保存");
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                LogService.Error($"[ERR] 文件 '{file_path}' 正在使用中，请关闭它并重试");
                return false;
            }
        }
    }
}
