namespace eSportsTournament
{
    partial class FormUser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUser));
            this.label3 = new System.Windows.Forms.Label();
            this.FormUser_Button_Exit = new System.Windows.Forms.Button();
            this.FormUser_Button_CreateTeam = new System.Windows.Forms.Button();
            this.FormUser_TextBox_TeamName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.FormUser_Button_JoinTeam = new System.Windows.Forms.Button();
            this.FormUser_dataGridView_Teams = new System.Windows.Forms.DataGridView();
            this.FormUser_dataGridView_Tournaments = new System.Windows.Forms.DataGridView();
            this.FormUser_Button_JoinTournaments = new System.Windows.Forms.Button();
            this.FormUser_dataGridView_Users = new System.Windows.Forms.DataGridView();
            this.FormUser_dataGridView_TournamentStandings = new System.Windows.Forms.DataGridView();
            this.FormUser_Button_ExitTeam = new System.Windows.Forms.Button();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.FormCaptain_dataGridView_TeamRequests = new System.Windows.Forms.DataGridView();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormUser_dataGridView_Teams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormUser_dataGridView_Tournaments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormUser_dataGridView_Users)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormUser_dataGridView_TournamentStandings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormCaptain_dataGridView_TeamRequests)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(341, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 35);
            this.label3.TabIndex = 54;
            this.label3.Text = "USER";
            // 
            // FormUser_Button_Exit
            // 
            this.FormUser_Button_Exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.FormUser_Button_Exit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.FormUser_Button_Exit.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormUser_Button_Exit.ForeColor = System.Drawing.Color.Black;
            this.FormUser_Button_Exit.Location = new System.Drawing.Point(12, 12);
            this.FormUser_Button_Exit.Name = "FormUser_Button_Exit";
            this.FormUser_Button_Exit.Size = new System.Drawing.Size(42, 36);
            this.FormUser_Button_Exit.TabIndex = 53;
            this.FormUser_Button_Exit.Text = "<";
            this.FormUser_Button_Exit.UseVisualStyleBackColor = false;
            this.FormUser_Button_Exit.Click += new System.EventHandler(this.FormUser_Button_Exit_Click);
            // 
            // FormUser_Button_CreateTeam
            // 
            this.FormUser_Button_CreateTeam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.FormUser_Button_CreateTeam.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.FormUser_Button_CreateTeam.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormUser_Button_CreateTeam.Location = new System.Drawing.Point(520, 404);
            this.FormUser_Button_CreateTeam.Name = "FormUser_Button_CreateTeam";
            this.FormUser_Button_CreateTeam.Size = new System.Drawing.Size(246, 34);
            this.FormUser_Button_CreateTeam.TabIndex = 48;
            this.FormUser_Button_CreateTeam.Text = "Құру";
            this.FormUser_Button_CreateTeam.UseVisualStyleBackColor = false;
            this.FormUser_Button_CreateTeam.Click += new System.EventHandler(this.FormUser_Button_CreateTeam_Click);
            // 
            // FormUser_TextBox_TeamName
            // 
            this.FormUser_TextBox_TeamName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FormUser_TextBox_TeamName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FormUser_TextBox_TeamName.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormUser_TextBox_TeamName.ForeColor = System.Drawing.SystemColors.Window;
            this.FormUser_TextBox_TeamName.Location = new System.Drawing.Point(252, 404);
            this.FormUser_TextBox_TeamName.Name = "FormUser_TextBox_TeamName";
            this.FormUser_TextBox_TeamName.Size = new System.Drawing.Size(262, 34);
            this.FormUser_TextBox_TeamName.TabIndex = 46;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(-19, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(846, 20);
            this.label2.TabIndex = 45;
            this.label2.Text = "_________________________________________________________________________________" +
    "____________";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.Image = global::eSportsTournament.Properties.Resources.icons8_игроки;
            this.pictureBox4.Location = new System.Drawing.Point(520, 63);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(73, 55);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 44;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Image = global::eSportsTournament.Properties.Resources.icons8_группы_Команда;
            this.pictureBox3.Location = new System.Drawing.Point(347, 63);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(73, 55);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 43;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::eSportsTournament.Properties.Resources.icons8_режим_турнира;
            this.pictureBox2.Location = new System.Drawing.Point(188, 63);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(73, 55);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 42;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::eSportsTournament.Properties.Resources.icons8_таблица_лидеров;
            this.pictureBox1.Location = new System.Drawing.Point(30, 63);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(73, 55);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 41;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // FormUser_Button_JoinTeam
            // 
            this.FormUser_Button_JoinTeam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.FormUser_Button_JoinTeam.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.FormUser_Button_JoinTeam.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormUser_Button_JoinTeam.Location = new System.Drawing.Point(30, 404);
            this.FormUser_Button_JoinTeam.Name = "FormUser_Button_JoinTeam";
            this.FormUser_Button_JoinTeam.Size = new System.Drawing.Size(111, 34);
            this.FormUser_Button_JoinTeam.TabIndex = 55;
            this.FormUser_Button_JoinTeam.Text = "Қосылу";
            this.FormUser_Button_JoinTeam.UseVisualStyleBackColor = false;
            this.FormUser_Button_JoinTeam.Click += new System.EventHandler(this.FormUser_Button_JoinTeam_Click);
            // 
            // FormUser_dataGridView_Teams
            // 
            this.FormUser_dataGridView_Teams.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.FormUser_dataGridView_Teams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FormUser_dataGridView_Teams.Location = new System.Drawing.Point(30, 160);
            this.FormUser_dataGridView_Teams.Name = "FormUser_dataGridView_Teams";
            this.FormUser_dataGridView_Teams.RowHeadersWidth = 51;
            this.FormUser_dataGridView_Teams.RowTemplate.Height = 24;
            this.FormUser_dataGridView_Teams.Size = new System.Drawing.Size(736, 238);
            this.FormUser_dataGridView_Teams.TabIndex = 56;
            // 
            // FormUser_dataGridView_Tournaments
            // 
            this.FormUser_dataGridView_Tournaments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.FormUser_dataGridView_Tournaments.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.FormUser_dataGridView_Tournaments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FormUser_dataGridView_Tournaments.Location = new System.Drawing.Point(30, 160);
            this.FormUser_dataGridView_Tournaments.Name = "FormUser_dataGridView_Tournaments";
            this.FormUser_dataGridView_Tournaments.RowHeadersWidth = 51;
            this.FormUser_dataGridView_Tournaments.RowTemplate.Height = 24;
            this.FormUser_dataGridView_Tournaments.Size = new System.Drawing.Size(736, 238);
            this.FormUser_dataGridView_Tournaments.TabIndex = 57;
            // 
            // FormUser_Button_JoinTournaments
            // 
            this.FormUser_Button_JoinTournaments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.FormUser_Button_JoinTournaments.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.FormUser_Button_JoinTournaments.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormUser_Button_JoinTournaments.Location = new System.Drawing.Point(30, 404);
            this.FormUser_Button_JoinTournaments.Name = "FormUser_Button_JoinTournaments";
            this.FormUser_Button_JoinTournaments.Size = new System.Drawing.Size(736, 34);
            this.FormUser_Button_JoinTournaments.TabIndex = 58;
            this.FormUser_Button_JoinTournaments.Text = "Қосылу";
            this.FormUser_Button_JoinTournaments.UseVisualStyleBackColor = false;
            this.FormUser_Button_JoinTournaments.Click += new System.EventHandler(this.FormUser_Button_JoinTournaments_Click);
            // 
            // FormUser_dataGridView_Users
            // 
            this.FormUser_dataGridView_Users.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.FormUser_dataGridView_Users.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.FormUser_dataGridView_Users.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FormUser_dataGridView_Users.Location = new System.Drawing.Point(30, 160);
            this.FormUser_dataGridView_Users.Name = "FormUser_dataGridView_Users";
            this.FormUser_dataGridView_Users.RowHeadersWidth = 51;
            this.FormUser_dataGridView_Users.RowTemplate.Height = 24;
            this.FormUser_dataGridView_Users.Size = new System.Drawing.Size(736, 278);
            this.FormUser_dataGridView_Users.TabIndex = 59;
            // 
            // FormUser_dataGridView_TournamentStandings
            // 
            this.FormUser_dataGridView_TournamentStandings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.FormUser_dataGridView_TournamentStandings.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.FormUser_dataGridView_TournamentStandings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FormUser_dataGridView_TournamentStandings.Location = new System.Drawing.Point(30, 160);
            this.FormUser_dataGridView_TournamentStandings.Name = "FormUser_dataGridView_TournamentStandings";
            this.FormUser_dataGridView_TournamentStandings.RowHeadersWidth = 51;
            this.FormUser_dataGridView_TournamentStandings.RowTemplate.Height = 24;
            this.FormUser_dataGridView_TournamentStandings.Size = new System.Drawing.Size(736, 278);
            this.FormUser_dataGridView_TournamentStandings.TabIndex = 60;
            // 
            // FormUser_Button_ExitTeam
            // 
            this.FormUser_Button_ExitTeam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.FormUser_Button_ExitTeam.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.FormUser_Button_ExitTeam.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormUser_Button_ExitTeam.Location = new System.Drawing.Point(147, 404);
            this.FormUser_Button_ExitTeam.Name = "FormUser_Button_ExitTeam";
            this.FormUser_Button_ExitTeam.Size = new System.Drawing.Size(99, 34);
            this.FormUser_Button_ExitTeam.TabIndex = 61;
            this.FormUser_Button_ExitTeam.Text = "Шығу";
            this.FormUser_Button_ExitTeam.UseVisualStyleBackColor = false;
            this.FormUser_Button_ExitTeam.Click += new System.EventHandler(this.FormUser_Button_ExitTeam_Click);
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::eSportsTournament.Properties.Resources.icons8_приглашение;
            this.pictureBox5.Location = new System.Drawing.Point(693, 63);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(73, 55);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 62;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.Click += new System.EventHandler(this.pictureBox5_Click);
            // 
            // FormCaptain_dataGridView_TeamRequests
            // 
            this.FormCaptain_dataGridView_TeamRequests.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.FormCaptain_dataGridView_TeamRequests.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.FormCaptain_dataGridView_TeamRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FormCaptain_dataGridView_TeamRequests.Location = new System.Drawing.Point(30, 160);
            this.FormCaptain_dataGridView_TeamRequests.Name = "FormCaptain_dataGridView_TeamRequests";
            this.FormCaptain_dataGridView_TeamRequests.RowHeadersWidth = 51;
            this.FormCaptain_dataGridView_TeamRequests.RowTemplate.Height = 24;
            this.FormCaptain_dataGridView_TeamRequests.Size = new System.Drawing.Size(736, 278);
            this.FormCaptain_dataGridView_TeamRequests.TabIndex = 63;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::eSportsTournament.Properties.Resources.icons8_Кубок;
            this.pictureBox6.Location = new System.Drawing.Point(285, 211);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(206, 160);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox6.TabIndex = 85;
            this.pictureBox6.TabStop = false;
            // 
            // FormUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.FormCaptain_dataGridView_TeamRequests);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.FormUser_Button_ExitTeam);
            this.Controls.Add(this.FormUser_dataGridView_TournamentStandings);
            this.Controls.Add(this.FormUser_dataGridView_Users);
            this.Controls.Add(this.FormUser_Button_JoinTournaments);
            this.Controls.Add(this.FormUser_dataGridView_Tournaments);
            this.Controls.Add(this.FormUser_dataGridView_Teams);
            this.Controls.Add(this.FormUser_Button_JoinTeam);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.FormUser_Button_Exit);
            this.Controls.Add(this.FormUser_Button_CreateTeam);
            this.Controls.Add(this.FormUser_TextBox_TeamName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormUser";
            this.Load += new System.EventHandler(this.FormUser_Load);
            this.Click += new System.EventHandler(this.FormUser_Click);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormUser_dataGridView_Teams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormUser_dataGridView_Tournaments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormUser_dataGridView_Users)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormUser_dataGridView_TournamentStandings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormCaptain_dataGridView_TeamRequests)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button FormUser_Button_Exit;
        private System.Windows.Forms.Button FormUser_Button_CreateTeam;
        public System.Windows.Forms.TextBox FormUser_TextBox_TeamName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button FormUser_Button_JoinTeam;
        private System.Windows.Forms.DataGridView FormUser_dataGridView_Teams;
        private System.Windows.Forms.DataGridView FormUser_dataGridView_Tournaments;
        private System.Windows.Forms.Button FormUser_Button_JoinTournaments;
        private System.Windows.Forms.DataGridView FormUser_dataGridView_Users;
        private System.Windows.Forms.DataGridView FormUser_dataGridView_TournamentStandings;
        private System.Windows.Forms.Button FormUser_Button_ExitTeam;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.DataGridView FormCaptain_dataGridView_TeamRequests;
        private System.Windows.Forms.PictureBox pictureBox6;
    }
}