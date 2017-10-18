using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Modbus_RTU
{
    public partial class Form1 : Form
    {
        #region 串口私有属性
        private string _portName;
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        #region 控件初始化

        /// <summary>
        /// 窗口加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadPortAttribute();
            
        }


        /// <summary>
        /// 加载默认设置
        /// </summary>
        private void LoadPortAttribute()
        {

            try
            {
                //端口号
                String[] ports = SerialPort.GetPortNames();
                if (ports.Length > 0)
                {
                    comboBoxPort.Items.Clear();
                    comboBoxPort.Items.AddRange(ports); 
                }

                //波特率
                comboBoxBaud.SelectedIndex = 4;//115200
                //数据位
                comboBoxDateBite.SelectedIndex = 3;//8
                //校验位
                comboBoxParity.SelectedIndex = 0; //None
                //停止位
                comboBoxStopBit.SelectedIndex = 0;//1
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }

        }


        #endregion
   
        /// <summary>
        /// 串口属性写入
        /// </summary>
        private void getPortAttributes()
        {

                if (serialPort1 == null)
                {
                    serialPort1 = new SerialPort();
                }

            try
            {
                serialPort1.PortName = comboBoxPort.Text;
                serialPort1.BaudRate = Convert.ToInt32(comboBoxBaud.Text);
                serialPort1.DataBits = Convert.ToInt32(comboBoxDateBite.Text);


                switch (comboBoxStopBit.SelectedIndex)
                {
                    case 0:
                        serialPort1.StopBits = StopBits.One;
                        break;
                    case 1:
                        serialPort1.StopBits = StopBits.OnePointFive;
                        break;
                    case 2:
                        serialPort1.StopBits = StopBits.Two;
                        break;
                    default:
                        break;
                }

                switch (comboBoxParity.SelectedIndex)
                {
                    case 0:
                        serialPort1.Parity = Parity.None;
                        break;
                    case 1:
                        serialPort1.Parity = Parity.Even;
                        break;
                    case 2:
                        serialPort1.Parity = Parity.Odd;
                        break;
                    case 3:
                        serialPort1.Parity = Parity.Mark;
                        break;
                    case 4:
                        serialPort1.Parity = Parity.Space;
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);     
            }
        }

        //串口发送按钮
        private void buttonSend_Click(object sender, EventArgs e)
        {
            string sendString = textBoxSend.Text;
            //开启发送线程
         ThreadPool.QueueUserWorkItem(new WaitCallback(Background.BackgroundSend), sendString);

        }

        //串口打开与关闭
        private void buttonOpen_Click(object sender, EventArgs e)
        {
           
            if (comboBoxPort.Text.Equals(""))
            {
                MessageBox.Show("请选择串口");
            }
            else
            {
                
                if (!serialPort1.IsOpen)//开始
                {
                    getPortAttributes();
                    try
                    {
                        serialPort1.Open();
                        buttonOpen.Text = "停止";

                        comboBoxBaud.Enabled = false;
                        comboBoxDateBite.Enabled = false;
                        comboBoxPort.Enabled = false;
                        comboBoxParity.Enabled = false;
                        comboBoxStopBit.Enabled = false;

                  //      ThreadPool.QueueUserWorkItem(new WaitCallback())
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        
                    }

                }
                else //停止
                {
                    buttonOpen.Text = "打开串口";

                    serialPort1.Close();

                    comboBoxBaud.Enabled = true;
                    comboBoxDateBite.Enabled = true;
                    comboBoxPort.Enabled = true;
                    comboBoxParity.Enabled = true;
                    comboBoxStopBit.Enabled = true;


                }
                
            }
            
        }
    }
}
