using Google.Cloud.Firestore;
using Google.Type;
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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            RegAuth form = new RegAuth();
            form.ShowDialog();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text;
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("Userdata").Document(username);
            Userdata data = docRef.GetSnapshotAsync().Result.ConvertTo<Userdata>();

            if(data != null)
            {
                if (password == data.Password)
                {
                    MessageBox.Show("Login Succes");
                    Hide();
                    Form1 form = new Form1();
                    form.ShowDialog();
                    Close();
                }
                else
                {
                    MessageBox.Show("Login Failed");
                }
            }
            else
            {
                MessageBox.Show("Login Failed");
            }
            
        }
    }
}
