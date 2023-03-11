using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Text;
using System.Diagnostics.PerformanceData;
using System.Globalization;

// kilde koder Canvas 2023
// kilde koder:https://github.com/SharpenMyC 
//Kilde koder :https://github.com/SharpenMyC/SerialPortCom
//kilde koder https://usn.instructure.com/courses/23018/files/folder/%C3%98vinger?preview=1798534
//kilde koder:https://usn.instructure.com/courses/23018/files/folder/%C3%98vinger?
//kilde koder:https://usn.instructure.com/courses/25900
// Fikk masse hjelp av Christian Hovden og Odd Smith-Jahansen

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {


        DateTime startTime;
        DateTime summaryTime;
        List<int> analogRead = new List<int>();
        List<DateTime> voltageDiff = new List<DateTime>();
        int indexTime = 0;


        public Form1()
        {
            InitializeComponent();
            chart1.Titles.Add("Serial Communication");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ComPorts = SerialPort.GetPortNames();
            timer1.Interval = 5000;
            //timer1.Tick += new EventHandler(timer1_Tick);

            startTime = DateTime.Now;

        }

        private void Form1_MouseEnter(object sender, EventArgs e)// koder fra Canvas 2023
        {
            this.BackColor = SystemColors.Control;
            Trace.WriteLine(this.BackColor.ToString());
            Control anyComponent = sender as Control;
            if (anyComponent != null)
            {
                anyComponent.Cursor = Cursors.Hand;
            }
        }

        private void Form1_MouseLeave(object sender, EventArgs e)// koder fra Canvas 2023
        {
            this.BackColor = SystemColors.Window;
            Trace.WriteLine(this.BackColor.ToString());
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)// koder fra Canvas 2023
        {
            if (e.KeyChar == '+')
            {
                this.Width += 20;
                this.Width += 15;
            }
            if (e.KeyChar == '-')
            {
                this.Width -= 20;
                this.Width -= 15;
            }
        }

        private void label1_Click_1(object sender, EventArgs e)// koder fra Canvas 2023
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) // kiled koder Canvas pdf 2023
        {
            textBoxRegister.AppendText("SensorName:" + textBoxSensorName.Text + "\r\n");
            textBoxRegister.AppendText("SerialNumber:" + maskedTextBoxSerialNumber.Text + "\r\n");
            textBoxRegister.AppendText("SignalType:" + comboBoxSignalType.Text + "\r\n");
            textBoxRegister.AppendText("Options:" + listBoxOptions.Text + "\r\n");
            textBoxRegister.AppendText("Comment:" + textBoxComment.Text + "\r\n");
            textBoxRegister.AppendText("MeasurType:" + comboBoxMeasurType.Text + "\r\n");

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)// koder fra Canvas 2023
        {
            maskedTextBoxSerialNumber.Text = "";
            checkBoxRegistered.Checked = false;
        }

        private void maskedTextBoxSerialNumber_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            this.maskedTextBoxSerialNumber.Mask= "00/00/0000";
            //this.maskedTextBoxSerialNumber.Select(0,0);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)// koder fra Canvas 2023
        {
            this.Close();

        }

        private void buttonRegisterNew_Click(object sender, EventArgs e) // koder fra Canvas 2023
        {

            textBoxRegister.AppendText("SensorName: " + textBoxSensorName.Text + "\r\n");
            textBoxRegister.AppendText("Serial Number: " + maskedTextBoxSerialNumber.Text + "\r\n");
            textBoxRegister.AppendText("SignalType: " + comboBoxSignalType.Text + "\r\n");
            textBoxRegister.AppendText("Mesure Type: " + comboBoxMeasurType.Text + "\r\n");
            textBoxRegister.AppendText("Options: " + listBoxOptions.Text + "\r\n");
            textBoxRegister.AppendText("Comment: " + textBoxComment.Text + "\r\n");
            textBoxRegister.AppendText("" + dateTimePicker1+"\r\n");
            textBoxRegister.AppendText("Tagname:" + textBoxTagname.Text + "\r\n\n");

            toolStripStatusLabel3.Text = "New register";

        }

        private void Form1_Leave(object sender, EventArgs e)
        {
            Control anyComponent = sender as Control;
            if (anyComponent != null)
            {
                anyComponent.Cursor = Cursors.Default;
            }
        }

        private void tabPage1_Click_1(object sender, EventArgs e)// koder fra Canvas 2023
        {
            if (maskedTextBoxSerialNumber.Text.Length > 0)
            {
                this.maskedTextBoxSerialNumber.Select(maskedTextBoxSerialNumber.Text.Length, 0);
            }
            else
            {
                this.maskedTextBoxSerialNumber.Select(0, 0);
            }
        }

        private void closeToolStripMenuItem_Click_1(object sender, EventArgs e)// koder fra Canvas 2023
        {
            this.Close();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)// koder fra Canvas 2023
        {

            textBoxSensorName.Text = "";
            maskedTextBoxSerialNumber.Text = "";
            dateTimePicker1.Value = System.DateTime.Now;
            textBoxComment.Clear();
            listBoxOptions.SelectedIndex = 0;
            textBoxTagname.Text = "";
            comboBoxSignalType.Text = "";
            comboBoxMeasurType.Text = "";
            checkBoxRegistered.Checked = false;  

        }

        private void buttonSend_Click(object sender, EventArgs e)// koder fra Canvas 2023
        {

            if(serialPort1.IsOpen)
            {
                serialPort1.WriteLine(textBoxSend.Text);
                textBoxTEXTserialResult.AppendText("Sent" + textBoxSend.Text + "\r\n");
                textBoxSend.Text = null;
            }
            else
            {
                MessageBox.Show("porten er ikke åpen!");// koden tatt fra Canvas pdf 04-6 serial-Port
            }
  
           //  if (textBoxSend.Text.Length > 0)
           // {
           //     serialPort1.WriteLine(textBoxSend.Text);
           // }
           
        }

        private void buttonConnect_Click(object sender, EventArgs e)  // kilde:https://github.com/SharpenMyC/SerialPortCom
        {
            string[] ComPorts = System.IO.Ports.SerialPort.GetPortNames();


            if (comboBoxComPort.SelectedIndex > -1)
            {
                serialPort1.PortName = comboBoxComPort.Items[comboBoxComPort.SelectedIndex].ToString();
                if (comboBoxBaudRate.SelectedIndex > -1)
                {
                    serialPort1.BaudRate = Convert.ToInt32(comboBoxBaudRate.Items[comboBoxBaudRate.SelectedIndex]);
                    try
                    {

                        serialPort1.Open();
                        radioButtonConnected.Checked = true;
                        textBoxTEXTserialResult.Text = "Open";
                        MessageBox.Show(" Connect");

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Unable to connect");
                        radioButtonConnected.Checked = false;
                    }


                    timer1.Enabled = true;
                }

            }


        }
       

  
        private void comboBoxComPort_Enter(object sender, EventArgs e)// koder fra Canvas 2023
        {
            comboBoxComPort.Items.Clear();
            string[] comPorts = SerialPort.GetPortNames();
            foreach (string ports in comPorts)
            {
                comboBoxComPort.Items.Add(ports);
            }
        }

        private void buttonConnet_Click(object sender, EventArgs e)
        {
            SocketSendCommand(textBoxIP_Addresse.Text, textBoxPort.Text, textBoxCommand.Text);

        }

        private string SocketSendCommand(string IPaddress, string Port,string Command) // Fikk masse hjelp av Christian Hovden og Odd Smith-Jahansen
        {
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(IPaddress),Convert.ToInt32(Port));
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(endpoint);
            textBoxResult.AppendText("Connected to server.");
            client.Send(Encoding.ASCII.GetBytes(Command));
            byte[] buffer = new byte[1024];
            int bytesReceived = client.Receive(buffer);
            string recievedString = Encoding.ASCII.GetString(buffer, 0, bytesReceived);
            textBoxCommunication.AppendText("Received: " + Encoding.ASCII.GetString(buffer, 0, bytesReceived));
            client.Close();
            textBoxCommunication.AppendText("Disconnected from server.");
            return recievedString;
        }

        private void timerChartAdd_Tick(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine("readscaled");
                //timerSerialReceive.Enabled = true;
                timerChartAdd.Enabled =false;

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SocketSendCommand(textBoxIP_Addresse.Text, textBoxPort.Text, "readconf");
        }
        

        void DataRecievedHandler(object sender, SerialDataReceivedEventArgs e)// Fikk masse hjelp av Christian Hovden og Odd Smith-Jahansen
        {
            
            int scaled;
            string RecievedData = ((SerialPort)sender).ReadLine();
            textBoxTEXTserialResult.Invoke((MethodInvoker)delegate { textBoxTEXTserialResult.AppendText("Recieved: " + RecievedData + "\r\n"); });
            textBoxChart1.Invoke((MethodInvoker)delegate { textBoxChart1.AppendText("Recieved: " + RecievedData + "\r\n" + analogRead.Count + "\r\n"); });// koder tatt fra Canvas pdf 2022
            string[] separateParts = RecievedData.Split(';');
            if (int.TryParse(separateParts[2], out scaled))
            {
                analogRead.Add(scaled);
               // timeStamp.Add(DateTime.Now);
                chart1.Series[" scaled"].Points.DataBindXY(analogRead);
                chart1.Invalidate();
            }
            else
            {
                MessageBox.Show("Gikk Ikke.");
            }
            
        }
        
        private void buttonDisconnect_Click_1(object sender, EventArgs e)
        {
            serialPort1.Close();
            MessageBox.Show("Disconneet");
            radioButtonConnected.Checked = false;

        }

        private void buttonRecived_Click(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen)
            {
                string indata = serialPort1.ReadExisting();
                textBoxTEXTserialResult.AppendText("Sent:" + indata + "\r\n");


            }
            else
            {
                MessageBox.Show("Porten er ikke åpen!"); // koder tatt fra Canvas 04-6-serialport
            }
        }

        private void textBoxTEXTserialResult_TextChanged(object sender, EventArgs e)   // koder tatt fra Canvas 
        {

        }

        void RecievedData(object sender,SerialDataReceivedEventArgs e)     // koder tatt fra Canvas 04-6-serialport
        {

             string RecievedData=((SerialPort)sender).ReadLine();            // koder tatt fra Canvas 04-6-serialport
             textBoxTEXTserialResult.Invoke((MethodInvoker)delegate
             { textBoxTEXTserialResult.AppendText("Recieved:" + RecievedData + "\r\n"); }); // koder tatt fra Canvas 04-6-serialport

        }
   
        private void buttonstart_Click(object sender, EventArgs e)
        {
          
            timerChartAdd.Enabled =true;

        }

        private void buttonAddpoint_Click(object sender, EventArgs e) // Koder fra Canvas pdf 2023
        {
            //chart1.Series[0].Points.AddXY(Convert.ToDouble(textBoxX.Text), Convert.ToDouble(textBoxY.Text)); // kiled kod Canvas pdf 2023
            //Text = textBoxY.Text = "";

            
        }

        
        private void timerChartAdd_Tick_1(object sender, EventArgs e) // Fikk masse hjelp av Christian Hovden og Odd Smith-Jahansen
        {
            indexTime++;
            string scaled = SocketSendCommand(textBoxIP_Addresse.Text, textBoxPort.Text, "readscaled");
            string[] scaledSplit = scaled.Split(';');
            string scaledRenset = scaledSplit[1].Substring(0, scaledSplit[1].Length - 1);
            double scaledValue = Double.Parse(scaledRenset, CultureInfo.InvariantCulture);
            textBoxChart1.AppendText(scaledRenset);
            chart1.Series[0].Points.AddXY(indexTime, scaledValue);
        }

        private void timerSerialReceiver_Tick(object sender, EventArgs e)
        {

           
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        
        
        private void buttonScaled_Click(object sender, EventArgs e) 
        {
            SocketSendCommand(textBoxIP_Addresse.Text, textBoxPort.Text, "readscaled");
        }

        private void buttonWriteConfiguration_Click(object sender, EventArgs e) 
        {
            string instrumentConfig = "writeconf>";
            string password = "password";

            instrumentConfig += password + ">";
            instrumentConfig += textBoxSensorName.Text
                                + ";" + textBoxLRV.Text
                                + ";" + textBoxURV.Text
                                + ";" + textBoxAL.Text
                                + ";" + textBoxAH.Text;

            SocketSendCommand(textBoxIP_Addresse.Text, textBoxPort.Text, instrumentConfig);

        }

        private void buttonSaveFile_Click(object sender, EventArgs e) // koder fra Canvas pdf
        {
            StreamWriter outputFile = new StreamWriter(@"C:\c#tmp\Test.txt");
            outputFile.Write(textBoxRegister.Text);
            outputFile.Close();
        }

        private void LoadFromFile_Click(object sender, EventArgs e) //koder fra Canvas pdf
        {
            var inputFile = new StreamReader(@"C:\c#tmp\Test.txt");
            textBoxRegister.Text = inputFile.ReadToEnd();
            inputFile.Close();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxSensorName.Text = "";
            maskedTextBoxSerialNumber.Text = "";
            dateTimePicker1.Value = System.DateTime.Now;
            textBoxComment.Clear();
            listBoxOptions.SelectedIndex = 0;
            textBoxTagname.Text = "";
            comboBoxSignalType.Text = "";
            comboBoxMeasurType.Text = "";
            checkBoxRegistered.Checked = false;
;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutForm = new AboutBox1();
            aboutForm.Show(this);
            
           
        }
    }
}


