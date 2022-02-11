using System.Data;
using System.Windows.Forms;

namespace Zadatak
{
    public partial class SelectResultsForm : Form
    {
        
        internal SelectResultsForm(DataTable dataTable)
        {
            InitializeComponent();
            Init(dataTable);
        }

        private void Init(DataTable dataTable)
        {
            Text = dataTable.TableName;
            DgResults.DataSource = dataTable;
        }
    }
}
