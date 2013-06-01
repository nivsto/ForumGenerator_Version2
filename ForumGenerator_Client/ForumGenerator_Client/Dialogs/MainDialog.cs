using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ForumGenerator_Client.Communication;
using ForumGenerator_Client.Dialogs;
using ForumGenerator_Client.ServiceReference1;


namespace ForumGenerator_Client
{
    public partial class MainDialog : Form
    {
        enum loginLevels
        {
            GUEST,
            MEMBER,
            MODERATOR,
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

        User currUser;

        newCommunicator communicator;

        Forum[] forumsList;
        SubForum[] subforumsList;
        Comment[] commentsList;
        Discussion[] discussionList;


        public MainDialog()
        {
            InitializeComponent();
            this.communicator = new newCommunicator();
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
                currUser = userLog.getUser();
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
                if (loginLevel != (int)loginLevels.SUPER)
                    communicator.logout(currForumId, currUser.userName, currUser.password);

                else
                    communicator.superUserLogout(userName, password);

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
            NewSubForumDialog sub = new NewSubForumDialog(userName, password, currForumId);
            sub.ShowDialog();
            currSubForumId = sub.getSubForumId();
            updateVisibilty();
        }

        /*************************************/
        /*   Publish New Message             */
        /*************************************/
        private void publishNewMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewThreadDialog thr = new NewThreadDialog(userName, password, currForumId, currSubForumId);
            thr.ShowDialog();
            currThreadId = thr.getThreadId();
            updateVisibilty();

        }

        /*************************************/
        /*   Add Comment                     */
        /*************************************/
        private void addACommentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommentAddingDialog cmnt = new CommentAddingDialog(userName, password, currForumId, currSubForumId, currThreadId);
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
            currForumId = nForum.getForumId();
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
                if (loginLevel == (int)loginLevels.MODERATOR)
                    loginLevel = (int)loginLevels.MEMBER;
                initSubForumsList();
            }
          
            if (currentView == (int)view.SUB)
            {
                btnView.Text = "View Thread";
                btnGoBack.Visible = true;
                btnView.Visible = true;
                newToolStripMenuItem1.Visible = true;
                publishNewMessageToolStripMenuItem.Visible = true;
                addACommentToolStripMenuItem.Visible = false;
                lblTitle.Text = "Choose a Thread";
                //check if the user is moderator
                loginLevel = this.communicator.getUserType(currForumId, currSubForumId, userName);

                initThreadList();
            }
          
            if (currentView == (int)view.THREAD)
            {
                btnView.Visible = false;
                btnGoBack.Visible = true;
                newToolStripMenuItem1.Visible = true;
                publishNewMessageToolStripMenuItem.Visible = false;
                addACommentToolStripMenuItem.Visible = true;
                lblTitle.Text = "Current Discussion:";

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
            forumsList = this.communicator.getForums();
            listBox1.Items.Clear();
            for (int i = 0; i < forumsList.Length; i++)
                listBox1.Items.Add(forumsList.ElementAt(i).forumName);

                       
        }

        private void initSubForumsList()
        {
            subforumsList = this.communicator.getSubForums(currForumId);
            listBox1.Items.Clear();
            for (int i = 0; i < subforumsList.Length; i++)
                listBox1.Items.Add(subforumsList.ElementAt(i).subForumTitle);

            
        }


        private void initThreadList()
        {
            discussionList = this.communicator.getDiscussions(currForumId, currSubForumId);
            listBox1.Items.Clear();
            for (int i = 0; i < discussionList.Length; i++)
                listBox1.Items.Add(discussionList.ElementAt(i).title);

    
        }

        private void initMsgList()
        {
            commentsList = this.communicator.getComments(currForumId, currSubForumId, currThreadId);
            listBox1.Items.Clear();
            for (int i = 0; i < commentsList.Length; i++)
                listBox1.Items.Add(commentsList.ElementAt(i).content);

   
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if (index >= 0)
            {
                if (currentView == (int)view.MAIN)
                {
                    currForumId = forumsList.ElementAt(index).forumId;
                }

                if (currentView == (int)view.FORUM)
                {
                    currSubForumId = subforumsList.ElementAt(index).subForumId;
                }

                if (currentView == (int)view.SUB)
                {
                    currThreadId = discussionList.ElementAt(index).discussionId;
                }

            }
        }



        /*************************************/
        /*   View Number Of Sub-Forums       */
        /*************************************/
        private void numberOfSubForumsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int num = (this.communicator.getSubForums(currForumId)).Length;
            string msg = "Number of Sub-Forums:" + num;

            MessageBox.Show(msg, "Info", MessageBoxButtons.OK);
        }




        /*************************************/
        /*   View Num Of Forums              */
        /*************************************/
        private void numberOfForumsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int num = (this.communicator.getForums()).Length;
            string msg = "Number of Forums:" + num;

            MessageBox.Show(msg, "Info", MessageBoxButtons.OK);
        }


        /*************************************/
        /*   Add New Moderator               */
        /*************************************/
        private void newModrtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddModeratorDialog mod = new AddModeratorDialog(currForumId, currSubForumId, userName, password);
            mod.ShowDialog();
        }


        /*************************************/
        /*   Delete Moderator                */
        /*************************************/
        private void DeleteModrtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteModeratorDialog del = new DeleteModeratorDialog(currForumId, currSubForumId, userName, password);
            del.ShowDialog();

        }

        /*************************************/
        /*   Delete Message                  */
        /*************************************/
        private void deleteMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You Are About To Delete The Message. Are You Sure?", "Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.communicator.deleteDiscussion(currForumId, currSubForumId, currThreadId, userName, password);
                updateVisibilty();
            }
        }


        /*************************************/
        /*   Edit Message                    */
        /*************************************/
        private void editMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditMsgDialog edit = new EditMsgDialog(currForumId, currSubForumId, currThreadId, userName, password);
            edit.ShowDialog();
            updateVisibilty();
        }

        /*************************************/
        /*   View Repliers Per User          */
        /*************************************/
        private void repliersPerUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RepliersPerUserDialog rep = new RepliersPerUserDialog(userName, password, currForumId);
            rep.ShowDialog();
        }


        /*************************************/
        /*   View Number Of Posts Per User   */
        /*************************************/
        private void userMessagesNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MsgPerUserDialog mpu = new MsgPerUserDialog(userName, password, currForumId);
            mpu.ShowDialog();
        }

        /*************************************/
        /*   View Mutual Members             */
        /*************************************/
        private void mutualMembersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MutualMembersDialog mut = new MutualMembersDialog(userName, password);
            mut.ShowDialog();
        }

        /*****************************************/
        /* Not Implemented Yet!                  */                                                                              
        /*****************************************/
        /*************************************/
        /*   Delete Sub Forum                */
        /*************************************/
        private void deleteSubForumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You Are About To Delete The Sub-Forum. Are You Sure?", "Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
              
            }
        }












    }
}

