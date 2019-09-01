namespace Consolespaceships
{
    partial class ClientWindowForm
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
            this.btnSend = new System.Windows.Forms.Button();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.listMsgHistory = new System.Windows.Forms.ListView();
            this.colTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSend.Location = new System.Drawing.Point(253, 439);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 20);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // textBoxInput
            // 
            this.textBoxInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxInput.Location = new System.Drawing.Point(12, 439);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(235, 20);
            this.textBoxInput.TabIndex = 1;
            // 
            // listMsgHistory
            // 
            this.listMsgHistory.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listMsgHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listMsgHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTime,
            this.colMessage});
            this.listMsgHistory.GridLines = true;
            this.listMsgHistory.HoverSelection = true;
            this.listMsgHistory.Location = new System.Drawing.Point(12, 12);
            this.listMsgHistory.Name = "listMsgHistory";
            this.listMsgHistory.Scrollable = false;
            this.listMsgHistory.Size = new System.Drawing.Size(776, 421);
            this.listMsgHistory.TabIndex = 2;
            this.listMsgHistory.UseCompatibleStateImageBehavior = false;
            this.listMsgHistory.View = System.Windows.Forms.View.Details;
            // 
            // colTime
            // 
            this.colTime.Text = "Time";
            this.colTime.Width = 150;
            // 
            // colMessage
            // 
            this.colMessage.Text = "Message";
            this.colMessage.Width = 1000;
            // 
            // ClientWindowForm
            // 
            this.AcceptButton = this.btnSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 471);
            this.Controls.Add(this.listMsgHistory);
            this.Controls.Add(this.textBoxInput);
            this.Controls.Add(this.btnSend);
            this.Name = "ClientWindowForm";
            this.Text = "Terminal Space";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.ListView listMsgHistory;
        private System.Windows.Forms.ColumnHeader colTime;
        private System.Windows.Forms.ColumnHeader colMessage;
    }
}