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
    public partial class NewForumDialog : Form
    {
        string name = null;
        string admin = null;

        public NewForumDialog()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtBoxName.Text.Trim()))
                MessageBox.Show("Please Enter Forum Name!", "Error");
            else
            {
                name = txtBoxName.Text;
                admin = comboBox1.Text;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
