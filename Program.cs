using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Com.GitHub.ZachDeibert.BatchLauncher {
	class MainClass {
		public static void Main(string[] args) {
			UriBuilder uri = new UriBuilder(Assembly.GetExecutingAssembly().CodeBase);
			string filename = string.Concat(Uri.UnescapeDataString(uri.Path), ".bat");
			if (File.Exists(filename)) {
				ProcessStartInfo psi = new ProcessStartInfo();
				psi.Arguments = args.Aggregate("", (a, b) => string.Concat(a, " \"", b, "\""));
				psi.FileName = filename;
				psi.UseShellExecute = true;
				Process proc = Process.Start(psi);
				proc.WaitForExit();
				Environment.Exit(proc.ExitCode);
			} else {
				Console.Error.WriteLine("Please call the batch script {0}", filename);
			}
		}
	}
}
