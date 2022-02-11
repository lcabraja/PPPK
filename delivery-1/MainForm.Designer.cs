
namespace Zadatak
{
    partial class MainForm
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
            this.CbDatabases = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LbTables = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LbViews = new System.Windows.Forms.ListBox();
            this.LbTableColums = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.LbViewColumns = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.LbProcedures = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.TbProcedure = new System.Windows.Forms.TextBox();
            this.LbProcedureParameters = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.BtnXMLTable = new System.Windows.Forms.Button();
            this.BtnSelectTable = new System.Windows.Forms.Button();
            this.BtnSelectView = new System.Windows.Forms.Button();
            this.BtnXMLView = new System.Windows.Forms.Button();
            this.BtnQuery = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Databases:";
            // 
            // CbDatabases
            // 
            this.CbDatabases.FormattingEnabled = true;
            this.CbDatabases.Location = new System.Drawing.Point(74, 31);
            this.CbDatabases.Name = "CbDatabases";
            this.CbDatabases.Size = new System.Drawing.Size(142, 21);
            this.CbDatabases.TabIndex = 1;
            this.CbDatabases.SelectedIndexChanged += new System.EventHandler(this.CbDatabases_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 79);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tables:";
            // 
            // LbTables
            // 
            this.LbTables.FormattingEnabled = true;
            this.LbTables.Location = new System.Drawing.Point(74, 79);
            this.LbTables.Margin = new System.Windows.Forms.Padding(2);
            this.LbTables.Name = "LbTables";
            this.LbTables.Size = new System.Drawing.Size(239, 251);
            this.LbTables.TabIndex = 3;
            this.LbTables.SelectedIndexChanged += new System.EventHandler(this.LbTables_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(615, 79);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Views:";
            // 
            // LbViews
            // 
            this.LbViews.FormattingEnabled = true;
            this.LbViews.Location = new System.Drawing.Point(656, 79);
            this.LbViews.Margin = new System.Windows.Forms.Padding(2);
            this.LbViews.Name = "LbViews";
            this.LbViews.Size = new System.Drawing.Size(185, 251);
            this.LbViews.TabIndex = 5;
            this.LbViews.SelectedIndexChanged += new System.EventHandler(this.LbViews_SelectedIndexChanged);
            // 
            // LbTableColums
            // 
            this.LbTableColums.FormattingEnabled = true;
            this.LbTableColums.Location = new System.Drawing.Point(445, 79);
            this.LbTableColums.Margin = new System.Windows.Forms.Padding(2);
            this.LbTableColums.Name = "LbTableColums";
            this.LbTableColums.Size = new System.Drawing.Size(142, 251);
            this.LbTableColums.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(361, 79);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Table Columns:";
            // 
            // LbViewColumns
            // 
            this.LbViewColumns.FormattingEnabled = true;
            this.LbViewColumns.Location = new System.Drawing.Point(980, 79);
            this.LbViewColumns.Margin = new System.Windows.Forms.Padding(2);
            this.LbViewColumns.Name = "LbViewColumns";
            this.LbViewColumns.Size = new System.Drawing.Size(142, 251);
            this.LbViewColumns.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(902, 79);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "View Columns:";
            // 
            // LbProcedures
            // 
            this.LbProcedures.FormattingEnabled = true;
            this.LbProcedures.Location = new System.Drawing.Point(74, 377);
            this.LbProcedures.Margin = new System.Windows.Forms.Padding(2);
            this.LbProcedures.Name = "LbProcedures";
            this.LbProcedures.Size = new System.Drawing.Size(239, 251);
            this.LbProcedures.TabIndex = 11;
            this.LbProcedures.SelectedIndexChanged += new System.EventHandler(this.LbProcedures_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 377);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Procedures:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(336, 377);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Procedure definition:";
            // 
            // TbProcedure
            // 
            this.TbProcedure.Location = new System.Drawing.Point(445, 377);
            this.TbProcedure.Margin = new System.Windows.Forms.Padding(2);
            this.TbProcedure.Multiline = true;
            this.TbProcedure.Name = "TbProcedure";
            this.TbProcedure.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TbProcedure.Size = new System.Drawing.Size(396, 251);
            this.TbProcedure.TabIndex = 13;
            // 
            // LbProcedureParameters
            // 
            this.LbProcedureParameters.FormattingEnabled = true;
            this.LbProcedureParameters.Location = new System.Drawing.Point(980, 377);
            this.LbProcedureParameters.Margin = new System.Windows.Forms.Padding(2);
            this.LbProcedureParameters.Name = "LbProcedureParameters";
            this.LbProcedureParameters.Size = new System.Drawing.Size(142, 251);
            this.LbProcedureParameters.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(860, 377);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Procedure parameters:";
            // 
            // BtnXMLTable
            // 
            this.BtnXMLTable.Location = new System.Drawing.Point(326, 243);
            this.BtnXMLTable.Margin = new System.Windows.Forms.Padding(2);
            this.BtnXMLTable.Name = "BtnXMLTable";
            this.BtnXMLTable.Size = new System.Drawing.Size(74, 41);
            this.BtnXMLTable.TabIndex = 16;
            this.BtnXMLTable.Text = "XML";
            this.BtnXMLTable.UseVisualStyleBackColor = true;
            this.BtnXMLTable.Click += new System.EventHandler(this.BtnXml_Click);
            // 
            // BtnSelectTable
            // 
            this.BtnSelectTable.Location = new System.Drawing.Point(326, 288);
            this.BtnSelectTable.Margin = new System.Windows.Forms.Padding(2);
            this.BtnSelectTable.Name = "BtnSelectTable";
            this.BtnSelectTable.Size = new System.Drawing.Size(74, 41);
            this.BtnSelectTable.TabIndex = 17;
            this.BtnSelectTable.Text = "Select";
            this.BtnSelectTable.UseVisualStyleBackColor = true;
            this.BtnSelectTable.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // BtnSelectView
            // 
            this.BtnSelectView.Location = new System.Drawing.Point(856, 288);
            this.BtnSelectView.Margin = new System.Windows.Forms.Padding(2);
            this.BtnSelectView.Name = "BtnSelectView";
            this.BtnSelectView.Size = new System.Drawing.Size(74, 41);
            this.BtnSelectView.TabIndex = 18;
            this.BtnSelectView.Text = "Select";
            this.BtnSelectView.UseVisualStyleBackColor = true;
            this.BtnSelectView.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // BtnXMLView
            // 
            this.BtnXMLView.Location = new System.Drawing.Point(856, 243);
            this.BtnXMLView.Margin = new System.Windows.Forms.Padding(2);
            this.BtnXMLView.Name = "BtnXMLView";
            this.BtnXMLView.Size = new System.Drawing.Size(74, 41);
            this.BtnXMLView.TabIndex = 19;
            this.BtnXMLView.Text = "XML";
            this.BtnXMLView.UseVisualStyleBackColor = true;
            this.BtnXMLView.Click += new System.EventHandler(this.BtnXml_Click);
            // 
            // BtnQuery
            // 
            this.BtnQuery.Location = new System.Drawing.Point(222, 29);
            this.BtnQuery.Name = "BtnQuery";
            this.BtnQuery.Size = new System.Drawing.Size(154, 23);
            this.BtnQuery.TabIndex = 20;
            this.BtnQuery.Text = "Perform Arbitrary Query";
            this.BtnQuery.UseVisualStyleBackColor = true;
            this.BtnQuery.Click += new System.EventHandler(this.BtnQuery_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(1178, 679);
            this.Controls.Add(this.BtnQuery);
            this.Controls.Add(this.BtnXMLView);
            this.Controls.Add(this.BtnSelectView);
            this.Controls.Add(this.BtnSelectTable);
            this.Controls.Add(this.BtnXMLTable);
            this.Controls.Add(this.LbProcedureParameters);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.TbProcedure);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.LbProcedures);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.LbViewColumns);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.LbTableColums);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LbViews);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LbTables);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CbDatabases);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sql Viewer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CbDatabases;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox LbTables;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox LbViews;
        private System.Windows.Forms.ListBox LbTableColums;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox LbViewColumns;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox LbProcedures;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TbProcedure;
        private System.Windows.Forms.ListBox LbProcedureParameters;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button BtnXMLTable;
        private System.Windows.Forms.Button BtnSelectTable;
        private System.Windows.Forms.Button BtnSelectView;
        private System.Windows.Forms.Button BtnXMLView;
        private System.Windows.Forms.Button BtnQuery;
    }
}

