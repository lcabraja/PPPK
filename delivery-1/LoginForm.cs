using System;
using System.Windows.Forms;
using Zadatak.Dal;

namespace Zadatak
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void BtnLogIn_Click(object sender, EventArgs e)
        {
            try
            {
                // exception driven, but ok
                
                RepositoryFactory.GetRepository().LogIn(TbServer.Text.Trim(), TbUserName.Text.Trim(), TbPassword.Text.Trim());
                new MainForm().Show();
                Hide(); // we cannot dispose or close cause the child dies!
                // do not forget to catch Form_Closing of child to kill the application!
            }
            catch (Exception ex)
            {
                LbError.Text = ex.Message;
            }
        }
    }
}
