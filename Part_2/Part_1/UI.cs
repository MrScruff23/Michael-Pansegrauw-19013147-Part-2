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
        public int rounds = 0; // variable to keep track of the number of rounds that have passed
        // variables to keep track of the amount of resources 
        private int team1resources = 0;
        private int team2resources = 0;

        public UI()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnStop_Click(object sender, EventArgs e) // When the stop button is clicked the timer will bet stopped to stop the animation
        {
            timer1.Enabled = false;
        }

        private void Timer1_Tick(object sender, EventArgs e) // each tick of the timer will triger a 'round' in the game engine 
        {
            GameEngine.Round();
        }

        private void BtnStart_Click(object sender, EventArgs e) // When the start button is clicked the timer will bet resumed to resume the animation
        {
            timer1.Enabled = true;
        }

        public void RoundUpdate(int rounds) // updates the number of rounds that have passed
        {
            this.rounds = rounds;
            lblRound.Text = "Round: " + rounds;
        }

        // updates the display of the amount of resources and the variable that keep tracks of the resources
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

        // calls save method for the game
        private void btnSave_Click(object sender, EventArgs e)
        {
            GameEngine.map.Save();
        }

        // calls load method for the game
        private void btnLoad_Click(object sender, EventArgs e)
        {
            GameEngine.map.Load();
        }

        // ignore
        private void grbMap_Enter(object sender, EventArgs e)
        {

        }
    }
}
