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
    public partial class MutualMembersDialog : Form
    {
        newCommunicator communicator = new newCommunicator();
        string userName;
        string password;
        Forum[] forumsList;

        public MutualMembersDialog(string userName, string password)
        {
            InitializeComponent();
            this.userName = userName;
            this.password = password;
            forumsList = communicator.getForums();

            this.comboBox1.Items.Clear();
            this.comboBox2.Items.Clear();

            for (int i = 0; i < forumsList.Length; i++)
            {
                this.comboBox1.Items.Add(forumsList.ElementAt(i).forumName);
                this.comboBox2.Items.Add(forumsList.ElementAt(i).forumName); 
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void btnCompare_Click(object sender, EventArgs e)
        {
            int index1 = comboBox1.SelectedIndex;
            int index2 = comboBox2.SelectedIndex;

            User[] users = communicator.getMutualUsers(userName, password, forumsList.ElementAt(index1).forumId, forumsList.ElementAt(index2).forumId);
            listBox1.Items.Clear();

            for (int i = 0; i < users.Length; i++)
            {
                listBox1.Items.Add(users.ElementAt(i).userName);
            }
        }




    }
}
