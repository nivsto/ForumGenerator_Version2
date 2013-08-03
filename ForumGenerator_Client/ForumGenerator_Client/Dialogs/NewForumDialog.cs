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
                Forum.RegPolicy policy = Forum.RegPolicy.NONE;
                if (rdbtnAdminConfirm.Checked)
                    policy = Forum.RegPolicy.ADMIN_CONFIRMATION;
                if (rdbtnMailAct.Checked)
                    policy = Forum.RegPolicy.MAIL_ACTIVATION;

                forumName = txtBoxName.Text;
                admin = txtBoxAdmin.Text;
                adminPassword = encryptPassword(txtBoxPassword.Text);

                try
                {
                    Forum forum = communicator.createNewForum(currUser, userPassword, forumName, admin, adminPassword, policy);
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
