using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ForumGenerator_Client
{
    public partial class UserLoginDialog : Form
    {
        int loginLevel = 0;
        bool okClicked = false;
        string userName = null;
        

        public UserLoginDialog()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            okClicked = false;


            Hide();

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            okClicked = true;


            Hide();
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
    }
}
