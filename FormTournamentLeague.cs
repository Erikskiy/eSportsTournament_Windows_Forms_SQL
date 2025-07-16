using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
namespace eSportsTournament
{
    public partial class FormTournamentLeague : Form
    {
        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Admin\\source\\repos\\eSportsTournament\\eSportsDB.mdf;Integrated Security=True";
        public FormTournamentLeague()
        {
            InitializeComponent();
        }

        private void FormTournamentLeague_Load(object sender, EventArgs e)
        {
            MessageBox.Show($"ID {ClassGlobal.TournamentID}");
            HideFormElements();
            LoadTournamentJoinRequests();
            ColorforFormElements();
            LoadMatches(ClassGlobal.TournamentID);
            GenerateLeagueMatches(ClassGlobal.TournamentID);
            UpdateTournamentStandings(ClassGlobal.TournamentID); 
            LoadTournamentStandings(ClassGlobal.TournamentID);  
        }

        private void FormTournamentLeague_Click(object sender, EventArgs e)
        {
            HideFormElements();
            ColorforFormElements();
        }

        private void HideFormElements()
        {
            pictureBox4.Show();
            FormTournamentLeague_Button_SaveResults.Hide();
            FormTournamentLeague_dataGridView_Matches.Hide();
            FormTournamentLeague_dataGridView_Standings.Hide();
            FormTournamentLeague_dataGridView_TournamentJoinRequests.Hide();
            buttonPDF.Hide();
        }

        private void ColorforFormElements()
        {
            pictureBox3.BackColor = Color.Transparent;
            pictureBox2.BackColor = Color.Transparent;
            pictureBox1.BackColor = Color.Transparent;
        }

        private void FormTournamentLeague_Button_Exit_Click(object sender, EventArgs e)
        {
            FormAdmin formAdmin = new FormAdmin();
            formAdmin.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            HideFormElements();
            ColorforFormElements();
            pictureBox2.BackColor = Color.FromArgb(255, 193, 7);
            FormTournamentLeague_Button_SaveResults.Show();
            FormTournamentLeague_dataGridView_Matches.Show();
            pictureBox4.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            HideFormElements();
            ColorforFormElements();
            pictureBox1.BackColor = Color.FromArgb(255, 193, 7);
            FormTournamentLeague_dataGridView_Standings.Show();
            buttonPDF.Show();   
            UpdateTournamentStandings(ClassGlobal.TournamentID);
            LoadTournamentStandings(ClassGlobal.TournamentID);
            pictureBox4.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            HideFormElements();
            ColorforFormElements();
            pictureBox3.BackColor = Color.FromArgb(255, 193, 7);
            FormTournamentLeague_dataGridView_TournamentJoinRequests.Show();
            pictureBox4.Hide();
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

        private void GenerateLeagueMatches(int tournamentID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                string query = @"
SELECT Teams_TeamID FROM Teams
WHERE Teams_TournamentID = @TournamentID";

                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@TournamentID", tournamentID);

                List<int> teams = new List<int>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        teams.Add(reader.GetInt32(0));
                    }
                }

                string existingMatchesQuery = @"
SELECT Matches_Team1ID, Matches_Team2ID FROM Matches 
WHERE Matches_TournamentID = @TournamentID";

                SqlCommand existingMatchesCmd = new SqlCommand(existingMatchesQuery, sqlConnection);
                existingMatchesCmd.Parameters.AddWithValue("@TournamentID", tournamentID);

                HashSet<string> existingPairs = new HashSet<string>();
                using (SqlDataReader reader = existingMatchesCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int t1 = reader.GetInt32(0);
                        int t2 = reader.GetInt32(1);
                        string pairKey = t1 < t2 ? $"{t1}_{t2}" : $"{t2}_{t1}";
                        existingPairs.Add(pairKey);
                    }
                }

                foreach (int team1 in teams)
                {
                    foreach (int team2 in teams)
                    {
                        if (team1 < team2)
                        {
                            string pairKey = $"{team1}_{team2}";
                            if (!existingPairs.Contains(pairKey))
                            {
                                string insertQuery = @"
INSERT INTO Matches (Matches_TournamentID, Matches_Team1ID, Matches_Team2ID, Matches_ScoreTeam1, Matches_ScoreTeam2) 
VALUES (@TournamentID, @Team1ID, @Team2ID, NULL, NULL)";

                                SqlCommand insertCommand = new SqlCommand(insertQuery, sqlConnection);
                                insertCommand.Parameters.AddWithValue("@TournamentID", tournamentID);
                                insertCommand.Parameters.AddWithValue("@Team1ID", team1);
                                insertCommand.Parameters.AddWithValue("@Team2ID", team2);
                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }

                string addStandingsQuery = @"
INSERT INTO TournamentStandings (TournamentStandings_TournamentID, TournamentStandings_TeamID)
SELECT @TournamentID, T.Teams_TeamID
FROM Teams T
WHERE T.Teams_TournamentID = @TournamentID
  AND NOT EXISTS (
      SELECT 1 FROM TournamentStandings TS 
      WHERE TS.TournamentStandings_TournamentID = @TournamentID 
        AND TS.TournamentStandings_TeamID = T.Teams_TeamID
  )";

                SqlCommand addStandingsCommand = new SqlCommand(addStandingsQuery, sqlConnection);
                addStandingsCommand.Parameters.AddWithValue("@TournamentID", tournamentID);
                addStandingsCommand.ExecuteNonQuery();
            }

            LoadMatches(tournamentID);
        }




        private void LoadMatches(int tournamentID)
        {
            FormTournamentLeague_dataGridView_Matches.ReadOnly = false;

            foreach (DataGridViewColumn column in FormTournamentLeague_dataGridView_Matches.Columns)
            {
                if (column.HeaderText != "Голы 1" && column.HeaderText != "Голы 2")
                {
                    column.ReadOnly = true;
                }
            }

            string query = @"
        SELECT 
            T1.Teams_TeamName AS [Команда 1], 
            Matches.Matches_ScoreTeam1 AS [Голы 1],
            Matches.Matches_ScoreTeam2 AS [Голы 2], 
            T2.Teams_TeamName AS [Команда 2]
        FROM Matches
        JOIN Teams T1 ON Matches.Matches_Team1ID = T1.Teams_TeamID
        JOIN Teams T2 ON Matches.Matches_Team2ID = T2.Teams_TeamID
        WHERE Matches.Matches_TournamentID = @TournamentID;";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@TournamentID", tournamentID);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                FormTournamentLeague_dataGridView_Matches.DataSource = dataTable;
            }
        }

        private void FormTournamentLeague_Button_SaveResults_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                foreach (DataGridViewRow row in FormTournamentLeague_dataGridView_Matches.Rows)
                {
                    if (row.Cells["Голы 1"].Value != DBNull.Value && row.Cells["Голы 2"].Value != DBNull.Value)
                    {
                        int score1 = Convert.ToInt32(row.Cells["Голы 1"].Value);
                        int score2 = Convert.ToInt32(row.Cells["Голы 2"].Value);

                        if (row.Cells["Команда 1"].Value == null || row.Cells["Команда 2"].Value == null)
                        {
                            continue;
                        }

                        string team1 = row.Cells["Команда 1"].Value.ToString();
                        string team2 = row.Cells["Команда 2"].Value.ToString();

                        string getTeamIDsQuery = @"
                SELECT T1.Teams_TeamID AS Team1ID, T2.Teams_TeamID AS Team2ID
                FROM Teams T1, Teams T2
                WHERE T1.Teams_TeamName = @Team1 AND T2.Teams_TeamName = @Team2";

                        using (SqlCommand getIDsCommand = new SqlCommand(getTeamIDsQuery, sqlConnection))
                        {
                            getIDsCommand.Parameters.AddWithValue("@Team1", team1);
                            getIDsCommand.Parameters.AddWithValue("@Team2", team2);

                            using (SqlDataReader reader = getIDsCommand.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    int team1ID = reader.GetInt32(0);
                                    int team2ID = reader.GetInt32(1);

                                    reader.Close();

                                    string updateQuery = @"
                            UPDATE Matches 
                            SET Matches_ScoreTeam1 = @Score1, Matches_ScoreTeam2 = @Score2
                            WHERE Matches_TournamentID = @TournamentID 
                            AND Matches_Team1ID = @Team1ID 
                            AND Matches_Team2ID = @Team2ID";

                                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, sqlConnection))
                                    {
                                        updateCommand.Parameters.AddWithValue("@Score1", score1);
                                        updateCommand.Parameters.AddWithValue("@Score2", score2);
                                        updateCommand.Parameters.AddWithValue("@TournamentID", ClassGlobal.TournamentID);
                                        updateCommand.Parameters.AddWithValue("@Team1ID", team1ID);
                                        updateCommand.Parameters.AddWithValue("@Team2ID", team2ID);
                                        updateCommand.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show($"Ақау: Командалардың ID табылмады {team1} және {team2} мәліметтер қорынан.", "Ақау", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }

                MessageBox.Show("Ойындардың нәтижесі сақталды!", "Сәтті орындалды", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void UpdateTournamentStandings(int tournamentID)
        {
            string query = @"
-- 1. Добавим отсутствующие команды в таблицу турнирной таблицы
INSERT INTO TournamentStandings (TournamentStandings_TournamentID, TournamentStandings_TeamID)
SELECT t.Teams_TournamentID, t.Teams_TeamID
FROM Teams t
LEFT JOIN TournamentStandings ts 
    ON ts.TournamentStandings_TeamID = t.Teams_TeamID AND ts.TournamentStandings_TournamentID = t.Teams_TournamentID
WHERE t.Teams_TournamentID = @tournamentID AND ts.TournamentStandings_TeamID IS NULL;

-- 2. Обновим статистику по матчам
UPDATE ts
SET 
    TournamentStandings_MatchesPlayed = (
        SELECT COUNT(*) FROM Matches 
        WHERE (Matches_Team1ID = ts.TournamentStandings_TeamID OR Matches_Team2ID = ts.TournamentStandings_TeamID) 
              AND Matches_TournamentID = ts.TournamentStandings_TournamentID
    ),
    TournamentStandings_Wins = (
        SELECT COUNT(*) FROM Matches 
        WHERE ((Matches_Team1ID = ts.TournamentStandings_TeamID AND Matches_ScoreTeam1 > Matches_ScoreTeam2)
               OR (Matches_Team2ID = ts.TournamentStandings_TeamID AND Matches_ScoreTeam2 > Matches_ScoreTeam1))
              AND Matches_TournamentID = ts.TournamentStandings_TournamentID
    ),
    TournamentStandings_Draws = (
        SELECT COUNT(*) FROM Matches 
        WHERE ((Matches_Team1ID = ts.TournamentStandings_TeamID OR Matches_Team2ID = ts.TournamentStandings_TeamID) 
              AND Matches_ScoreTeam1 = Matches_ScoreTeam2)
              AND Matches_TournamentID = ts.TournamentStandings_TournamentID
    ),
    TournamentStandings_Losses = (
        SELECT COUNT(*) FROM Matches 
        WHERE ((Matches_Team1ID = ts.TournamentStandings_TeamID AND Matches_ScoreTeam1 < Matches_ScoreTeam2)
               OR (Matches_Team2ID = ts.TournamentStandings_TeamID AND Matches_ScoreTeam2 < Matches_ScoreTeam1))
              AND Matches_TournamentID = ts.TournamentStandings_TournamentID
    ),
    TournamentStandings_GoalsScored = (
        SELECT COALESCE(SUM(Matches_ScoreTeam1), 0) FROM Matches 
        WHERE Matches_Team1ID = ts.TournamentStandings_TeamID AND Matches_TournamentID = ts.TournamentStandings_TournamentID
    ) + 
    (
        SELECT COALESCE(SUM(Matches_ScoreTeam2), 0) FROM Matches 
        WHERE Matches_Team2ID = ts.TournamentStandings_TeamID AND Matches_TournamentID = ts.TournamentStandings_TournamentID
    ),
    TournamentStandings_GoalsConceded = (
        SELECT COALESCE(SUM(Matches_ScoreTeam2), 0) FROM Matches 
        WHERE Matches_Team1ID = ts.TournamentStandings_TeamID AND Matches_TournamentID = ts.TournamentStandings_TournamentID
    ) + 
    (
        SELECT COALESCE(SUM(Matches_ScoreTeam1), 0) FROM Matches 
        WHERE Matches_Team2ID = ts.TournamentStandings_TeamID AND Matches_TournamentID = ts.TournamentStandings_TournamentID
    )
FROM TournamentStandings ts
WHERE ts.TournamentStandings_TournamentID = @tournamentID;

-- 3. Обновим очки
UPDATE ts
SET 
    TournamentStandings_Points = (TournamentStandings_Wins * 3) + (TournamentStandings_Draws * 1)
FROM TournamentStandings ts
WHERE ts.TournamentStandings_TournamentID = @tournamentID;
";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@tournamentID", tournamentID);
                cmd.ExecuteNonQuery();
            }
        }

        private void LoadTournamentStandings(int tournamentID)
        {
            string query = @"
        SELECT 
            Teams.Teams_TeamName AS 'Команда',
            TournamentStandings.TournamentStandings_MatchesPlayed AS 'Матчи',
            TournamentStandings.TournamentStandings_Wins AS 'Победы',
            TournamentStandings.TournamentStandings_Draws AS 'Ничьи',
            TournamentStandings.TournamentStandings_Losses AS 'Поражения',
            TournamentStandings.TournamentStandings_GoalsScored AS 'Забитые голы',
            TournamentStandings.TournamentStandings_GoalsConceded AS 'Пропущенные голы',
            TournamentStandings.TournamentStandings_GoalDifference AS 'Разница мячей',
            TournamentStandings.TournamentStandings_Points AS 'Очки'
        FROM TournamentStandings
        JOIN Teams ON TournamentStandings.TournamentStandings_TeamID = Teams.Teams_TeamID
        WHERE TournamentStandings.TournamentStandings_TournamentID = @tournamentID
        ORDER BY TournamentStandings.TournamentStandings_Points DESC, TournamentStandings.TournamentStandings_GoalDifference DESC;
    ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@tournamentID", tournamentID);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                FormTournamentLeague_dataGridView_Standings.DataSource = dt;
            }
        }

        private void FormTournamentPlayoff_btn_Tournament_Update_Click(object sender, EventArgs e)
        {
            int tournamentId = ClassGlobal.TournamentID;
            string winnerTeam = "";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
            SELECT TOP 1 tm.Teams_TeamName
            FROM TournamentStandings ts
            JOIN Teams tm ON ts.TournamentStandings_TeamID = tm.Teams_TeamID
            WHERE ts.TournamentStandings_TournamentID = @TournamentID
            ORDER BY ts.TournamentStandings_Points DESC
        ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TournamentID", tournamentId);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        winnerTeam = result.ToString();
                    }
                }
            }

            if (!string.IsNullOrEmpty(winnerTeam))
            {
                string tournamentName = GetTournamentName(tournamentId);

                SaveTournamentWinner(tournamentName, winnerTeam);

            }
            else
            {
                MessageBox.Show("Чемпионды анықтау мүмкін емес");
            }
        }

        private string GetTournamentName(int tournamentId)
        {
            string tournamentName = "";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Tournaments_TournamentName FROM Tournaments WHERE Tournaments_TournamentID = @TournamentID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TournamentID", tournamentId);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                        tournamentName = result.ToString();
                }
            }
            return tournamentName;
        }

        private void SaveTournamentWinner(string tournamentName, string teamName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO TournamentWinners (TournamentWinners_TournamentName, TournamentWinners_TeamName)
                         VALUES (@TournamentName, @TeamName)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TournamentName", tournamentName);
                    cmd.Parameters.AddWithValue("@TeamName", teamName);
                    cmd.ExecuteNonQuery();
                }
            }
            DeleteTournament(ClassGlobal.TournamentID);
            MessageBox.Show($"Чемпион {teamName} сақталды!");
        }

        private void DeleteTournament(int tournamentId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Tournaments WHERE Tournaments_TournamentID = @TournamentID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TournamentID", tournamentId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void buttonPDF_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF файл (*.pdf)|*.pdf";
                saveFileDialog.Title = "PDF түрінде сақтау";
                saveFileDialog.FileName = "Турнирдің таблицасы.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = saveFileDialog.FileName;

                    DataTable dt = (DataTable)FormTournamentLeague_dataGridView_Standings.DataSource;

                    if (dt != null)
                    {
                        Document document = new Document(PageSize.A4, 10, 10, 10, 10);
                        PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));
                        document.Open();

                        string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                        BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                        iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 12);
                        iTextSharp.text.Font titleFont = new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD);

                        Paragraph header = new Paragraph("Турнирдің таблицасы", titleFont);
                        header.Alignment = Element.ALIGN_CENTER;
                        document.Add(header);
                        document.Add(new Paragraph("\n"));

                        PdfPTable pdfTable = new PdfPTable(dt.Columns.Count);
                        pdfTable.WidthPercentage = 100;

                        foreach (DataColumn column in dt.Columns)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(column.ColumnName, font));
                            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                            pdfTable.AddCell(cell);
                        }

                        foreach (DataRow row in dt.Rows)
                        {
                            foreach (var item in row.ItemArray)
                            {
                                pdfTable.AddCell(new Phrase(item.ToString(), font));
                            }
                        }

                        document.Add(pdfTable);
                        document.Close();

                        MessageBox.Show("Сәтті сақталды!", "Сәтті орындалды", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Ішінде ақпарат жоқ", "Ақау", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

    }
}
