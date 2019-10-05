using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Part_1
{
    class Map
    {
        public object[,] map = new Unit[20, 20];
        public List<Unit> units = new List<Unit>();
        public List<ButtonUnit> unitButton = new List<ButtonUnit>();
        public List<ButtonBuilding> buildingButton = new List<ButtonBuilding>();

        Random rand = new Random();

        public Map(int numberOfBuildings)
        {
            int size = Program.UI.grbMap.Size.Height / 21;
            for (int i = 0; i < numberOfBuildings; i++)
            {
                buildingButton.Add(new ButtonBuilding(CreateBuilding(i % 2)));
                buildingButton[buildingButton.Count - 1].Text = buildingButton[buildingButton.Count - 1].Building.Symbol;
                buildingButton[buildingButton.Count - 1].ForeColor = (buildingButton[buildingButton.Count - 1].Building.Team == 0) ? Color.Blue : Color.Red;
                buildingButton[buildingButton.Count - 1].SetBounds(buildingButton[buildingButton.Count - 1].Building.XPos * size, buildingButton[buildingButton.Count - 1].Building.YPos * size, size, size);
            }
            DisplayAll();
        }

        // Both the type of unit, as well as their X and Y position, should be randomised; 
        // Meelee: int xPos, int yPos, double maxHealth, double attack, int team
        // Ranged: int xPos, int yPos, double maxHealth, double attack, int team
        public Building CreateBuilding(int team)
        {
            int xPos = rand.Next(0, 20);
            int yPos = rand.Next(0, 20);
            while (true)
            {
                if (map[xPos, yPos] == null)
                {
                    break;
                }
                xPos = rand.Next(0, 20);
                yPos = rand.Next(0, 20);
            }

            if (rand.NextDouble() < 0.5) // deturmines whether a unit is a ranged unit or a meelee unit
            {
                return new FactoryBuilding(xPos, yPos, 500, team, 3);
            }
            else
            {
                return new ResourceBuilding(xPos, yPos, 500, team, 500);
            }
        }

        public void DisplayAll()
        {
            foreach (Button butt in buildingButton)
            {
                Program.UI.grbMap.Controls.Add(butt);
            }
        }

        void button_Click(object sender, System.EventArgs e)
        {
            ButtonUnit butt = sender as ButtonUnit;
            Program.UI.txtUnitInfo.Text = butt.Unit.ToString();
        }

        public void UpDatePosition()
        {
            int Ysize = Program.UI.grbMap.Height / 20;
            int Xsize = Program.UI.grbMap.Width / 20;

            for (int i = 0; i < unitButton.Count; i++)
            {
                unitButton[i].SetBounds(unitButton[i].Unit.XPos * Xsize + 3, unitButton[i].Unit.YPos * Ysize + 3, Xsize, Ysize);
                unitButton[i].Text = unitButton[i].Unit.Symbol;
                unitButton[i].Refresh();
            }
        }

        public bool Save() // returns a boolean value for indication to whether the process was successful or not
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (FileStream fs = new FileStream("unit.dat", FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    bf.Serialize(fs, unitButton);
                    Console.WriteLine("saved units!");
                }
                using (FileStream fs = new FileStream("building.dat", FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    bf.Serialize(fs, buildingButton);
                    Console.WriteLine("saved buildings!");
                }
                using (FileStream fs = new FileStream("map.dat", FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    bf.Serialize(fs, buildingButton);
                    Console.WriteLine("saved buildings!");
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool Load() // returns a boolean value for indication to whether the process was successful or not
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (FileStream f = new FileStream("unit.dat", FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    unitButton = (List<ButtonUnit>)bf.Deserialize(f);
                    Console.WriteLine("unit Buttons loaded");
                }
                using (FileStream f = new FileStream("building.dat", FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    buildingButton = (List<ButtonBuilding>)bf.Deserialize(f);
                    Console.WriteLine("unit Buttons loaded");
                }
                using (FileStream f = new FileStream("map.dat", FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    map = (object[,])bf.Deserialize(f);
                    Console.WriteLine("unit Buttons loaded");
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public void AddUnit(Unit u)
        {
            unitButton.Add(new ButtonUnit(u));
            Program.UI.grbMap.Controls.Add(unitButton[unitButton.Count - 1]);
        }
    }

    // class that alows a button to save which unit it is holding
    [Serializable]
    class ButtonUnit : Button {
        public Unit Unit;

        public ButtonUnit(Unit Unit)
        {
            this.Unit = Unit;
        }
    }

    // class that alows a button to save which building it is holding
    [Serializable]
    class ButtonBuilding : Button
    {
        public Building Building;

        public ButtonBuilding(Building b)
        {
            this.Building = b;
        }
    }
}
