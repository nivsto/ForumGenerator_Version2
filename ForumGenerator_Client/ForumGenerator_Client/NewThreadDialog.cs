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
    public partial class NewThreadDialog : Form
    {
        string subject = null;
        string text = null;

        public NewThreadDialog()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(txtBoxSubject.Text.Trim()) || String.IsNullOrEmpty(txyBoxMsg.Text.Trim()))
                MessageBox.Show("One of the fields is missing!", "Error");
            else
            {
                subject = txtBoxSubject.Text;
                text = txyBoxMsg.Text;

                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
