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
    public partial class AddModeratorDialog : Form
    {
        newCommunicator communicator = new newCommunicator();
        int forumId;
        int subForumId;
        string adderUsrName;
        string adderPswd;
        User[] users = null;

        public AddModeratorDialog(int forumId, int subForumId, string adderUsrName, string adderPswd)
        {
            InitializeComponent();
            this.forumId = forumId;
            this.subForumId = subForumId;
            this.adderPswd = adderPswd;
            this.adderUsrName = adderUsrName;

            
            //init users combo box list
            this.comboBox1.Items.Clear();
            users = communicator.getUsers(forumId);

            for (int i = 0; i < users.Length; i++)
                comboBox1.Items.Add(users.ElementAt(i).userName);


        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            string name = users.ElementAt(index).userName;
            try
            {
                communicator.addModerator(name, forumId, subForumId, adderUsrName, adderPswd);
                Hide();
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
