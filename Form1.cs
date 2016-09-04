using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;


namespace SerialTool
{
    
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            SerialPort_init();
            
        }
        private SerialPort myport;
        private SerialTest serialtest;
        static long CommandCount= 0;
        static long CommandErrorCount = 0;
        static long CommandAll = 0;
        const int CommondLength = 10; 

        private void SerialPort_init()
        {
            string[] port = SerialPort.GetPortNames();
            Array.Sort(port);
            myport = new SerialPort();
            if (port.Length > 0)
            {
                comboBox_Serial.Items.AddRange(port);
                comboBox_Serial.SelectedIndex = 0;
                myport.PortName = comboBox_Serial.Text;
            }
        }
        
        private void btn_open_Click(object sender, EventArgs e)
        {
            if(btn_open.Text.Equals("打开"))
            {
                if (myport.PortName != null)
                {
                    myport.BaudRate = 9600;
                    btn_open.Text = "关闭";
                    myport.Open();
                    serialtest = new SerialTest(myport);
                    serialtest.SerialBuffer.Clear();
                    this.serialtest.SerialDealDelegate = this.SerialDataUIProcess;

                }
                
            }
            else if (btn_open.Text.Equals("关闭"))
            {
                if (myport.IsOpen)
                {
                    myport.Close();
                    btn_open.Text = "打开";
                }
                
            }
        }

        private void comboBox_Serial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (myport.IsOpen)
            {
                if (myport.PortName != comboBox_Serial.SelectedItem.ToString())
                {
                    MessageBox.Show("请先关闭串口");
                }
            }
            else
            {
                myport.PortName = comboBox_Serial.SelectedItem.ToString();
            }
        }

        public void PrintMesg(string str)
        {
            this.Invoke(new EventHandler(delegate
            {
                this.richTextBox_Msg.AppendText(str);
                this.richTextBox_Msg.ScrollToCaret();
            }));
        }

        public void DrawCommandCount()
        {
            this.Invoke(new EventHandler(delegate
            {
                this.textBox_Msg.Clear();
                this.textBox_Msg.Text += CommandCount.ToString();
                this.textBox_All.Clear();
                this.textBox_All.Text += CommandAll.ToString();
            }));
        }
        public void DrawCommandErrorCount()
        {
            this.Invoke(new EventHandler(delegate
            {
                this.textBox_Error.Clear();
                this.textBox_Error.Text += CommandErrorCount.ToString();
                
            }));
        }

        private string Resident_Msg(byte Floor, byte Room)
        {
            StringBuilder str = new StringBuilder();
            byte N;
            str.AppendFormat("住户");
            N = (byte)(Floor & 0x80);
            if(N == 0x80)
            {
                str.AppendFormat("-");
            }
            N = (byte)(Floor & 0x70);
            N = (byte)(N >> 4);
            str.AppendFormat(N.ToString()); 
            N = (byte)(Floor & 0x0F);
            str.AppendFormat(N.ToString()); 
            str.AppendFormat("层");
            N = (byte)(Room & 0xF0);
            N = (byte)(N >> 4);
            str.AppendFormat(N.ToString());
            N = (byte)(Room & 0x0F);
            str.AppendFormat(N.ToString());
            str.AppendFormat("房");
            return str.ToString();       
        }

        private void WriteLog(string loginfo)
        {
            string fname = Directory.GetCurrentDirectory() + "\\LogFile.txt";
            FileInfo finfo = new FileInfo(fname);
            string str = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " " + loginfo;
            if (!finfo.Exists)
            {
                FileStream fs;
                fs = File.Create(fname);
                fs.Close();
                finfo = new FileInfo(fname);
            }
            if (finfo.Length > 1024 * 1024 * 10)
            {
                ///文件超过10MB则重命名
                File.Move(Directory.GetCurrentDirectory() + "\\LogFile.txt", Directory.GetCurrentDirectory() + DateTime.Now.TimeOfDay + "\\LogFile.txt");
            }

            StreamWriter sw = new StreamWriter(fname,true);
            sw.WriteLine(str);            
            sw.Close();
        }
        private void SerialDataUIProcess(List<byte>Buffer)
        {
            byte CheckTemp = 0;
            int times = Buffer.Count();
            StringBuilder str = new StringBuilder();
            byte[] DataArray = new byte[CommondLength]; 
            if (Buffer.Count() >= CommondLength)
            {


                while (Buffer.Count() >= CommondLength && Buffer[0] != 0x7E)
                {
                    Buffer.RemoveAt(0);
                    CommandAll++;
                    if (Buffer.Count() < CommondLength)
                    {
                        return;
                    }

                }
                for (int j = 0; j < times/CommondLength; j++)
                {
                    CheckTemp = (byte)(Buffer[0] + Buffer[1] + Buffer[2] + Buffer[3] + Buffer[4] + Buffer[5] + Buffer[6] + Buffer[7]);

                    for (int i = 0; i < CommondLength; i++)
                    {
                        str.AppendFormat(Buffer[i].ToString("X2"));
                        str.AppendFormat(" ");
                    }

                    str.AppendFormat(":");
                    if (Buffer[0] != 0x7E || Buffer[9] != 0x0F || Buffer[8] != CheckTemp)
                    {
                        Buffer.RemoveRange(0, CommondLength);
                        CommandErrorCount++;
                        DrawCommandErrorCount();
                        str.AppendFormat("头尾或者校验错误\n");
                        
                        WriteLog(str.ToString());
                        PrintMesg(str.ToString());
                        
                        return;


                    }
                    //PrintMesg("校验正确");
                    switch (Buffer[1])
                    {
                        case 0xA1:
                            str.AppendFormat(Resident_Msg(Buffer[4], Buffer[5]));
                            str.AppendFormat("给门口机开锁，电梯将会到达");
                            str.AppendFormat(Resident_Msg(Buffer[6], Buffer[7]));
                            str.AppendFormat("\n");
                            //PrintMesg(str.ToString());

                            break;

                        case 0xA2:
                            str.AppendFormat("户户通呼梯，");
                            str.AppendFormat(Resident_Msg(Buffer[6], Buffer[7]));
                            str.AppendFormat("前往");
                            str.AppendFormat(Resident_Msg(Buffer[4], Buffer[5]));
                            str.AppendFormat("\n");
                            //PrintMesg(str.ToString());


                            break;
                        case 0xA3:
                            str.AppendFormat(Resident_Msg(Buffer[4], Buffer[5]));
                            str.AppendFormat("主动呼梯\n");
                           // PrintMesg(str.ToString());


                            break;
                        case 0xA4:
                            str.AppendFormat(Resident_Msg(Buffer[4], Buffer[5]));
                            str.AppendFormat("刷卡开锁或者密码开锁\n");
                            //PrintMesg(str.ToString());

                            break;
                        case 0xA5:
                            str.AppendFormat("❤\n");
                           // PrintMesg(str.ToString());

                            break;

                        default:

                            str.AppendFormat("错误指令\n");
                            //PrintMesg(str.ToString());
                            break;

                    }
                    Buffer.RemoveRange(0, CommondLength);
                    PrintMesg(str.ToString());
                    WriteLog(str.ToString());
                    str.Clear();
                    CommandCount++;
                    CommandAll += CommondLength;
                    DrawCommandCount();
                }
                
            }
            else
            {
               

            }
            
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.Invoke(new EventHandler(delegate
            {
                this.richTextBox_Msg.Clear();           
            }));
        }
    }
}
