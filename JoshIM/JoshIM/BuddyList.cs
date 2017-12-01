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
    public partial class BuddyList : Form
    {
        
        public static ArrayList clientAL = new ArrayList();
        public string recMessage = "";

        public BuddyList()
        {
            InitializeComponent();
            this.buddyList();
        }

        public void OnClick(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedItem == null)
            {
                MessageBox.Show("There is noone there");
            }
            else
            {
                Form chatForm = new chatWindow();
                chatForm.Show();
                chatForm.Text = listBox1.SelectedItem.ToString();
            }
        }

        public void buddyList()
        {
            this.Text = "Connected";

            Thread updtthread = new Thread(new ThreadStart(UpdatePanelContact));
            updtthread.Start();

            listBox1.Click += new EventHandler(OnClick);
        }

        private void UpdatePanelContact()
        {
            loop:
            Control.CheckForIllegalCrossThreadCalls = false;
            byte[] rec = new byte[256];
            int i = 0;while ((i = Variables.myNetworkStream.Read(rec, 0, 256)) != 0)
            {
                Variables.myNetworkStream.Read(rec, 0, 256);
                recMessage = Encoding.ASCII.GetString(rec);
                this.listBox1.Items.Add("");
                char[] ch = recMessage.ToCharArray();
                string addme = new string(ch);
                this.listBox1.Items.Add(addme);
                goto loop;
            } 
        }

        public static void UpdateBuddyList(IEnumerable myList)
        {
            foreach (Object obj in myList)
            clientAL.Add(obj);
            clientAL.Add("user2");
            clientAL.Add("user3");
        }

        private void BuddyList_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            
            Application.Exit();
        }  
    }
}