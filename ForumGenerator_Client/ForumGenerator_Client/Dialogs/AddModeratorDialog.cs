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
        Communicator communicator = new Communicator();
        int forumId;
        int subForumId;
        string adderUsrName;
        string adderPswd;
        User[] users = null;
        SubForum[] subForums = null;

        public AddModeratorDialog(int forumId, string adderUsrName, string adderPswd)
        {
            InitializeComponent();
            this.forumId = forumId;
            this.adderPswd = adderPswd;
            this.adderUsrName = adderUsrName;


            //init users combo box list
            this.comboBox1.Items.Clear();
            this.comboBox2.Items.Clear();

            //init sub forums list
            try
            {
                subForums = communicator.getSubForums(forumId);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK);
            }
            for (int i = 0; i < subForums.Length; i++)
                comboBox2.Items.Add(subForums.ElementAt(i).subForumTitle);

            try
            {
                users = communicator.getUsers(forumId);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK);
            }
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.comboBox2.SelectedIndex;
            this.subForumId = subForums[index].subForumId;

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


    }
}
