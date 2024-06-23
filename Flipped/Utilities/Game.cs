using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Flipped.Utilities
{
    public static class Game
    {
        public static Process _FortniteProcess;
        public static void Start(string PATH, string args, string exchange_code)
        {
            if (exchange_code == null)
            {
                return;
            }
            if (File.Exists(Path.Combine(PATH, "FortniteGame\\Binaries\\Win64\\", "FortniteClient-Win64-Shipping.exe")))
            {
                Game._FortniteProcess = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        Arguments = args + $"-AUTH_LOGIN=unused -AUTH_PASSWORD={exchange_code} -AUTH_TYPE=exchangecode ",
                        FileName = Path.Combine(PATH, "FortniteGame\\Binaries\\Win64\\", "FortniteClient-Win64-Shipping.exe")
                    },
                    EnableRaisingEvents = true
                };
                Game._FortniteProcess.Exited += new EventHandler(Game.OnFortniteExit);
                Game._FortniteProcess.Start();
            }
            else
            {

            }
        }

        public static void OnFortniteExit(object sender, EventArgs e)
        {
            Process fortniteProcess = Game._FortniteProcess;
            if (fortniteProcess != null && fortniteProcess.HasExited)
            {
                Game._FortniteProcess = (Process)null;
            }
            FakeAC._FNLauncherProcess?.Kill();
            FakeAC._FNAntiCheatProcess?.Kill();
        }
    }
}
