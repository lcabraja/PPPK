using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zadatak.Dal;

namespace Zadatak0102
{
    public partial class ArbitraryQueryForm : Form
    {
        internal ArbitraryQueryForm()
        {
            InitializeComponent();
        }

        private void BtnExecuteQuery_Click(object sender, EventArgs e)
        {
            TbMessage.Text = string.Empty;
            if (string.IsNullOrEmpty(TbQuery.Text.Trim())) { return; }
            DataTable dataTable = RepositoryFactory.GetRepository()
                .ExecuteArbitraryQuery(TbQuery.Text.Trim(), OnInfoMessageGenerated, OnStatementCompleted);
            DgResults.DataSource = dataTable;
        }

        private void OnInfoMessageGenerated(object sender, SqlInfoMessageEventArgs e)
        {
            TbMessage.Text += $"{e.Message}" + Environment.NewLine;
        }
        private void OnStatementCompleted(object sender, StatementCompletedEventArgs e)
        {
            // emulating line for line the output of SSMS
            TbMessage.Text += $"({e.RecordCount} rows affected)" + Environment.NewLine;
            TbMessage.Text += Environment.NewLine;
            TbMessage.Text += $"Completion time: {DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")}" + Environment.NewLine;
        }
    }
}
