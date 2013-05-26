using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ForumGenerator_Client.Communication;

namespace ForumGenerator_Client.Dialogs
{
    public partial class EditMsgDialog : Form
    {
        newCommunicator communicator = new newCommunicator();
        int forumId;
        int subForumId;
        int discussionId;
        string userName; 
        string pswd;

        public EditMsgDialog(int forumId, int subForumId, int discussionId, string userName, string pswd)
        {
            InitializeComponent();

            this.forumId = forumId;
            this.subForumId = subForumId;
            this.discussionId = discussionId;
            this.userName = userName;
            this.pswd = pswd;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            communicator.editDiscussion(forumId, subForumId, discussionId, userName, pswd, txyBoxMsg.Text);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
