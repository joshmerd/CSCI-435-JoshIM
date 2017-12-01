using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsApplication1
{
    public partial class FormRegister : Form
    {
        public FormRegister()
        {
            InitializeComponent();
        }

        private void btnRegSubmit_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == txtConfirm.Text)
            {
                try
                {
                    FileStream fs = File.Create("test.dat");
                    BinaryWriter bw = new BinaryWriter(fs);

                    string userName = txtUsername.Text;
                    string passWord = txtPassword.Text;
                    bw.Write(userName);
                    bw.Write(passWord);
                    bw.Close();
                    fs.Close();

                    MessageBox.Show("Account successfully created.");
                }
                catch
                {
                    MessageBox.Show("Connection to server failed. Please try some other time.");
                }
            }
            else
            {
                MessageBox.Show("Your password fields do not match");
            }
        }

        private void FormRegister_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.DialogResult == DialogResult.None)
            {
                e.Cancel = true;
            }
        }
    }
}