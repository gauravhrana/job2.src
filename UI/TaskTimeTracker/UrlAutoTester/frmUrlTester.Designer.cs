namespace UrlAutoTester
{
    partial class frmUrlTester
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
            this.btnTest1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.treeViewEntity = new System.Windows.Forms.TreeView();
            this.listBoxAction = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnTest1
            // 
            this.btnTest1.Location = new System.Drawing.Point(411, 158);
            this.btnTest1.Name = "btnTest1";
            this.btnTest1.Size = new System.Drawing.Size(75, 23);
            this.btnTest1.TabIndex = 1;
            this.btnTest1.Tag = "";
            this.btnTest1.Text = "Test";
            this.btnTest1.UseVisualStyleBackColor = true;
            this.btnTest1.Click += new System.EventHandler(this.btn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(354, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Action :";
            // 
            // treeViewEntity
            // 
            this.treeViewEntity.CheckBoxes = true;
            this.treeViewEntity.Location = new System.Drawing.Point(12, 12);
            this.treeViewEntity.Name = "treeViewEntity";
            this.treeViewEntity.ShowNodeToolTips = true;
            this.treeViewEntity.Size = new System.Drawing.Size(325, 330);
            this.treeViewEntity.TabIndex = 6;
            this.treeViewEntity.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewEntity_AfterCheck);
            // 
            // listBoxAction
            // 
            this.listBoxAction.FormattingEnabled = true;
            this.listBoxAction.Items.AddRange(new object[] {
            "Default",
            "Insert",
            "Details",
            "Update",
            "Delete"});
            this.listBoxAction.Location = new System.Drawing.Point(411, 56);
            this.listBoxAction.Name = "listBoxAction";
            this.listBoxAction.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxAction.Size = new System.Drawing.Size(120, 82);
            this.listBoxAction.TabIndex = 7;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(411, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(343, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Base URL :";
            // 
            // frmUrlTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 354);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listBoxAction);
            this.Controls.Add(this.treeViewEntity);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnTest1);
            this.MaximizeBox = false;
            this.Name = "frmUrlTester";
            this.Text = "Auto Url Tester";
            this.Load += new System.EventHandler(this.frmUrlTester_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTest1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView treeViewEntity;
        private System.Windows.Forms.ListBox listBoxAction;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}

