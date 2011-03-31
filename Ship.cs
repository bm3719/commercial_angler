using System;
using System.Collections.Generic;
using System.Text;

namespace commercial_angler
{
    public class Ship
    {
        public int ID;
        public String Name;
        public float Cost;
        public float HoldCap;
        public int HP;
        public int AC;
        public int CurrentHP;

        public CargoList Cargo;

        public Ship(int inID, String inName, float inCost, float inHoldCap, int inHP, int inAC)
        {
            ID = inID;
            Name = inName;
            Cost = inCost;
            HoldCap = inHoldCap;
            HP = inHP;
            AC = inAC;
            CurrentHP = HP; // if player's ship, set the HP after constructor call
            Cargo = new CargoList();
        }

        public Ship Copy()
        {
            return new Ship(ID, Name, Cost, HoldCap, HP, AC);
        }
    }

    public class CargoList : System.Collections.Generic.List<Fish>
    {
        public void Increment(int index)
        {
            base.ToArray()[index].Quantity++;
        }

        public int IndexOf(int ID)
        {
            for (int i = 0; i < this.ToArray().Length; i++)
            {
                if (base.ToArray()[i].ID == ID)
                {
                    return i;
                }
            }
            return -1;
        }

        public float GetWeight()
        {
            float total = 0.0f;
            foreach (Fish f in this)
            {
                total += (f.Weight * f.Quantity);
            }
            return total;
        }

        public void AddCargo(Fish f)
        {
            if (IndexOf(f.ID) > -1)
            {
                base.ToArray()[IndexOf(f.ID)].Quantity++;
            }
            else
            {
                Add(f);
            }
        }
    }
}
