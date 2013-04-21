﻿using System;
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
    public partial class MainDialog : Form
    {
        enum loginLevels
        {
            GUEST,
            MEMBER,
            ADMIN,
            SUPER
        };

        enum view
        {
            MAIN,
            FORUM,
            SUB,
            THREAD
        };

      
        int currentView = (int)view.MAIN;
        int loginLevel = (int)loginLevels.GUEST;
        string userName = null;
        string password = null;

        int currForumId = 0;
        int currSubForumId = 0;
        int currThreadId = 0;

        public MainDialog()
        {
            InitializeComponent();
            updateVisibilty();
        }

        /*************************************/
        /*   Login User                      */
        /*************************************/
        private void loginUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loginUser();
        
        }

        private void loginUser()
        {
            UserLoginDialog userLog = new UserLoginDialog(currForumId, false);
            userLog.ShowDialog(this);
            if (userLog.isOkClicked())
            {
                    loginLevel = userLog.getLoginLevel();
                    userName = userLog.getUserName();
                    password = userLog.getPassword();
                    updateVisibilty();               

            }
        }

        /*************************************/
        /*   Login Super  User               */
        /*************************************/
        private void loginSuperUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loginSuperUser();
            Show();
        }

        private void loginAsSuperUSerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loginSuperUser();
            Show();
        }


        private void loginSuperUser()
        {
            UserLoginDialog userLog = new UserLoginDialog(currForumId, true);

            userLog.ShowDialog(this);
            if (userLog.isOkClicked())
            {

                //check that the user level entered is the highest and user clicked ok
                if (userLog.getLoginLevel() == (int)loginLevels.SUPER && userLog.getUserName() != null)
                {
                    loginLevel = (int)loginLevels.SUPER;
                    userName = userLog.getUserName();
                    password = userLog.getPassword();
                    updateVisibilty();

                }
                else
                {
                    //incorrect super user
                    MessageBox.Show("Incorrect Username Or Password. Try Again!", "Error", MessageBoxButtons.OK);
                    loginSuperUser();
                }
            }
        }


        /*************************************/
        /*   Quit                            */
        /*************************************/
        private void quitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure?", "Quit", MessageBoxButtons.OKCancel) == DialogResult.OK)
                Close();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure?", "Quit", MessageBoxButtons.OKCancel) == DialogResult.OK)
                Close();
        }

        /*************************************/
        /*   Logout User                     */
        /*************************************/
        private void logoutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            logout();   

        }

        private void logoutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            logout();
        }

        private void logout()
        {
            if (MessageBox.Show("Are You Sure?", "Logout", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Communicator com = new Communicator();
                if (loginLevel != (int)loginLevels.SUPER)
                    com.sendLogoutReq(currForumId, userName);
                else
                    com.sendAdminLogoutReq(userName);

                loginLevel = 0;
                userName = null;
                password = null;
           
                updateVisibilty();
            }
        }
        

        /*************************************/
        /*   Register User                   */
        /*************************************/
        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistrationDialog reg = new RegistrationDialog(currForumId);
            reg.ShowDialog();
            if (reg.getUserName() != null)
            {
                userName = reg.getUserName();
                password = reg.getPassword();
                loginLevel = 1;
                updateVisibilty();
            }

        }


        /*************************************/
        /*   Go To Button                    */
        /*************************************/
        private void btnView_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                currentView++;
                updateVisibilty();
            }
            else
            {
                MessageBox.Show("Please Select From List", "Nothing Selected", MessageBoxButtons.OK);
            }
        }


        /*************************************/
        /*   Back Button                     */
        /*************************************/
        private void btnGoBack_Click(object sender, EventArgs e)
        {

            if (currentView == (int)view.FORUM && loginLevel != (int)loginLevels.SUPER && loginLevel != (int)loginLevels.GUEST)
            {
                if (MessageBox.Show("You Are About To Leave The Forum. Are You Sure?", "Logout", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    loginLevel = 0;
                    userName = null;
                    updateVisibilty();

                    currentView--;
                    updateVisibilty();
                }
            }
            else
            {
                currentView--;
                updateVisibilty();
            }

        }
    
        /*************************************/
        /*   Create New Sub Forum            */
        /*************************************/
        private void createNewSubForumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewSubForumDialog sub = new NewSubForumDialog();
            sub.ShowDialog();
            updateVisibilty();
        }

        /*************************************/
        /*   Publish New Message             */
        /*************************************/
        private void publishNewMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewThreadDialog thr = new NewThreadDialog();
            thr.ShowDialog();
            updateVisibilty();

        }

        /*************************************/
        /*   Add Comment                     */
        /*************************************/
        private void addACommentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommentAddingDialog cmnt = new CommentAddingDialog();
            cmnt.ShowDialog();
            updateVisibilty();
        }

        /*************************************/
        /*   Create New Forum                */
        /*************************************/
        private void newForumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewForumDialog nForum = new NewForumDialog(userName, password);
            nForum.ShowDialog();
            updateVisibilty();
        }


        private void updateVisibilty()
        {
            mainSuperToolStripMenuItem.Visible = false;
            mainForumToolStripMenuItem.Visible = true;
            superToolStripMenuItem.Visible = false;
            adminToolStripMenuItem.Visible = false;
            btnGoBack.Visible = true;
            logoutToolStripMenuItem.Visible = true;
            logoutToolStripMenuItem1.Visible = true;

            toolStripStatusLabel1.Text = "Hello " + userName +"!";



            if (currentView == (int)view.MAIN)
            {
                mainSuperToolStripMenuItem.Visible = true;
                mainForumToolStripMenuItem.Visible = false;
                btnGoBack.Visible = false;
                btnView.Visible = true;
                newToolStripMenuItem1.Visible = false;

                lblTitle.Text = "Choose a Forum or Login as a Super-user ";
                btnView.Text = "View Forum";
                initForumsList();

            }
         
            if (currentView == (int)view.FORUM)
            {
                btnView.Text = "View Sub-Forum";
                btnGoBack.Visible = true;
                btnView.Visible = true;
                newToolStripMenuItem1.Visible = false;
                lblTitle.Text = "Choose a Sub-Forum";
                initSubForumsList();
            }
          
            if (currentView == (int)view.SUB)
            {
                btnView.Text = "View Thread";
                btnGoBack.Visible = true;
                btnView.Visible = true;
                newToolStripMenuItem1.Visible = false;
                lblTitle.Text = "Choose a Thread";
                initThreadList();
            }
          
            if (currentView == (int)view.THREAD)
            {
                btnView.Visible = false;
                btnGoBack.Visible = true;
                newToolStripMenuItem1.Visible = true;
                lblTitle.Text = "Current Sub-Forum Messages:";

                initMsgList();
                
            }

            if (loginLevel == (int)loginLevels.ADMIN)
                adminToolStripMenuItem.Visible = true;

            if (loginLevel == (int)loginLevels.SUPER)
                superToolStripMenuItem.Visible = true;

            if (loginLevel == (int)loginLevels.GUEST)
            {
                newToolStripMenuItem1.Visible = false;
                logoutToolStripMenuItem.Visible = false;
                logoutToolStripMenuItem1.Visible = false;
                toolStripStatusLabel1.Text = "Hello Guest!";
            }

        }


        private void initForumsList()
        {
            Communicator com = new Communicator();
      //      com.sendGetForumsReq();

            Tuple<int, string> t1 = new Tuple<int, string>(0, "Forum #0");
            Tuple<int, string> t2 = new Tuple<int, string>(1, "Forum #1");
            Tuple<int, string> t3 = new Tuple<int, string>(2, "Forum #2");
            Tuple<int, string> t4 = new Tuple<int, string>(3, "Forum #3");
            Tuple<int, string> t5 = new Tuple<int, string>(4, "Forum #4");

            listBox1.Items.Clear();
            listBox1.Items.Add(t1);
            listBox1.Items.Add(t2);
            listBox1.Items.Add(t3);
            listBox1.Items.Add(t4);
            listBox1.Items.Add(t5);
        }

        private void initSubForumsList()
        {
            Communicator com = new Communicator();
        //    com.sendGetSubForumsReq();
            Tuple<int, string> t1 = new Tuple<int, string>(0, "Sub-Forum #0");
            Tuple<int, string> t2 = new Tuple<int, string>(1, "Sub-Forum #1");
            Tuple<int, string> t3 = new Tuple<int, string>(2, "Sub-Forum #2");
            Tuple<int, string> t4 = new Tuple<int, string>(3, "Sub-Forum #3");
            Tuple<int, string> t5 = new Tuple<int, string>(4, "Sub-Forum #4");

            listBox1.Items.Clear();
            listBox1.Items.Add(t1);
            listBox1.Items.Add(t2);
            listBox1.Items.Add(t3);
            listBox1.Items.Add(t4);
            listBox1.Items.Add(t5);
        }


        private void initThreadList()
        {
            Communicator com = new Communicator();
         //   com.sendGetDiscussionsReq();
            Tuple<int, string> t1 = new Tuple<int, string>(0, "Thread #0");
            Tuple<int, string> t2 = new Tuple<int, string>(1, "Thread #1");
            Tuple<int, string> t3 = new Tuple<int, string>(2, "Thread #2");
            Tuple<int, string> t4 = new Tuple<int, string>(3, "Thread #3");
            Tuple<int, string> t5 = new Tuple<int, string>(4, "Thread #4");

            listBox1.Items.Clear();
            listBox1.Items.Add(t1);
            listBox1.Items.Add(t2);
            listBox1.Items.Add(t3);
            listBox1.Items.Add(t4);
            listBox1.Items.Add(t5);
    
        }

        private void initMsgList()
        {
            Communicator com = new Communicator();
           // com.sendGetCommentsReq();
            Tuple<int, string> t1 = new Tuple<int, string>(0, "First Message");
            Tuple<int, string> t2 = new Tuple<int, string>(1, "Comment #1");
            Tuple<int, string> t3 = new Tuple<int, string>(2, "Comment #2");
            Tuple<int, string> t4 = new Tuple<int, string>(3, "Comment #3");
            Tuple<int, string> t5 = new Tuple<int, string>(4, "Comment #4");

            listBox1.Items.Clear();
            listBox1.Items.Add(t1);
            listBox1.Items.Add(t2);
            listBox1.Items.Add(t3);
            listBox1.Items.Add(t4);
            listBox1.Items.Add(t5);

            
        }


    }
}
