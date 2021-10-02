using SQLManager.DAL;
using SQLManager.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLManager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
            => LoadDatabases();

        private void LoadDatabases()
        {
            CbDatabases.DataSource = new List<Database>(Repository.GetDatabases());
            LbTables.DataSource = new List<DBEntity>(Repository.GetDBEntities((Database)CbDatabases.SelectedItem, DBEntityType.Table));
        }


        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
            => Application.Exit();

        private void CbDatabases_SelectedValueChanged(object sender, EventArgs e)
        {
            LbTables.DataSource = new List<DBEntity>(Repository.GetDBEntities((Database)CbDatabases.SelectedItem, DBEntityType.Table));
            //LbViews.DataSource = new List<DBEntity>(Repository.GetDBEntities((Database)CbDatabases.SelectedItem, DBEntityType.View));

        }
    }
}
