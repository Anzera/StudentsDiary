using System;
using System.IO;
using System.Windows.Forms;

namespace StudentsDiary
{
    static class Program
    {
        public static string FilePath = Path.Combine(Environment.CurrentDirectory, "students.txt");//combine -> przkazujemy parametry, które mają być połączone / 
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
