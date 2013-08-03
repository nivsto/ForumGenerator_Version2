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
    public partial class RegistrationDialog : Form
    {
        string userName = null;
        string password = null;
        int forumId = 0;
        Communicator communicator = new Communicator();

        public RegistrationDialog(int forumId)
        {
            this.forumId = forumId;
            InitializeComponent();
        }

        private void txtBoxUsername_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtBoxUsername.Text.Trim()) && !String.IsNullOrEmpty(txtBoxPassword.Text.Trim()))
            {
                try
                {
                    userName = txtBoxUsername.Text;
                    password =  encryptPassword(txtBoxPassword.Text);
                    communicator.register(forumId, userName, password, txtBoxEmail.Text, txtBoxSignature.Text);

                    Close();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Please Fill All Fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
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

        public string getUserName()
        {
            return userName;
        }


        public string getPassword()
        {
            return password;
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
