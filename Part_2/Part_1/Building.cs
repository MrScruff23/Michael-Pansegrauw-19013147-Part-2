using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part_1
{
    [Serializable]
    abstract class Building
    {
        // variable declaration 
        protected int xPos = 0; // x position of the unit
        protected int yPos = 0; // y position of the unit
        protected int health; // current health points of the unit
        protected int maxHealth; // maximum health point the unit can have
        protected int team; // intiger that tells what team a unit is on. It is used to tell whether a unit is an enemy or not
        protected string symbol; // the symbol used to visually represent the unit on the screen

        // properties useds to access variables
        public int XPos
        {
            get { return xPos; }
            set { xPos = value; }
        }
        public int YPos
        {
            get { return yPos; }
            set { yPos = value; }
        }
        public int Health
        {
            get { return health; }
            set { health = value; }
        }
        public int MaxHealth
        {
            get { return maxHealth; }
        }
        public int Team     
        {
            get { return team; }
        }
        public string Symbol
        {
            get { return symbol; }
        }


        public abstract void Destroy();
        public abstract override string ToString();

        public void save() //for saving
        {

        }

    }
}
