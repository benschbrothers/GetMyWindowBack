using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetMyWindowBack
{
    class Program
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("##########################################");
            Console.WriteLine("#                                        #");
            Console.WriteLine("#    GetMyWindowBack !                   #");
            Console.WriteLine("#    Author: Chris B.                    #");
            Console.WriteLine("#                                        #");
            Console.WriteLine("##########################################");
            Console.WriteLine(" ");

            bool interrupt = false;

            Process[] processes = Process.GetProcesses();

            int pCount = 0;
            IntPtr[] handle = new IntPtr[processes.Length];

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Window list:");
            foreach (var process in processes)
            {
                handle[pCount] = process.MainWindowHandle;

                if (IsWindowVisible(handle[pCount]))
                    Console.WriteLine("{0} {1}", pCount, process.ProcessName);

                pCount++;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            while (!interrupt)
            {
                Console.WriteLine(" ");
                Console.WriteLine("Enter number or X to exit:");
                string line = Console.ReadLine();
                if (line == "X" || line == "x")
                    interrupt = true;
                else
                    SetWindowPos(handle[Int32.Parse(line)], 0, 10, 10, 500, 500, 0);
            }

            Console.WriteLine(" ");
            Console.WriteLine("Bye!");
        }
    }
}
