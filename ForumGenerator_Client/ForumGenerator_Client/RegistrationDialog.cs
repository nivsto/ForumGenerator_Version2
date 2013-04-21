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
    public partial class RegistrationDialog : Form
    {
        string userName = null;


        public RegistrationDialog()
        {
            InitializeComponent();
        }

        private void txtBoxUsername_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            userName = txtBoxUsername.Text;

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        public string getUserName()
        {
            return userName;
        }
    }
}
