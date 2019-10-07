using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Part_1
{
    static class GameEngine
    {

        public static int rounds = 0; // varaible to keep track of the amount of rounds that have passed
        static int numOfEnemies = 12; // variable to dectate the amount of units that need to be spawned
        public static Map map; // creates an object of the map class
        static bool initialised = false; // tells the class if variables are initialised before trying to use them

        // method runs every round and perfirms the nessesary actions that are needed to be done every round
        public static void Round()
        {
            if (initialised == false)
            {
                map = new Map(numOfEnemies);
                initialised = true;
            }
            // Unit round actions ***************************************************************************************************************************
            foreach (ButtonUnit b in map.unitButton)
            {
                Unit u = b.Unit;
                if (u.Health > 0)  // checks if the unit is alive or not
                {
                    Unit unit;
                    if (u.GetType() == typeof(MeeleeUnit)) //  gets the type of unit
                    {
                        unit = u as MeeleeUnit;
                    }
                    else
                    {
                        unit = u as RangedUnit;
                    }

                    Unit enemy = unit.FindClosestUnit(map.unitButton);
                    if (enemy != null) // checks the type of unit and if there is a unit
                        if (enemy.GetType() == typeof(MeeleeUnit))
                        {
                            enemy = enemy as MeeleeUnit;
                        }
                        else
                        {
                            enemy = enemy as RangedUnit;
                        }

                    if (!unit.DestroyUnit()) // checks if the unit is alive
                    {
                        if (unit.Health / unit.MaxHealth >= 0.25)
                        {
                            if (unit.IsInRange(enemy)) // checks if the unit is in range of the enemy
                            {
                                try
                                {
                                    unit.IsAttacking = true;
                                    enemy.Combat(u.Attack);
                                    enemy.DestroyUnit();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                            }
                            else
                            {
                                try
                                {
                                    unit.IsAttacking = false;
                                    unit.Move(unit.DirectionOfEnemy(enemy), 1);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                            }
                        }
                        else
                        {
                            unit.IsAttacking = false;
                            Random r = new Random();
                            u.Move((Direction)r.Next(), 1);
                        }
                    }
                    unit.DestroyUnit();
                }
            }

            // building round actions ***************************************************************************************************************************
            foreach (ButtonBuilding b in map.buildingButton)
            {
                try
                {
                    Building building = b.Building;
                    if (building.GetType() == typeof(ResourceBuilding))
                    {
                        ResourceBuilding rb = building as ResourceBuilding;
                        rb.GenerateResources();
                    }
                    else if (building.GetType() == typeof(FactoryBuilding))
                    {
                        FactoryBuilding fb = building as FactoryBuilding;
                        if (rounds % fb.ProductionSpeed == 0)
                        {
                            map.AddUnit(fb.CreateUnit());
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            map.UpDatePosition();
            rounds++;
            Program.UI.RoundUpdate(rounds);
        }
        
        public static bool Save() // returns a boolean value for indication to whether the process was successful or not
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();

                // saving of units
                using (FileStream fs = new FileStream("unit.dat", FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    List<Unit> unitSaveList = new List<Unit>();
                    foreach (ButtonUnit b in map.unitButton)
                    {
                        unitSaveList.Add(b.Unit);
                    }
                    bf.Serialize(fs, unitSaveList);
                    Console.WriteLine("saved units!");
                }

                // saving of buildings
                using (FileStream fs = new FileStream("building.dat", FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    List<Building> buildingSaveList = new List<Building>();
                    foreach (ButtonBuilding b in map.buildingButton)
                    {
                        buildingSaveList.Add(b.Building);
                    }
                    bf.Serialize(fs, buildingSaveList);
                    Console.WriteLine("saved buildings!");
                }

                // saving of map
                using (FileStream fs = new FileStream("map.dat", FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    bf.Serialize(fs, map.map);
                    Console.WriteLine("saved map!");
                }
                return true;  // returns true if the process was successfull
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;  // returns false if process was un successful
            }
        }

        public static bool Load() // returns a boolean value for indication to whether the process was successful or not
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();

                // loading of units
                using (FileStream f = new FileStream("unit.dat", FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    List<Unit> unitLoadList = new List<Unit>();
                    unitLoadList = (List<Unit>)bf.Deserialize(f);
                    map.unitButton.Clear(); // clears all other objects
                    foreach (Unit u in unitLoadList)
                    {
                        map.AddUnit(u);
                    }
                    Console.WriteLine("unit Buttons loaded");
                }

                // loading of buildings
                using (FileStream f = new FileStream("building.dat", FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    List<Building> buildingLoadLiast = new List<Building>();
                    buildingLoadLiast = (List<Building>)bf.Deserialize(f);
                    map.buildingButton.Clear(); // clears all other objects
                    foreach (Building b in buildingLoadLiast)
                    {
                        map.AddBuilding(b);
                    }
                    Console.WriteLine("building Buttons loaded");
                }

                // loading of map
                using (FileStream f = new FileStream("map.dat", FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    map.map = (object[,])bf.Deserialize(f);
                    Console.WriteLine("map loaded");
                }

                // refreshes display
                map.DisplayAll();

                return true; // returns true if the process was successfull
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false; // returns false if process was un successful
            }
        }

    }
}
