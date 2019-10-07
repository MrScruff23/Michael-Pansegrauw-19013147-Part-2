using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part_1
{
    [Serializable]
    class ResourceBuilding : Building
    {
        // enum for resource type
        public enum ResourceType
        {
            Minerals,
            VespienGas
        }

        private ResourceType resourceType;
        private int resourcesGenerated = 0;
        private int resourcesPerRound;
        private int resourcesRemaining;
    /*
        public string ResourceType
        {
            get { return resourceType; }
        }
        public int ResourcesGenerated
        {
            get { return resourcesGenerated; }
        }
        public int ResourcesPerRound
        {
            get { return resourcesPerRound; }
        }
        public int ResourcesRemaining
        {
            get { return resourcesRemaining; }
        }
        */


        // resources per round has a default value incase no alternative value is given to resourcesPerRound
        public ResourceBuilding(int xPos, int yPos, int health, int team, int resourcesRemaining, int resourcesPerRound = 10, ResourceType resourceType = ResourceType.Minerals) 
        {
            this.xPos = xPos;
            this.yPos = yPos;
            this.health = health;
            this.maxHealth = health;
            this.team = team;
            this.symbol = "R";
            this.resourcesRemaining = resourcesRemaining;
            this.resourcesPerRound = resourcesPerRound;
            this.resourceType = resourceType;
        }

        // checks if the unit is destroyed or if it needs to be destroyed
        public override void Destroy()
        {
            if (health <= 0)
            {
                symbol = "D";
                health = 0;
            }
        }

        // to string to display information
        public override string ToString()
        {
            string str = string.Format(
                    "Position (x, y): (" + xPos + ", " + yPos + ")" +
                    "\nHealth\\MaxHealth : " + health + "\\" + maxHealth +
                    "\nresources per round: " + resourcesPerRound +
                    "\nResources remaining: " + resourcesRemaining +
                    "\nResources Generated" + resourcesGenerated +
                    "\n + Team : " + team + " + " +
                    "\nResource type: " + resourceType +
                    "\nSymbol: " + Symbol);
            return str;
        }

        // adds the resource to the main resources colection place
        public void GenerateResources() 
        {
            int Temp = resourcesGenerated;
            if (Health > 0)
            {
                if (resourcesRemaining >= resourcesPerRound)
                {
                    resourcesGenerated += resourcesPerRound;
                    resourcesRemaining -= resourcesPerRound;
                } else if (resourcesRemaining > 0)
                {
                    resourcesGenerated += resourcesRemaining;
                    resourcesRemaining = 0;
                }
            }
            Temp = resourcesGenerated - Temp; // indicates how many resources are accumulated during this phase
            Program.UI.ResourcesUpdate(Team, Temp);
        }
    }
}
