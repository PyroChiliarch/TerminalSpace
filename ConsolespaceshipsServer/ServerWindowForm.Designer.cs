namespace ConsolespaceshipsServer
{
    partial class ServerWindowForm
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
            this.lstClients = new System.Windows.Forms.ListView();
            this.columnHeaderEndPoint = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLastMsg = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLastMsgTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lstClients
            // 
            this.lstClients.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderEndPoint,
            this.columnHeaderID,
            this.columnHeaderLastMsg,
            this.columnHeaderLastMsgTime});
            this.lstClients.Location = new System.Drawing.Point(12, 12);
            this.lstClients.Name = "lstClients";
            this.lstClients.Size = new System.Drawing.Size(737, 282);
            this.lstClients.TabIndex = 0;
            this.lstClients.UseCompatibleStateImageBehavior = false;
            this.lstClients.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderEndPoint
            // 
            this.columnHeaderEndPoint.Text = "End Point";
            this.columnHeaderEndPoint.Width = 120;
            // 
            // columnHeaderID
            // 
            this.columnHeaderID.Text = "ID";
            this.columnHeaderID.Width = 189;
            // 
            // columnHeaderLastMsg
            // 
            this.columnHeaderLastMsg.Text = "Last Message";
            this.columnHeaderLastMsg.Width = 180;
            // 
            // columnHeaderLastMsgTime
            // 
            this.columnHeaderLastMsgTime.Text = "Last Msg Time";
            this.columnHeaderLastMsgTime.Width = 212;
            // 
            // ServerWindowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 306);
            this.Controls.Add(this.lstClients);
            this.Name = "ServerWindowForm";
            this.Text = "ServerWindowForm";
            this.Load += new System.EventHandler(this.ServerWindowForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstClients;
        private System.Windows.Forms.ColumnHeader columnHeaderEndPoint;
        private System.Windows.Forms.ColumnHeader columnHeaderID;
        private System.Windows.Forms.ColumnHeader columnHeaderLastMsg;
        private System.Windows.Forms.ColumnHeader columnHeaderLastMsgTime;
    }
}