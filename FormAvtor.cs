using System;
using System.Windows.Forms;

namespace eSportsTournament
{
    public partial class FormAvtor: Form
    {
        public FormAvtor()
        {
            InitializeComponent();
        }

        private void FormLogin_Button_Exit_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
            this.Hide();
        }
    }
}
