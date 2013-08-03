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

namespace ForumGenerator_Client.Dialogs
{
    public unsafe partial class SubForumDialog : Form
    {
        enum loginLevels
        {
            GUEST,
            MEMBER,
            MODERATOR,
            ADMIN,
            SUPER
        };
 

        public struct line
        {
            public TreeNode tnodeTitle;
            public TreeNode tnodeContent;
            public List<TreeNode> comments;
            public TreeView tree;
            public Label lblEdit;
            public Label lblDelete;

            public TextBox lnedComment;
            public Button btnComment;

            public ListBox lstDates;
            public ListBox lstPublishers;

            public List<string> dates;
            public List<string> publishers;

        }
       
        List<line> lines;
        List<int> linesHeight;
        Timer timer;

        MainMethods mainMethods;
        Discussion[] disList;
        int nextY = 0;
        int delta = 7;
        int loginLevel = 0;
        int hieght = 0;
        int minHeight = 45;

        public SubForumDialog(MainMethods parent)
        {
            lines = new List<line>();
            linesHeight = new List<int>();
            mainMethods = parent;
            InitializeComponent();
            timer = new Timer();
            timer.Tick += new EventHandler(TimerOnTick);
            timer.Interval = 4000;
            timer.Start();
        }

        private unsafe void quit()
        {
            mainMethods.quit();

        }

        private void mnuLogin_Click(object sender, EventArgs e)
        {
            mainMethods.loginUser();
        }

        private void mnuRegister_Click(object sender, EventArgs e)
        {
            mainMethods.register();
        }

        private void mnuSuper_Click(object sender, EventArgs e)
        {
            mainMethods.superDialog();
        }

        private void mnuAdmin_Click(object sender, EventArgs e)
        {
            mainMethods.adminDialog();
        }

        private void mnuLogput_Click(object sender, EventArgs e)
        {
            mainMethods.logout();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            mainMethods.btnGoBack();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            mainMethods.addNew();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            mainMethods.refresh();
        }

        private void btnNotifications_Click(object sender, EventArgs e)
        {
            mainMethods.notifications();
        }

        public void updateView(int loginLevel, string userName, string forumName, string subForumName)
        {
            this.loginLevel = loginLevel;
            this.btnNotifications.Visible = false;
            this.btnAddNew.Visible = false;
            this.lblAddNew.Visible = false;
            this.mnuAdmin.Visible = false;
            this.mnuLogput.Visible = false;
            this.mnuLogin.Visible = true;
            this.mnuAdmin.Visible = false;
            this.mnuSuper.Visible = false;
            this.mnuRegister.Visible = true;
            lblHello.Text = "Hello Guest!";
            lblTitle.Text = forumName + ": " + subForumName;

            if (loginLevel >= (int)loginLevels.MEMBER)
            {
                btnNotifications.Visible = true;
                btnAddNew.Visible = true;
                lblAddNew.Visible = true;
                mnuLogput.Visible = true;
                mnuLogin.Visible = false;
                mnuRegister.Visible = false;
                lblHello.Text = "Hello " +userName +"!";

                //show all edit and delete labels
            }

            if (loginLevel == (int)loginLevels.ADMIN)
            {
                this.mnuAdmin.Visible = true;
            }

            if (loginLevel == (int)loginLevels.SUPER)
            {
                this.mnuSuper.Visible = true;
                lblHello.Text = "Hello SuperUser!";
            }
        }

        public void updateDiscussionList(Discussion[] discussions)
        {
            disList = discussions;
            clearList();

            for (int i = 0; i < disList.Length; i++)
                createNewLine(disList[i]);

            updateList();
        }

        private void clearList()
        {
            this.pnlDiscussion.Controls.Clear();

            lines.Clear();
            linesHeight.Clear();
            nextY = 0;

        }

        private void createNewLine(Discussion discussion)
        {
            int index = lines.Count;
            line tmp = new line();
            hieght = minHeight;

            linesHeight.Add(hieght);


            tmp.comments = new List<TreeNode>();
            tmp.tree = new TreeView();
            tmp.dates = new List<string>();
            tmp.publishers = new List<string>();
            tmp.lblEdit = new Label();
            tmp.lblDelete = new Label();
            tmp.lnedComment = new TextBox();
            tmp.btnComment = new Button();
            tmp.lstDates = new ListBox();
            tmp.lstPublishers = new ListBox();

            initTreeView(tmp, index, discussion);
            initDeleteEditLbls(tmp ,index);
            initCommentLine(tmp, index);
            initDateList(tmp, index);
            initPublishersList(tmp, index);

     
         
            if (index % 2 == 0)
            {
                tmp.lblEdit.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
                tmp.lblDelete.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
                tmp.tree.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
                tmp.lstDates.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
                tmp.lstPublishers.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            }
            else
            {
                tmp.lblEdit.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
                tmp.lblDelete.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
                tmp.tree.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
                tmp.lstDates.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
                tmp.lstPublishers.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            }

            if (loginLevel != (int)loginLevels.GUEST)
            {
                 this.pnlDiscussion.Controls.Add(tmp.lblDelete);
                 this.pnlDiscussion.Controls.Add(tmp.lblEdit);

            }



            this.pnlDiscussion.Controls.Add(tmp.lstDates);
            this.pnlDiscussion.Controls.Add(tmp.lstPublishers);

     
            this.pnlDiscussion.Controls.Add(tmp.tree);
            lines.Add(tmp);
            nextY += tmp.tree.Size.Height;
            nextY += delta;
        }


       

        private void initTreeView(line tmp, int index, Discussion discussion)
        {
            // 
            // treeView + nodes
            // 
            Comment[] comm = mainMethods.getCommentList(discussion.discussionId);
            tmp.publishers.Add(discussion.publisher.userName);
            tmp.dates.Add(discussion.publishDate.ToString());
            tmp.dates.Add("");
            tmp.publishers.Add("");
         

            for (int i = 0; i < comm.Length; i++)
            {
                TreeNode treeNodeTmp = new TreeNode(comm[i].content);

                tmp.comments.Add(treeNodeTmp);
                tmp.publishers.Add(comm[i].publisher.userName);
                tmp.dates.Add(comm[i].publishDate.ToString());
         

            }
            tmp.tnodeContent = new TreeNode("Msg Content", tmp.comments.ToArray<TreeNode>());
            tmp.tnodeTitle = new TreeNode("Hello Forum!", new System.Windows.Forms.TreeNode[] { tmp.tnodeContent });

            tmp.tree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            tmp.tree.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            tmp.tree.ForeColor = System.Drawing.SystemColors.InfoText;
            tmp.tree.FullRowSelect = true;
            tmp.tree.Indent = 13;
            tmp.tree.ItemHeight = 20;
            tmp.tree.LineColor = System.Drawing.Color.White;
            tmp.tree.Location = new System.Drawing.Point(0, nextY);
            tmp.tree.Name = index.ToString();       //
            tmp.tnodeContent.Name = "content";
            tmp.tnodeContent.Text = discussion.content;     //
            tmp.tnodeTitle.Name = "title";


            tmp.tnodeTitle.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            tmp.tnodeTitle.Text = discussion.title;         //
            tmp.tree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] { tmp.tnodeTitle });
            tmp.tree.Size = new System.Drawing.Size(780, hieght); //need to calculate according to the number of comments + add comment line
            tmp.tree.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(treeViewBeforeCollapse);
            tmp.tree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(treeViewBeforeExpand);

        }


        private void initDeleteEditLbls(line tmp, int index)
        {
            // 
            // lbl Edit
            // 
            tmp.lblEdit.AutoSize = true;
            tmp.lblEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            tmp.lblEdit.Location = new System.Drawing.Point(711, nextY + 4);
            tmp.lblEdit.Name = index.ToString();       //
            tmp.lblEdit.Size = new System.Drawing.Size(38, 13);
            tmp.lblEdit.TabIndex = 48;
            tmp.lblEdit.Text = "Delete";
            tmp.lblEdit.Cursor = System.Windows.Forms.Cursors.Hand;

            tmp.lblEdit.Click += new System.EventHandler(this.deleteMsgClicked);
            // 
            // lbl Delete
            // 
            tmp.lblDelete.AutoSize = true;
            tmp.lblDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            tmp.lblDelete.Location = new System.Drawing.Point(742, nextY + 4);
            tmp.lblDelete.Name = index.ToString();       //
            tmp.lblDelete.Size = new System.Drawing.Size(25, 13);
            tmp.lblDelete.TabIndex = 47;
            tmp.lblDelete.Text = "Edit";
            tmp.lblDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            tmp.lblDelete.Click += new System.EventHandler(this.editMsgClicked);


 
        }

        private void initCommentLine(line tmp, int index)
        {
            int y = nextY + tmp.tree.Size.Height;

            y -= 35;
            // 
            // button - comment
            // 
            tmp.btnComment.BackColor = System.Drawing.SystemColors.HighlightText;
            tmp.btnComment.Location = new System.Drawing.Point(714, y);
            tmp.btnComment.Name = index.ToString();       //
            tmp.btnComment.Size = new System.Drawing.Size(63, 20);
            tmp.btnComment.TabIndex = 46;
            tmp.btnComment.Text = "Comment";
            tmp.btnComment.UseVisualStyleBackColor = false;
            tmp.btnComment.Click += new System.EventHandler(this.commentMsgClicked);

            // 
            // textBox - comment
            // 
            tmp.lnedComment.Location = new System.Drawing.Point(35, y);
            tmp.lnedComment.Name = index.ToString();       //
            tmp.lnedComment.Size = new System.Drawing.Size(673, 20);
            tmp.lnedComment.TabIndex = 45;
            tmp.lnedComment.MaxLength = 80;


        }


        private void initDateList(line tmp, int index)
        {

            // 
            // Dates
            // 
            tmp.lstDates.BorderStyle = System.Windows.Forms.BorderStyle.None;
            tmp.lstDates.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            tmp.lstDates.FormattingEnabled = true;
            tmp.lstDates.IntegralHeight = false;
            tmp.lstDates.DrawMode = DrawMode.OwnerDrawVariable;
            tmp.lstDates.ItemHeight = 20;
            tmp.lstDates.Items.AddRange(new object[] { tmp.dates[0]});
            tmp.lstDates.Location = new System.Drawing.Point(786, nextY);
            tmp.lstDates.Name = index.ToString();       //
            tmp.lstDates.Size = new System.Drawing.Size(120, hieght); //need to calculate height
            tmp.lstDates.TabIndex = 44;
            tmp.lstDates.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstDastes_DrawItem);
   
        }
        private void lstDastes_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox tmp = (ListBox)sender;
            int index = Convert.ToInt32(tmp.Name);

            e.DrawBackground();
            e.DrawFocusRectangle();
            e.Graphics.DrawString(
                 (string)lines[index].lstDates.Items[e.Index],
                 e.Font,
                 new SolidBrush(e.ForeColor),
                 e.Bounds);
        }




        private void initPublishersList(line tmp, int index)
        {
            // 
            // Publishers
            // 
            tmp.lstPublishers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            tmp.lstPublishers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            tmp.lstPublishers.FormattingEnabled = true;
            tmp.lstPublishers.IntegralHeight = false;
            tmp.lstPublishers.DrawMode = DrawMode.OwnerDrawVariable;
            tmp.lstPublishers.ItemHeight = 20;
            tmp.lstPublishers.Items.AddRange(new object[] { tmp.publishers[0] });
            tmp.lstPublishers.Location = new System.Drawing.Point(912, nextY);
            tmp.lstPublishers.Name = index.ToString();       //
            tmp.lstPublishers.Size = new System.Drawing.Size(130, hieght); //need to calculate height
            tmp.lstPublishers.TabIndex = 43;
            tmp.lstPublishers.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstPublishers_DrawItem);

        }

        private void lstPublishers_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox tmp = (ListBox)sender;
            int index = Convert.ToInt32(tmp.Name);

            e.DrawBackground();
            e.DrawFocusRectangle();
            e.Graphics.DrawString(
                 (string)lines[index].lstPublishers.Items[e.Index],
                 e.Font,
                 new SolidBrush(e.ForeColor),
                 e.Bounds);
        }
        







        private void treeViewBeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            System.Windows.Forms.TreeView tmp = (System.Windows.Forms.TreeView)sender;
            if ((tmp.Nodes[0].IsExpanded && tmp.Nodes[0].Nodes[0] == e.Node) || (tmp.Nodes[0] == e.Node && tmp.Nodes[0].Nodes[0].IsExpanded))
            {
            
                int index = Convert.ToInt32(tmp.Name);
                int numOfcom = lines[index].comments.Count;
       
                linesHeight[index] = minHeight + numOfcom * 20;
                if (loginLevel != (int)loginLevels.GUEST)
                    linesHeight[index] += 21;

                updateList();
            }
            else if ((tmp.Nodes[0].Nodes[0].Nodes == null || tmp.Nodes[0].Nodes[0].Nodes.Count == 0) && loginLevel != (int)loginLevels.GUEST)
            {
                int index = Convert.ToInt32(tmp.Name);

                linesHeight[index] = 66;

                updateList();

            }

        }

        private void treeViewBeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            System.Windows.Forms.TreeView tmp = (System.Windows.Forms.TreeView)sender;
         
            int index = Convert.ToInt32(tmp.Name);

            linesHeight[index] = minHeight;

            updateList();
    
        }




        private void updateList()
        {

            this.pnlDiscussion.Controls.Clear();
            nextY = 0;

            for (int i = 0; i < lines.Count; i++)
                updateLineView(lines[i], i);

        }

        private void updateLineView(line tmp, int index)
        {
           
            hieght = linesHeight[index];
            int y = nextY + hieght;

            y -= 24;

            tmp.tree.Location = new System.Drawing.Point(0, nextY);

            tmp.tree.Size = new System.Drawing.Size(780, hieght); //need to calculate according to the number of comments + add comment line
            tmp.lblDelete.Location = new System.Drawing.Point(711, nextY + 4);
            tmp.lblEdit.Location = new System.Drawing.Point(742, nextY + 4);
            tmp.btnComment.Location = new System.Drawing.Point(714, y);
            tmp.lnedComment.Location = new System.Drawing.Point(35, y);
            tmp.lstDates.Location = new System.Drawing.Point(786, nextY);
            tmp.lstDates.Size = new System.Drawing.Size(120, hieght);
            tmp.lstPublishers.Location = new System.Drawing.Point(912, nextY);
            tmp.lstPublishers.Size = new System.Drawing.Size(130, hieght);

            if (loginLevel != (int)loginLevels.GUEST)
            {
                this.pnlDiscussion.Controls.Add(tmp.lblDelete);
                this.pnlDiscussion.Controls.Add(tmp.lblEdit);

            }

            if (hieght > minHeight && loginLevel != (int)loginLevels.GUEST)
            {
                this.pnlDiscussion.Controls.Add(tmp.btnComment);
                this.pnlDiscussion.Controls.Add(tmp.lnedComment);
            }

            tmp.lstDates.Items.Clear(); ;
            tmp.lstPublishers.Items.Clear();

            if (hieght > minHeight)
            {
                tmp.lstDates.Items.AddRange( tmp.dates.ToArray<Object>() );
                tmp.lstPublishers.Items.AddRange(tmp.publishers.ToArray<Object>());
            }
            else
            {
                tmp.lstDates.Items.AddRange(new object[] { tmp.dates[0] });
                tmp.lstPublishers.Items.AddRange(new object[] { tmp.publishers[0] });
           

            }

            this.pnlDiscussion.Controls.Add(tmp.tree);

            this.pnlDiscussion.Controls.Add(tmp.lstDates);
            this.pnlDiscussion.Controls.Add(tmp.lstPublishers);

            nextY += tmp.tree.Size.Height;
            nextY += delta;

        }

        private void deleteMsgClicked(object sender, EventArgs e)
        {
            System.Windows.Forms.Label tmp = ( System.Windows.Forms.Label) sender;
            int index = Convert.ToInt32(tmp.Name);
            int id = disList[index].discussionId;

            mainMethods.deleteMessage(id);

        }


        private void editMsgClicked(object sender, EventArgs e)
        {
            System.Windows.Forms.Label tmp = (System.Windows.Forms.Label)sender;
            int index = Convert.ToInt32(tmp.Name);
            int id = disList[index].discussionId;
            mainMethods.editMessage(id);
        }


        private void commentMsgClicked(object sender, EventArgs e)
        {
            System.Windows.Forms.Button tmp = (System.Windows.Forms.Button)sender;
            int index = Convert.ToInt32(tmp.Name);
            int id = disList[index].discussionId;
            string comment = lines[index].lnedComment.Text;

            mainMethods.addAComment(id, comment);
        }


        public void setStatusMsg(string msg)
        {
            toolStripStatusLabel2.Text = msg;
            timer.Start();
        }

        private void TimerOnTick(object obj, EventArgs ea)
        {
            toolStripStatusLabel2.Text = "";
        }


        protected override void OnLoad(EventArgs e)
        {
            if (this.FormBorderStyle == System.Windows.Forms.FormBorderStyle.None)
            {
                this.MouseDown += new MouseEventHandler(AppFormBase_MouseDown);
                this.MouseMove += new MouseEventHandler(AppFormBase_MouseMove);
                this.MouseUp += new MouseEventHandler(AppFormBase_MouseUp);
            }

            base.OnLoad(e);
        }

        void AppFormBase_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            downPoint = new Point(e.X, e.Y);
        }

        void AppFormBase_MouseMove(object sender, MouseEventArgs e)
        {
            if (downPoint == Point.Empty)
            {
                return;
            }
            Point location = new Point(
                this.Left + e.X - downPoint.X,
                this.Top + e.Y - downPoint.Y);
            this.Location = location;
        }

        void AppFormBase_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            downPoint = Point.Empty;
        }


        private const int CS_DROPSHADOW = 0x00020000;
        protected override CreateParams CreateParams
        {
            get
            {
                // add the drop shadow flag for automatically drawing
                // a drop shadow around the form
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        public Point downPoint = Point.Empty;


        private void close_Click(object sender, EventArgs e)
        {
            mainMethods.quit();
            Close();
        }

        private void minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
