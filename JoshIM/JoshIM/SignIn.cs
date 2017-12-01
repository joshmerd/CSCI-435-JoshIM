using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections;

namespace WindowsApplication1
{
    public partial class SignIn : Form
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void btnSignIn_Click_1(object sender, EventArgs e)
        {
            byte[] myWriteBuffer = new byte[256];
            if (Variables.clientTCP.Connected)
            {
                string recv = userNameBox.Text;

                myWriteBuffer = Encoding.ASCII.GetBytes(recv);
                Variables.myNetworkStream.Write(myWriteBuffer, 0, myWriteBuffer.Length);

                this.Hide();

                BuddyList bList = new BuddyList();
                bList.ShowDialog();

                this.Dispose(true);
                Application.Exit();
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            FormRegister regUser = new FormRegister();
            regUser.ShowDialog();
        }

        private void SignIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }
    }
}