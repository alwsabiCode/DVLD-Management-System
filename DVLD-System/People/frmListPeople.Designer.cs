namespace DVLD_System
{
    partial class frmListPeople
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DGVPeople = new System.Windows.Forms.DataGridView();
            this.CMSPeople = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TSMShowDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.TSMAddNewPerson = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.TSMSendEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMOhoneCall = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFilter = new System.Windows.Forms.ComboBox();
            this.txtfilter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRecordCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.DGVPeople)).BeginInit();
            this.CMSPeople.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVPeople
            // 
            this.DGVPeople.AllowUserToAddRows = false;
            this.DGVPeople.AllowUserToDeleteRows = false;
            this.DGVPeople.AllowUserToResizeRows = false;
            this.DGVPeople.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVPeople.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGVPeople.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVPeople.ContextMenuStrip = this.CMSPeople;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGVPeople.DefaultCellStyle = dataGridViewCellStyle2;
            this.DGVPeople.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DGVPeople.GridColor = System.Drawing.Color.Teal;
            this.DGVPeople.Location = new System.Drawing.Point(5, 194);
            this.DGVPeople.MultiSelect = false;
            this.DGVPeople.Name = "DGVPeople";
            this.DGVPeople.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVPeople.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DGVPeople.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.DarkTurquoise;
            this.DGVPeople.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.DGVPeople.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVPeople.Size = new System.Drawing.Size(1172, 217);
            this.DGVPeople.TabIndex = 1;
            this.DGVPeople.DoubleClick += new System.EventHandler(this.DGVPeople_DoubleClick);
            // 
            // CMSPeople
            // 
            this.CMSPeople.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMSPeople.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMShowDetail,
            this.toolStripMenuItem1,
            this.TSMAddNewPerson,
            this.TSMEdit,
            this.TSMDelete,
            this.toolStripMenuItem2,
            this.TSMSendEmail,
            this.TSMOhoneCall});
            this.CMSPeople.Name = "CMSPeople";
            this.CMSPeople.Size = new System.Drawing.Size(193, 292);
            // 
            // TSMShowDetail
            // 
            this.TSMShowDetail.Image = global::DVLD_System.Properties.Resources.PersonDetails_32;
            this.TSMShowDetail.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSMShowDetail.Name = "TSMShowDetail";
            this.TSMShowDetail.Size = new System.Drawing.Size(192, 46);
            this.TSMShowDetail.Text = "Show Details";
            this.TSMShowDetail.Click += new System.EventHandler(this.TSMShowDetail_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.AutoSize = false;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(189, 6);
            // 
            // TSMAddNewPerson
            // 
            this.TSMAddNewPerson.Image = global::DVLD_System.Properties.Resources.Add_Person_40;
            this.TSMAddNewPerson.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSMAddNewPerson.Name = "TSMAddNewPerson";
            this.TSMAddNewPerson.Size = new System.Drawing.Size(192, 46);
            this.TSMAddNewPerson.Text = "Add New Person";
            this.TSMAddNewPerson.Click += new System.EventHandler(this.TSMAddNewPerson_Click);
            // 
            // TSMEdit
            // 
            this.TSMEdit.Image = global::DVLD_System.Properties.Resources.edit_32;
            this.TSMEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSMEdit.Name = "TSMEdit";
            this.TSMEdit.Size = new System.Drawing.Size(192, 46);
            this.TSMEdit.Text = "Edit";
            this.TSMEdit.Click += new System.EventHandler(this.TSMEdit_Click);
            // 
            // TSMDelete
            // 
            this.TSMDelete.Image = global::DVLD_System.Properties.Resources.Delete_32;
            this.TSMDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSMDelete.Name = "TSMDelete";
            this.TSMDelete.Size = new System.Drawing.Size(192, 46);
            this.TSMDelete.Text = "Delete";
            this.TSMDelete.Click += new System.EventHandler(this.TSMDelete_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(189, 6);
            // 
            // TSMSendEmail
            // 
            this.TSMSendEmail.Image = global::DVLD_System.Properties.Resources.send_email_32;
            this.TSMSendEmail.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSMSendEmail.Name = "TSMSendEmail";
            this.TSMSendEmail.Size = new System.Drawing.Size(192, 46);
            this.TSMSendEmail.Text = "Send Email";
            this.TSMSendEmail.Click += new System.EventHandler(this.TSMSendEmail_Click);
            // 
            // TSMOhoneCall
            // 
            this.TSMOhoneCall.Image = global::DVLD_System.Properties.Resources.call_32;
            this.TSMOhoneCall.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSMOhoneCall.Name = "TSMOhoneCall";
            this.TSMOhoneCall.Size = new System.Drawing.Size(192, 46);
            this.TSMOhoneCall.Text = "Phon Call";
            this.TSMOhoneCall.Click += new System.EventHandler(this.TSMOhoneCall_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Filter By :";
            // 
            // cbFilter
            // 
            this.cbFilter.BackColor = System.Drawing.Color.White;
            this.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilter.FormattingEnabled = true;
            this.cbFilter.Items.AddRange(new object[] {
            "None",
            "Person ID",
            "National No.",
            "First Name",
            "Second Name",
            "Third Name",
            "Last Name",
            "Nationality",
            "Gendor",
            "Phone",
            "Email"});
            this.cbFilter.Location = new System.Drawing.Point(88, 164);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(151, 24);
            this.cbFilter.TabIndex = 3;
            this.cbFilter.SelectedIndexChanged += new System.EventHandler(this.cbFilter_SelectedIndexChanged);
            // 
            // txtfilter
            // 
            this.txtfilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtfilter.Location = new System.Drawing.Point(245, 165);
            this.txtfilter.Name = "txtfilter";
            this.txtfilter.Size = new System.Drawing.Size(151, 22);
            this.txtfilter.TabIndex = 4;
            this.txtfilter.TextChanged += new System.EventHandler(this.txtfilter_TextChanged);
            this.txtfilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtfilter_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 429);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "# Records :";
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.AutoSize = true;
            this.lblRecordCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCount.Location = new System.Drawing.Point(109, 429);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(54, 20);
            this.lblRecordCount.TabIndex = 8;
            this.lblRecordCount.Text = "? ? ? ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(536, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(215, 31);
            this.label3.TabIndex = 9;
            this.label3.Text = "Manage People";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD_System.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1052, 417);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(125, 44);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.White;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Image = global::DVLD_System.Properties.Resources.Add_Person_40;
            this.btnAdd.Location = new System.Drawing.Point(1079, 143);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(51, 44);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.TabStop = false;
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = global::DVLD_System.Properties.Resources.People_400;
            this.pictureBox1.Location = new System.Drawing.Point(511, -3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Padding = new System.Windows.Forms.Padding(50, 0, 50, 0);
            this.pictureBox1.Size = new System.Drawing.Size(263, 119);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // frmListPeople
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1189, 467);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblRecordCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtfilter);
            this.Controls.Add(this.cbFilter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DGVPeople);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmListPeople";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage People";
            this.Load += new System.EventHandler(this.frmPeople_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVPeople)).EndInit();
            this.CMSPeople.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView DGVPeople;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFilter;
        private System.Windows.Forms.TextBox txtfilter;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRecordCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip CMSPeople;
        private System.Windows.Forms.ToolStripMenuItem TSMShowDetail;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem TSMAddNewPerson;
        private System.Windows.Forms.ToolStripMenuItem TSMEdit;
        private System.Windows.Forms.ToolStripMenuItem TSMDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem TSMSendEmail;
        private System.Windows.Forms.ToolStripMenuItem TSMOhoneCall;
    }
}