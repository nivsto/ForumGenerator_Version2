namespace ConsoleApplication1.AccTests
{
    partial class testGui
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
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRunFg = new System.Windows.Forms.Button();
            this.btnRunScal = new System.Windows.Forms.Button();
            this.btnRunConnect = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOpenLog = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(197, 187);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(83, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(90, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Forum generator tests";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(90, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Scalability Test";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(90, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Connection Overload test";
            // 
            // btnRunFg
            // 
            this.btnRunFg.Location = new System.Drawing.Point(272, 60);
            this.btnRunFg.Name = "btnRunFg";
            this.btnRunFg.Size = new System.Drawing.Size(50, 23);
            this.btnRunFg.TabIndex = 3;
            this.btnRunFg.Text = "Run";
            this.btnRunFg.UseVisualStyleBackColor = true;
            this.btnRunFg.Click += new System.EventHandler(this.btnRunFg_Click);
            // 
            // btnRunScal
            // 
            this.btnRunScal.Location = new System.Drawing.Point(272, 102);
            this.btnRunScal.Name = "btnRunScal";
            this.btnRunScal.Size = new System.Drawing.Size(50, 23);
            this.btnRunScal.TabIndex = 3;
            this.btnRunScal.Text = "Run";
            this.btnRunScal.UseVisualStyleBackColor = true;
            this.btnRunScal.Click += new System.EventHandler(this.btnRunScal_Click);
            // 
            // btnRunConnect
            // 
            this.btnRunConnect.Location = new System.Drawing.Point(272, 145);
            this.btnRunConnect.Name = "btnRunConnect";
            this.btnRunConnect.Size = new System.Drawing.Size(50, 23);
            this.btnRunConnect.TabIndex = 3;
            this.btnRunConnect.Text = "Run";
            this.btnRunConnect.UseVisualStyleBackColor = true;
            this.btnRunConnect.Click += new System.EventHandler(this.btnRunConnect_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox1.Location = new System.Drawing.Point(26, 60);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(44, 22);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox2.Location = new System.Drawing.Point(26, 103);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(44, 22);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox3.Location = new System.Drawing.Point(26, 146);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(44, 22);
            this.pictureBox3.TabIndex = 6;
            this.pictureBox3.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.Location = new System.Drawing.Point(59, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(221, 25);
            this.label4.TabIndex = 7;
            this.label4.Text = "Welcome to Test Gui!";
            // 
            // btnOpenLog
            // 
            this.btnOpenLog.Location = new System.Drawing.Point(81, 187);
            this.btnOpenLog.Name = "btnOpenLog";
            this.btnOpenLog.Size = new System.Drawing.Size(87, 23);
            this.btnOpenLog.TabIndex = 1;
            this.btnOpenLog.Text = "Open Log File";
            this.btnOpenLog.UseVisualStyleBackColor = true;
            this.btnOpenLog.Click += new System.EventHandler(this.btnOpenLog_Click);
            // 
            // testGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 222);
            this.Controls.Add(this.btnOpenLog);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnRunConnect);
            this.Controls.Add(this.btnRunScal);
            this.Controls.Add(this.btnRunFg);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Name = "testGui";
            this.Text = "testGui";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRunFg;
        private System.Windows.Forms.Button btnRunScal;
        private System.Windows.Forms.Button btnRunConnect;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOpenLog;
    }
}