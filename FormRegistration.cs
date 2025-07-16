using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace eSportsTournament
{
    public partial class FormRegistration : Form
    {
        String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Admin\\source\\repos\\eSportsTournament\\eSportsDB.mdf;Integrated Security=True";
        public FormRegistration()
        {
            InitializeComponent();
        }

        private void FormRegistration_Button_X_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
            this.Hide();
        }

        private void FormRegistration_Button_Registration_Click(object sender, EventArgs e)
        {
            string UserName = FormRegistration_TextBox_UserName.Text.Trim();
            string Email = FormRegistration_TextBox_Email.Text.Trim();
            string PhoneNumber = FormRegistration_TextBox_Number.Text.Trim();
            string Password = FormRegistration_TextBox_Password.Text.Trim();

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(PhoneNumber) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Толық енгізіңіз!", "Ақау");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Users (Users_UserName, Users_Email, Users_PhoneNumber, Users_Password) VALUES (@Username, @Email, @PhoneNumber, @Password)";
                using (SqlCommand sqlCommand = new SqlCommand(query, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@Username", UserName);
                    sqlCommand.Parameters.AddWithValue("@Email", Email);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                    sqlCommand.Parameters.AddWithValue("@Password", Password);

                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Сәтті тіркелдіңіз!", "Сатті орынлады");

                        FormLogin formLogin = new FormLogin();
                        formLogin.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Қате табылды!", "Ақау");
                    }
                }
            }
        }

        private void pictureBoxPasswordShow_Click(object sender, EventArgs e)
        {
            FormRegistration_TextBox_Password.PasswordChar = '*';
            pictureBoxPasswordHide.Show();
            pictureBoxPasswordShow.Hide();
        }

        private void pictureBoxPasswordHide_Click(object sender, EventArgs e)
        {
            FormRegistration_TextBox_Password.PasswordChar = '\0';
            pictureBoxPasswordShow.Show();
            pictureBoxPasswordHide.Hide();
        }
    }
}
