using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KeepPingTool
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            this.lbxLogs.Items.Clear();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string [] allIps = ConfigurationManager.AppSettings.AllKeys;
            foreach (var key in allIps)
            {
                string ip = ConfigurationManager.AppSettings[key];
                BackgroundWorker pingWork = new BackgroundWorker();
                pingWork.DoWork += pingWork_DoWork;
                pingWork.RunWorkerAsync(ip);
            }
        }

        private void pingWork_DoWork(object sender, DoWorkEventArgs e)
        {
            string ip = e.Argument.ToString();
            CmdUtil.SendCommand("ping "+ip+" -t",lbxLogs);
        }

        #region Bottom Notify
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (WindowState == FormWindowState.Minimized)
            {
                this.Visible = false;
                notifyIcon.Visible = true;
                this.notifyIcon.DoubleClick += (sender, args) =>
                {
                    this.Visible = true;
                    WindowState = FormWindowState.Normal;
                    notifyIcon.Visible = false;
                };
            }
        }
        #endregion

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Process process in CmdUtil.cmdProcess) 
            {
                process.Kill();
            }

            Process[] processes = Process.GetProcesses().Where(c => c.ProcessName.StartsWith("PING")).ToArray();

            foreach (Process process in processes)
            {
                process.Kill();
            }
        }
    }
}
