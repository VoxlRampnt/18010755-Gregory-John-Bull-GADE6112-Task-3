using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18010755__Gregory_John_Bull__GADE5112__POE_Task_1
{
    class ResourceBuilding : Building
    {
        
        private ResourceType type;
        private int generatedPerRound;
        private int generated;
        private int pool;
        

        public ResourceBuilding(int x, int y, string faction) : base(x, y, 100, faction, 'R')
        {
            generatedPerRound = GameEngine.random.Next(1, 6);
            generated = 0;
            pool = GameEngine.random.Next(100, 200);
            type = (ResourceType)GameEngine.random.Next(0, 3);
        }

        public ResourceBuilding(string values)
        {
            string[] parameters = values.Split(',');

            X = int.Parse(parameters[1]);
            Y = int.Parse(parameters[2]);
            health = int.Parse(parameters[3]);
            maxHealth = int.Parse(parameters[4]);
            type = (ResourceType) int.Parse(parameters[5]);
            generatedPerRound = int.Parse(parameters[6]);
            generated = int.Parse(parameters[7]);
            pool = int.Parse(parameters[8]);
            faction = parameters[9];
            symbol = parameters[10] [0];
            destroyed =parameters[11] == "True" ? true : false;
        }

        public override void Destroy()
        {
            destroyed = true;
            symbol = '_';
        }

        public override string Save()
        {
            return string.Format(
                $"Resources, {x},{y},{health}, {maxHealth},{(int)type}," +
                $"{generatedPerRound},{generated},{pool}," +
                $"{faction},{symbol},{destroyed}"
                );
        }

        public void GeneratedResources()
        {
            if (destroyed)
            {
                return;
            }
            if(pool > 0)
            {
                int resourcesGenerated = Math.Min(pool, generatedPerRound);
                generated += resourcesGenerated;
                pool += resourcesGenerated;
            }
        }

        private string GetResourceName()
        {
            return new string[] { "Gold", "Food", "Wood" }[(int)type];
        }

        public override string ToString()
        {
            return "_____________________" + Environment.NewLine +
                "Resource Building (" + "/" + faction[0] + ")" + Environment.NewLine +
                "_____________________" + Environment.NewLine +
                GetResourceName() + ":" + generated + Environment.NewLine +
                "Pool" + pool + Environment.NewLine +
                base.ToString() + Environment.NewLine;

        }
    }//
}//
