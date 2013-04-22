using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ForumGenerator_Client.Communication;


namespace ForumGenerator_Client
{
    public partial class NewForumDialog : Form
    {
        string currUser = null;
        string userPassword = null;

        string forumName = null;
        string admin = null;
        string adminPassword = null;

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
                MessageBox.Show("Please Enter All Fields!", "Error");
            else
            {
                forumName = txtBoxName.Text;
                admin = txtBoxAdmin.Text;
                adminPassword = txtBoxPassword.Text;
                Communicator com = new Communicator();
                Tuple<int, String> result = com.sendCreateNewForumReq(currUser, userPassword, forumName, admin, adminPassword);
                forumId = Convert.ToInt16(result.Item2);
                Close();
            }
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
