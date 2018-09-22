using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using rtChart;
using System.IO.Ports;
using SerialCommunication;
using System.Data.SqlClient;

namespace SerialdataMonitoring {
    public partial class Form1 : MetroForm {


        Serial serial;
        kayChart serialDataChart1;
        kayChart serialDataChart2;
        kayChart serialDataChart3;
        kayChart serialDataChart4;
        kayChart serialDataChart5;

        SqlConnection conn = new SqlConnection (@"Data Source=PC-ENIGMA\PDA_PERERA;Initial Catalog=WSN;Integrated Security=True");
        SqlCommand cmd;

        public Form1() {

            InitializeComponent();

            string[] ports = SerialPort.GetPortNames();

            metroComboBox1.Items.AddRange(ports);
            metroComboBox1.SelectedIndex = 0;
            metroComboBox2.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e) {

            
            serialDataChart1 = new kayChart(chart1, 60);
            serialDataChart2 = new kayChart(chart2, 60);
            serialDataChart3 = new kayChart(chart3, 60);
            serialDataChart4 = new kayChart(chart4, 60);
            serialDataChart5 = new kayChart(chart5, 60);


            chart1.Hide();
            chart2.Hide();
            chart3.Hide();
            chart4.Hide();
        }

        private void Serialtab_Click(object sender, EventArgs e) {

            

        }

        private void Homtab_Click(object sender, EventArgs e) {

        }

        private void timer1_Tick(object sender, EventArgs e) {

            double data0, data1,data2,data3,data4,data5,data6,data7,data8,data9;
       
            

            if (serial == null && !serial.isPortOpen()) return;

            if (serial.getList().Count == 0) return;

           

            string[] setData = serial.getList()[serial.getList().Count - 1].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);



            

                data0 = Convert.ToDouble(setData[0]);
                bool result0 = double.TryParse(setData[0], out data0);
                if (result0) {
                    serialDataChart1.TriggeredUpdate(data0);
                }

                data1 = Convert.ToDouble(setData[1]);
                bool result1 = double.TryParse(setData[1], out data1);
                if (result1) {
                    serialDataChart2.TriggeredUpdate(data1);
                }

                data2 = Convert.ToDouble(setData[2]);
                bool result2 = double.TryParse(setData[2], out data2);
                if (result2) {
                    serialDataChart3.TriggeredUpdate(data2);
                }
                data3 = Convert.ToDouble(setData[3]);
                bool result3 = double.TryParse(setData[3], out data3);
                if (result3) {
                    serialDataChart4.TriggeredUpdate(data3);

                }

                 data4 = Convert.ToDouble(setData[4]);
                 bool result4 = double.TryParse(setData[4], out data3);
                 if (result4) {
                     serialDataChart5.TriggeredUpdate(data4);

                 }

                 data5 = Convert.ToDouble(setData[5]);
                 data6 = Convert.ToDouble(setData[6]);
                 data7 = Convert.ToDouble(setData[7]);
                 data8 = Convert.ToDouble(setData[8]);
                 data9 = Convert.ToDouble(setData[9]);
                 






                if (data0 == 1) {

                 textBox1.Text = Convert.ToString(data1);
                 textBox2.Text = Convert.ToString(data2);
                 textBox3.Text = Convert.ToString(data3);
                 textBox4.Text = Convert.ToString(data4);
                 textBox5.Text = Convert.ToString(data5);
                 textBox6.Text = Convert.ToString(data6);
                 textBox7.Text = Convert.ToString(data7);
                 textBox8.Text = Convert.ToString(data8);
                 textBox9.Text = Convert.ToString(data9);

                 conn.Open();
                 cmd = new SqlCommand("insert into Node1 (data1,data2,data3,data4,data5,data6,data7,data8,data9) values (@data1,@data2,@data3,@data4,@data5,@data6,@data7,@data8,@data9)", conn);

                 cmd.Parameters.Add("@data1",textBox1.Text);
                 cmd.Parameters.Add("@data2",textBox2.Text);
                 cmd.Parameters.Add("@data3",textBox3.Text);
                 cmd.Parameters.Add("@data4",textBox4.Text);
                 cmd.Parameters.Add("@data5",textBox5.Text);
                 cmd.Parameters.Add("@data6",textBox6.Text);
                 cmd.Parameters.Add("@data7",textBox7.Text);
                 cmd.Parameters.Add("@data8",textBox8.Text);
                 cmd.Parameters.Add("@data9",textBox9.Text);

                 cmd.ExecuteNonQuery();
                 conn.Close();
 


            }

            

                

            
          

           

        }

        private void metroButton1_Click(object sender, EventArgs e) {

            try {
                serial.open();
                metroProgressBar1.Value = 100;
                chart1.Show();
                chart2.Show();
                chart3.Show();
                chart4.Show();
            } catch (Exception err) {

                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void metroButton2_Click(object sender, EventArgs e) {

            try {
                serial.close();
                metroProgressBar1.Value = 0;
            } catch (Exception err) {

                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void metroComboBox1_SelectedValueChanged(object sender, EventArgs e) {

            try {

                if (metroComboBox1.Text != "" && metroComboBox2.Text != "") {

                    serial = new Serial(metroComboBox1.Text, metroComboBox2.Text);
                    timer1.Enabled = true;
                }
            } catch (Exception err) {


                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void tabPage1_Click(object sender, EventArgs e) {

        }

        private void label4_Click(object sender, EventArgs e) {

        }
    }
}
