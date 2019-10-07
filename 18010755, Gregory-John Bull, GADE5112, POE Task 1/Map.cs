using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _18010755__Gregory_John_Bull__GADE5112__POE_Task_1
{
    class Map
    {
        public const int SIZE = 20;
        Unit[] units;
        Building[] buildings;

        string[,] map;
        string[] factions = { "Exo-Team", "Alt-Team" };

        int numUnits;
        int numBuildings;

        public Map(int numUnits, int numBuildings)
        {
            this.numUnits = numUnits;
            this.numBuildings = numBuildings;

            Reset();
        }

        public Unit[] Units
        {
            get { return units; }
        }
        
        public Building[] Buildings
        {
            get { return buildings; }
        }  
        
        public int Size
        {
            get { return SIZE; }
        }

        private void InitializeUnits()
        {
            units = new Unit[numUnits];

            for(int i = 0; i < units.Length; i++)
            {
                int x = GameEngine.random.Next(0, SIZE);
                int y = GameEngine.random.Next(0, SIZE);
                int factionIndex = GameEngine.random.Next(0, 2);
                int buildingType = GameEngine.random.Next(0, 2);

                while(map[x,y] != null)
                {
                    x = GameEngine.random.Next(0, SIZE);
                    y = GameEngine.random.Next(0, SIZE);
                }
                if(unitType == 0)
                {
                    units[i] = new MeleeUnit(x, y, factions[factionIndex]);
                }
                else
                {
                    units[i] = new RangedUnit(x, y, factions[factionIndex]);
                }
                map[x, y] = units[i].Faction[0] + "/" + units[i].Symbol;
            }
        }

        private void InitializeBuildings()
        {
            buildings = new Building[numBuildings];

            for(int i  = 0; i < buildings.Length; i++)
            {
                int x = GameEngine.random.Next(0, SIZE);
                int y = GameEngine.random.Next(0, SIZE);
                int factionIndex = GameEngine.random.Next(0, 2);
                int buildingType = GameEngine.random.Next(0, 2);

                while(map[x, y] != null)
                {
                    x = GameEngine.random.Next(0, SIZE);
                    y = GameEngine.random.Next(0, SIZE);
                }
                if(buildingType == 0)
                {
                    buildings[i] = new ResourceBuilding(x, y, factions[factionIndex]);

                }
                else
                {
                    buildings[i] = new FactoryBuildin(x, y, factions[factionIndex]);
                }
                map[x, y] = buildings[i].Faction[0] + "/" + buildings[i].Symbol;
            }
        }

        public void AddUnit(Unit unit)
        {
            Unit[] resizeUnits = new Unit[units.Length + 1];
            for (int i = 0; i < units.Length; i++)
            {
                resizeUnits[i] = units[i];
            }
            resizeUnits[resizeUnits.Length - 1] = unit;
            units = resizeUnits;
        }
        public void AddBuilding (Building building)
        {
            Array.Resize(ref buildings, building.Length + 1);
            Buildings[buildings.Length - 1] = building;
        }
        public void UpdateMap()
        {
            for (int y = 0; y < SIZE; y++)
            {
                for (int x = 0; x < SIZE; x++)
                {
                    map[x, y] = "  ";
                }
            }
            foreach (Unit unit in units)
            {
                map[unit.X, unit.Y] = unit.Symbol + "|" + unit.Faction[0];
            }
            foreach (Building building in buildings)
            {
                map[building.x, building.y] = building.Symbol + "|" + building.Faction[0];
            }
        }

            public string GetMapDisplay()
            {
                string mapString = "";
                for (int y = 0; y < SIZE; y++)
                {
                    for (int x = 0; x < SIZE; x++)
                    {
                        mapString += map[x, y];
                    }
                }
                return mapString;
            }
        public void Clear()
        {
            units = new Unit[0];
            buildings = new Building[0];
        }

        public void Reset()
        {
            map = new string[SIZE, SIZE];
            InitializeUnits();
            InitializeBuildings();
            UpdateMap();
        }
        
    }//
}//
