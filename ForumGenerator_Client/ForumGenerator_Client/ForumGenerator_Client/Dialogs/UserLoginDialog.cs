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
    public partial class UserLoginDialog : Form
    {
        enum loginLevels
        {
            GUEST,
            MEMBER,
            MODERATOR,
            ADMIN,
            SUPER
        };

        int loginLevel = 0;
        bool okClicked = false;
        string userName = null;
        int forumId = 0;
        string password = null;
        bool superUser = false;
        User user;
        Communicator communicator = new Communicator();

        public UserLoginDialog(int forumId, bool superUser)
        {
            this.forumId = forumId;
            this.superUser = superUser;
            InitializeComponent();
            if (superUser)
            {
                this.chkbxSuperUser.Checked = true;
              }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            okClicked = false;
            Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtBoxUserName.Text.Trim()) && !String.IsNullOrEmpty(txtBoxPassword.Text.Trim()))
            {

                okClicked = true;
                userName = txtBoxUserName.Text;
                password = encryptPassword( txtBoxPassword.Text);

                superUser = chkbxSuperUser.Checked;
                if (superUser)
                {
                    try
                    {
                        this.communicator.superUserLogin(userName, password);
                        loginLevel = (int)loginLevels.SUPER;
                        Hide();
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                else
                {
                    try
                    {
                        user = this.communicator.login(forumId, userName, password);
                        loginLevel = this.communicator.getUserType(forumId, user.userName);
                        Hide();
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Fill All Fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private string encryptPassword(string pass)
        {
            string crptPass = "";
            for (int i = 0; i < pass.Count(); i++)
            {
                char c = pass.ElementAt(i);
                char e = (char)( 126 - (c - 32));
                crptPass = crptPass + e;
            }
            return crptPass;
        }

        public int getLoginLevel()
        {
            return loginLevel;
        }

        public bool isOkClicked()
        {
            return okClicked;
        }

        public string getUserName()
        {
            return userName;
        }

        public string getPassword()
        {
            return password;
        }

        public User getUser()
        {
            return user;
        }

        private void chkbxSuperUser_CheckedChanged(object sender, EventArgs e)
        {
            if (superUser)
                chkbxSuperUser.Checked = true;
        }

    }
}
