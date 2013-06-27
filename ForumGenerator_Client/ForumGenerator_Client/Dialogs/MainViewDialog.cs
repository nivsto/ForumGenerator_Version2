using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ForumGenerator_Client.Communication;
using ForumGenerator_Client.Dialogs;
using ForumGenerator_Client.ServiceReference1;

namespace ForumGenerator_Client.Dialogs
{
    public partial class MainViewDialog : Form
    {
        enum loginLevels
        {
            GUEST,
            MEMBER,
            MODERATOR,
            ADMIN,
            SUPER
        };

        enum view
        {
            MAIN,
            FORUM
        };

        MainMethods mainMethods;
        System.ComponentModel.ComponentResourceManager resources;
        int nextX = 27;
        int nextY = 0;
        int delta = 47;
        int currentView = 0;
        int loginLevel = 0;
        string forumName = "";
        Timer timer;
        struct line
        {
            public System.Windows.Forms.Label lblName;
            public System.Windows.Forms.Label lblDelete;
        }

        List<line> lines;

        public MainViewDialog(MainMethods parent)
        {
            resources = new System.ComponentModel.ComponentResourceManager(typeof(MainViewDialog));
            lines = new List<line>();
            mainMethods = parent;
            InitializeComponent();

          timer = new Timer();
          timer.Tick += new EventHandler(TimerOnTick);
          timer.Interval = 4000;
          timer.Start();
        }

        private void mnuLogin_Click(object sender, EventArgs e)
        {
            mainMethods.loginUser();
        }

        private void mnuRegister_Click(object sender, EventArgs e)
        {
            mainMethods.register();
        }

        private void mnuSuper_Click(object sender, EventArgs e)
        {
            mainMethods.superDialog();
        }

        private void mnuAdmin_Click(object sender, EventArgs e)
        {
            mainMethods.adminDialog();
        }

        private void mnuLogput_Click(object sender, EventArgs e)
        {
            mainMethods.logout();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            mainMethods.btnGoBack();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            mainMethods.addNew();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            mainMethods.refresh();
        }

        private void btnNotifications_Click(object sender, EventArgs e)
        {
            mainMethods.notifications();
        }


        public void updateView(int level, int currView, string userName)
        {
            mnuAdmin.Visible = false;
            mnuSuper.Visible = false;
            mnuLogin.Visible = false;
            mnuRegister.Visible = false;
            mnuLogput.Visible = true;
            btnNotifications.Visible = true;
            lblBack.Visible = true;
            btnBack.Visible = true;
            lblAddNew.Visible = false;
            btnAddNew.Visible = false;
            this.currentView = currView;
            this.loginLevel = level;

            if (currView == (int)view.MAIN)
            {
                lblBack.Visible = false;
                btnBack.Visible = false;
                lblTitle.Text = "Welcome to The Forum Genarator!";
                lblAddNew.Text = "Add New Forum";
                lblSecondTitle.Text = "Forums List:";
            }

            if (currView == (int)view.FORUM)
            {
                lblAddNew.Text = "Add New Sub Forum";
                lblSecondTitle.Text = "Sub-Forums List:";
                lblTitle.Text = forumName;

                btnNotifications.Visible = true;
            }

            if (level == (int)loginLevels.GUEST)
            {
                btnNotifications.Visible = false;
                mnuLogput.Visible = false;
                mnuRegister.Visible = true;
                mnuLogin.Visible = true;
                lblHello.Text = "Hello Guest!";

                if (currView == (int)view.MAIN)
                    mnuRegister.Visible = false;


            }
            else
                lblHello.Text = "Hello" + userName + "!";


            if (level == (int)loginLevels.SUPER)
            {
                btnNotifications.Visible = false;
                mnuSuper.Visible = true;
                lblHello.Text = "Hello SuperUser!";

                if (currView == (int)view.MAIN)
                {
                    lblAddNew.Visible = true;
                    btnAddNew.Visible = true;
                }
            }

            if (level == (int)loginLevels.ADMIN)
            {
                mnuAdmin.Visible = true;
                lblAddNew.Visible = true;
                btnAddNew.Visible = true;
            }
            this.Show();

        }

        private void viewItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Label lbl = (System.Windows.Forms.Label)sender;
            int index = Convert.ToInt32(lbl.Name);
            if (currentView == (int)view.MAIN)
            {
                forumName = lbl.Text;
                forumName = forumName.Substring(3);
            }
            mainMethods.btnGoto(index);
      

        }

        private void deleteItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Label lbl = (System.Windows.Forms.Label)sender;
            int index = Convert.ToInt32(lbl.Name);

            mainMethods.delete(index);
        }

        public void updateForumsList(Forum[] forums)
        {
            clearList();

            for (int i = 0; i < forums.Length; i++)
                createNewLine(forums[i].forumName);

        }

        public void updateSubForumsList(SubForum[] subforums)
        {
            clearList();

            for (int i = 0; i < subforums.Length; i++)
                createNewLine(subforums[i].subForumTitle);

        }

        private void clearList()
        {
            this.pnlForums.Controls.Clear();

            lines.Clear();
            nextY = 0;

        }


        private void createNewLine(string name)
        {
            int index = lines.Count;
            line tmp = new line();
            tmp.lblName = new System.Windows.Forms.Label();
            tmp.lblDelete = new System.Windows.Forms.Label();


            tmp.lblName.AutoSize = true;
            tmp.lblName.Cursor = System.Windows.Forms.Cursors.Hand;
            tmp.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            tmp.lblName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            tmp.lblName.Location = new System.Drawing.Point(nextX, nextY);
            tmp.lblName.MinimumSize = new System.Drawing.Size(200, 40);
            tmp.lblName.Name = index.ToString();
            tmp.lblName.Size = new System.Drawing.Size(200, 40);
            tmp.lblName.Text = "       " + name;
            tmp.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            tmp.lblName.Click += new System.EventHandler(this.viewItem_Click);
            tmp.lblDelete.AutoSize = true;
            tmp.lblDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            tmp.lblDelete.Location = new System.Drawing.Point(209, nextY+4);
            tmp.lblDelete.Name = index.ToString();
            tmp.lblDelete.Size = new System.Drawing.Size(14, 13);
            tmp.lblDelete.Text = "X";
            tmp.lblDelete.Click += new System.EventHandler(this.deleteItem_Click);


            if (currentView == (int)view.MAIN)
                tmp.lblName.Image = ((System.Drawing.Image)(resources.GetObject("label11.Image")));
            else
                tmp.lblName.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));

            if (index % 2 == 0)
            {
                tmp.lblName.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
                tmp.lblDelete.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            }
            else
            {
                tmp.lblName.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
                tmp.lblDelete.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            }

             if (loginLevel == (int)loginLevels.ADMIN)
                this.pnlForums.Controls.Add(tmp.lblDelete);
            
            this.pnlForums.Controls.Add(tmp.lblName);

            lines.Add(tmp);
            nextY += delta;
        }

        public void setStatusMsg(string msg)
        {
            toolStripStatusLabel2.Text = msg;
            timer.Start();
        }

        private void TimerOnTick(object obj, EventArgs ea)
        {
            toolStripStatusLabel2.Text = "";
        }

    }
}
