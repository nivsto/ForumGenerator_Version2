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

namespace ForumGenerator_Client
{
    public partial class NewSubForumDialog : Form
    {
        string name = null;
        int forumId = 0;
        string currUser = null;
        string userPassword = null;
        newCommunicator communicator = new newCommunicator();


        int subForumId = -1; 

        public NewSubForumDialog(string currUser, string userPassword, int forumId)
        {
            this.currUser = currUser;
            this.userPassword = userPassword;
            this.forumId = forumId;
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtBoxName.Text.Trim()))
                MessageBox.Show("Please Enter Sub-Forum Name!", "Error");
            else
            {
                name = txtBoxName.Text;
                try
                {
                    SubForum sub = communicator.createNewSubForum(currUser, userPassword, forumId, name);
                    subForumId = sub.subForumId;
                    Close();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        public int getSubForumId()
        {
            return this.subForumId;
        }
    }
}
