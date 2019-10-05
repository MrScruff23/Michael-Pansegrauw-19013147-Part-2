using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part_1
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        nul
    }

    [Serializable]
    abstract class Unit
    {
        protected int xPos = 0;
        protected int yPos = 0;
        protected double health;
        protected double maxHealth;
        protected double attack;
        protected double speed;
        protected double attackRange;
        protected int team;
        protected string symbol;
        protected bool isAttacking;
        protected string name;

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
        public double Health
        {
            get { return health; }
        }
        public double MaxHealth
        {
            get { return maxHealth; }
        }
        public double Attack
        {
            get { return attack; }
        }
        public double Speed
        {
            get { return speed; }
        }
        public double AttackRange
        {
            get { return attackRange; }
        }
        public double Team
        {
            get { return team; }
        }
        public string Symbol
        {
            get { return symbol; }
        }
        public bool IsAttacking
        {
            get { return isAttacking; }
            set { isAttacking = value; }
        }
        public string Name
        {
            get { return name; }
        }
        public abstract void Move(Direction d, int distance); // handles the movement of the unit
        public abstract void Combat(double damage); // handles the combat of the unit
        public abstract bool IsInRange(Unit u); // deturmins if a unit is in range for the unit to attack
        public abstract Unit FindClosestUnit(List<ButtonUnit> listOfUnits); // closest position to another unit; each co-ordinate will be separated by a semi-colon ";" and is going to be stored in the form of a string
        public abstract bool DestroyUnit(); // handles destruction and death of unit
        public abstract override string ToString(); // displays unit information in a neat format
        public abstract Direction DirectionOfEnemy(Unit enemy);

        public void save() // for saving
        {

        }
    }
}
