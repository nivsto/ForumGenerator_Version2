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
    public partial class NewThreadDialog : Form
    {
        string subject = null;
        string text = null;
        string userName;
        string password;
        int forumId;
        int subForumId;
        int threadId;

        public NewThreadDialog(string userName,  string password,  int forumId,  int subForumId)
        {
            this.userName = userName;
            this.password = password;
            this.forumId = forumId;
            this.subForumId = subForumId;
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(txtBoxSubject.Text.Trim()) || String.IsNullOrEmpty(txyBoxMsg.Text.Trim()))
                MessageBox.Show("One of the fields is missing!", "Error");
            else
            {
                subject = txtBoxSubject.Text;
                text = txyBoxMsg.Text;

                Communicator com = new Communicator();
                Tuple<int, String> result = com.sendCreateNewDiscussionReq(userName, password, forumId, subForumId, subject, text);

                threadId = Convert.ToInt16(result.Item2);


                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        public int getThreadId()
        {
            return threadId;
        }
    }
}
