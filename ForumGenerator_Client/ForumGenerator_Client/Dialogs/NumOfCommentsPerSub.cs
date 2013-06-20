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
    public partial class NumOfCommentsPerSub : Form
    {

        newCommunicator communicator = new newCommunicator();
        string reqUserName;
        string reqPswd;
        int forumId;
        SubForum[] subForums = null;

        public NumOfCommentsPerSub(string reqUserName, string reqPswd, int forumId)
        {
            InitializeComponent();

            this.reqUserName = reqUserName;
            this.reqPswd = reqPswd;
            this.forumId = forumId;

            comboBox1.Items.Clear();

            subForums = communicator.getSubForums(forumId);

            for (int i = 0; i < subForums.Length; i++)
                comboBox1.Items.Add(subForums.ElementAt(i).subForumTitle);

            comboBox1.SelectedIndex = -1;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            int num = communicator.getNumOfCommentsSubForum(reqUserName, reqPswd, forumId, subForums[index].subForumId);
            lblNum.Text = num.ToString();
        }

    }
}
