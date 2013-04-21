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
    public partial class NewSubForumDialog : Form
    {
        string name = null;
        string[] admins = null;

        public NewSubForumDialog()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtBoxName.Text.Trim()))
                MessageBox.Show("Please Enter Sub-Forum Name!", "Error");
            else
            {
                if (checkedListBox1.Items.Count <= 0)
                    MessageBox.Show("Please Select Sub-Forum Admins!", "Error");
                else
                {
                    name = txtBoxName.Text;
                   // admins = checkedListBox1.CheckedItems;
                    Close();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
