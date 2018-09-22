using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using rtChart;

namespace SerialCommunication {
    public class Serial {

    
        private string comPort;
        private int baudRate;

        SerialPort serailPort;
        //kayChart serialDataChart;
        public Serial(string comPort, string baudRate) {

            this.comPort = comPort;
            this.baudRate = Convert.ToInt32(baudRate);

            serailPort = new SerialPort();

            serailPort.PortName = comPort;
            serailPort.BaudRate = this.baudRate;

            serailPort.DataReceived += Port_DataReceived;
        }

        public bool isPortOpen() {
            return serailPort.IsOpen;
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e) {
            try {
                SetText(serailPort.ReadLine());
            } catch (Exception ex) {
                SetText(ex.ToString());
            }
        }

        delegate void SetTextCallback(string text);

        private List<string> _data = new List<string>();
        private void SetText(string text) {
            string[] data = text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            if (data.Length != 10) return; 

            _data.Add(text);
        }

        public List<string> getList() {
            return _data;
        }

        public void open() {
            try {
                if (!serailPort.IsOpen)
                    serailPort.Open();
            } catch (Exception err) {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void close(){
            try {
                if (serailPort.IsOpen) {
                    serailPort.Close();
                }
            } catch (Exception err) {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
