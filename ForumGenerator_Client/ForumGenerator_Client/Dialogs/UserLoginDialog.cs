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
    public partial class UserLoginDialog : Form
    {
        int loginLevel = 0;
        bool okClicked = false;
        string userName = null;
        int forumId = 0;
        string password = null;
        bool superUser = false;


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
                Communicator com = new Communicator();
                Tuple<int, String> result;

                if (superUser)
                    result = com.sendAdminLoginReq(userName, password);
                else
                    result = com.sendLoginReq(forumId, userName, password);

                if (result.Item1 == 1)
                {
                    string tmp = result.Item2;
                    if (tmp == "Member")
                        loginLevel = 1;
                    if (tmp == "Administrator")
                        loginLevel = 2;
                    if (tmp == "SuperUser")
                        loginLevel = 3;
                }
                else
                    MessageBox.Show("Incorrect Username Or Password. Try Again!", "Error", MessageBoxButtons.OK);

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

    }
}
