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
    public partial class NewThreadDialog : Form
    {
        string subject = null;
        string text = null;
        string userName;
        string password;
        int forumId;
        int subForumId;
        int threadId;
        Communicator communicator = new Communicator();

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
                MessageBox.Show("One of the fields is missing!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                try
                {
                    subject = txtBoxSubject.Text;
                    text = txyBoxMsg.Text;
                    Discussion dis = communicator.createNewDiscussion(userName, password, forumId, subForumId, subject, text);
                    threadId = dis.discussionId;
                    Close();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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


        private void close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private const int CS_DROPSHADOW = 0x00020000;
        protected override CreateParams CreateParams
        {
            get
            {
                // add the drop shadow flag for automatically drawing
                // a drop shadow around the form
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (this.FormBorderStyle == System.Windows.Forms.FormBorderStyle.None)
            {
                this.MouseDown += new MouseEventHandler(Dialog_MouseDown);
                this.MouseMove += new MouseEventHandler(Dialog_MouseMove);
                this.MouseUp += new MouseEventHandler(Dialog_MouseUp);
            }

            base.OnLoad(e);
        }

        void Dialog_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            downPoint = new Point(e.X, e.Y);
        }

        void Dialog_MouseMove(object sender, MouseEventArgs e)
        {
            if (downPoint == Point.Empty)
            {
                return;
            }
            Point location = new Point(
                this.Left + e.X - downPoint.X,
                this.Top + e.Y - downPoint.Y);
            this.Location = location;
        }

        void Dialog_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            downPoint = Point.Empty;
        }

        public Point downPoint = Point.Empty;

    }
}
