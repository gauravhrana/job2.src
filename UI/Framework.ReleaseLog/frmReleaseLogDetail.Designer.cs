namespace Framework.UI.ReleaseLog.Desktop
{
    partial class frmReleaseLogDetail
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
            this.SuspendLayout();
            // 
            // frmReleaseLogDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 504);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReleaseLogDetail";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Release Log Details";
            this.Load += new System.EventHandler(this.frmReleaseLogDetail_Load);
            this.ResumeLayout(false);

        }

        #endregion

    }
}