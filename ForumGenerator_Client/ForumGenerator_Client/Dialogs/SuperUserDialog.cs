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


namespace ForumGenerator_Client.Dialogs
{
    public partial class SuperUserDialog : Form
    {

        Communicator communicator = new Communicator();
        string userName;
        string password;
        Forum[] forumsList;

        public SuperUserDialog(string userName, string password)
        {
            InitializeComponent();
            this.userName = userName;
            this.password = password;

            try
            {
                forumsList = communicator.getForums();
                this.lblNumOfForums.Text = forumsList.Length.ToString();
                this.comboBox1.Items.Clear();
                this.comboBox2.Items.Clear();

                for (int i = 0; i < forumsList.Length; i++)
                {
                    this.comboBox1.Items.Add(forumsList.ElementAt(i).forumName);
                    this.comboBox2.Items.Add(forumsList.ElementAt(i).forumName);
                }

            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            int index1 = comboBox1.SelectedIndex;
            int index2 = comboBox2.SelectedIndex;
            try
            {
                User[] users = communicator.getMutualUsers(userName, password, forumsList.ElementAt(index1).forumId, forumsList.ElementAt(index2).forumId);
                listBox1.Items.Clear();

                for (int i = 0; i < users.Length; i++)
                {
                    listBox1.Items.Add(users.ElementAt(i).userName);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
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
