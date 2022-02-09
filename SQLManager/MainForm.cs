using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Zadatak.Dal;
using Zadatak.Models;
using Zadatak0102;

namespace Zadatak
{
    public partial class MainForm : Form
    {
        private const string FileFilter = "XML files(*.xml)|*.xml|All files(*.*)|*.*";
        private const string FileName = "{0}.xml";
        public MainForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init() => LoadDatabases();

        private void LoadDatabases() => CbDatabases.DataSource = new List<Database>(RepositoryFactory.GetRepository().GetDatabases());

        private void CbDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
            LbTables.DataSource = (CbDatabases.SelectedItem as Database).Tables;
            LbViews.DataSource = (CbDatabases.SelectedItem as Database).Views;
            LbProcedures.DataSource = (CbDatabases.SelectedItem as Database).Procedures;
        }

        private void Clear()
        {
            LbTableColums.DataSource = null;
            LbViewColumns.DataSource = null;
            TbProcedure.Text = string.Empty;
            LbProcedureParameters.DataSource = null;
        }

        private void LbTables_SelectedIndexChanged(object sender, EventArgs e) => LbTableColums.DataSource = (LbTables.SelectedItem as DBEntity).Columns;

        private void LbViews_SelectedIndexChanged(object sender, EventArgs e) => LbViewColumns.DataSource = (LbViews.SelectedItem as DBEntity).Columns;

        private void LbProcedures_SelectedIndexChanged(object sender, EventArgs e)
        {
            TbProcedure.Text = (LbProcedures.SelectedItem as Procedure).Definition;
            LbProcedureParameters.DataSource = (LbProcedures.SelectedItem as Procedure).Parameters;
        }
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            ArbitraryQueryForm arbitraryQueryForm = new ArbitraryQueryForm();
            arbitraryQueryForm.ShowDialog();
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            DBEntity dbEntity;
            switch ((sender as Button).Name)
            {
                case nameof(BtnSelectTable):
                    dbEntity = LbTables.SelectedItem as DBEntity;
                    break;
                case nameof(BtnSelectView):
                    dbEntity = LbViews.SelectedItem as DBEntity;
                    break;
                default:
                    throw new Exception("Wrong wiring");
            }
            DataSet ds = RepositoryFactory.GetRepository().CreateDataSet(dbEntity);
            new SelectResultsForm(ds.Tables[0]).ShowDialog();
        }


        private void BtnXml_Click(object sender, EventArgs e)
        {

            DBEntity dbEntity;
            switch ((sender as Button).Name)
            {
                case nameof(BtnXMLTable):
                    dbEntity = LbTables.SelectedItem as DBEntity;
                    break;
                case nameof(BtnXMLView):
                    dbEntity = LbViews.SelectedItem as DBEntity;
                    break;
                default:
                    throw new Exception("Wrong wiring");
            }
            var dialog = new SaveFileDialog()
            {
                FileName = string.Format(FileName, dbEntity.Name),
                Filter = FileFilter
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                DataSet ds = RepositoryFactory.GetRepository().CreateDataSet(dbEntity);
                ds.WriteXml(dialog.FileName, XmlWriteMode.WriteSchema);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e) => Application.Exit();
    }
}
