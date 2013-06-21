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
                try
                {
                    if (loginLevel != (int)loginLevels.SUPER)
                        communicator.logout(currForumId, currUser.userName, currUser.password);

                    else
                        communicator.superUserLogout(userName, password);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK);
                }
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

                loginLevel = 0;
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
                    try
                    {
                        communicator.logout(currForumId, userName, password);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK);
                    }

                    loginLevel = 0;
                    userName = null;
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
            //   currSubForumId = sub.getSubForumId();
            updateVisibilty();
        }

        /*************************************/
        /*   Publish New Message             */
        /*************************************/
        private void publishNewMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewThreadDialog thr = new NewThreadDialog(userName, password, currForumId, currSubForumId);
            thr.ShowDialog();
            //      currThreadId = thr.getThreadId();
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
            lblPosTitle.Text = "";
            deleteSubForumToolStripMenuItem.Visible = false;
            deleteMessageToolStripMenuItem.Visible = false;
            toolStripStatusLabel1.Text = "Hello " + userName + "!";
            editMessageToolStripMenuItem.Visible = false;


            if (currentView == (int)view.MAIN)
            {
                mainSuperToolStripMenuItem.Visible = true;
                loginAsSuperUSerToolStripMenuItem.Visible = true;

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

                for (int i = 0; i < forumsList.Length; i++)
                {
                    if (forumsList[i].forumId == currForumId)
                    {
                        lblPosTitle.Text = forumsList[i].forumName;
                    }
                }

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
                deleteMessageToolStripMenuItem.Visible = true;
                editMessageToolStripMenuItem.Visible = true;

                //set the forum name
                for (int i = 0; i < forumsList.Length; i++)
                {
                    if (forumsList[i].forumId == currForumId)
                    {
                        lblPosTitle.Text = forumsList[i].forumName;
                    }
                }
                lblPosTitle.Text += "->";
                //set the subforum name
                for (int i = 0; i < subforumsList.Length; i++)
                {
                    if (subforumsList[i].subForumId == currSubForumId)
                    {
                        lblPosTitle.Text += subforumsList[i].subForumTitle;
                    }
                }


                btnGoBack.Visible = true;
                btnView.Visible = true;
                newToolStripMenuItem1.Visible = true;
                publishNewMessageToolStripMenuItem.Visible = true;
                addACommentToolStripMenuItem.Visible = false;
                lblTitle.Text = "Choose a Thread";
                //check if the user is moderator
                try
                {
                    loginLevel = this.communicator.getUserType(currForumId, currSubForumId, userName);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK);
                }

                initThreadList();
            }

            if (currentView == (int)view.THREAD)
            {
                //set the forum name
                for (int i = 0; i < forumsList.Length; i++)
                {
                    if (forumsList[i].forumId == currForumId)
                    {
                        lblPosTitle.Text = forumsList[i].forumName;
                    }
                }
                lblPosTitle.Text += "->";
                //set the subforum name
                for (int i = 0; i < subforumsList.Length; i++)
                {
                    if (subforumsList[i].subForumId == currSubForumId)
                    {
                        lblPosTitle.Text += subforumsList[i].subForumTitle;
                    }
                }
                lblPosTitle.Text += "->";
                //set the discussion
                for (int i = 0; i < discussionList.Length; i++)
                {
                    if (discussionList[i].discussionId == currThreadId)
                    {
                        lblPosTitle.Text += discussionList[i].title;
                    }
                }

                btnView.Visible = false;
                btnGoBack.Visible = true;
                newToolStripMenuItem1.Visible = true;
                publishNewMessageToolStripMenuItem.Visible = false;
                addACommentToolStripMenuItem.Visible = true;
                lblTitle.Text = "Current Discussion:";

                initMsgList();

            }

            loginToolStripMenuItem1.Visible = false;
            registerToolStripMenuItem.Visible = false;

            if (loginLevel == (int)loginLevels.ADMIN)
            {
                adminToolStripMenuItem.Visible = true;
                if (currentView == (int)view.FORUM)
                {
                    deleteSubForumToolStripMenuItem.Visible = true;
                }

            }

            if (loginLevel == (int)loginLevels.SUPER)
            {
                superToolStripMenuItem.Visible = true;
                loginAsSuperUSerToolStripMenuItem.Visible = false;
            }

            if (loginLevel == (int)loginLevels.GUEST)
            {
                newToolStripMenuItem1.Visible = false;
                logoutToolStripMenuItem.Visible = false;
                logoutToolStripMenuItem1.Visible = false;
                toolStripStatusLabel1.Text = "Hello Guest!";
                loginToolStripMenuItem1.Visible = true;
                registerToolStripMenuItem.Visible = true;

            }



        }


        private void initForumsList()
        {
            try
            {
                forumsList = this.communicator.getForums();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK);
            }
            listBox1.Items.Clear();
            for (int i = 0; i < forumsList.Length; i++)
                listBox1.Items.Add(forumsList.ElementAt(i).forumName);


        }

        private void initSubForumsList()
        {
            try
            {
                subforumsList = this.communicator.getSubForums(currForumId);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK);
            }
            listBox1.Items.Clear();
            for (int i = 0; i < subforumsList.Length; i++)
                listBox1.Items.Add(subforumsList.ElementAt(i).subForumTitle);


        }


        private void initThreadList()
        {
            try
            {
                discussionList = this.communicator.getDiscussions(currForumId, currSubForumId);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK);
            }
            listBox1.Items.Clear();
            for (int i = 0; i < discussionList.Length; i++)
                listBox1.Items.Add(discussionList.ElementAt(i).title);


        }

        private void initMsgList()
        {
            try
            {
                commentsList = this.communicator.getComments(currForumId, currSubForumId, currThreadId);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK);
            } 
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



        /**************************************************/
        /*   View Number Of comments per Sub-Forums       */
        /**************************************************/
        private void numberOfSubForumsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NumOfCommentsPerSub num = new NumOfCommentsPerSub(userName, password, currForumId);
            num.ShowDialog();

        }




        /*************************************/
        /*   View Num Of Forums              */
        /*************************************/
        private void numberOfForumsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int num = (this.communicator.getForums()).Length;
                string msg = "Number of Forums:" + num;
                MessageBox.Show(msg, "Info", MessageBoxButtons.OK);
            
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK);
            }

        }


        /*************************************/
        /*   Add New Moderator               */
        /*************************************/
        private void newModrtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddModeratorDialog mod = new AddModeratorDialog(currForumId, userName, password);
            mod.ShowDialog();
        }


        /*************************************/
        /*   Delete Moderator                */
        /*************************************/
        private void DeleteModrtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteModeratorDialog del = new DeleteModeratorDialog(currForumId, userName, password);
            del.ShowDialog();

        }

        /*************************************/
        /*   Delete Message                  */
        /*************************************/
        private void deleteMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (listBox1.SelectedIndex >= 0)
            {

                if (MessageBox.Show("You Are About To Delete The Discussion. Are You Sure?", "Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        this.communicator.deleteDiscussion(currForumId, currSubForumId, currThreadId, userName, password);
                        updateVisibilty();
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Select Discussion to Delete", "Nothing Selected", MessageBoxButtons.OK);
            }



        }


        /*************************************/
        /*   Edit Message                    */
        /*************************************/
        private void editMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {

                try
                {

                    EditMsgDialog edit = new EditMsgDialog(currForumId, currSubForumId, currThreadId, userName, password);
                    edit.ShowDialog();
                    updateVisibilty();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK);
                }

            }
            else
            {
                MessageBox.Show("Please Select Discussion to Edit", "Nothing Selected", MessageBoxButtons.OK);
            }

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

        /*************************************/
        /*   Delete Sub Forum                */
        /*************************************/
        private void deleteSubForumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {

                if (MessageBox.Show("You Are About To Delete The Sub-Forum. Are You Sure?", "Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        communicator.removeSubForum(currForumId, currSubForumId, userName, password);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK);
                    }
                    updateVisibilty();
                }
            }
            else
            {
                MessageBox.Show("Please Select Sub Forum to Delete", "Nothing Selected", MessageBoxButtons.OK);
            }


        }












    }
}

