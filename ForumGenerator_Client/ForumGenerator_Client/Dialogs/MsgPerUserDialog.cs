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
    public partial class MsgPerUserDialog : Form
    {

        newCommunicator communicator = new newCommunicator();
        string reqUserName;
        string reqPswd;
        int forumId;

        public MsgPerUserDialog(string reqUserName, string reqPswd, int forumId)
        {
            InitializeComponent();

            this.reqUserName = reqUserName;
            this.reqPswd = reqUserName;
            this.forumId = forumId;

            comboBox1.Items.Clear();

            User[] users = communicator.getUsers(forumId);

            for (int i = 0; i < users.Length; i++)
                comboBox1.Items.Add(users.ElementAt(i).userName);

            comboBox1.SelectedIndex = -1;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int num = communicator.getNumOfCommentsSingleUser(reqUserName, reqPswd, forumId, comboBox1.Text);
            lblNum.Text = num.ToString();
        }
    }
}
