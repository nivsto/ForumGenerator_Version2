using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ForumGenerator_Client.Communication;
using ForumGenerator_Client.Dialogs;
using ForumGenerator_Client.ServiceReference1;
using System.Threading;


namespace ForumGenerator_Client.Dialogs
{
    public class MainMethods
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
            SUB
        };

        MainViewDialog mainViewDialog;
        SubForumDialog subForumView;

        int currentView = (int)view.MAIN;
        int loginLevel = (int)loginLevels.GUEST;
        string userName = null;
        string password = null;
        string forumName = "";
        string subForumName = "";
        int currForumId = 0;
        Forum.RegPolicy policy = Forum.RegPolicy.NONE;
        int currSubForumId = 0;

        User currUser;

        Communicator communicator;

        Forum[] forumsList;
        SubForum[] subforumsList;
        Comment[] commentsList;
        Discussion[] discussionList;

        splashScreen splashS;
        Thread t;
        public MainMethods()
        {
            t = new Thread(new ThreadStart(splash));
            t.Start();
            mainViewDialog = new MainViewDialog(this, t);
            subForumView = new SubForumDialog(this);
            this.communicator = new Communicator();
            updateVisibilty();
            Application.Run(mainViewDialog);
        }

        public void splash()
        {
            splashS = new splashScreen();
            Application.Run(splashS);
        }


        /*************************************/
        /*   Login User                      */
        /*************************************/
        public void loginUser()
        {

            bool super = false;

            if (currentView == (int)view.MAIN)
                super = true;

            UserLoginDialog userLog = new UserLoginDialog(currForumId, super);
            userLog.ShowDialog();
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
        /*   Logout User                     */
        /*************************************/
        public void logout()
        {
            if (MessageBox.Show("Are You Sure?", "Logout", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                try
                {
                    if (loginLevel != (int)loginLevels.SUPER)
                        communicator.logout(currForumId, currUser.userName, currUser.password);

                    else
                        communicator.superUserLogout(userName, password);

                    setStatusMsg("Loged out Successfully!");
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        public void register()
        {
            RegistrationDialog reg = new RegistrationDialog(currForumId);
            reg.ShowDialog();
            if (reg.getUserName() != null)
            {
                loginLevel = 0;
                updateVisibilty();
                setStatusMsg("New User Was Registered!");
            }
        }


        /*************************************/
        /*   Go To Button                    */
        /*************************************/
        public void btnGoto(int index)
        {
            if (currentView == (int)view.MAIN)
            {
                currForumId = forumsList[index].forumId;
                policy = forumsList[index].registrationPolicy;
                forumName = forumsList[index].forumName;
            }

            if (currentView == (int)view.FORUM)
            {
                currSubForumId = subforumsList[index].subForumId;
                subForumName = subforumsList[index].subForumTitle;
            }


            currentView++;
            updateVisibilty();
       
        }



        /*************************************/
        /*   Back Button                     */
        /*************************************/
        public void btnGoBack()
        {

            if (currentView == (int)view.FORUM && loginLevel != (int)loginLevels.SUPER && loginLevel != (int)loginLevels.GUEST)
            {
                if (MessageBox.Show("You Are About To Leave The Forum. Are You Sure?", "Logout", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    try
                    {
                        communicator.logout(currForumId, userName, password);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        public void createNewSubForum()
        {
            NewSubForumDialog sub = new NewSubForumDialog(userName, password, currForumId);
            sub.ShowDialog();
            updateVisibilty();
            setStatusMsg("New Sub Forum Created Successfully!");
        }

        /*************************************/
        /*   Publish New Message             */
        /*************************************/
        public void publishNewMessage()
        {
            NewThreadDialog thr = new NewThreadDialog(userName, password, currForumId, currSubForumId);
            thr.ShowDialog();
            updateVisibilty();
            setStatusMsg("New Discussion Was Created!");
        }

        /*************************************/
        /*   Add Comment                     */
        /*************************************/
        public void addAComment(int id, string text)
        {
            if (String.IsNullOrEmpty(text))
                MessageBox.Show("Please Enter Your Comment!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                try
                {
                    communicator.createNewComment(userName, password, currForumId, currSubForumId, id, text);
                    setStatusMsg("New Comment Was Added!");
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            updateVisibilty();
        }

        /*************************************/
        /*   Create New Forum                */
        /*************************************/
        public void newForum()
        {
            NewForumDialog nForum = new NewForumDialog(userName, password);
            nForum.ShowDialog();
            updateVisibilty();
        }


   
        /*************************************/
        /*   Delete Message                  */
        /*************************************/
        public void deleteMessage(int id)
        {

            if (MessageBox.Show("You Are About To Delete The Discussion. Are You Sure?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                try
                {
                    this.communicator.deleteDiscussion(currForumId, currSubForumId, id, userName, password);
                    updateVisibilty();
                    setStatusMsg("Discussion Was Deleted!");
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }


        /*************************************/
        /*   Edit Message                    */
        /*************************************/
        public void editMessage(int id)
        {
           try
            {

                EditMsgDialog edit = new EditMsgDialog(currForumId, currSubForumId, id, userName, password);
                edit.ShowDialog();
                updateVisibilty();
                setStatusMsg("Discussion Was Edited!");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        /*************************************/
        /*   Delete Sub Forum                */
        /*************************************/
        public void deleteSubForum(int id)
        {
            if (MessageBox.Show("You Are About To Delete The Sub-Forum. Are You Sure?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                try
                {
                    communicator.removeSubForum(currForumId, id, userName, password);
                    setStatusMsg("Sub Forum Was Deleted!");
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                updateVisibilty();
            }
        }


        public void adminDialog()
        {
            AdminDialog admin = new AdminDialog(currForumId, userName, password, policy);
            admin.Show();
        }

        public void superDialog()
        {
            SuperUserDialog sup = new SuperUserDialog(userName, password);
            sup.Show();
        }

        public void addNew()
        {
            if (currentView == (int)view.MAIN)
                newForum();

            if (currentView == (int)view.FORUM)
                createNewSubForum();
            
            if (currentView == (int)view.SUB)
                publishNewMessage();
            

        }


        public void delete(int index)
        {

            if (currentView == (int)view.FORUM)
            {
                deleteSubForum(subforumsList[index].subForumId);

            }


        }
        

        public void refresh()
        {
            updateVisibilty();
            setStatusMsg("Refreshed!");
        }

        public void notifications()
        {
        }





        public void updateVisibilty()
        {
            if (currentView == (int)view.MAIN)
            {
                this.mainViewDialog.updateView(loginLevel, currentView, userName);
                initForumsList();
                this.subForumView.Hide();
                this.mainViewDialog.Show();
                
            }
            if (currentView == (int)view.FORUM)
            {
                this.mainViewDialog.updateView(loginLevel, currentView, userName);
                initSubForumsList();
                this.subForumView.Hide();
                this.mainViewDialog.Show();
                
            }
            if (currentView == (int)view.SUB)
            {
                this.subForumView.updateView(loginLevel, userName, forumName, subForumName);           
                initThreadList();
                this.subForumView.Show();
                this.mainViewDialog.Hide();
                
            }
        }


        public void initForumsList()
        {
            try
            {
                forumsList = this.communicator.getForums();
                this.mainViewDialog.updateForumsList(forumsList);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void initSubForumsList()
        {
            try
            {
                subforumsList = this.communicator.getSubForums(currForumId);
                this.mainViewDialog.updateSubForumsList(subforumsList);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void initThreadList()
        {
            try
            {
                discussionList = this.communicator.getDiscussions(currForumId, currSubForumId);
                this.subForumView.updateDiscussionList(discussionList);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public Comment[] getCommentList(int discussion)
        {
            try
            {
                return commentsList = this.communicator.getComments(currForumId, currSubForumId, discussion);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public void quit()
        {
            try
            {
                if (loginLevel != (int)loginLevels.SUPER)
                    communicator.logout(currForumId, currUser.userName, currUser.password);

                else
                    communicator.superUserLogout(userName, password);
            }
            catch (Exception )
            {
               
            }

            this.mainViewDialog.Close();
        }


        public void setStatusMsg(string msg)
        {
            this.mainViewDialog.setStatusMsg(msg);
            this.subForumView.setStatusMsg(msg);
        }
    }
}
