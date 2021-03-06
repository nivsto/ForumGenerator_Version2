﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ForumGenerator_Client.Communication;
using ForumGenerator_Client.ServiceReference1;

namespace ForumGenerator_Client.Dialogs
{
    public partial class AdminDialog : Form
    {
        Communicator communicator = new Communicator();
        int forumId;
        string usrName;
        string pswd;

        int addTab_subForumId;
        User[] addTab_users = null;
        User[] addTab_moderators = null;
        SubForum[] addTab_subForums = null;

        SubForum[] permisionTab_subForums = null;     
        User[] permisionTab_moderators = null;
        int permisionTab_subId;
        bool edit;
        bool delete;
        
        SubForum[] msgTab_subForums = null;

        User[] commentTab_users = null;

        User[] repliersTab_users = null;


        public AdminDialog(int forumId, string adderUsrName, string adderPswd)
        {
            InitializeComponent();
            this.forumId = forumId;
            this.pswd = adderPswd;
            this.usrName = adderUsrName;
            initAddRemoveTab();
            initModeratorPermissionsTab();
            initMessagesTab();
            initCommentsTab();
            initRepliersTab();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /********************************/
        /*     Add / Remove Moderators  */
        /********************************/
        private void initAddRemoveTab()
        {
            this.cmbxSubs.Items.Clear();
            this.lstModerators.Items.Clear();
            this.lstUsers.Items.Clear();

            //init sub forums list
            try
            {
                addTab_subForums = communicator.getSubForums(forumId);
                for (int i = 0; i < addTab_subForums.Length; i++)
                    cmbxSubs.Items.Add(addTab_subForums.ElementAt(i).subForumTitle);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void initModeratorsList()
        {
            lstUsers.Items.Clear();
            lstModerators.Items.Clear();

            int index = this.cmbxSubs.SelectedIndex;

            this.addTab_subForumId = addTab_subForums[index].subForumId;

            try
            {
                addTab_moderators = communicator.getModerators(forumId, addTab_subForumId);
                for (int i = 0; i < addTab_moderators.Length; i++)
                    lstModerators.Items.Add(addTab_moderators.ElementAt(i).userName);

                bool exist = false;

                addTab_users = communicator.getUsers(forumId);
                for (int i = 0; i < addTab_users.Length; i++)
                {
                    for (int j = 0; j < addTab_moderators.Length; j++)
                    {
                        if (addTab_moderators[j].userName == addTab_users[i].userName)
                        {
                            exist = true;
                            break;
                        }
                    }
                    if (!exist)
                        lstUsers.Items.Add(addTab_users.ElementAt(i).userName);

                    exist = false;
                }


            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbxSubs_SelectedIndexChanged(object sender, EventArgs e)
        {
            initModeratorsList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int index = lstUsers.SelectedIndex;
            if (index >= 0)
            {
                string name = lstUsers.Text;

                try
                {
                    communicator.addModerator(name, forumId, addTab_subForumId, usrName, pswd);
                    initModeratorsList();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnRemove_Click(object sender, EventArgs e)
        {
            int index = lstModerators.SelectedIndex;
            if (index >= 0)
            {
                string name = lstModerators.Text;


                try
                {
                    communicator.removeModerator(name, forumId, addTab_subForumId, usrName, pswd);
                    initModeratorsList();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void lstModerators_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstModerators.SelectedIndex != -1)
                lstUsers.ClearSelected();
        }

        private void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstUsers.SelectedIndex != -1)
                lstModerators.ClearSelected();

        }

        /********************************/
        /*     Moderators Permissions   */
        /********************************/
        private void initModeratorPermissionsTab()
        {
            this.cmbxPer_moder.Items.Clear();
            this.edit = false;
            this.delete = false;

            chkbxDelete.Checked = delete;
            chkbxEdit.Checked = edit;
            try
            {
                permisionTab_subForums = communicator.getSubForums(forumId);

                for (int i = 0; i < permisionTab_subForums.Length; i++)
                    cmbxPer_subs.Items.Add(permisionTab_subForums.ElementAt(i).subForumTitle);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbxPer_subs_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.cmbxPer_subs.SelectedIndex;
            this.permisionTab_subId = permisionTab_subForums[index].subForumId;

            try
            {

                permisionTab_moderators = communicator.getModerators(forumId, permisionTab_subId);

                for (int i = 0; i < permisionTab_moderators.Length; i++)
                    cmbxPer_moder.Items.Add(permisionTab_moderators.ElementAt(i).userName);


            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbxPer_moder_SelectedIndexChanged(object sender, EventArgs e)
        {

            //show permissions
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //save permissions
        }



        /********************************/
        /*     Messages Per sub forums  */
        /********************************/
        private void initMessagesTab()
        {
            this.cmbxMsg_subs.Items.Clear();
            

            //init sub forums list
            try
            {
                msgTab_subForums = communicator.getSubForums(forumId);

                for (int i = 0; i < msgTab_subForums.Length; i++)
                    cmbxMsg_subs.Items.Add(msgTab_subForums.ElementAt(i).subForumTitle);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbxMsg_subs_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbxMsg_subs.SelectedIndex;
            try
            {
                int num = communicator.getNumOfCommentsSubForum(usrName, pswd, forumId, msgTab_subForums[index].subForumId);
                lblMsg_num.Text = num.ToString();

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /********************************/
        /*     Messages per user        */
        /********************************/
        private void initCommentsTab()
        {
            this.cmbxCom_User.Items.Clear();

            try
            {
                this.commentTab_users = communicator.getUsers(forumId);

                for (int i = 0; i < commentTab_users.Length; i++)
                    cmbxCom_User.Items.Add(commentTab_users.ElementAt(i).userName);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbxCom_User_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int num = communicator.getNumOfCommentsSingleUser(usrName, pswd, forumId, cmbxCom_User.Text);
                lblCom_Num.Text = num.ToString();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /********************************/
        /*     Repliers per user        */
        /********************************/
        private void initRepliersTab()
        {
            this.cmbxRep_user.Items.Clear();
            this.lstRep_repliers.Items.Clear();

  
            try
            {
                repliersTab_users = communicator.getUsers(forumId);

                for (int i = 0; i < repliersTab_users.Length; i++)
                    cmbxRep_user.Items.Add(repliersTab_users.ElementAt(i).userName);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbxRep_user_SelectedIndexChanged(object sender, EventArgs e)
        {
            string userName = this.cmbxRep_user.Text;
            lstRep_repliers.Items.Clear();
            try
            {
                User[] users = communicator.getResponsersForSingleUser(usrName, pswd, forumId, userName);

                for (int i = 0; i < users.Length; i++)
                    lstRep_repliers.Items.Add(users.ElementAt(i).userName);

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }










    }
}
