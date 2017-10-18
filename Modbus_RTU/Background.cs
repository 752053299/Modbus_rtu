using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Modbus_RTU
{
    class Background
    {

        public static void BackgroundSend(object sender)
        {
            try
            {
                string sendString = (string)sender;
                char[] buffer = sendString.ToCharArray();
                Program.mainForm.serialPort1.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
        }

    }

 
}
