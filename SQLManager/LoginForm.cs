using SQLManager.DAL;
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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void BtConnect_Click(object sender, EventArgs e)
        {
            try
            {
                Repository.LogIn(
                    TbURI.Text.Trim(), 
                    TbUsername.Text.Trim(), 
                    TbPassword.Text.Trim()
                );
                new MainForm().Show();
                Hide();
            }
            catch (Exception ex)
            {
                LbError.Text = ex.Message;
            }
        }
    }
}
