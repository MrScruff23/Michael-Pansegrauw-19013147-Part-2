using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Part_1
{
    public partial class UI : Form
    {
        public int rounds = 0;
        private int team1resources = 0;
        private int team2resources = 0;

        public UI()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            GameEngine.Round();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        public void RoundUpdate(int rounds)
        {
            this.rounds = rounds;
            lblRound.Text = "Round: " + rounds;
        }

        public void ResourcesUpdate(int team, int resources)
        {
            if (team == 0)
            {
                team1resources += resources;
                lblTeam1resources.Text = "Team 1 resources: " + team1resources;
            }
            else if (team == 1)
            {
                team2resources += resources;
                lblTeam2resources.Text = "Team 2 resources: " + team2resources;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GameEngine.map.Save();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            GameEngine.map.Load();
        }
    }
}
