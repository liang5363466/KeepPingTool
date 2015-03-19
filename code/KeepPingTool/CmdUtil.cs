using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KeepPingTool
{
    public class CmdUtil
    {
        public static IList<Process> cmdProcess = new List<Process>();
        
        public static string SendCommand(string command, ListBox lbxMsg)
        {
            string output = "";
            try
            {
                Process cmd = new Process();
                cmdProcess.Add(cmd);
                cmd.StartInfo.FileName = "cmd.exe";
                //关闭Shell的使用 
                cmd.StartInfo.UseShellExecute = false;
                //重定向标准输入 
                cmd.StartInfo.RedirectStandardInput = true;
                //重定向标准输出 
                cmd.StartInfo.RedirectStandardOutput = true;
                //重定向错误输出 
                cmd.StartInfo.RedirectStandardError = true;
                //设置不显示窗口 
                cmd.StartInfo.CreateNoWindow = true;
                cmd.Start();
                cmd.StandardInput.WriteLine(command);
                while (true)
                {
                    bool isArrive = cmd.StandardOutput.ReadLine().Contains(command);
                    if (isArrive) break;
                }
                while (true)
                {
                    output = cmd.StandardOutput.ReadLine();
                    lbxMsg.BeginInvoke(new Action(() =>
                    {
                        lbxMsg.Items.Add(output);
                    }));
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
