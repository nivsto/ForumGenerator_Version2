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
                    password = txtBoxPassword.Text;
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
