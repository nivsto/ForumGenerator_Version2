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
    public partial class CommentAddingDialog : Form
    { 
        string text = null;
        
        public CommentAddingDialog()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if ( String.IsNullOrEmpty(txtBoxTxt.Text.Trim()))
                MessageBox.Show("Please Enter Your Comment!", "Error");
            else
            {
                text = txtBoxTxt.Text;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
           Close();
        }
    }
}


