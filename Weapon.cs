using System;
using System.Collections.Generic;
using System.Text;

namespace commercial_angler
{
    public class Weapon
    {
        public int ID;
        public String Name;
        public float Price;
        public int MinDmg;
        public int MaxDmg;
        public float Weight; // not currently used
        public int HitBonus; // not currently used

        public Weapon(int inID, String inName, float inPrice, int inMinDmg, int inMaxDmg
                     , float inWeight, int inHitBonus)
        {
            ID = inID;
            Name = inName;
            Price = inPrice;
            MinDmg = inMinDmg;
            MaxDmg = inMaxDmg;
            Weight = inWeight;
            HitBonus = inHitBonus;
        }
    }
}
