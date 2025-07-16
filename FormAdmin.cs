using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace eSportsTournament
{
    public partial class FormAdmin : Form
    {
        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Admin\\source\\repos\\eSportsTournament\\eSportsDB.mdf;Integrated Security=True";
        int Tournaments_MaxTeams;
        string Tournaments_Format;
        string TournamentName;
        int selectedTournamentId;
        public FormAdmin()
        {
            InitializeComponent();
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            MessageBox.Show($"Name: {ClassGlobal.UserName}");

            LoadTeamsData();
            LoadUsersData();
            LoadTournamentsData();
            HideFormElements();
            LoadTournamentJoinRequests();
            AddAcceptedTeamsToTournaments();
            ColorforFormElements();
            LoadTournamentWinners();

            selectedTournamentId = -1;
            FormAdmin_ComboBox_MaxTeams.Items.Add(8);
            FormAdmin_ComboBox_MaxTeams.Items.Add(4);
            FormAdmin_ComboBox_MaxTeams.Items.Add(2);
            FormAdmin_ComboBox_Format.Items.Add("Playoff");
            FormAdmin_ComboBox_Format.Items.Add("League");
        }

        private void FormAdmin_Click(object sender, EventArgs e)
        {
            HideFormElements();
            ColorforFormElements();
        }

        private void LoadTeamsData()
        {
            string zapros = @"
            SELECT 
            Teams.Teams_TeamName AS [Название команды],
            COUNT(DISTINCT TeamMembers.TeamMembers_UserID) 
            + CASE WHEN Users.Users_UserID IS NOT NULL THEN 1 ELSE 0 END 
            - 1 AS [Число игроков],  -- Вычитаем 1, так как капитан учитывается дважды
            Users.Users_UserName AS [Капитан команды],
            Tournaments.Tournaments_TournamentName AS [Турнир]
            FROM Teams
            LEFT JOIN TeamMembers ON Teams.Teams_TeamID = TeamMembers.TeamMembers_TeamID
            LEFT JOIN Users ON Teams.Teams_CaptainID = Users.Users_UserID
            LEFT JOIN Tournaments ON Teams.Teams_TournamentID = Tournaments.Tournaments_TournamentID
            GROUP BY Teams.Teams_TeamName, Users.Users_UserName, Tournaments.Tournaments_TournamentName, Users.Users_UserID; 
            ";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(zapros, sqlConnection);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                FormAdmin_dataGridView_Teams.DataSource = dataTable;
            }
        }

        private void LoadUsersData()
        {
            string query = @"
            SELECT 
            u.Users_UserName AS 'Игрок',
            u.Users_PhoneNumber AS 'Телефон',
            t.Teams_TeamName AS 'Команда',
            tr.Tournaments_TournamentName AS 'Турнир',
            u.Users_Role AS 'Роль'
            FROM Users u
            LEFT JOIN TeamMembers tm ON u.Users_UserID = tm.TeamMembers_UserID
            LEFT JOIN Teams t ON tm.TeamMembers_TeamID = t.Teams_TeamID
            LEFT JOIN Tournaments tr ON t.Teams_TournamentID = tr.Tournaments_TournamentID
            WHERE u.Users_Role <> 'Admin';
            ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                FormAdmin_dataGridView_Users.DataSource = dt;
            }
        }

        private void LoadTournamentsData()
        {
            string zapros = @"
            SELECT 
            Tournaments.Tournaments_TournamentID AS [ID],
            Tournaments.Tournaments_TournamentName AS [Название турнира],
            Tournaments.Tournaments_Format AS [Формат],
            COUNT(DISTINCT Teams.Teams_TeamID) AS [Число команд],
            COUNT(DISTINCT CASE WHEN TournamentJoinRequests.TournamentJoinRequests_Status = 'Pending' 
            THEN TournamentJoinRequests.TournamentJoinRequests_RequestID END) AS [Собранные запросы]
            FROM Tournaments
            LEFT JOIN Teams ON Tournaments.Tournaments_TournamentID = Teams.Teams_TournamentID
            LEFT JOIN TournamentJoinRequests ON Tournaments.Tournaments_TournamentID = TournamentJoinRequests.TournamentJoinRequests_TournamentID
            GROUP BY Tournaments.Tournaments_TournamentID, Tournaments.Tournaments_TournamentName, Tournaments.Tournaments_Format;";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(zapros, sqlConnection);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                FormAdmin_dataGridView_Tournaments.DataSource = dataTable;

                FormAdmin_dataGridView_Tournaments.Columns["ID"].Visible = false;
            }
        }


        private void HideFormElements()
        {
            pictureBox6.Show();
            FormAdmin_ComboBox_Format.Hide();
            FormAdmin_ComboBox_MaxTeams.Hide();
            FormAdmin_TextBox_TournamentName.Hide();
            FormAdmin_Button_CreateTournament.Hide();
            FormAdmin_dataGridView_Teams.Hide();
            FormAdmin_dataGridView_Users.Hide();
            FormAdmin_dataGridView_Tournaments.Hide();
            FormTournamentLeague_dataGridView_TournamentJoinRequests.Hide();
            FormTournamentLeague_dataGridView_Winners.Hide();
        }

        private void ColorforFormElements()
        {
            pictureBox5.BackColor = Color.Transparent;
            pictureBox4.BackColor = Color.Transparent;
            pictureBox3.BackColor = Color.Transparent;
            pictureBox2.BackColor = Color.Transparent;
            pictureBox1.BackColor = Color.Transparent;
        }

        private void FormAdmin_ComboBox_Format_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FormAdmin_ComboBox_Format.SelectedItem != null)
            {
                Tournaments_Format = Convert.ToString(FormAdmin_ComboBox_Format.SelectedItem);
            }
        }

        private void FormAdmin_ComboBox_MaxTeams_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FormAdmin_ComboBox_MaxTeams.SelectedItem != null)
            {
                Tournaments_MaxTeams = Convert.ToInt32(FormAdmin_ComboBox_MaxTeams.SelectedItem);
            }
        }

     

        private void FormAdmin_Button_CreateTournament_Click(object sender, EventArgs e)
        {
            TournamentName = FormAdmin_TextBox_TournamentName.Text;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                string zapros = "INSERT INTO Tournaments(Tournaments_TournamentName, Tournaments_Format, Tournaments_MaxTeams) VALUES (@TournamentName, @Tournaments_Format, @Tournaments_MaxTeams)";
                using (SqlCommand sqlCommand = new SqlCommand(zapros, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@TournamentName", TournamentName);
                    sqlCommand.Parameters.AddWithValue("@Tournaments_Format", Tournaments_Format);
                    sqlCommand.Parameters.AddWithValue("@Tournaments_MaxTeams", Tournaments_MaxTeams);
                    int result = Convert.ToInt32(sqlCommand.ExecuteNonQuery());

                    if (result != 0)
                    {
                        MessageBox.Show("Cәтті орындалды");
                    }
                    else
                    {
                        MessageBox.Show("Сәтсіз орындалды");
                    }
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            HideFormElements();
            ColorforFormElements();
            pictureBox1.BackColor = Color.FromArgb(255, 193, 7);
            FormAdmin_ComboBox_Format.Show();
            FormAdmin_ComboBox_MaxTeams.Show();
            FormAdmin_TextBox_TournamentName.Show();
            FormAdmin_Button_CreateTournament.Show();
            pictureBox6.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ColorforFormElements();
            pictureBox3.BackColor = Color.FromArgb(255, 193, 7);
            LoadTeamsData();
            LoadUsersData();
            LoadTournamentsData();
            HideFormElements();
            HideFormElements();
            AddAcceptedTeamsToTournaments();
            FormAdmin_dataGridView_Teams.Show();
            pictureBox6.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pictureBox6.Hide();
            ColorforFormElements();
            pictureBox4.BackColor = Color.FromArgb(255, 193, 7);
            LoadTeamsData();
            LoadUsersData();
            LoadTournamentsData();
            HideFormElements();
            HideFormElements();
            AddAcceptedTeamsToTournaments();
            FormAdmin_dataGridView_Users.Show();
            pictureBox6.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ColorforFormElements();
            pictureBox2.BackColor = Color.FromArgb(255, 193, 7);
            LoadTeamsData();
            LoadUsersData();
            LoadTournamentsData();
            HideFormElements();
            HideFormElements();
            AddAcceptedTeamsToTournaments();
            FormAdmin_dataGridView_Tournaments.Show();
            pictureBox6.Hide();
        }

        private void FormAdmin_Button_Exit_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
            this.Hide();
        }

        private void FormAdmin_dataGridView_Tournaments_SelectionChanged(object sender, EventArgs e)
        {
            if (FormAdmin_dataGridView_Tournaments.SelectedRows.Count > 0)
            {
                selectedTournamentId = Convert.ToInt32(FormAdmin_dataGridView_Tournaments.SelectedRows[0].Cells["ID"].Value);
                ClassGlobal.TournamentID = selectedTournamentId;

                string tournamentFormat = GetTournamentFormat(selectedTournamentId);

                if (tournamentFormat == "League")
                {
                    FormTournamentLeague formTournamentLeague = new FormTournamentLeague();
                    formTournamentLeague.Show();
                }
                else if (tournamentFormat == "Playoff")
                {
                    FormTournamentPlayoff formTournamentPlayoff = new FormTournamentPlayoff();
                    formTournamentPlayoff.Show();
                }
                else
                {
                    MessageBox.Show("Қате формат!", "Ақау", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.Hide();
            }
        }

        private string GetTournamentFormat(int tournamentId)
        {
            string format = "";
            string query = "SELECT Tournaments_Format FROM Tournaments WHERE Tournaments_TournamentID = @tournamentId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tournamentId", tournamentId);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        format = result.ToString();
                    }
                }
            }
            return format;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            pictureBox6.Hide();
            ColorforFormElements();
            pictureBox5.BackColor = Color.FromArgb(255, 193, 7);
            HideFormElements();
            AddAcceptedTeamsToTournaments();
            FormTournamentLeague_dataGridView_TournamentJoinRequests.Show();
            pictureBox6.Hide();
        }

        private void FormTournamentLeague_dataGridView_TournamentJoinRequests_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridView dgv = sender as DataGridView;

                int requestID = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["ID"].Value);
                string action = "";

                if (dgv.Columns[e.ColumnIndex].Name == "Принять")
                {
                    action = "Accepted";
                }
                else if (dgv.Columns[e.ColumnIndex].Name == "Отклонить")
                {
                    action = "Rejected";
                }

                if (!string.IsNullOrEmpty(action))
                {
                    UpdateRequestStatus(requestID, action);
                    LoadTournamentJoinRequests();
                }
            }
        }

        private void LoadTournamentWinners()
        {
            string query = @"
        SELECT 
            TournamentWinners_TournamentName AS 'Турнир',
            TournamentWinners_TeamName AS 'Команда'
        FROM TournamentWinners";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable winnersTable = new DataTable();
                    adapter.Fill(winnersTable);
                    FormTournamentLeague_dataGridView_Winners.DataSource = winnersTable;
                }
            }
        }


        private void UpdateRequestStatus(int requestID, string status)
        {
            string updateQuery = @"
            UPDATE TournamentJoinRequests
            SET TournamentJoinRequests_Status = @Status
            WHERE TournamentJoinRequests_RequestID = @RequestID;
            ";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(updateQuery, sqlConnection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@RequestID", requestID);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void LoadTournamentJoinRequests()
        {
            string zapros = @"
            SELECT 
            TournamentJoinRequests.TournamentJoinRequests_RequestID AS [ID],
            Tournaments.Tournaments_TournamentName AS [Турнир], 
            Teams.Teams_TeamName AS [Команда]
            FROM TournamentJoinRequests
            JOIN Tournaments ON TournamentJoinRequests.TournamentJoinRequests_TournamentID = Tournaments.Tournaments_TournamentID
            JOIN Teams ON TournamentJoinRequests.TournamentJoinRequests_TeamID = Teams.Teams_TeamID
            WHERE TournamentJoinRequests.TournamentJoinRequests_Status = 'Pending';
            ";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(zapros, sqlConnection);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                FormTournamentLeague_dataGridView_TournamentJoinRequests.DataSource = dataTable;
            }

            if (FormTournamentLeague_dataGridView_TournamentJoinRequests.Columns["Принять"] == null)
            {
                DataGridViewButtonColumn acceptButton = new DataGridViewButtonColumn();
                acceptButton.Name = "Принять";
                acceptButton.HeaderText = "Принять";
                acceptButton.Text = "✔";
                acceptButton.UseColumnTextForButtonValue = true;
                FormTournamentLeague_dataGridView_TournamentJoinRequests.Columns.Add(acceptButton);
            }

            if (FormTournamentLeague_dataGridView_TournamentJoinRequests.Columns["Отклонить"] == null)
            {
                DataGridViewButtonColumn rejectButton = new DataGridViewButtonColumn();
                rejectButton.Name = "Отклонить";
                rejectButton.HeaderText = "Отклонить";
                rejectButton.Text = "✖";
                rejectButton.UseColumnTextForButtonValue = true;
                FormTournamentLeague_dataGridView_TournamentJoinRequests.Columns.Add(rejectButton);
            }

            FormTournamentLeague_dataGridView_TournamentJoinRequests.CellClick += FormTournamentLeague_dataGridView_TournamentJoinRequests_CellClick;
        }


        private void AddAcceptedTeamsToTournaments()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string getTournamentsQuery = @"
                    SELECT DISTINCT TournamentJoinRequests_TournamentID 
                    FROM TournamentJoinRequests 
                    WHERE TournamentJoinRequests_Status = 'Accepted';";

                        List<int> tournamentIds = new List<int>();

                        using (SqlCommand cmdGetTournaments = new SqlCommand(getTournamentsQuery, conn, transaction))
                        using (SqlDataReader reader = cmdGetTournaments.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                tournamentIds.Add(reader.GetInt32(0));
                            }
                        }

                        foreach (int tournamentId in tournamentIds)
                        {
                            string updateQuery = @"
                        UPDATE Teams
                        SET Teams_TournamentID = @TournamentID
                        WHERE Teams_TeamID IN (
                            SELECT TournamentJoinRequests_TeamID
                            FROM TournamentJoinRequests
                            WHERE TournamentJoinRequests_TournamentID = @TournamentID
                            AND TournamentJoinRequests_Status = 'Accepted'
                        );";

                            using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn, transaction))
                            {
                                cmdUpdate.Parameters.AddWithValue("@TournamentID", tournamentId);
                                cmdUpdate.ExecuteNonQuery();
                            }

                            string deleteQuery = @"
                        DELETE FROM TournamentJoinRequests
                        WHERE TournamentJoinRequests_TournamentID = @TournamentID
                        AND TournamentJoinRequests_Status = 'Accepted';";

                            using (SqlCommand cmdDelete = new SqlCommand(deleteQuery, conn, transaction))
                            {
                                cmdDelete.Parameters.AddWithValue("@TournamentID", tournamentId);
                                cmdDelete.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadTournamentWinners();
            ColorforFormElements();
            LoadTeamsData();
            LoadUsersData();
            LoadTournamentsData();
            HideFormElements();
            HideFormElements();
            AddAcceptedTeamsToTournaments();
            pictureBox6.Hide();
            FormTournamentLeague_dataGridView_Winners.Show();
        }
    }
}
