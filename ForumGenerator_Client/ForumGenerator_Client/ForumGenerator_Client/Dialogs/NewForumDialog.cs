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
    public partial class NewForumDialog : Form
    {
        string currUser = null;
        string userPassword = null;

        string forumName = null;
        string admin = null;
        string adminPassword = null;
        Communicator communicator = new Communicator();

        int forumId = 0;

        public NewForumDialog(string currUser,  string userPassword)
        {
            this.currUser = currUser;
            this.userPassword = userPassword;
            InitializeComponent();
           
       }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtBoxName.Text.Trim()) && String.IsNullOrEmpty(txtBoxPassword.Text.Trim())
                && String.IsNullOrEmpty(txtBoxAdmin.Text.Trim()))
                MessageBox.Show("Please Enter All Fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                
                forumName = txtBoxName.Text;
                admin = txtBoxAdmin.Text;
                adminPassword = encryptPassword(txtBoxPassword.Text);
                try
                {
                    Forum forum = communicator.createNewForum(currUser, userPassword, forumName, admin, adminPassword);
                    forumId = forum.forumId;
                    Close();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }

        private string encryptPassword(string pass)
        {
            string crptPass = "";
            for (int i = 0; i < pass.Count(); i++)
            {
                char c = pass.ElementAt(i);
                char e = (char)(126 - (c - 32));
                crptPass = crptPass + e;
            }
            return crptPass;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        public int getForumId()
        {
            return forumId;
        }
    }
}
