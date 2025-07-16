using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace eSportsTournament
{
    public partial class FormTournamentPlayoff: Form
    {
        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Admin\\source\\repos\\eSportsTournament\\eSportsDB.mdf;Integrated Security=True";
        public FormTournamentPlayoff()
        {
            InitializeComponent();
        }

        private void FormTournamentPlayoff_button_EXIT_Click(object sender, EventArgs e)
        {
            FormAdmin form = new FormAdmin();
            form.Show();
            this.Hide();
        }

        private void FormTournamentPlayoff_Load(object sender, EventArgs e)
        {
            int maxTeams = GetMaxTeamsForTournament(ClassGlobal.TournamentID);
            MessageBox.Show($"ID {ClassGlobal.TournamentID}, Teams: {maxTeams}");
            DisableAllComboBoxes();
            EnableComboBoxesForPlayoff(maxTeams);
            FillComboBoxesWithTeams(ClassGlobal.TournamentID);

        }

        private int GetMaxTeamsForTournament(int tournamentId)
        {
            int maxTeams = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Tournaments_MaxTeams FROM Tournaments WHERE Tournaments_TournamentID = @TournamentID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TournamentID", tournamentId);
                    object result = cmd.ExecuteScalar();
                    if (result != null) maxTeams = Convert.ToInt32(result);
                }
            }
            return maxTeams;
        }

        private void DisableAllComboBoxes()
        {
            foreach (Control control in this.Controls)
            {
                if (control is ComboBox comboBox)
                {
                    comboBox.Visible = false;
                }
            }
        }

        private void EnableComboBoxesForPlayoff(int maxTeams)
        {
            if (maxTeams == 8)
            {
                comboBox_Tournament_8_1.Visible = true;
                comboBox_Tournament_8_2.Visible = true;
                comboBox_Tournament_8_3.Visible = true;
                comboBox_Tournament_8_4.Visible = true;
                comboBox_Tournament_8_5.Visible = true;
                comboBox_Tournament_8_6.Visible = true;
                comboBox_Tournament_8_7.Visible = true;
                comboBox_Tournament_8_8.Visible = true;

                comboBox_Tournament_4_1.Visible = true;
                comboBox_Tournament_4_2.Visible = true;
                comboBox_Tournament_4_3.Visible = true;
                comboBox_Tournament_4_4.Visible = true;

                textBox_point_comboBox_Tournament_4_1.Show();
                textBox_point_comboBox_Tournament_4_2.Show();
                textBox_point_comboBox_Tournament_4_3.Show();
                textBox_point_comboBox_Tournament_4_4.Show();

                textBox_point_comboBox_Tournament_2_1.Show();
                textBox_point_comboBox_Tournament_2_2.Show();

                comboBox_Tournament_2_1.Visible = true;
                comboBox_Tournament_2_2.Visible = true;

                textBox_point_comboBox_Tournament_2_1.Show();
                textBox_point_comboBox_Tournament_2_2.Show();

                comboBox_Tournament_Chempion.Visible = true;
            }

            if (maxTeams == 4)
            {
                HideSchet();

                comboBox_Tournament_4_1.Visible = true;
                comboBox_Tournament_4_2.Visible = true;
                comboBox_Tournament_4_3.Visible = true;
                comboBox_Tournament_4_4.Visible = true;

                textBox_point_comboBox_Tournament_4_1.Show();
                textBox_point_comboBox_Tournament_4_2.Show();
                textBox_point_comboBox_Tournament_4_3.Show();
                textBox_point_comboBox_Tournament_4_4.Show();

                textBox_point_comboBox_Tournament_2_1.Show();
                textBox_point_comboBox_Tournament_2_2.Show();

                comboBox_Tournament_2_1.Visible = true;
                comboBox_Tournament_2_2.Visible = true;

                textBox_point_comboBox_Tournament_2_1.Show();
                textBox_point_comboBox_Tournament_2_2.Show();

                comboBox_Tournament_Chempion.Visible = true;
            }
            if (maxTeams == 2)
            {
                HideSchet();

                comboBox_Tournament_2_1.Visible = true;
                comboBox_Tournament_2_2.Visible = true;

                textBox_point_comboBox_Tournament_2_1.Show();
                textBox_point_comboBox_Tournament_2_2.Show();

                comboBox_Tournament_Chempion.Visible = true;
            }
            if (maxTeams == 1)
            {
                HideSchet();

                comboBox_Tournament_Chempion.Visible = true;
            }
        }

        private void HideSchet()
        {
            textBox_point_comboBox_Tournament_8_1.Hide();
            textBox_point_comboBox_Tournament_8_2.Hide();
            textBox_point_comboBox_Tournament_8_3.Hide();
            textBox_point_comboBox_Tournament_8_4.Hide();
            textBox_point_comboBox_Tournament_8_5.Hide();
            textBox_point_comboBox_Tournament_8_6.Hide();
            textBox_point_comboBox_Tournament_8_7.Hide();
            textBox_point_comboBox_Tournament_8_8.Hide();

            textBox_point_comboBox_Tournament_4_1.Hide();
            textBox_point_comboBox_Tournament_4_2.Hide();
            textBox_point_comboBox_Tournament_4_3.Hide();
            textBox_point_comboBox_Tournament_4_4.Hide();

            textBox_point_comboBox_Tournament_2_1.Hide();
            textBox_point_comboBox_Tournament_2_2.Hide();
        }

        private List<string> GetTeamsForTournament(int tournamentId)
        {
            List<string> teams = new List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
            SELECT Teams.Teams_TeamName 
            FROM Teams
            WHERE Teams.Teams_TournamentID = @TournamentID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TournamentID", tournamentId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            teams.Add(reader.GetString(0));
                        }
                    }
                }
            }

            return teams;
        }


        private void FillComboBoxesWithTeams(int tournamentId)
        {
            List<string> teams = GetTeamsForTournament(tournamentId);

            foreach (Control control in this.Controls)
            {
                if (control is ComboBox comboBox && comboBox.Name.StartsWith("comboBox_Tournament_"))
                {
                    comboBox.Items.Clear(); 
                    comboBox.Items.AddRange(teams.ToArray()); 
                }
            }
        }


        private int GetTournamentStage(int maxTeams)
        {
            if (maxTeams == 8) return 8;
            if (maxTeams == 4) return 4;
            if (maxTeams == 2) return 2;
            if (maxTeams == 1) return 1;
            return 0;
        }

        private void FormTournamentPlayoff_btn_Tournament_Update_Click(object sender, EventArgs e)
        {
            int maxTeams = GetMaxTeamsForTournament(ClassGlobal.TournamentID);
            int stage = GetTournamentStage(maxTeams);

            if (stage == 8)
            {
                UpdateNextRound(comboBox_Tournament_8_1, textBox_point_comboBox_Tournament_8_1,
                                comboBox_Tournament_8_2, textBox_point_comboBox_Tournament_8_2,
                                comboBox_Tournament_4_1);

                UpdateNextRound(comboBox_Tournament_8_3, textBox_point_comboBox_Tournament_8_3,
                                comboBox_Tournament_8_4, textBox_point_comboBox_Tournament_8_4,
                                comboBox_Tournament_4_2);

                UpdateNextRound(comboBox_Tournament_8_5, textBox_point_comboBox_Tournament_8_5,
                                comboBox_Tournament_8_6, textBox_point_comboBox_Tournament_8_6,
                                comboBox_Tournament_4_3);

                UpdateNextRound(comboBox_Tournament_8_7, textBox_point_comboBox_Tournament_8_7,
                                comboBox_Tournament_8_8, textBox_point_comboBox_Tournament_8_8,
                                comboBox_Tournament_4_4);
            }

            if (stage >= 4)
            {
                UpdateNextRound(comboBox_Tournament_4_1, textBox_point_comboBox_Tournament_4_1,
                                comboBox_Tournament_4_2, textBox_point_comboBox_Tournament_4_2,
                                comboBox_Tournament_2_1);

                UpdateNextRound(comboBox_Tournament_4_3, textBox_point_comboBox_Tournament_4_3,
                                comboBox_Tournament_4_4, textBox_point_comboBox_Tournament_4_4,
                                comboBox_Tournament_2_2);
            }

            if (stage >= 2)
            {
                UpdateNextRound(comboBox_Tournament_2_1, textBox_point_comboBox_Tournament_2_1,
                                comboBox_Tournament_2_2, textBox_point_comboBox_Tournament_2_2,
                                comboBox_Tournament_Chempion);
            }
            if (comboBox_Tournament_Chempion.SelectedItem != null)
            {
                string winnerTeam = comboBox_Tournament_Chempion.SelectedItem.ToString();
                string tournamentName = GetTournamentName(ClassGlobal.TournamentID);
                SaveTournamentWinner(tournamentName, winnerTeam);
                DeleteTournament(ClassGlobal.TournamentID);
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

            MessageBox.Show($"Чемпион: {teamName}!");
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

        private void UpdateNextRound(ComboBox team1, TextBox score1, ComboBox team2, TextBox score2, ComboBox nextRound)
        {
            if (int.TryParse(score1.Text, out int scoreTeam1) && int.TryParse(score2.Text, out int scoreTeam2))
            {
                if (scoreTeam1 > scoreTeam2)
                {
                    nextRound.Items.Clear();
                    nextRound.Items.Add(team1.SelectedItem);
                    nextRound.SelectedIndex = 0;
                }
                else if (scoreTeam2 > scoreTeam1)
                {
                    nextRound.Items.Clear();
                    nextRound.Items.Add(team2.SelectedItem);
                    nextRound.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Тең нәтиже! Жеңімпазды көрсетіңіз.");
                }
            }
            else
            {
            }
        }


    }
}
