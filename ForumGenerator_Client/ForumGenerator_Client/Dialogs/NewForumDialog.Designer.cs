namespace ForumGenerator_Client
{
    partial class NewForumDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewForumDialog));
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBoxPassword = new System.Windows.Forms.TextBox();
            this.txtBoxAdmin = new System.Windows.Forms.TextBox();
            this.rdbtnMailAct = new System.Windows.Forms.RadioButton();
            this.rdbtnAdminConfirm = new System.Windows.Forms.RadioButton();
            this.rdbtnNone = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.close = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.minimize = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Forum Name:";
            // 
            // txtBoxName
            // 
            this.txtBoxName.Location = new System.Drawing.Point(132, 45);
            this.txtBoxName.MaxLength = 30;
            this.txtBoxName.Name = "txtBoxName";
            this.txtBoxName.Size = new System.Drawing.Size(144, 20);
            this.txtBoxName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Forum Admin:";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(53, 242);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 4;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(169, 242);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Admin Password:";
            // 
            // txtBoxPassword
            // 
            this.txtBoxPassword.Location = new System.Drawing.Point(132, 97);
            this.txtBoxPassword.MaxLength = 10;
            this.txtBoxPassword.Name = "txtBoxPassword";
            this.txtBoxPassword.PasswordChar = '*';
            this.txtBoxPassword.Size = new System.Drawing.Size(144, 20);
            this.txtBoxPassword.TabIndex = 3;
            // 
            // txtBoxAdmin
            // 
            this.txtBoxAdmin.Location = new System.Drawing.Point(132, 71);
            this.txtBoxAdmin.MaxLength = 20;
            this.txtBoxAdmin.Name = "txtBoxAdmin";
            this.txtBoxAdmin.Size = new System.Drawing.Size(144, 20);
            this.txtBoxAdmin.TabIndex = 2;
            // 
            // rdbtnMailAct
            // 
            this.rdbtnMailAct.AutoSize = true;
            this.rdbtnMailAct.Location = new System.Drawing.Point(23, 23);
            this.rdbtnMailAct.Name = "rdbtnMailAct";
            this.rdbtnMailAct.Size = new System.Drawing.Size(94, 17);
            this.rdbtnMailAct.TabIndex = 10;
            this.rdbtnMailAct.TabStop = true;
            this.rdbtnMailAct.Text = "Mail Activation";
            this.rdbtnMailAct.UseVisualStyleBackColor = true;
            // 
            // rdbtnAdminConfirm
            // 
            this.rdbtnAdminConfirm.AutoSize = true;
            this.rdbtnAdminConfirm.Location = new System.Drawing.Point(23, 46);
            this.rdbtnAdminConfirm.Name = "rdbtnAdminConfirm";
            this.rdbtnAdminConfirm.Size = new System.Drawing.Size(115, 17);
            this.rdbtnAdminConfirm.TabIndex = 10;
            this.rdbtnAdminConfirm.TabStop = true;
            this.rdbtnAdminConfirm.Text = "Admin Confirmation";
            this.rdbtnAdminConfirm.UseVisualStyleBackColor = true;
            // 
            // rdbtnNone
            // 
            this.rdbtnNone.AutoSize = true;
            this.rdbtnNone.Checked = true;
            this.rdbtnNone.Location = new System.Drawing.Point(23, 69);
            this.rdbtnNone.Name = "rdbtnNone";
            this.rdbtnNone.Size = new System.Drawing.Size(51, 17);
            this.rdbtnNone.TabIndex = 10;
            this.rdbtnNone.TabStop = true;
            this.rdbtnNone.Text = "None";
            this.rdbtnNone.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbtnAdminConfirm);
            this.groupBox1.Controls.Add(this.rdbtnNone);
            this.groupBox1.Controls.Add(this.rdbtnMailAct);
            this.groupBox1.Location = new System.Drawing.Point(24, 128);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 100);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Registration Policy";
            // 
            // close
            // 
            this.close.AccessibleDescription = "Close";
            this.close.AccessibleName = "Close";
            this.close.AutoSize = true;
            this.close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.close.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.close.Location = new System.Drawing.Point(282, 3);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(19, 18);
            this.close.TabIndex = 76;
            this.close.Text = "X";
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label6.Location = new System.Drawing.Point(12, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 17);
            this.label6.TabIndex = 75;
            this.label6.Text = "Create New Forum";
            // 
            // minimize
            // 
            this.minimize.AutoSize = true;
            this.minimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.minimize.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.minimize.Location = new System.Drawing.Point(256, -3);
            this.minimize.Name = "minimize";
            this.minimize.Size = new System.Drawing.Size(20, 25);
            this.minimize.TabIndex = 74;
            this.minimize.Text = "-";
            this.minimize.Click += new System.EventHandler(this.minimize_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-29, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(343, 12);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 73;
            this.pictureBox1.TabStop = false;
            // 
            // NewForumDialog
            // 
            this.AcceptButton = this.btnCreate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(303, 284);
            this.Controls.Add(this.close);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.minimize);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtBoxAdmin);
            this.Controls.Add(this.txtBoxPassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBoxName);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NewForumDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NewForumDialog";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtBoxName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBoxPassword;
        private System.Windows.Forms.TextBox txtBoxAdmin;
        private System.Windows.Forms.RadioButton rdbtnMailAct;
        private System.Windows.Forms.RadioButton rdbtnAdminConfirm;
        private System.Windows.Forms.RadioButton rdbtnNone;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label close;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label minimize;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}