using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Resistr.DynamicDnsClient.AzCli
{
    internal static class ProcessStartInfoExtensions
    {
        public static async Task<(string, string, int)> RunProcess(this ProcessStartInfo processStartInfo)
        {
            var stdError = new StringBuilder();
            var stdOut = new StringBuilder();
            var process = new Process
            {
                EnableRaisingEvents = true,
                StartInfo = processStartInfo
            };
            process.ErrorDataReceived += (sender, e) => stdError.Append(e.Data);
            process.OutputDataReceived += (sender, e) => stdOut.Append(e.Data);
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            await Task.Run(() => process.WaitForExit());
            return (stdOut.ToString(), stdError.ToString(), process.ExitCode);
        }
    }
}
