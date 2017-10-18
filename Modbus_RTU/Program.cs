using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Modbus_RTU
{
    static class Program
    {
       public static Form1 mainForm;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainForm = new Form1();
            Application.Run(mainForm);
        }
    }
}
