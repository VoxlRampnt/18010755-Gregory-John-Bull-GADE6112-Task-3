using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18010755__Gregory_John_Bull__GADE5112__POE_Task_1
{
    public abstract class Unit
    {
        protected int x, y, health, maxHealth, speed, attack, attackRange;
        protected string faction;
        protected char symbol;
        protected bool attacking = false;
        protected bool destroyed = false;
        protected string name;
        public static Random random = new Random();

        public Unit(int x, int y, int health, int speed, int attack, int attackRange, string faction, char symbol, string name)
        {
            this.x = x;
            this.y = y;
            this.health = health;
            maxHealth = health;
            this.speed = speed;
            this.attack = attack;
            this.attackRange = attackRange;
            this.faction = faction;
            this.symbol = symbol;
            this.name = name;

        }

        public Unit(string values)
        {
            string[] parameters = values.Split(',');

            x = int.Parse(parameters[1]);
            y = int.Parse(parameters[2]);
            health = int.Parse(parameters[3]);
            maxHealth = int.Parse(parameters[4]);
            speed = int.Parse(parameters[5]);
            attack = int.Parse(parameters[6]);
            attackRange = int.Parse(parameters[7]);
            faction = parameters[8];
            symbol = parameters[9][0];
            name = parameters[10];
            destroyed = parameters[11] == "True" ? true : false;
        }

        public abstract string Save();
        // the getters and setters for all of the above
        public abstract int X
        {
            get;
            set;
        }
        public abstract int Y
        {
            get;
            set;
        }
        public abstract int Health
        {
            get;
            set;
        }
        public abstract int MaxHealth
        {
            get;

        }
        public abstract char Symbol
        {
            get;
        }
        public abstract string Faction
        {
            get;

        }
        public abstract bool Destroyed
        {
            get;

        }
        public string Name
        {
            get { return name; }
        }


        public abstract void Move(Unit closestUnit);
        // made these virtual from before
        public virtual void Attack(Unit otherUnit)
        {
            attacking = true;
            otherUnit.Health -= attack;

            if (otherUnit.Health <= 0)
            {
                otherUnit.Health = 0;
                otherUnit.DeathCheck();
            }
        }
        public virtual void Attack(Building otherBuilding)
        {
            attacking = true;
            otherBuilding.health -= attack;

            if (otherBuilding.health <= 0)
            {
                otherBuilding.health = 0;
                otherBuilding.Destroy();
            }
        }
                
            

        public abstract void RunAway();
        public abstract bool AttackRange(Unit otherUnit);
        public abstract Unit ClosestUnit(Unit[] units);
        public abstract void DeathCheck();

        public double GetDistance(Unit otherUnit)
        {
            double xDistance = otherUnit.X - X;
            double yDistance = otherUnit.Y - Y;
            return Math.Sqrt(xDistance * xDistance + yDistance * yDistance);
        }

        public double GetBuildingDistance(Building otherBuilding) //get distance for the unit for the building using pythagoroas 
        {
            double xDistance = otherBuilding.X - X;
            double yDistance = otherBuilding.Y - Y;
            return Math.Sqrt(xDistance * yDistance + yDistance * yDistance);
        }

        public override string ToString()
        {
            return "Postion: " + x + ", " + y + "\n" + "Health: " + health + " / " + maxHealth + "\n" + "Faction: " + faction + "(" + symbol + ")\n";
        }

        public Building ClosestBuilding(Building[] buildings)
        {
            throw new NotImplementedException();
        }

        internal double BuildingDistance(Building closestBuilding)
        {
            throw new NotImplementedException();
        }
    }//
}//
