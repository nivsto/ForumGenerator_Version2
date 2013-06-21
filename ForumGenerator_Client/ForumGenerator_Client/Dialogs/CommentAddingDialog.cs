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
    public partial class CommentAddingDialog : Form
    { 
        string text = null;
        string userName;
        string password;
        int forumId;
        int subForumId;
        int discussionId;
        
        public CommentAddingDialog( string userName, string password, int forumId,  int subForumId, int discussionId)
        {
            this.userName = userName;
            this.password = password;
            this.forumId = forumId;
            this.subForumId = subForumId;
            this.discussionId = discussionId;
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if ( String.IsNullOrEmpty(txtBoxTxt.Text.Trim()))
                MessageBox.Show("Please Enter Your Comment!", "Error");
            else
            {
                try
                {
                    text = txtBoxTxt.Text;
                    Communicator com = new Communicator();
                    com.createNewComment(userName, password, forumId, subForumId, discussionId, text);
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

        private void txtBoxTxt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


