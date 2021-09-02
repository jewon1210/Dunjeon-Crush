using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.Origin.Scripts;

namespace Assets.Origin.Scripts
{
    public class PlayerStatus : MonoBehaviour
    {
        int TotalPower;
        int PowerWeapon;
        int PowerArmor;
        int Damage;
        int Hp;
        int Critical;
        int Defense;

        public int weaponLevel;
        public int armorLevel;


        public LoadLevelDataScript LevelData;

        void Start()
        {
            LevelData = FindObjectOfType<LoadLevelDataScript>();

            WeaponUpdate(0);

            ArmorUpdate(0);
        }

        private void Update()
        {
            WeaponUpdate(0);

            ArmorUpdate(0);
        }

        public void WeaponUpdate(int WpLevel)
        {
            weaponLevel = WpLevel;

            PowerWeapon = (int)LevelData.getValue(weaponLevel, 0);
            Damage = (int)LevelData.getValue(weaponLevel, 1);
            Critical = (int)LevelData.getValue(weaponLevel, 3);

            TotalPower = PowerArmor + PowerWeapon;
        }

        public void ArmorUpdate(int AmLevel)
        {
            armorLevel = AmLevel;

            PowerArmor = (int)LevelData.getValue(armorLevel, 0);
            Hp = (int)LevelData.getValue(armorLevel, 2);
            Defense = (int)LevelData.getValue(armorLevel, 4);

            TotalPower = PowerArmor + PowerWeapon;
        }

        public int Power(int type)
        {
            switch (type)
            {
                case 0:
                    return PowerArmor;
                case 1:
                    return PowerWeapon;
                default:
                    return TotalPower;
            }
        }

        public int getStatus(int type)
        {
            switch (type)
            {
                case 0:
                    return Damage;
                case 1:
                    return Critical;
                case 2:
                    return Hp;
                case 3:
                    return Defense;
                default:
                    return 0;
            }
        }

        public int getLevel(int Type)
        {
            switch (Type)
            {
                case 1:
                    return weaponLevel;
                case 2:
                    return armorLevel;
                default:
                    return 0;
            }
        }
    }
}
