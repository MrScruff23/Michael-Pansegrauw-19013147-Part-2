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
    class MeeleeUnit: Unit
    {
        public MeeleeUnit(int xPos, int yPos, double maxHealth, double attack, int speed, int team)
        {
            base.xPos = xPos;
            base.yPos = yPos;
            base.maxHealth = maxHealth;
            base.health = maxHealth;
            base.attack = attack;
            base.team = team;
            base.speed = speed;
            base.attackRange = 1;
            base.isAttacking = false;
            base.symbol = "O";
            base.name = "Zergling";
        }

        public override void Move(Direction d, int distance) // handles the movement of the unit
        {
            try
            {
                GameEngine.map.map[xPos, yPos] = this;
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
        
        public override bool DestroyUnit() // handles destruction and death of unit
        {
            if (health <= 0)
            {
                symbol = "X";
                return true;
            }
            return false;
        }

        public override string ToString() // displays unit information in a neat format
        {
            try
            {
                string s = string.Format(
                    "Position (x, y): (" + xPos + ", " + yPos + ")" +
                    "\nHealth\\MaxHealth : " + health + "\\" + maxHealth + 
                    "\nAttack : " + attack + 
                    "\nAttack Range : " + attackRange + 
                    "\n + Team : " + (team + 1) + " + " +
                    "\nIs attacking : " + isAttacking);
                return s;
            }
            catch (Exception ex)
            {
                return "Error Formating ToString :\n" + ex;
            }
        }

        public override void Combat(double damage)
        {
            health -= damage;
        }

        // checks of a unit is in range or not
        public override bool IsInRange(Unit u)
        {
            int distance = -1;
            try
            {
                distance = Convert.ToInt32(Math.Sqrt(Math.Pow((u.XPos - xPos), 2) + Math.Pow((u.YPos - yPos), 2))); // using pythagorus to deturmin the distance the unit is
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return (distance <= attackRange)? true: false;
        }

        // finds the unid that is clossest enemy unit to this unit
        public override Unit FindClosestUnit(List<ButtonUnit> listOfUnits)
        {
            int distance = -1;
            Unit enemy = null;
            foreach (ButtonUnit b in listOfUnits)
            {
                Unit u = b.Unit;
                if (u.Health > 0)
                    if (u.Team != team)
                    {
                        int temp = Convert.ToInt32(Math.Sqrt(Math.Pow((u.XPos - xPos), 2) + Math.Pow((u.YPos - yPos), 2)));
                        if (temp >= distance)
                        {
                            distance = temp;
                            enemy = u;
                        }
                    }
            }
            return enemy;
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
