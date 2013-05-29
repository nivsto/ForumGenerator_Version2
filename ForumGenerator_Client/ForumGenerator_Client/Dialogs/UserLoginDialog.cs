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
        newCommunicator communicator = new newCommunicator();

        public UserLoginDialog(int forumId, bool superUser)
        {
            this.forumId = forumId;
            this.superUser = superUser;
            InitializeComponent();
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
                password = txtBoxPassword.Text;
         
                if (superUser)
                {
                    this.communicator.superUserLogin(userName, password);
                    loginLevel = (int) loginLevels.SUPER;
                }

                else
                {
                    user = this.communicator.login(forumId, userName, password);
                    loginLevel = this.communicator.getUserType(forumId, userName);
                }

               //       MessageBox.Show("Incorrect Username Or Password. Try Again!", "Error", MessageBoxButtons.OK);

                Hide();
            }
            else
                MessageBox.Show("Please Fill All Fields!", "Error", MessageBoxButtons.OK);
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

    }
}
