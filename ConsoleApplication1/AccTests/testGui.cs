using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using ForumGenerator_Version2_Server;
//using ForumGenerator_Version2_Server.Sys;
using ForumGenerator_Version2_Server;
using ForumGenerator_Version2_Server.Sys;

namespace ConsoleApplication1.AccTests
{
    public partial class testGui : Form
    {
        TestForumGenerator tests;
        public testGui(string logFileName)
        {
            this.tests = new TestForumGenerator(new ForumGenerator("admin", "admin",true), logFileName);
            InitializeComponent();
        }
        /*  Run Forum Generator Tests    */
        private void btnRunFg_Click(object sender, EventArgs e)
        {
            tests.runTests();
        }

        private void btnRunScal_Click(object sender, EventArgs e)
        {

        }

        private void btnRunConnect_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void btnOpenLog_Click(object sender, EventArgs e)
        {

        }


    }
}
