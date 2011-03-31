using System;
using System.Collections.Generic;
using System.Text;

namespace commercial_angler
{
    public class Character
    {
        public String Name;
        public float Money;
        public Weapon CurrentWeapon;
        public Ship CurrentShip;

        public Character(String inName, float inMoney, Weapon inCurrentWeapon, Ship inCurrentShip)
        {
            Name = inName;
            Money = inMoney;
            CurrentWeapon = inCurrentWeapon;
            CurrentShip = inCurrentShip;
        }

    }
}
