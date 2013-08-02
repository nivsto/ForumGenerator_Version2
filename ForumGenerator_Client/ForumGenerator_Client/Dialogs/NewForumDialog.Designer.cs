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
            this.label1 = new System.Windows.Forms.Label();
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
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(21, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Create New Forum:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Forum Name:";
            // 
            // txtBoxName
            // 
            this.txtBoxName.Location = new System.Drawing.Point(132, 59);
            this.txtBoxName.Name = "txtBoxName";
            this.txtBoxName.Size = new System.Drawing.Size(144, 20);
            this.txtBoxName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Forum Admin:";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(53, 256);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 4;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(169, 256);
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
            this.label4.Location = new System.Drawing.Point(21, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Admin Password:";
            // 
            // txtBoxPassword
            // 
            this.txtBoxPassword.Location = new System.Drawing.Point(132, 111);
            this.txtBoxPassword.MaxLength = 10;
            this.txtBoxPassword.Name = "txtBoxPassword";
            this.txtBoxPassword.PasswordChar = '*';
            this.txtBoxPassword.Size = new System.Drawing.Size(144, 20);
            this.txtBoxPassword.TabIndex = 3;
            // 
            // txtBoxAdmin
            // 
            this.txtBoxAdmin.Location = new System.Drawing.Point(132, 85);
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
            this.groupBox1.Location = new System.Drawing.Point(24, 142);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 100);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Registration Policy";
            // 
            // NewForumDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 293);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtBoxAdmin);
            this.Controls.Add(this.txtBoxPassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBoxName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "NewForumDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NewForumDialog";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
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
    }
}