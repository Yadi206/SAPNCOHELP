using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPNCOHELP
{
    public class Ini
    {
        // 声明INI文件的写操作函数 WritePrivateProfileString()

        [System.Runtime.InteropServices.DllImport("kernel32")]

        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        // 声明INI文件的读操作函数 GetPrivateProfileString()

        [System.Runtime.InteropServices.DllImport("kernel32")]

        private static extern int GetPrivateProfileString(string section, string key, string def, System.Text.StringBuilder retVal, int size, string filePath);

        // 声明INI文件的读操作函数 GetPrivateProfileString()

        [System.Runtime.InteropServices.DllImport("kernel32")]

        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);

        private string sPath = null;
        public Ini(string path)
        {
            this.sPath = path;
        }

        public void Writue(string section, string key, string value)
        {
            // section=配置节，key=键名，value=键值，path=路径 
            WritePrivateProfileString(section, key, value, sPath);
        }
        public string ReadValue(string section, string key)
        {
            int aa;
            // 每次从ini中读取多少字节 
            System.Text.StringBuilder temp = new System.Text.StringBuilder(255);
            // section=配置节，key=键名，temp=上面，path=路径 
            aa = GetPrivateProfileString(section, key, "", temp, 255, sPath);
            return temp.ToString();
        }
        /// <summary>
        /// 获取ini文件内所有的section名称
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>返回一个包含section名称的集合</returns>
        public   List<string> GetSectionNames(string filePath)
        {
            byte[] buffer = new byte[2048];
            System.Text.StringBuilder temp = new System.Text.StringBuilder(255);
            int length = GetPrivateProfileString("Configuration", "", "", buffer, 999, filePath); 
            String[] rs = System.Text.UTF8Encoding.Default.GetString(buffer, 0, length).Split(new string[] { "\0" }, StringSplitOptions.RemoveEmptyEntries);
            return rs.ToList();
        }

    }
}
