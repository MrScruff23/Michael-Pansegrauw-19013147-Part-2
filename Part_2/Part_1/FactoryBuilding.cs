using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part_1
{
    [Serializable]
    class FactoryBuilding : Building
    {
        // indicates what type of unit the building will spawn
        public enum unitType
        {
            MeeleeUnit,
            RangedUnit
        }

        private unitType typeOfUnit;
        private int productionSpeed; 
        private bool spawnpointAbove; // indicates whether the spawn point is above or not

        public int ProductionSpeed
        {
            get { return productionSpeed; }
        }

        // constructor      
        public FactoryBuilding(int xPos, int yPos, int health, int team, int productionSpeed)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            this.health = health;
            this.maxHealth = health;
            this.team = team;
            this.symbol = "F";
            this.productionSpeed = productionSpeed;
            if (YPos < 20)
            {
                spawnpointAbove = false;
            } else
            {
                spawnpointAbove = true;
            }
        }

        public override void Destroy()
        {
            if (Health <= 0)
            {
                symbol = "D";
            }
        }

        // creates a unit and assigns the nessesary atributes to the units
        public Unit CreateUnit()
        {
            if (typeOfUnit == unitType.MeeleeUnit) // deturmines whether a unit is a ranged unit or a meelee unit
            {
                return new MeeleeUnit(xPos, yPos, 60.0, 10.0, 1, team);
            }
            else
            {
                return new RangedUnit(xPos, yPos, 35.0, 7.0, 3, 1, team);
            }
        }

        public override string ToString()
        {
            string str = string.Format(
                    "Position (x, y): (" + xPos + ", " + yPos + ")" +
                    "\nHealth\\MaxHealth : " + health + "\\" + maxHealth +
                    "\nProduction Speed: " + productionSpeed +
                    "\nType of unit: " + productionSpeed.ToString() +
                    "\nSpawn-point: {0}" +
                    "\n + Team : " + team + " + " +
                    "\nSymbol: " + symbol, (spawnpointAbove) ? "above": "bellow" );
            return str;
        }
    }
}
