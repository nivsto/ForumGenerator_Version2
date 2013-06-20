namespace ForumGenerator_Client
{
    partial class MainDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainDialog));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnView = new System.Windows.Forms.Button();
            this.btnGoBack = new System.Windows.Forms.Button();
            this.mainSuperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginAsSuperUSerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainForumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loginUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginSuperUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.superToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newForumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.numberOfForumsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mutualMembersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewSubForumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moderatorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newModrtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteModrtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.userMessagesNumberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.repliersPerUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.numberOfSubForumsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSubForumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.publishNewMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addACommentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lblPosTitle = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 421);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(371, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(73, 17);
            this.toolStripStatusLabel1.Text = "Hello Guest!";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblTitle.Location = new System.Drawing.Point(12, 61);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(279, 15);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Choose a Forum or Login as a Super-user ";
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(177)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.listBox1.Location = new System.Drawing.Point(12, 90);
            this.listBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(341, 272);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(247, 370);
            this.btnView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(109, 22);
            this.btnView.TabIndex = 3;
            this.btnView.Text = "Go To Forum";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnGoBack
            // 
            this.btnGoBack.Location = new System.Drawing.Point(12, 370);
            this.btnGoBack.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGoBack.Name = "btnGoBack";
            this.btnGoBack.Size = new System.Drawing.Size(105, 22);
            this.btnGoBack.TabIndex = 2;
            this.btnGoBack.Text = "Go Back";
            this.btnGoBack.UseVisualStyleBackColor = true;
            this.btnGoBack.Click += new System.EventHandler(this.btnGoBack_Click);
            // 
            // mainSuperToolStripMenuItem
            // 
            this.mainSuperToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginAsSuperUSerToolStripMenuItem,
            this.logoutToolStripMenuItem1,
            this.quitToolStripMenuItem});
            this.mainSuperToolStripMenuItem.Name = "mainSuperToolStripMenuItem";
            this.mainSuperToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.mainSuperToolStripMenuItem.Text = "Main";
            // 
            // loginAsSuperUSerToolStripMenuItem
            // 
            this.loginAsSuperUSerToolStripMenuItem.Name = "loginAsSuperUSerToolStripMenuItem";
            this.loginAsSuperUSerToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.loginAsSuperUSerToolStripMenuItem.Text = "Login As Super-User";
            this.loginAsSuperUSerToolStripMenuItem.Click += new System.EventHandler(this.loginAsSuperUSerToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem1
            // 
            this.logoutToolStripMenuItem1.Name = "logoutToolStripMenuItem1";
            this.logoutToolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
            this.logoutToolStripMenuItem1.Text = "Logout";
            this.logoutToolStripMenuItem1.Click += new System.EventHandler(this.logoutToolStripMenuItem1_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // mainForumToolStripMenuItem
            // 
            this.mainForumToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem1,
            this.registerToolStripMenuItem,
            this.logoutToolStripMenuItem,
            this.quitToolStripMenuItem1});
            this.mainForumToolStripMenuItem.Name = "mainForumToolStripMenuItem";
            this.mainForumToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.mainForumToolStripMenuItem.Text = "Main";
            // 
            // loginToolStripMenuItem1
            // 
            this.loginToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginUserToolStripMenuItem,
            this.loginSuperUserToolStripMenuItem});
            this.loginToolStripMenuItem1.Name = "loginToolStripMenuItem1";
            this.loginToolStripMenuItem1.Size = new System.Drawing.Size(118, 22);
            this.loginToolStripMenuItem1.Text = "Login";
            // 
            // loginUserToolStripMenuItem
            // 
            this.loginUserToolStripMenuItem.Name = "loginUserToolStripMenuItem";
            this.loginUserToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.loginUserToolStripMenuItem.Text = "User";
            this.loginUserToolStripMenuItem.Click += new System.EventHandler(this.loginUserToolStripMenuItem_Click);
            // 
            // loginSuperUserToolStripMenuItem
            // 
            this.loginSuperUserToolStripMenuItem.Name = "loginSuperUserToolStripMenuItem";
            this.loginSuperUserToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.loginSuperUserToolStripMenuItem.Text = "Super-User";
            this.loginSuperUserToolStripMenuItem.Click += new System.EventHandler(this.loginSuperUserToolStripMenuItem_Click);
            // 
            // registerToolStripMenuItem
            // 
            this.registerToolStripMenuItem.Name = "registerToolStripMenuItem";
            this.registerToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.registerToolStripMenuItem.Text = "Register";
            this.registerToolStripMenuItem.Click += new System.EventHandler(this.registerToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click_1);
            // 
            // quitToolStripMenuItem1
            // 
            this.quitToolStripMenuItem1.Name = "quitToolStripMenuItem1";
            this.quitToolStripMenuItem1.Size = new System.Drawing.Size(118, 22);
            this.quitToolStripMenuItem1.Text = "Quit";
            this.quitToolStripMenuItem1.Click += new System.EventHandler(this.quitToolStripMenuItem1_Click);
            // 
            // superToolStripMenuItem
            // 
            this.superToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newForumToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.superToolStripMenuItem.Name = "superToolStripMenuItem";
            this.superToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.superToolStripMenuItem.Text = "Super-User";
            // 
            // newForumToolStripMenuItem
            // 
            this.newForumToolStripMenuItem.Name = "newForumToolStripMenuItem";
            this.newForumToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.newForumToolStripMenuItem.Text = "Create New Forum";
            this.newForumToolStripMenuItem.Click += new System.EventHandler(this.newForumToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.numberOfForumsToolStripMenuItem,
            this.mutualMembersToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // numberOfForumsToolStripMenuItem
            // 
            this.numberOfForumsToolStripMenuItem.Name = "numberOfForumsToolStripMenuItem";
            this.numberOfForumsToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.numberOfForumsToolStripMenuItem.Text = "Number of Forums";
            this.numberOfForumsToolStripMenuItem.Click += new System.EventHandler(this.numberOfForumsToolStripMenuItem_Click);
            // 
            // mutualMembersToolStripMenuItem
            // 
            this.mutualMembersToolStripMenuItem.Name = "mutualMembersToolStripMenuItem";
            this.mutualMembersToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.mutualMembersToolStripMenuItem.Text = "Mutual Members";
            this.mutualMembersToolStripMenuItem.Click += new System.EventHandler(this.mutualMembersToolStripMenuItem_Click);
            // 
            // adminToolStripMenuItem
            // 
            this.adminToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewSubForumToolStripMenuItem,
            this.moderatorsToolStripMenuItem,
            this.viewToolStripMenuItem1,
            this.deleteSubForumToolStripMenuItem});
            this.adminToolStripMenuItem.Name = "adminToolStripMenuItem";
            this.adminToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.adminToolStripMenuItem.Text = "Admin";
            // 
            // createNewSubForumToolStripMenuItem
            // 
            this.createNewSubForumToolStripMenuItem.Name = "createNewSubForumToolStripMenuItem";
            this.createNewSubForumToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.createNewSubForumToolStripMenuItem.Text = "Create New Sub-Forum";
            this.createNewSubForumToolStripMenuItem.Click += new System.EventHandler(this.createNewSubForumToolStripMenuItem_Click);
            // 
            // moderatorsToolStripMenuItem
            // 
            this.moderatorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newModrtToolStripMenuItem,
            this.DeleteModrtToolStripMenuItem});
            this.moderatorsToolStripMenuItem.Name = "moderatorsToolStripMenuItem";
            this.moderatorsToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.moderatorsToolStripMenuItem.Text = "Moderators";
            // 
            // newModrtToolStripMenuItem
            // 
            this.newModrtToolStripMenuItem.Name = "newModrtToolStripMenuItem";
            this.newModrtToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.newModrtToolStripMenuItem.Text = "New";
            this.newModrtToolStripMenuItem.Click += new System.EventHandler(this.newModrtToolStripMenuItem_Click);
            // 
            // DeleteModrtToolStripMenuItem
            // 
            this.DeleteModrtToolStripMenuItem.Name = "DeleteModrtToolStripMenuItem";
            this.DeleteModrtToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.DeleteModrtToolStripMenuItem.Text = "Delete";
            this.DeleteModrtToolStripMenuItem.Click += new System.EventHandler(this.DeleteModrtToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem1
            // 
            this.viewToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userMessagesNumberToolStripMenuItem,
            this.repliersPerUserToolStripMenuItem,
            this.numberOfSubForumsToolStripMenuItem});
            this.viewToolStripMenuItem1.Name = "viewToolStripMenuItem1";
            this.viewToolStripMenuItem1.Size = new System.Drawing.Size(202, 22);
            this.viewToolStripMenuItem1.Text = "View";
            // 
            // userMessagesNumberToolStripMenuItem
            // 
            this.userMessagesNumberToolStripMenuItem.Name = "userMessagesNumberToolStripMenuItem";
            this.userMessagesNumberToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            this.userMessagesNumberToolStripMenuItem.Text = "User Messages Number";
            this.userMessagesNumberToolStripMenuItem.Click += new System.EventHandler(this.userMessagesNumberToolStripMenuItem_Click);
            // 
            // repliersPerUserToolStripMenuItem
            // 
            this.repliersPerUserToolStripMenuItem.Name = "repliersPerUserToolStripMenuItem";
            this.repliersPerUserToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            this.repliersPerUserToolStripMenuItem.Text = "Repliers Per User";
            this.repliersPerUserToolStripMenuItem.Click += new System.EventHandler(this.repliersPerUserToolStripMenuItem_Click);
            // 
            // numberOfSubForumsToolStripMenuItem
            // 
            this.numberOfSubForumsToolStripMenuItem.Name = "numberOfSubForumsToolStripMenuItem";
            this.numberOfSubForumsToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            this.numberOfSubForumsToolStripMenuItem.Text = "Number of Comments per Sub-Forum";
            this.numberOfSubForumsToolStripMenuItem.Click += new System.EventHandler(this.numberOfSubForumsToolStripMenuItem_Click);
            // 
            // deleteSubForumToolStripMenuItem
            // 
            this.deleteSubForumToolStripMenuItem.Name = "deleteSubForumToolStripMenuItem";
            this.deleteSubForumToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.deleteSubForumToolStripMenuItem.Text = "Delete Sub-Forum";
            this.deleteSubForumToolStripMenuItem.Click += new System.EventHandler(this.deleteSubForumToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem1
            // 
            this.newToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.publishNewMessageToolStripMenuItem,
            this.addACommentToolStripMenuItem,
            this.editMessageToolStripMenuItem,
            this.deleteMessageToolStripMenuItem});
            this.newToolStripMenuItem1.Name = "newToolStripMenuItem1";
            this.newToolStripMenuItem1.Size = new System.Drawing.Size(40, 20);
            this.newToolStripMenuItem1.Text = "Edit";
            // 
            // publishNewMessageToolStripMenuItem
            // 
            this.publishNewMessageToolStripMenuItem.Name = "publishNewMessageToolStripMenuItem";
            this.publishNewMessageToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.publishNewMessageToolStripMenuItem.Text = "Publish New Message";
            this.publishNewMessageToolStripMenuItem.Click += new System.EventHandler(this.publishNewMessageToolStripMenuItem_Click);
            // 
            // addACommentToolStripMenuItem
            // 
            this.addACommentToolStripMenuItem.Name = "addACommentToolStripMenuItem";
            this.addACommentToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.addACommentToolStripMenuItem.Text = "Add a Comment";
            this.addACommentToolStripMenuItem.Click += new System.EventHandler(this.addACommentToolStripMenuItem_Click);
            // 
            // editMessageToolStripMenuItem
            // 
            this.editMessageToolStripMenuItem.Name = "editMessageToolStripMenuItem";
            this.editMessageToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.editMessageToolStripMenuItem.Text = "Edit Message";
            this.editMessageToolStripMenuItem.Click += new System.EventHandler(this.editMessageToolStripMenuItem_Click);
            // 
            // deleteMessageToolStripMenuItem
            // 
            this.deleteMessageToolStripMenuItem.Name = "deleteMessageToolStripMenuItem";
            this.deleteMessageToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.deleteMessageToolStripMenuItem.Text = "Delete Discussion";
            this.deleteMessageToolStripMenuItem.Click += new System.EventHandler(this.deleteMessageToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainSuperToolStripMenuItem,
            this.mainForumToolStripMenuItem,
            this.superToolStripMenuItem,
            this.adminToolStripMenuItem,
            this.newToolStripMenuItem1,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(371, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(101, 20);
            this.toolStripMenuItem1.Text = "Notifications";
            // 
            // lblPosTitle
            // 
            this.lblPosTitle.AutoSize = true;
            this.lblPosTitle.Location = new System.Drawing.Point(12, 34);
            this.lblPosTitle.Name = "lblPosTitle";
            this.lblPosTitle.Size = new System.Drawing.Size(24, 13);
            this.lblPosTitle.TabIndex = 6;
            this.lblPosTitle.Text = "text";
            // 
            // MainDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(371, 443);
            this.Controls.Add(this.lblPosTitle);
            this.Controls.Add(this.btnGoBack);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(30, 38);
            this.Name = "MainDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Forum Generator";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnGoBack;
        private System.Windows.Forms.ToolStripMenuItem mainSuperToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginAsSuperUSerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mainForumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loginUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginSuperUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem superToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newForumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem numberOfForumsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mutualMembersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adminToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNewSubForumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moderatorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newModrtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteModrtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem userMessagesNumberToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem repliersPerUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem numberOfSubForumsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteSubForumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem publishNewMessageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addACommentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editMessageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteMessageToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Label lblPosTitle;
    }
}