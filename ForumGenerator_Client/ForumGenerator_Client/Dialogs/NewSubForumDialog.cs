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
    public partial class NewSubForumDialog : Form
    {
        string name = null;
        string[] admins = null;
        int forumId = 0;
        string currUser = null;
        string userPassword = null;


        int subForumId = 0; 

        public NewSubForumDialog(string currUser, string userPassword, int forumId)
        {
            this.currUser = currUser;
            this.userPassword = userPassword;
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtBoxName.Text.Trim()))
                MessageBox.Show("Please Enter Sub-Forum Name!", "Error");
            else
            {
                if (checkedListBox1.Items.Count <= 0)
                    MessageBox.Show("Please Select Sub-Forum Admins!", "Error");
                else
                {
                    name = txtBoxName.Text;
                   // admins = checkedListBox1.CheckedItems;
                    Communicator com = new Communicator();
                    Tuple<int, String> result = com.sendCreateNewSubForumReq(currUser, userPassword, forumId, name);
                  
                    subForumId = Convert.ToInt16(result.Item2);

                    Close();
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
