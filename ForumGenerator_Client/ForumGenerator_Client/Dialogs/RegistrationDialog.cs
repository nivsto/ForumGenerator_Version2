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


                userName = txtBoxUsername.Text;
                password = txtBoxPassword.Text;
                Communicator com = new Communicator();
                Tuple<int, String> result = com.sendRegisterReq(forumId, userName, password, txtBoxEmail.Text, txtBoxSignature.Text);

                Close();
            }
            else
                MessageBox.Show("Please Fill All Fields!", "Error");
            
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



    }
}
