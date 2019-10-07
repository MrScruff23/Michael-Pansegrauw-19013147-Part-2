using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part_1
{
    /*
    protected int xPos = 0;
    protected int yPos = 0;
    protected int health;
    protected int maxHealth;
    protected int attack;
    protected int attackRange;
    protected int team;
    protected bool isAttacking;
    */
    [Serializable]
    class RangedUnit : Unit
    {
        // properties  to access variables
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

        // constructor to assign variable values to atribute of the object
        public RangedUnit(int xPos, int yPos, double maxHealth, double attack, int range, int speed, int team)
        {
            base.xPos = xPos;
            base.yPos = yPos;
            base.maxHealth = maxHealth;
            base.health = maxHealth;
            base.attack = attack;
            base.team = team;
            base.attackRange = range;
            base.isAttacking = false;
            base.speed = speed;
            base.symbol = "<>";
            base.name = "Roach";
        }

        // method to make the unit take damage
        public override void Combat(double damage)
        {
            health -= damage;
        }

        public override bool DestroyUnit() // handles destruction and death of unit
        {
            if (health <= 0)
            {
                symbol = "X";
                return true;
            }
            return false;
        }

        public override Unit FindClosestUnit(List<ButtonUnit> listOfUnits) // Finds the clossest enemy that is not on the same team as the unit
        {
            int distance = -1;
            Unit enemy = null;
            foreach (ButtonUnit b in listOfUnits)
            {
                Unit u = b.Unit;
                if (u.Health > 0)
                    if (u.Team != base.team)
                    {
                        int temp = Convert.ToInt32(Math.Sqrt(Math.Pow((u.XPos- xPos), 2) + Math.Pow((u.YPos - yPos), 2)));
                        if (temp >= distance)
                        {
                            distance = temp;
                            enemy = u;
                        }
                    }
            }
            return enemy;
        }

        public override bool IsInRange(Unit u)// ddeturmines whether a unit is in range of the unit or not
        {
            try
            {
                int distance = Convert.ToInt32(Math.Sqrt(Math.Pow((u.XPos - xPos), 2) + Math.Pow((u.YPos - yPos), 2)));
                return (distance <= attackRange) ? true : false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        // Move() is done like this for future proofing so that if a special ability is added it can be easily implimented
        public override void Move(Direction d, int distance)
        {
            try
            {
                GameEngine.map.map[xPos, yPos] = null;
                switch (d)
                {
                    case Direction.Up:
                        if (yPos - distance >= 0)
                        {
                            yPos -= distance;
                        }
                        else
                        {
                            yPos = 0;
                        }
                        break;
                    case Direction.Down:
                        if (yPos + distance < 20)
                        {
                            yPos += distance;
                        }
                        else
                        {
                            yPos = 19;
                        }
                        break;
                    case Direction.Left:
                        if (xPos - distance >= 0)
                        {
                            xPos -= distance;
                        }
                        else
                        {
                            xPos = 0;
                        }
                        break;
                    case Direction.Right:
                        if (xPos + distance < 20)
                        {
                            xPos += distance;
                        }
                        else
                        {
                            xPos = 19;
                        }
                        break;
                    default:
                        break;
                }
                GameEngine.map.map[xPos, yPos] = this;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public override string ToString() // displays unit information in a neat format
        {
            try
            {
                string s = string.Format(
                    "Name : " + name + 
                    "\nPosition (x, y): (" + xPos + ", " + yPos + ")" +
                    "\nHealth\\MaxHealth : " + health + "\\" + maxHealth +
                    "\nAttack : " + attack +
                    "\nAttack Range : " + attackRange +
                    "\n + Team : " + (team + 1)  + " +" +
                    "\nIs attacking : " + isAttacking);
                return s;
            }
            catch (Exception ex)
            {
                return "Error Formating ToString : " + ex;
            }
        }

        // record the direction the unit needs to move inorder to get closser to the unit
        public override Direction DirectionOfEnemy(Unit enemy)
        {
            int xdis = enemy.XPos - xPos;
            int ydis = enemy.YPos - yPos;
            if (Math.Abs(xdis) > Math.Abs(ydis))
            {
                if (xdis > 0)
                {
                    return Direction.Right;
                }
                else
                {
                    return Direction.Left;
                }
            }
            else
            {
                if (ydis > 0)
                {
                    return Direction.Down;
                }
                else
                {
                    return Direction.Up;
                }
            }

        }
    }
}
