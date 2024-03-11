using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class RegAuth : Form
    {
        public RegAuth()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Login form = new Login();
            form.ShowDialog();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var db = FirestoreHelper.Database;
            if (CheckIfUserAlreadyExist()) { MessageBox.Show("User Already Exist"); return; }
            var data = GetWriteData();
            DocumentReference docRef = db.Collection("Userdata").Document(data.Username);
            docRef.SetAsync(data);
            MessageBox.Show("Succes");

        }

       private Userdata GetWriteData()
        {
            


            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();
            string gender = comboBox1.Text.Trim();

            return new Userdata()
            {
                Username = username,
                Password = password,
                Gender = gender
            };

       }

        private bool CheckIfUserAlreadyExist()
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text;
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("Userdata").Document(username);
            Userdata data = docRef.GetSnapshotAsync().Result.ConvertTo<Userdata>();

            if (data != null)
            {
                return true;
            }
            else return false;
        }

        private void RegAuth_Load(object sender, EventArgs e)
        {

        }
    }
}
