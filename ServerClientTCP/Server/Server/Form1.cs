using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using SuperSimpleTcp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Net.Sockets;

namespace Server
{
    public partial class Form1 : Form
    {
        List<Packet> packets = new List<Packet>();
        SimpleTcpServer server;
        IFirebaseClient client;
        string fileName = "";
        public Form1()
        {
            InitializeComponent();
            server = new SimpleTcpServer("127.0.0.1", 5555);
            server.Events.DataReceived += DataReceived;
            IFirebaseConfig cfg = new FirebaseConfig()
            {
                AuthSecret = "fayWyY4GfH2fdRuS07X34WyZprX78wEITTeBhzpS",
                BasePath = "https://azir-lr-db-default-rtdb.firebaseio.com/"
            };
            try
            {
                client = new FireSharp.FirebaseClient(cfg);
            }
            catch (Exception)
            {

            }

        }

        private void DataReceived(object sender, DataReceivedEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                Packet packet = StringToJSon(Encoding.UTF8.GetString(e.Data.Array));
                listBox1.Items.Add($"{e.IpPort} | [{packet.Time}] = {packet.image}");
                packets.Add(packet);
                client.Set("ImageData" + "/", packet);
                File.WriteAllText($"{packet.ID}.json", Encoding.UTF8.GetString(e.Data.Array));
            }));
        }

        private Packet StringToJSon(string json)
        {
            return JsonConvert.DeserializeObject<Packet>(json);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                server.Start();
                checkBox1.Text = "Server Start";
            }
            else
            {
                server.Stop();
                checkBox1.Text = "Server Stop";
            }
        }

        class Packet
        {
            public int ID;
            public DateTime Time;
            public string image;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromStream(new MemoryStream(Convert.FromBase64String(packets[listBox1.SelectedIndex].image)));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JSON(.json)|*.JSON";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            fileName = openFileDialog1.FileName;
            string jsonText = File.ReadAllText(fileName, Encoding.UTF8);
            Packet packet = JsonConvert.DeserializeObject<Packet>(jsonText);

            string base64Image = packet.image;

            byte[] imageBytes = Convert.FromBase64String(base64Image);
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                Image image = Image.FromStream(ms);
                pictureBox1.Image = image;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.GetAsync("ImageData/");
            Packet packetFireBase = response.ResultAs<Packet>();
            string base64Image = packetFireBase.image;
            byte[] imageBytes = Convert.FromBase64String(base64Image);
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                Image image = Image.FromStream(ms);
                pictureBox1.Image = image;
            }
        }
    }
}
