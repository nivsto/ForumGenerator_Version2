namespace ForumGenerator_Client.Dialogs
{
    partial class AdminDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminDialog));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lstUsers = new System.Windows.Forms.ListBox();
            this.lstModerators = new System.Windows.Forms.ListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnRemove = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbxSubs = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.cmbxPer_subs = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkbxDelete = new System.Windows.Forms.CheckBox();
            this.chkbxEdit = new System.Windows.Forms.CheckBox();
            this.cmbxPer_moder = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblMsg_num = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbxMsg_subs = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lblCom_Num = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbxCom_User = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.cmbxRep_user = new System.Windows.Forms.ComboBox();
            this.lstRep_repliers = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabConfirm = new System.Windows.Forms.TabPage();
            this.lstConfirmed = new System.Windows.Forms.ListBox();
            this.lstNotConfirmed = new System.Windows.Forms.ListBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.minimize = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabConfirm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabConfirm);
            this.tabControl1.Location = new System.Drawing.Point(12, 40);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(491, 277);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage1.Controls.Add(this.lstUsers);
            this.tabPage1.Controls.Add(this.lstModerators);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.btnRemove);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.cmbxSubs);
            this.tabPage1.Controls.Add(this.btnAdd);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(483, 251);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Change Moderators";
            // 
            // lstUsers
            // 
            this.lstUsers.FormattingEnabled = true;
            this.lstUsers.Location = new System.Drawing.Point(288, 84);
            this.lstUsers.Name = "lstUsers";
            this.lstUsers.Size = new System.Drawing.Size(120, 134);
            this.lstUsers.TabIndex = 16;
            this.lstUsers.SelectedIndexChanged += new System.EventHandler(this.lstUsers_SelectedIndexChanged);
            // 
            // lstModerators
            // 
            this.lstModerators.FormattingEnabled = true;
            this.lstModerators.Location = new System.Drawing.Point(62, 84);
            this.lstModerators.Name = "lstModerators";
            this.lstModerators.Size = new System.Drawing.Size(120, 134);
            this.lstModerators.TabIndex = 15;
            this.lstModerators.SelectedIndexChanged += new System.EventHandler(this.lstModerators_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(285, 68);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "All Users:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(59, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Current Moderators:";
            // 
            // btnRemove
            // 
            this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
            this.btnRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemove.Location = new System.Drawing.Point(197, 142);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 10;
            this.btnRemove.Text = "Remove";
            this.btnRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(47, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Choose Sub Forum:";
            // 
            // cmbxSubs
            // 
            this.cmbxSubs.FormattingEnabled = true;
            this.cmbxSubs.Location = new System.Drawing.Point(166, 31);
            this.cmbxSubs.Name = "cmbxSubs";
            this.cmbxSubs.Size = new System.Drawing.Size(242, 21);
            this.cmbxSubs.TabIndex = 6;
            this.cmbxSubs.SelectedIndexChanged += new System.EventHandler(this.cmbxSubs_SelectedIndexChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(197, 113);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage5.Controls.Add(this.cmbxPer_subs);
            this.tabPage5.Controls.Add(this.label5);
            this.tabPage5.Controls.Add(this.btnSave);
            this.tabPage5.Controls.Add(this.chkbxDelete);
            this.tabPage5.Controls.Add(this.chkbxEdit);
            this.tabPage5.Controls.Add(this.cmbxPer_moder);
            this.tabPage5.Controls.Add(this.label11);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(483, 251);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Moderator Permissions";
            // 
            // cmbxPer_subs
            // 
            this.cmbxPer_subs.FormattingEnabled = true;
            this.cmbxPer_subs.Location = new System.Drawing.Point(262, 29);
            this.cmbxPer_subs.Name = "cmbxPer_subs";
            this.cmbxPer_subs.Size = new System.Drawing.Size(121, 21);
            this.cmbxPer_subs.TabIndex = 6;
            this.cmbxPer_subs.SelectedIndexChanged += new System.EventHandler(this.cmbxPer_subs_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Choose Sub-Forum:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(181, 174);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkbxDelete
            // 
            this.chkbxDelete.AutoSize = true;
            this.chkbxDelete.Location = new System.Drawing.Point(69, 131);
            this.chkbxDelete.Name = "chkbxDelete";
            this.chkbxDelete.Size = new System.Drawing.Size(160, 17);
            this.chkbxDelete.TabIndex = 3;
            this.chkbxDelete.Text = "Enable Discussions Deleting";
            this.chkbxDelete.UseVisualStyleBackColor = true;
            // 
            // chkbxEdit
            // 
            this.chkbxEdit.AutoSize = true;
            this.chkbxEdit.Location = new System.Drawing.Point(69, 99);
            this.chkbxEdit.Name = "chkbxEdit";
            this.chkbxEdit.Size = new System.Drawing.Size(153, 17);
            this.chkbxEdit.TabIndex = 2;
            this.chkbxEdit.Text = "Enable Discussions Editing";
            this.chkbxEdit.UseVisualStyleBackColor = true;
            this.chkbxEdit.CheckedChanged += new System.EventHandler(this.chkbxEdit_CheckedChanged);
            // 
            // cmbxPer_moder
            // 
            this.cmbxPer_moder.FormattingEnabled = true;
            this.cmbxPer_moder.Location = new System.Drawing.Point(261, 62);
            this.cmbxPer_moder.Name = "cmbxPer_moder";
            this.cmbxPer_moder.Size = new System.Drawing.Size(121, 21);
            this.cmbxPer_moder.TabIndex = 1;
            this.cmbxPer_moder.SelectedIndexChanged += new System.EventHandler(this.cmbxPer_moder_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(32, 65);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(223, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Choose Moderator to Change his Permissions:";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage2.Controls.Add(this.lblMsg_num);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.cmbxMsg_subs);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(483, 251);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Messages";
            // 
            // lblMsg_num
            // 
            this.lblMsg_num.AutoSize = true;
            this.lblMsg_num.Location = new System.Drawing.Point(145, 69);
            this.lblMsg_num.Name = "lblMsg_num";
            this.lblMsg_num.Size = new System.Drawing.Size(13, 13);
            this.lblMsg_num.TabIndex = 12;
            this.lblMsg_num.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Number Of Messages:";
            // 
            // cmbxMsg_subs
            // 
            this.cmbxMsg_subs.FormattingEnabled = true;
            this.cmbxMsg_subs.Location = new System.Drawing.Point(292, 30);
            this.cmbxMsg_subs.Name = "cmbxMsg_subs";
            this.cmbxMsg_subs.Size = new System.Drawing.Size(121, 21);
            this.cmbxMsg_subs.TabIndex = 10;
            this.cmbxMsg_subs.SelectedIndexChanged += new System.EventHandler(this.cmbxMsg_subs_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(245, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Choose Sub-Forum To View Number of Messages:";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage3.Controls.Add(this.lblCom_Num);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.cmbxCom_User);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(483, 251);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Comments";
            // 
            // lblCom_Num
            // 
            this.lblCom_Num.AutoSize = true;
            this.lblCom_Num.Location = new System.Drawing.Point(147, 68);
            this.lblCom_Num.Name = "lblCom_Num";
            this.lblCom_Num.Size = new System.Drawing.Size(13, 13);
            this.lblCom_Num.TabIndex = 7;
            this.lblCom_Num.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Number Of Messages:";
            // 
            // cmbxCom_User
            // 
            this.cmbxCom_User.FormattingEnabled = true;
            this.cmbxCom_User.Location = new System.Drawing.Point(261, 29);
            this.cmbxCom_User.Name = "cmbxCom_User";
            this.cmbxCom_User.Size = new System.Drawing.Size(121, 21);
            this.cmbxCom_User.TabIndex = 5;
            this.cmbxCom_User.SelectedIndexChanged += new System.EventHandler(this.cmbxCom_User_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Choose User To View Number of Messages:";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage4.Controls.Add(this.cmbxRep_user);
            this.tabPage4.Controls.Add(this.lstRep_repliers);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(483, 251);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Repliers";
            // 
            // cmbxRep_user
            // 
            this.cmbxRep_user.FormattingEnabled = true;
            this.cmbxRep_user.Location = new System.Drawing.Point(161, 27);
            this.cmbxRep_user.Name = "cmbxRep_user";
            this.cmbxRep_user.Size = new System.Drawing.Size(161, 21);
            this.cmbxRep_user.TabIndex = 1;
            this.cmbxRep_user.SelectedIndexChanged += new System.EventHandler(this.cmbxRep_user_SelectedIndexChanged);
            // 
            // lstRep_repliers
            // 
            this.lstRep_repliers.FormattingEnabled = true;
            this.lstRep_repliers.Location = new System.Drawing.Point(161, 66);
            this.lstRep_repliers.Name = "lstRep_repliers";
            this.lstRep_repliers.Size = new System.Drawing.Size(161, 121);
            this.lstRep_repliers.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Choose User From List:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Users That Commented:";
            // 
            // tabConfirm
            // 
            this.tabConfirm.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabConfirm.Controls.Add(this.lstConfirmed);
            this.tabConfirm.Controls.Add(this.lstNotConfirmed);
            this.tabConfirm.Controls.Add(this.label12);
            this.tabConfirm.Controls.Add(this.label13);
            this.tabConfirm.Controls.Add(this.btnConfirm);
            this.tabConfirm.Location = new System.Drawing.Point(4, 22);
            this.tabConfirm.Name = "tabConfirm";
            this.tabConfirm.Padding = new System.Windows.Forms.Padding(3);
            this.tabConfirm.Size = new System.Drawing.Size(483, 251);
            this.tabConfirm.TabIndex = 5;
            this.tabConfirm.Text = "Confirm Users";
            // 
            // lstConfirmed
            // 
            this.lstConfirmed.FormattingEnabled = true;
            this.lstConfirmed.Location = new System.Drawing.Point(296, 57);
            this.lstConfirmed.Name = "lstConfirmed";
            this.lstConfirmed.Size = new System.Drawing.Size(120, 134);
            this.lstConfirmed.TabIndex = 22;
            // 
            // lstNotConfirmed
            // 
            this.lstNotConfirmed.FormattingEnabled = true;
            this.lstNotConfirmed.Location = new System.Drawing.Point(70, 57);
            this.lstNotConfirmed.Name = "lstNotConfirmed";
            this.lstNotConfirmed.Size = new System.Drawing.Size(120, 134);
            this.lstNotConfirmed.TabIndex = 21;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(293, 41);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "Confirmed Users:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(67, 41);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(107, 13);
            this.label13.TabIndex = 19;
            this.label13.Text = "Not Confirmed Users:";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Image = ((System.Drawing.Image)(resources.GetObject("btnConfirm.Image")));
            this.btnConfirm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfirm.Location = new System.Drawing.Point(205, 90);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 18;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(428, 323);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // close
            // 
            this.close.AccessibleDescription = "Close";
            this.close.AccessibleName = "Close";
            this.close.AutoSize = true;
            this.close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.close.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.close.Location = new System.Drawing.Point(495, 3);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(19, 18);
            this.close.TabIndex = 76;
            this.close.Text = "X";
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label14.Location = new System.Drawing.Point(9, 4);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(91, 17);
            this.label14.TabIndex = 75;
            this.label14.Text = "Admin Dialog";
            // 
            // minimize
            // 
            this.minimize.AutoSize = true;
            this.minimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.minimize.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.minimize.Location = new System.Drawing.Point(469, -3);
            this.minimize.Name = "minimize";
            this.minimize.Size = new System.Drawing.Size(20, 25);
            this.minimize.TabIndex = 74;
            this.minimize.Text = "-";
            this.minimize.Click += new System.EventHandler(this.minimize_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-15, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(542, 10);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 73;
            this.pictureBox1.TabStop = false;
            // 
            // AdminDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(517, 358);
            this.Controls.Add(this.close);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.minimize);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AdminDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin ";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabConfirm.ResumeLayout(false);
            this.tabConfirm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblCom_Num;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbxCom_User;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbxRep_user;
        private System.Windows.Forms.ListBox lstRep_repliers;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMsg_num;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbxMsg_subs;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox lstUsers;
        private System.Windows.Forms.ListBox lstModerators;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbxSubs;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkbxDelete;
        private System.Windows.Forms.CheckBox chkbxEdit;
        private System.Windows.Forms.ComboBox cmbxPer_moder;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbxPer_subs;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabConfirm;
        private System.Windows.Forms.ListBox lstConfirmed;
        private System.Windows.Forms.ListBox lstNotConfirmed;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label close;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label minimize;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}