using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18010755__Gregory_John_Bull__GADE5112__POE_Task_1
{
    enum FactoryType
    {
        RANGED,
        MELEE
    }
    class FactoryBuildin : Building
    {
        

        private FactoryType type;
        private int productionSpeed;
        private int SpawnY;

        public FactoryBuildin(int x, int y, string faction) : base(x, y, 100, faction, 'B')
        {
            if(y >= Map.SIZE - 1)
            {
                SpawnY = y - 1;
            }
            else
            {
                SpawnY = y + 1; 
            }
            type = (FactoryType)GameEngine.random.Next(0, 2);
            productionSpeed = GameEngine.random.Next(3, 7);
        }

        public FactoryBuildin(string values)
        {
            string[] parameters = values.Split(',');

            X = int.Parse(parameters[1]);
            Y = int.Parse(parameters[2]);
            health = int.Parse(parameters[3]);
            maxHealth = int.Parse(parameters[4]);
            type = (FactoryType)int.Parse(parameters[5]);
            productionSpeed = int.Parse(parameters[6]);
            SpawnY = int.Parse(parameters[7]);
            faction = parameters[8];
            symbol = parameters[9][0];
            destroyed = parameters[10] == "True" ? true : false;
        }

        public int ProductionSpeed
        {
            get { return productionSpeed; }
        }

        public override void Destroy()
        {
            destroyed = true;
            symbol = '_';
        }

        public override string Save()
        {
            return string.Format($"Resources, {x},{y},{health}, {maxHealth},{(int)type}," +
                $"{productionSpeed},{SpawnY}," +
                $"{faction},{symbol},{destroyed}"
                );
        }
        public Unit SpawnUnit()
        {
            Unit unit;
            if(type == FactoryType.MELEE)
            {
                unit = new MeleeUnit(x, SpawnY, faction);

            }
            else
            {
                unit = new RangedUnit(x, SpawnY, faction);
            }
            return unit;
        }

        private string GetFactoryTypeName()
        {
            return new string[] { "Melee", "Ranged" }[(int)type];
        }

        public override string ToString()
        {
            return
                "_____________________" + Environment.NewLine +
                "Factory Building (" + symbol + "/" + faction[0] + ")" + Environment.NewLine +
                "_____________________" + Environment.NewLine +
                "Type: " + GetFactoryTypeName() + Environment.NewLine +
                base.ToString() + Environment.NewLine;
               
        }
    }//
}//
