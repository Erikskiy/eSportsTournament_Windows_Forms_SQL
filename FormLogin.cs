using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace eSportsTournament
{
    public partial class FormLogin : Form
    {
        String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Admin\\source\\repos\\eSportsTournament\\eSportsDB.mdf;Integrated Security=True";
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Button_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormLogin_Button_Enter_Click(object sender, EventArgs e)
        {
            string Email = FormLogin_TextBox_Email.Text;
            string Password = FormLogin_TextBox_Password.Text;

            if (string.IsNullOrEmpty(Email) ||  string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Толық енгізіңіз!", "Ақау");
                return;
            }

            string zapros = "SELECT Users_UserID, Users_UserName, Users_Email, Users_Role FROM Users WHERE Users_Email = @Email AND Users_Password = @Password";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(zapros, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Email", Email);
                    sqlCommand.Parameters.AddWithValue("@Password", Password);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ClassGlobal.UserID = Convert.ToInt32(reader["Users_UserID"]);
                            ClassGlobal.UserName = reader["Users_UserName"].ToString();
                            ClassGlobal.UserEmail = reader["Users_Email"].ToString();

                            string role = reader["Users_Role"].ToString();

                            if (role == "Admin")
                            {
                                FormAdmin formAdmin = new FormAdmin();
                                formAdmin.Show();
                                this.Hide();
                            }
                            else
                            {
                                FormUser formUser = new FormUser();
                                formUser.Show();
                                this.Hide();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Қате email немесе пароль!");
                        }
                    }
                }
            }
        }

        private void label_Button_RegistrationFormOpen_Click(object sender, EventArgs e)
        {
            FormRegistration formRegistration = new FormRegistration();
            formRegistration.Show();
            this.Hide();
        }

        private void pictureBoxPasswordHide_Click(object sender, EventArgs e)
        {
            FormLogin_TextBox_Password.PasswordChar = '\0';
            pictureBoxPasswordShow.Show();
            pictureBoxPasswordHide.Hide();

        }

        private void pictureBoxPasswordShow_Click(object sender, EventArgs e)
        {
            FormLogin_TextBox_Password.PasswordChar = '*';
            pictureBoxPasswordHide.Show();
            pictureBoxPasswordShow.Hide();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormAvtor formAvtor = new FormAvtor();
            formAvtor.Show();
            this.Hide();
        }
    }
}
