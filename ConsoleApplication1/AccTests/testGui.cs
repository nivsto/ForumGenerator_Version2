﻿using System;
using System.Windows.Forms;
using ForumGenerator_Version2_Server.Sys;


namespace ConsoleApplication1.AccTests
{
    public partial class testGui : Form
    {
        TestForumGenerator testsFG;
        const string SU_NAME = ForumGenerator_Version2_Server.Sys.ForumGeneratorDefs.SU_USERNAME; // ForumGenerator.SU_NAME;
        const string SU_PSWD = ForumGenerator_Version2_Server.Sys.ForumGeneratorDefs.SU_PSWD;
        public testGui(string logFileName)
        {
            this.testsFG = new TestForumGenerator(new ForumGenerator(SU_NAME, SU_PSWD, true), logFileName);
            InitializeComponent();
        }
        /*  Run Forum Generator Tests    */
        private void btnRunFg_Click(object sender, EventArgs e)
        {
            this.testsFG.runTests(1);
        }

        private void btnRunScal_Click(object sender, EventArgs e)
        {
            testsFG.runTests(2);
        }

        private void btnRunConnect_Click(object sender, EventArgs e)
        {
            //testsClient.runTests(3);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnOpenLog_Click(object sender, EventArgs e)
        {
            string dir = "C:\\Users\\doronc\\Documents\\GitHub\\ForumGenerator_Version2\\ConsoleApplication1\\Logger\\TestForumGenerator.Log.txt";

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C notepad " + dir;
            process.StartInfo = startInfo;
            process.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void testGui_Load(object sender, EventArgs e)
        {

        }

    }
}
