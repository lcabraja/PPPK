namespace Zadatak0102
{
    partial class ArbitraryQueryForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TbQuery = new System.Windows.Forms.TextBox();
            this.TpMain = new System.Windows.Forms.TableLayoutPanel();
            this.TcQueryResponse = new System.Windows.Forms.TabControl();
            this.TpResults = new System.Windows.Forms.TabPage();
            this.DgResults = new System.Windows.Forms.DataGridView();
            this.TpMessages = new System.Windows.Forms.TabPage();
            this.TbMessage = new System.Windows.Forms.TextBox();
            this.BtnExecuteQuery = new System.Windows.Forms.Button();
            this.TpMain.SuspendLayout();
            this.TcQueryResponse.SuspendLayout();
            this.TpResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgResults)).BeginInit();
            this.TpMessages.SuspendLayout();
            this.SuspendLayout();
            // 
            // TbQuery
            // 
            this.TbQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TbQuery.Location = new System.Drawing.Point(3, 33);
            this.TbQuery.Multiline = true;
            this.TbQuery.Name = "TbQuery";
            this.TbQuery.Size = new System.Drawing.Size(794, 204);
            this.TbQuery.TabIndex = 0;
            // 
            // TpMain
            // 
            this.TpMain.ColumnCount = 1;
            this.TpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TpMain.Controls.Add(this.TcQueryResponse, 0, 2);
            this.TpMain.Controls.Add(this.TbQuery, 0, 1);
            this.TpMain.Controls.Add(this.BtnExecuteQuery, 0, 0);
            this.TpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TpMain.Location = new System.Drawing.Point(0, 0);
            this.TpMain.Name = "TpMain";
            this.TpMain.RowCount = 3;
            this.TpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TpMain.Size = new System.Drawing.Size(800, 450);
            this.TpMain.TabIndex = 1;
            // 
            // TcQueryResponse
            // 
            this.TcQueryResponse.Controls.Add(this.TpResults);
            this.TcQueryResponse.Controls.Add(this.TpMessages);
            this.TcQueryResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TcQueryResponse.Location = new System.Drawing.Point(3, 243);
            this.TcQueryResponse.Name = "TcQueryResponse";
            this.TcQueryResponse.SelectedIndex = 0;
            this.TcQueryResponse.Size = new System.Drawing.Size(794, 204);
            this.TcQueryResponse.TabIndex = 1;
            // 
            // TpResults
            // 
            this.TpResults.Controls.Add(this.DgResults);
            this.TpResults.Location = new System.Drawing.Point(4, 22);
            this.TpResults.Name = "TpResults";
            this.TpResults.Padding = new System.Windows.Forms.Padding(3);
            this.TpResults.Size = new System.Drawing.Size(786, 178);
            this.TpResults.TabIndex = 0;
            this.TpResults.Text = "Results";
            this.TpResults.UseVisualStyleBackColor = true;
            // 
            // DgResults
            // 
            this.DgResults.AllowUserToAddRows = false;
            this.DgResults.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Silver;
            this.DgResults.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.DgResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgResults.Location = new System.Drawing.Point(3, 3);
            this.DgResults.Name = "DgResults";
            this.DgResults.ReadOnly = true;
            this.DgResults.Size = new System.Drawing.Size(780, 172);
            this.DgResults.TabIndex = 0;
            // 
            // TpMessages
            // 
            this.TpMessages.Controls.Add(this.TbMessage);
            this.TpMessages.Location = new System.Drawing.Point(4, 22);
            this.TpMessages.Name = "TpMessages";
            this.TpMessages.Padding = new System.Windows.Forms.Padding(3);
            this.TpMessages.Size = new System.Drawing.Size(786, 178);
            this.TpMessages.TabIndex = 1;
            this.TpMessages.Text = "Messages";
            this.TpMessages.UseVisualStyleBackColor = true;
            // 
            // TbMessage
            // 
            this.TbMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TbMessage.Location = new System.Drawing.Point(3, 3);
            this.TbMessage.Multiline = true;
            this.TbMessage.Name = "TbMessage";
            this.TbMessage.ReadOnly = true;
            this.TbMessage.Size = new System.Drawing.Size(780, 172);
            this.TbMessage.TabIndex = 0;
            // 
            // BtnExecuteQuery
            // 
            this.BtnExecuteQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnExecuteQuery.Location = new System.Drawing.Point(3, 3);
            this.BtnExecuteQuery.Name = "BtnExecuteQuery";
            this.BtnExecuteQuery.Size = new System.Drawing.Size(794, 24);
            this.BtnExecuteQuery.TabIndex = 2;
            this.BtnExecuteQuery.Text = "Execute Query";
            this.BtnExecuteQuery.UseVisualStyleBackColor = true;
            this.BtnExecuteQuery.Click += new System.EventHandler(this.BtnExecuteQuery_Click);
            // 
            // ArbitraryQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TpMain);
            this.Name = "ArbitraryQueryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Arbitrary Query Form";
            this.TpMain.ResumeLayout(false);
            this.TpMain.PerformLayout();
            this.TcQueryResponse.ResumeLayout(false);
            this.TpResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgResults)).EndInit();
            this.TpMessages.ResumeLayout(false);
            this.TpMessages.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox TbQuery;
        private System.Windows.Forms.TableLayoutPanel TpMain;
        private System.Windows.Forms.TabControl TcQueryResponse;
        private System.Windows.Forms.TabPage TpResults;
        private System.Windows.Forms.TabPage TpMessages;
        private System.Windows.Forms.DataGridView DgResults;
        private System.Windows.Forms.Button BtnExecuteQuery;
        private System.Windows.Forms.TextBox TbMessage;
    }
}