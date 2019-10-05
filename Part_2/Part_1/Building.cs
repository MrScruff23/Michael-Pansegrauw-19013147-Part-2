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
        protected int xPos;
        protected int yPos;
        protected int health;
        protected int maxHealth;
        protected int team;
        protected string symbol;

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
