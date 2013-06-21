using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ForumGenerator_Client.Communication;
using ForumGenerator_Client.ServiceReference1;

namespace ForumGenerator_Client.Dialogs
{
    public partial class RepliersPerUserDialog : Form
    {
        newCommunicator communicator = new newCommunicator();
        string reqUserName;
        string reqPswd;
        int forumId;

        public RepliersPerUserDialog(string reqUserName, string reqPswd, int forumId)
        {
            InitializeComponent();
            this.reqUserName = reqUserName;
            this.reqPswd = reqPswd;
            this.forumId = forumId;

            this.comboBox1.Items.Clear();
            try
            {
                User[] users = communicator.getUsers(forumId);  
                for (int i = 0; i < users.Length ; i++)
                comboBox1.Items.Add(users.ElementAt(i).userName);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK);
            }
     

            comboBox1.SelectedIndex = -1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string userName = this.comboBox1.Text;
            listBox1.Items.Clear();
            try
            {
                User[] users =  communicator.getResponsersForSingleUser(reqUserName, reqPswd, forumId, userName);
                for (int i = 0; i < users.Length; i++)
                    listBox1.Items.Add(users.ElementAt(i).userName);

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK);
            }
      
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
