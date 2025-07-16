using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace eSportsTournament
{
    public partial class FormUser : Form
    {
        String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Admin\\source\\repos\\eSportsTournament\\eSportsDB.mdf;Integrated Security=True";
        public FormUser()
        {
            InitializeComponent();
        }

        private void FormUser_Load(object sender, EventArgs e)
        {
            MessageBox.Show($"Name: {ClassGlobal.UserName}");
            HideFormElements();
            LoadUsers();
            LoadTeamJoinRequests(ClassGlobal.UserID);
            LoadUserTournamentStandings(ClassGlobal.UserID);
            LoadTournaments();
            LoadTeams();
            ColorforFormElements();
        }

        private void HideFormElements()
        {
            pictureBox6.Show();
            FormUser_Button_CreateTeam.Hide();
            FormUser_TextBox_TeamName.Hide();
            FormUser_Button_JoinTeam.Hide();
            FormUser_dataGridView_Teams.Hide();
            FormUser_dataGridView_Users.Hide();
            FormUser_Button_JoinTournaments.Hide();
            FormUser_dataGridView_Tournaments.Hide();
            FormUser_dataGridView_TournamentStandings.Hide();
            FormUser_Button_ExitTeam.Hide();
            FormCaptain_dataGridView_TeamRequests.Hide();
        }

        private void ColorforFormElements()
        {
            pictureBox5.BackColor = Color.Transparent;
            pictureBox4.BackColor = Color.Transparent;
            pictureBox3.BackColor = Color.Transparent;
            pictureBox2.BackColor = Color.Transparent;
            pictureBox1.BackColor = Color.Transparent;
        }

        private void FormUser_Click(object sender, EventArgs e)
        {
            HideFormElements();
            ColorforFormElements();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ColorforFormElements();
            pictureBox3.BackColor = Color.FromArgb(255, 193, 7);
            HideFormElements();
            FormUser_Button_CreateTeam.Show();
            FormUser_TextBox_TeamName.Show();
            FormUser_Button_JoinTeam.Show();
            FormUser_Button_ExitTeam.Show();
            FormUser_dataGridView_Teams.Show();
            LoadTeams();
            pictureBox6.Hide();
        }

        private void LoadTeams()
        {
            string query = @"
SELECT 
    Teams.Teams_TeamID AS 'ID',  -- Добавляем ID команды
    Teams.Teams_TeamName AS 'Название команды',
    ISNULL(Tournaments.Tournaments_TournamentName, N'Не участвует') AS 'Турнир',
    ISNULL((SELECT COUNT(*) FROM TeamMembers WHERE TeamMembers.TeamMembers_TeamID = Teams.Teams_TeamID), 0) AS 'Число игроков',
    ISNULL(Users.Users_UserName, 'Нет капитана') AS 'Капитан'
FROM Teams
LEFT JOIN Tournaments ON Teams.Teams_TournamentID = Tournaments.Tournaments_TournamentID
LEFT JOIN Users ON Teams.Teams_CaptainID = Users.Users_UserID;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    FormUser_dataGridView_Teams.DataSource = dataTable;
                    FormUser_dataGridView_Teams.Columns["ID"].Visible = false;
                }
            }
        }





        private void FormUser_Button_Exit_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            HideFormElements();
            ColorforFormElements();
            pictureBox4.BackColor = Color.FromArgb(255, 193, 7);
            FormUser_dataGridView_Users.Show();
            LoadUsers();
            pictureBox6.Hide();
        }

        private void LoadUsers()
        {
            string query = @"
SELECT 
    Users.Users_UserName AS 'Имя игрока',
    ISNULL(Teams.Teams_TeamName, N'Не в команде') AS 'Команда'
FROM Users
LEFT JOIN TeamMembers ON Users.Users_UserID = TeamMembers.TeamMembers_UserID
LEFT JOIN Teams ON TeamMembers.TeamMembers_TeamID = Teams.Teams_TeamID
WHERE Users.Users_Role <> 'Admin';";

    using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    FormUser_dataGridView_Users.DataSource = dataTable;
                }
            }
        }




        private void pictureBox2_Click(object sender, EventArgs e)
        {
            HideFormElements();
            ColorforFormElements();
            pictureBox2.BackColor = Color.FromArgb(255, 193, 7);
            FormUser_dataGridView_Tournaments.Show();
            FormUser_Button_JoinTournaments.Show();
            LoadTournaments();
            pictureBox6.Hide();
        }

        private void LoadTournaments()
        {
            string query = @"
    SELECT 
        Tournaments.Tournaments_TournamentName AS 'Название турнира',
        Tournaments.Tournaments_Format AS 'Формат',
        COUNT(Teams.Teams_TeamID) AS 'Участвующие команды',
        Tournaments.Tournaments_MaxTeams AS 'Максимум команд'
    FROM Tournaments
    LEFT JOIN Teams ON Tournaments.Tournaments_TournamentID = Teams.Teams_TournamentID
    GROUP BY 
        Tournaments.Tournaments_TournamentID, 
        Tournaments.Tournaments_TournamentName, 
        Tournaments.Tournaments_Format, 
        Tournaments.Tournaments_MaxTeams;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    FormUser_dataGridView_Tournaments.DataSource = dataTable;
                }
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            HideFormElements();
            ColorforFormElements();
            pictureBox1.BackColor = Color.FromArgb(255, 193, 7);
            FormUser_dataGridView_TournamentStandings.Show();
            pictureBox6.Hide();
        }

        private void LoadUserTournamentStandings(int userID)
        {
            string query = @"
SELECT ts.TournamentStandings_TournamentID, 
       t.Tournaments_TournamentName AS 'Турнир',
       tm.Teams_TeamName AS 'Команда',
       ts.TournamentStandings_MatchesPlayed AS 'Игры',
       ts.TournamentStandings_Wins AS 'Победы',
       ts.TournamentStandings_Draws AS 'Ничьи',
       ts.TournamentStandings_Losses AS 'Поражения',
       ts.TournamentStandings_GoalsScored AS 'Забито',
       ts.TournamentStandings_GoalsConceded AS 'Пропущено',
       ts.TournamentStandings_GoalDifference AS 'Разница',
       ts.TournamentStandings_Points AS 'Очки'
FROM TournamentStandings ts
JOIN Teams tm ON ts.TournamentStandings_TeamID = tm.Teams_TeamID
JOIN Tournaments t ON ts.TournamentStandings_TournamentID = t.Tournaments_TournamentID
WHERE ts.TournamentStandings_TournamentID IN (
    SELECT Teams_TournamentID FROM Teams 
    WHERE Teams_TeamID IN (
        SELECT TeamMembers_TeamID FROM TeamMembers WHERE TeamMembers_UserID = @userID
    )
)
AND (
    SELECT Users_Role FROM Users WHERE Users_UserID = @userID
) IN ('User', 'Captain')
ORDER BY ts.TournamentStandings_Points DESC;
";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userID", userID);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    FormUser_dataGridView_TournamentStandings.DataSource = dataTable;
                }
            }
        }



        private void FormUser_Button_CreateTeam_Click(object sender, EventArgs e)
        {
            string teamName = FormUser_TextBox_TeamName.Text.Trim();
            int userId = ClassGlobal.UserID;

            if (string.IsNullOrEmpty(teamName))
            {
                MessageBox.Show("Команданың атын енгізіңіз!", "Ақау", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string checkCaptainQuery = "SELECT COUNT(*) FROM Teams WHERE Teams_CaptainID = @userID";
                using (SqlCommand checkCmd = new SqlCommand(checkCaptainQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@userID", userId);
                    int isCaptain = (int)checkCmd.ExecuteScalar();
                    if (isCaptain > 0)
                    {
                        MessageBox.Show("Жаңа команда құрмастан алдын кәзіргі командаңыздан шығыңыз.",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string checkQuery = "SELECT COUNT(*) FROM Teams WHERE Teams_TeamName = @teamName";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@teamName", teamName);
                    int count = (int)checkCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Осындай команданың аты бос емес!", "Ақау", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string insertTeamQuery = @"
INSERT INTO Teams (Teams_TeamName, Teams_CaptainID, Teams_MaxMembers) 
VALUES (@teamName, @userID, 8); 
SELECT SCOPE_IDENTITY();";

                int newTeamId;
                using (SqlCommand cmd = new SqlCommand(insertTeamQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@teamName", teamName);
                    cmd.Parameters.AddWithValue("@userID", userId);
                    newTeamId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                string insertMemberQuery = "INSERT INTO TeamMembers (TeamMembers_UserID, TeamMembers_TeamID) VALUES (@userID, @teamID)";
                using (SqlCommand cmd = new SqlCommand(insertMemberQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@userID", userId);
                    cmd.Parameters.AddWithValue("@teamID", newTeamId);
                    cmd.ExecuteNonQuery();
                }

                string updateUserQuery = "UPDATE Users SET Users_Role = 'Captain' WHERE Users_UserID = @userID";
                using (SqlCommand cmd = new SqlCommand(updateUserQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@userID", userId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Команда сәтті құрылды!", "Сәтті орындалды", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LoadUsers();
            LoadTeams();
        }



        private void FormUser_Button_JoinTeam_Click(object sender, EventArgs e)
        {
            if (FormUser_dataGridView_Teams.SelectedRows.Count == 0)
            {
                MessageBox.Show("Қосылатын команданы таңдаңыз.", "Ақау", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedTeamId = Convert.ToInt32(FormUser_dataGridView_Teams.SelectedRows[0].Cells["ID"].Value);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string checkQuery = @"
SELECT TeamMembers_TeamID FROM TeamMembers WHERE TeamMembers_UserID = @userId";

                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@userId", ClassGlobal.UserID);
                    object result = checkCmd.ExecuteScalar();
                    if (result != null)
                    {
                        MessageBox.Show("Сіздің командаңыз бар.", "Ақау", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                string checkRequestQuery = @"
SELECT COUNT(*) FROM TeamJoinRequests 
WHERE TeamJoinRequests_UserID = @userId AND TeamJoinRequests_TeamID = @teamId AND TeamJoinRequests_Status = 'Pending'";

                using (SqlCommand checkReqCmd = new SqlCommand(checkRequestQuery, conn))
                {
                    checkReqCmd.Parameters.AddWithValue("@userId", ClassGlobal.UserID);
                    checkReqCmd.Parameters.AddWithValue("@teamId", selectedTeamId);
                    int count = (int)checkReqCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Сіз осыған дейн заявка жібергенсіз.", "Ақпарат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                string insertRequestQuery = @"
INSERT INTO TeamJoinRequests (TeamJoinRequests_UserID, TeamJoinRequests_TeamID, TeamJoinRequests_Status) 
VALUES (@userId, @teamId, 'Pending')";

                using (SqlCommand insertCmd = new SqlCommand(insertRequestQuery, conn))
                {
                    insertCmd.Parameters.AddWithValue("@userId", ClassGlobal.UserID);
                    insertCmd.Parameters.AddWithValue("@teamId", selectedTeamId);
                    insertCmd.ExecuteNonQuery();
                }

                MessageBox.Show("Заявка сәтті тасталды!", "Сәтті орындалды", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void FormUser_Button_ExitTeam_Click(object sender, EventArgs e)
        {
            int userId = ClassGlobal.UserID;
            int? teamId = null;
            bool isCaptain = false;

            string checkQuery = @"
SELECT Teams_TeamID, 
       CASE WHEN Teams_CaptainID = @userId THEN 1 ELSE 0 END AS IsCaptain 
FROM Teams 
WHERE Teams_TeamID = (SELECT TeamMembers_TeamID FROM TeamMembers WHERE TeamMembers_UserID = @userId)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(checkQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            teamId = reader.GetInt32(0);
                            isCaptain = reader.GetInt32(1) == 1;
                        }
                    }
                }
            }

            if (teamId == null)
            {
                MessageBox.Show("Сіз командада тұрмағансыз!", "Ақау", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string deleteQuery = @"DELETE FROM TeamMembers WHERE TeamMembers_UserID = @userId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.ExecuteNonQuery();
                }

                if (isCaptain)
                {
                    string updateRoleQuery = @"UPDATE Users SET Users_Role = 'User' WHERE Users_UserID = @userId";
                    using (SqlCommand cmd = new SqlCommand(updateRoleQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.ExecuteNonQuery();
                    }

                    string resetCaptainQuery = @"UPDATE Teams SET Teams_CaptainID = NULL WHERE Teams_TeamID = @teamId";
                    using (SqlCommand cmd = new SqlCommand(resetCaptainQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@teamId", teamId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            MessageBox.Show("Сіз сәтті командадан шықтыңыз!", "Сәтті орындалды", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadTeams();
            LoadUsers();

        }

        private void FormUser_Button_JoinTournaments_Click(object sender, EventArgs e)
        {
            if (FormUser_dataGridView_Tournaments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Турнирді таңдаңыз!", "Ақау", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int userId = ClassGlobal.UserID; 

            string tournamentName = FormUser_dataGridView_Tournaments.SelectedRows[0].Cells["Название турнира"].Value.ToString();
            int tournamentId = -1;

            string getTeamQuery = @"
        SELECT Teams_TeamID FROM Teams 
        WHERE Teams_CaptainID = @userId";

            int? teamId = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(getTeamQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        teamId = Convert.ToInt32(result);
                    }
                }
            }

            if (teamId == null)
            {
                MessageBox.Show("Сіз команда капитаны емессіз!", "Ақау", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string getTournamentIdQuery = @"
        SELECT Tournaments_TournamentID FROM Tournaments 
        WHERE Tournaments_TournamentName = @tournamentName";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(getTournamentIdQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@tournamentName", tournamentName);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        tournamentId = Convert.ToInt32(result);
                    }
                }
            }

            if (tournamentId == -1)
            {
                MessageBox.Show("Турнирдің ID аларда қате!", "Ақау", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string checkRequestQuery = @"
        SELECT COUNT(*) FROM TournamentJoinRequests 
        WHERE TournamentJoinRequests_TournamentID = @tournamentId 
          AND TournamentJoinRequests_TeamID = @teamId";

            int requestCount = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(checkRequestQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@tournamentId", tournamentId);
                    cmd.Parameters.AddWithValue("@teamId", teamId);
                    requestCount = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            if (requestCount > 0)
            {
                MessageBox.Show("Заявка жіберілген!", "Ақау", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string checkTeamsCountQuery = @"
        SELECT COUNT(*), Tournaments_MaxTeams 
        FROM Tournaments 
        LEFT JOIN Teams ON Tournaments.Tournaments_TournamentID = Teams.Teams_TournamentID 
        WHERE Tournaments.Tournaments_TournamentID = @tournamentId
        GROUP BY Tournaments.Tournaments_MaxTeams";

            int currentTeamsCount = 0;
            int maxTeams = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(checkTeamsCountQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@tournamentId", tournamentId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            currentTeamsCount = reader.GetInt32(0);
                            maxTeams = reader.GetInt32(1);
                        }
                    }
                }
            }

            if (currentTeamsCount >= maxTeams)
            {
                MessageBox.Show("Турнирде қосылуға орын қалмаған!", "Ақау", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string insertQuery = @"
        INSERT INTO TournamentJoinRequests (TournamentJoinRequests_TournamentID, TournamentJoinRequests_TeamID, TournamentJoinRequests_Status) 
        VALUES (@tournamentId, @teamId, 'Pending')";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@tournamentId", tournamentId);
                    cmd.Parameters.AddWithValue("@teamId", teamId);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Заявка сәтті жіберілді!", "Сәтті орындалды", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadTeamJoinRequests(int captainUserID)
        {
            string zapros = @"
SELECT 
    TeamJoinRequests.TeamJoinRequests_RequestID AS [ID],
    Teams.Teams_TeamName AS [Команда],
    Users.Users_UserName AS [Игрок]  -- Здесь меняем Users_Login на Users_UserName
FROM TeamJoinRequests
JOIN Teams ON TeamJoinRequests.TeamJoinRequests_TeamID = Teams.Teams_TeamID
JOIN Users ON TeamJoinRequests.TeamJoinRequests_UserID = Users.Users_UserID  -- Здесь меняем Users_ID на Users_UserID
WHERE TeamJoinRequests.TeamJoinRequests_Status = 'Pending'
  AND Teams.Teams_CaptainID = @CaptainID;
";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(zapros, sqlConnection);
                command.Parameters.AddWithValue("@CaptainID", captainUserID);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                FormCaptain_dataGridView_TeamRequests.DataSource = dataTable;
            }

            if (FormCaptain_dataGridView_TeamRequests.Columns["Принять"] == null)
            {
                DataGridViewButtonColumn acceptButton = new DataGridViewButtonColumn();
                acceptButton.Name = "Принять";
                acceptButton.HeaderText = "Принять";
                acceptButton.Text = "✔";
                acceptButton.UseColumnTextForButtonValue = true;
                FormCaptain_dataGridView_TeamRequests.Columns.Add(acceptButton);
            }

            if (FormCaptain_dataGridView_TeamRequests.Columns["Отклонить"] == null)
            {
                DataGridViewButtonColumn rejectButton = new DataGridViewButtonColumn();
                rejectButton.Name = "Отклонить";
                rejectButton.HeaderText = "Отклонить";
                rejectButton.Text = "✖";
                rejectButton.UseColumnTextForButtonValue = true;
                FormCaptain_dataGridView_TeamRequests.Columns.Add(rejectButton);
            }

            FormCaptain_dataGridView_TeamRequests.CellClick -= FormCaptain_dataGridView_TeamRequests_CellClick;
            FormCaptain_dataGridView_TeamRequests.CellClick += FormCaptain_dataGridView_TeamRequests_CellClick;
        }


        private void FormCaptain_dataGridView_TeamRequests_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridView dgv = sender as DataGridView;
                int requestID = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["ID"].Value);

                if (dgv.Columns[e.ColumnIndex].Name == "Принять")
                {
                    ApproveTeamJoinRequest(requestID);
                }
                else if (dgv.Columns[e.ColumnIndex].Name == "Отклонить")
                {
                    RejectTeamJoinRequest(requestID);
                }

                LoadTeamJoinRequests(ClassGlobal.UserID);
            }
        }

        private void ApproveTeamJoinRequest(int requestID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                string selectQuery = @"
        SELECT TeamJoinRequests_Status, TeamJoinRequests_UserID, TeamJoinRequests_TeamID
        FROM TeamJoinRequests 
        WHERE TeamJoinRequests_RequestID = @RequestID";

                SqlCommand selectCommand = new SqlCommand(selectQuery, sqlConnection);
                selectCommand.Parameters.AddWithValue("@RequestID", requestID);
                string currentStatus = "";
                int userID = 0, teamID = 0;

                using (SqlDataReader reader = selectCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        currentStatus = reader.GetString(0);
                        userID = reader.GetInt32(1);
                        teamID = reader.GetInt32(2);
                    }
                }

                if (currentStatus != "Accepted" && currentStatus != "Rejected")
                {
                    string updateQuery = @"
            UPDATE TeamJoinRequests 
            SET TeamJoinRequests_Status = 'Accepted' 
            WHERE TeamJoinRequests_RequestID = @RequestID";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, sqlConnection);
                    updateCommand.Parameters.AddWithValue("@RequestID", requestID);
                    updateCommand.ExecuteNonQuery();

                    string insertQuery = @"
            INSERT INTO TeamMembers (TeamMembers_UserID, TeamMembers_TeamID)
            VALUES (@UserID, @TeamID)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, sqlConnection);
                    insertCommand.Parameters.AddWithValue("@UserID", userID);
                    insertCommand.Parameters.AddWithValue("@TeamID", teamID);
                    insertCommand.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Заявка қабылданған.");
                }
            }
        }

        private void RejectTeamJoinRequest(int requestID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                string updateQuery = @"
        UPDATE TeamJoinRequests 
        SET TeamJoinRequests_Status = 'Rejected' 
        WHERE TeamJoinRequests_RequestID = @RequestID";
                SqlCommand updateCommand = new SqlCommand(updateQuery, sqlConnection);
                updateCommand.Parameters.AddWithValue("@RequestID", requestID);
                updateCommand.ExecuteNonQuery();
            }
        }



        private void pictureBox5_Click(object sender, EventArgs e)
        {
            HideFormElements();
            ColorforFormElements();
            pictureBox5.BackColor = Color.FromArgb(255, 193, 7);
            FormCaptain_dataGridView_TeamRequests.Show();
            LoadTeamJoinRequests(ClassGlobal.UserID);
            pictureBox6.Hide();
        }
    }
}
