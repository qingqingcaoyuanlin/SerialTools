using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace SerialTool
{
    class SerialTest
    {

        private Thread ThreadSerialRead;
        private SerialPort serialport;
        //public delegate void Serial_Deal_Delegate(byte [] buffer);        //串口数据处理委托
        public delegate void Serial_Deal_Delegate(List <byte>buffer);
        public Serial_Deal_Delegate SerialDealDelegate;
        public List<byte> SerialBuffer = new List<byte>(4096);
        
        public SerialTest(SerialPort port)
        {
            serialport = port;
            ThreadSerialRead = new Thread(Thread_Serial_Deal);
            ThreadSerialRead.IsBackground = true;
            ThreadSerialRead.Start();
        }
       
        public void Thread_Serial_Deal()
        {
            
            int n = 0;
            while (true)
            {
                try
                {
                    if (serialport.IsOpen)
                    {
                        System.Threading.Thread.Sleep(100);
                        n = serialport.BytesToRead;
                        
                        if (n <= 0)
                        {                            
                            continue;
                        }
                        else
                        {
                            byte[] Data_Recv = new byte[n];
                            serialport.Read(Data_Recv, 0, n);
                            SerialBuffer.AddRange(Data_Recv);
                            if(SerialDealDelegate != null)
                            {
                                SerialDealDelegate(SerialBuffer);
                            }
                            
                        }

                    }
                    else
                    { 
                    
                    }
                    

                }
                catch
                {

                }
                finally
                { 
                
                }
                

            }
        
        }
    }
}
