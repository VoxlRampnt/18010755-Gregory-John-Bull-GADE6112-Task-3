using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18010755__Gregory_John_Bull__GADE5112__POE_Task_1
{
    class MeleeUnit : Unit
    {
        

        public MeleeUnit(int x, int y, string faction) : base(x, y, 100, 1, 10, 1, faction, 'M', "Knight")
        {
        }

        public MeleeUnit(string values) : base(values)
        {

        }

        public override int X
        {
            get { return x; }
            set { x = value; }
        }

        public override int Y
        {
            get { return y; }
            set { y = value; }
        }

        public override int Health
        {
            get { return health; }
            set { health = value; }
        }

        public override int MaxHealth
        {
            get { return health; }
        }

        public override string Faction
        {
            get { return faction; }

        }

        public override char Symbol
        {
            get { return symbol; }
        }

        public override bool Destroyed
        {
            get { return destroyed; }
        }

        public override bool AttackRange(Unit otherUnit)
        {
            return GetDistance(otherUnit) <= attackRange;
        }

        public override void DeathCheck() //  looks to see if units are dead
        {
            destroyed = true;
            attacking = false;
            symbol = 'X';
        }

        public override Unit ClosestUnit(Unit[] units) //looks to see if enemy units are in range
        {
            double closestDistance = int.MaxValue;
            Unit closestUnit = null;

            foreach (Unit otherUnit in units)
            {
                if (otherUnit == this || otherUnit.Faction == faction || otherUnit.Destroyed)
                {
                    continue;
                }
                double distance = GetDistance(otherUnit);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestUnit = otherUnit;
                }
            }
            return closestUnit;
        }

        public override void Combat(Unit otherUnit) //controls combat between units
        {
            attacking = false;
            otherUnit.Health -= attack;

            if (otherUnit.Health <= 0)
            {
                otherUnit.DeathCheck();
            }
        }

        public override void Move(Unit closestUnit) // contols the movement of units and to make sure units stay in map
        {
            attacking = false;
            int xDistance = closestUnit.X - X;
            int yDistance = closestUnit.Y - Y;

            if (Math.Abs(xDistance) > Math.Abs(yDistance))
            {
                x += Math.Sign(xDistance);
            }
            else
            {
                y += Math.Sign(yDistance);
            }
        }

        public override void RunAway() // looks to see if units are to low for combat and must run away as well as that they don't run out of map
        {
            attacking = false;
            int direction = random.Next(0, 4);
            if (direction == 0)
            {
                x += 1;
            }
            else if (direction == 1)
            {
                x -= 1;
            }
            else if (direction == 2)
            {
                y += 1;
            }
            else
            {
                y -= 1;
            }
        }

        public override string Save()
        {
            return string.Format(
                $"Melee,{x},{y},{health},{maxHealth},{speed},{attack}, {attackRange}," +
                $"{faction},{symbol},{name},{destroyed}"
                );
        }
    }//
}//


