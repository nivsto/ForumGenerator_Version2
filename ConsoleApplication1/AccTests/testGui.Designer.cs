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
            this.label4 = new System.Windows.Forms.Label();
            this.btnOpenLog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(223, 230);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(111, 28);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(80, 74);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Forum generator tests";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(80, 127);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "Scalability Test";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(80, 180);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(222, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "Connection Overload test";
            // 
            // btnRunFg
            // 
            this.btnRunFg.Location = new System.Drawing.Point(363, 74);
            this.btnRunFg.Margin = new System.Windows.Forms.Padding(4);
            this.btnRunFg.Name = "btnRunFg";
            this.btnRunFg.Size = new System.Drawing.Size(67, 28);
            this.btnRunFg.TabIndex = 3;
            this.btnRunFg.Text = "Run";
            this.btnRunFg.UseVisualStyleBackColor = true;
            this.btnRunFg.Click += new System.EventHandler(this.btnRunFg_Click);
            // 
            // btnRunScal
            // 
            this.btnRunScal.Location = new System.Drawing.Point(363, 126);
            this.btnRunScal.Margin = new System.Windows.Forms.Padding(4);
            this.btnRunScal.Name = "btnRunScal";
            this.btnRunScal.Size = new System.Drawing.Size(67, 28);
            this.btnRunScal.TabIndex = 3;
            this.btnRunScal.Text = "Run";
            this.btnRunScal.UseVisualStyleBackColor = true;
            this.btnRunScal.Click += new System.EventHandler(this.btnRunScal_Click);
            // 
            // btnRunConnect
            // 
            this.btnRunConnect.Location = new System.Drawing.Point(363, 178);
            this.btnRunConnect.Margin = new System.Windows.Forms.Padding(4);
            this.btnRunConnect.Name = "btnRunConnect";
            this.btnRunConnect.Size = new System.Drawing.Size(67, 28);
            this.btnRunConnect.TabIndex = 3;
            this.btnRunConnect.Text = "Run";
            this.btnRunConnect.UseVisualStyleBackColor = true;
            this.btnRunConnect.Click += new System.EventHandler(this.btnRunConnect_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.Location = new System.Drawing.Point(79, 11);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(277, 29);
            this.label4.TabIndex = 7;
            this.label4.Text = "Welcome to Test Gui!";
            // 
            // btnOpenLog
            // 
            this.btnOpenLog.Location = new System.Drawing.Point(68, 230);
            this.btnOpenLog.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenLog.Name = "btnOpenLog";
            this.btnOpenLog.Size = new System.Drawing.Size(116, 28);
            this.btnOpenLog.TabIndex = 1;
            this.btnOpenLog.Text = "Open Log File";
            this.btnOpenLog.UseVisualStyleBackColor = true;
            this.btnOpenLog.Click += new System.EventHandler(this.btnOpenLog_Click);
            // 
            // testGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 273);
            this.Controls.Add(this.btnOpenLog);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnRunConnect);
            this.Controls.Add(this.btnRunScal);
            this.Controls.Add(this.btnRunFg);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "testGui";
            this.Text = "testGui";
            this.Load += new System.EventHandler(this.testGui_Load);
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOpenLog;
    }
}