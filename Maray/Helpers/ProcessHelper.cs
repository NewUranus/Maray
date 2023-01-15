using Maray.Configs;
using Maray.Models.Configs;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.Helpers
{
    public class ProcessHelper
    {
        public Action<object> logAction;
        private Process Process;

        public ProcessHelper(Action<object> LogDelegate)
        {
            logAction = LogDelegate;

            v2rayCorePath = PathConfig.v2rayExePath;
            v2ratCoreWorkingPath = PathConfig.GetParentDirectory(v2rayCorePath);
        }

        public void ShowMessage(object strMsg)
        {
            logAction?.Invoke(strMsg);
        }

        public string v2rayCorePath { get; set; }
        public string v2ratCoreWorkingPath { get; set; }

        public void StartCore(CoreInfo coreInfo)
        {
            Process = new Process();
            Process.StartInfo = new ProcessStartInfo()
            {
                FileName = v2rayCorePath,
                Arguments = coreInfo.arguments,
                WorkingDirectory = v2ratCoreWorkingPath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8,
            };

            if (true)
            {
                Process.OutputDataReceived += Process_OutputDataReceived;
            }
            Process.Start();
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                ShowMessage(e.Data);
            }
        }
    }
}