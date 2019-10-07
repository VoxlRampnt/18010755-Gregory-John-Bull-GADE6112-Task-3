using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace _18010755__Gregory_John_Bull__GADE5112__POE_Task_1
{
    class GameEngine
    {
        public static Random random = new Random(); // single random over all classes

        const string UNITS_FILENAME = "units.txt";
        const string BUILDINGS_FILENAME = "buildings.txt";
        const string ROUNDS_FILENAME = "rounds.txt";

        Map map;
        bool isGameOver = false;
        string winningFaction = "";
        int round = 0;

        public GameEngine()
        {
            map = new Map(10, 10);
        }

        public bool IsGameOver
        {
            get { return isGameOver; }
        }

        public string WinningFaction
        {
            get { return winningFaction; }
        }
        public int Round
        {
            get { return round; }
        }

        public string GetMapDisplay()
        {
            return map.GetMapDisplay();
        }

        public string GetUnitInfo()
        {
            string unitInfo = " ";
            foreach (Unit unit in map.Units)
            {
                unitInfo += unit + "\n";
            }
            return unitInfo;
        }

        public string GetBuildingInfo()
        {
            string builingsInfo = "";
            foreach (Building building in map.Buildings)
            {
                builingsInfo += building + Environment.NewLine;
            }
            return builingsInfo;
        }

       

        public void Reset()
        {
            map.Reset();
            isGameOver = false;
            round = 0;
        }

        public void GameLoop()
        {
            UpdateUnits();
            UpdateBuildings();
            map.UpdateMap();
            round++;
        }

        void UpdateBuildings() // updates buildings
        {
            foreach (Building buildings in map.Buildings)
            {
                if(buildings is FactoryBuildin)
                {
                    FactoryBuildin factoryBuildin = (FactoryBuildin) buildings;

                    if(round % factoryBuildin.ProductionSpeed == 0)
                    {
                        Unit newUnit = factoryBuildin.SpawnUnit();
                        map.AddUnit(newUnit);
                    }
                }
                else if(buildings is ResourceBuilding)
                {
                    ResourceBuilding resourceBuilding = (ResourceBuilding)buildings;
                    resourceBuilding.GeneratedResources();
                }
            }
        }

        void UpdateUnits()
        {
            foreach(Unit unit in map.Units)
            {
                //ignore this unit if dead
                if (unit.Destroyed)
                {
                    continue;
                }
                Unit closestUnit = unit.ClosestUnit(map.Units);
                if(closestUnit == null)
                {
                    // if unit has no target game is ended
                    isGameOver = true;
                    winningFaction = unit.Faction;
                    map.UpdateMap();
                    return;
                }
                double healthPercentage = unit.Health / unit.MaxHealth;
                if(healthPercentage <= 0.25)
                {
                    unit.RunAway();
                }
                else if (unit.AttackRange(closestUnit))
                {
                    unit.Combat(closestUnit);
                }
                else
                {
                    unit.Move(closestUnit);
                }
                StayInBounds(unit, map.Size);
            }
        }

        private void StayInBounds(Unit unit, int size) // makes sure all is in map
        {
                if (unit.X < 0)
                {
                    unit.X = 0;
                }
                else if (unit.X >= size)
                {
                    unit.X = size - 1;
                }
                if (unit.Y < 0)
                {
                    unit.Y = 0;
                }
                else if (unit.Y >= size)
                {
                    unit.Y = size - 1;
                }
        }
        public int NumUnits
        {
            get { return map.Units.Length; }
        }

        public int NumBuildings
        {
            get { return map.Buildings.Length; }
        }

        public string MapDisplay
        {
            get { return map.GetMapDisplay(); }
        }

        public void SaveGame()
        {
            Save(UNITS_FILENAME, map.Units);
            Save(BUILDINGS_FILENAME, map.Buildings);
            SaveRound();
        }

        public void LoadGame()
        {
            map.Clear();
            Load(UNITS_FILENAME);
            Load(BUILDINGS_FILENAME);
            LoadRound();
            map.UpdateMap();
        }

        private void Load(string filename) //load game
        {
            FileStream inFile = new FileStream(filename, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFile);

            string recordIn;
            recordIn = reader.ReadLine();
            while(recordIn != null)
            {
                int Length = recordIn.IndexOf(",");
                string firstField = recordIn.Substring(0, Length);
                switch (firstField)
                {
                    case "Melee": map.AddUnit(new MeleeUnit(recordIn)); break;
                    case "Ranged": map.AddUnit(new RangedUnit(recordIn)); break;
                    case "Factory": map.AddBuilding(new FactoryBuildin(recordIn)); break;
                    case "Resource": map.AddBuilding(new ResourceBuilding(recordIn)); break;
                }
                recordIn = reader.ReadLine();
            }
            reader.Close();
            inFile.Close();
        }
        private void Save(string filename, object[] objects) // save game
        {
            FileStream outFile = new FileStream(filename, FileMode.Open, FileAccess.Read);
            StreamReader writer = new StreamReader(outFile);
            foreach (object obj in objects)
            {
                if (obj is Unit)
                {
                    Unit unit = (Unit)obj;
                    writer.WriteLine(Unit.Save());

                }
                else if (obj is Building)
                {
                    Building unit = (Building)obj;
                    writer.WriteLine(unit.Save());
                }
            }
            writer.Close();
            outFile.Close();
        }

        private void SaveRound()// saves round
        {
            FileStream outFile = new FileStream(filename, FileMode.Open, FileAccess.Read);
            StreamReader writer = new StreamReader(outFile);
            writer.WriteLine(round);
            writer.Close();
            outFile.Close();
        }
        private void LoadRound()// loads round
        {
            FileStream inFile = new FileStream(
                ROUNDS_FILENAME, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFile);
            round = int.Parse(reader.ReadLine());
            reader.Close();
            inFile.Close();
        }
    }//
    
}//
