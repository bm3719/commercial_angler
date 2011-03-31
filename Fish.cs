using System;
using System.Collections.Generic;
using System.Text;

namespace commercial_angler
{
    public class Fish
    {
        public int ID;
        public String Name;
        public int Level;
        public int BaseHP;
        public int MinDmg;
        public int MaxDmg;
        public int AC;
        public float Weight;
        public float Value;
        public int Quantity;

        public Fish(int inID, String inName, int inLevel, int inBaseHP
                   , int inMinDmg, int inMaxDmg, int inAC, float inWeight, float inValue)
        {
            ID = inID;
            Name = inName;
            Level = inLevel;
            BaseHP = inBaseHP;
            MinDmg = inMinDmg;
            MaxDmg = inMaxDmg;
            AC = inAC;
            Weight = inWeight;
            Value = inValue;
            Quantity = 1;
        }

        public Fish Copy()
        {
            return new Fish(ID, Name, Level, BaseHP, MinDmg, MaxDmg, AC, Weight, Value);
        }
    }
}
