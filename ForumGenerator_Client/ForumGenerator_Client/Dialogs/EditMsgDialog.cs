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
        Communicator communicator = new Communicator();
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
            try
            {
                communicator.editDiscussion(forumId, subForumId, discussionId, userName, pswd, txyBoxMsg.Text);
                Hide();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
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
