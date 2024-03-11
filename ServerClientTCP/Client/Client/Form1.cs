using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using SuperSimpleTcp;
namespace Client
{
    public partial class Form1 : Form
    {
        int msgCount = 0;
        string fileName = "";
        SimpleTcpClient client;
        string json;
        public Form1()
        {
            InitializeComponent();
            client = new SimpleTcpClient("127.0.0.1", 5555);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image (.jpg)|*.jpg|Png Image (.png)|*.png";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            fileName = openFileDialog1.FileName;
            pictureBox1.Image = Image.FromFile(fileName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!client.IsConnected)
            {
                client.Connect();
            }

            json = ParseToJson(new Packet(msgCount++, pictureBox1.Image, DateTime.Now));
            timer1.Enabled = true;
            client.Send(json);
        }

        private string ParseToJson(Packet packet)
        {
            return JsonConvert.SerializeObject(packet);
        }

        class Packet
        {
            public int ID;
            public DateTime Time;
            public byte[] image;

            public Packet(int num, Image image, DateTime Time)
            {
                this.ID = num;
                this.Time = Time;
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    this.image = m.ToArray();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            json = ParseToJson(new Packet(msgCount++, pictureBox1.Image, DateTime.Now));
            client.Send(json);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Interval = 10000;
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
