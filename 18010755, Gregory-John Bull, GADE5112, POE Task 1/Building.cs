using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18010755__Gregory_John_Bull__GADE5112__POE_Task_1
{
    public abstract class Building
    {
        protected int X;
        protected int Y;
        protected int health;
        protected int maxHealth;
        protected string faction;
        protected char symbol;
        protected bool destroyed = false;
        public static Random random = new Random();

       

        public Building(int x, int y, int health, string faction, char symbol)
        {
            this.X = x;
            this.Y = y;
            this.health = health;
            this.maxHealth = health;
            this.faction = faction;
            this.symbol = symbol;
        }

        public Building() // empty constructor to make loading simpler
        {

        }
        public int x
        {
            get { return X; }
        }
        public int y
        {
            get { return Y; }
        }
        public string Faction
        {
            get { return faction; }
        }
        public char Symbol
        {
            get { return symbol; }
        }

        public abstract void Destroy();
        public abstract string Save();
        // deliberately didn't abstract ToString()
        //because it's already a virtual method

        public override string ToString()
        {
            return "Faction: " + faction + Environment.NewLine + "Position: " + x + ", " + y + Environment.NewLine + "HEalth: " + health + " / " + maxHealth + Environment.NewLine;
        }
    }//
    public enum ResourceType
    {
        GOLD,
        FOOD,
        WOOD
    }
}//
