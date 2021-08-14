using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.Origin.Scripts;

namespace Assets.Origin.Scripts
{
    public class HealthSliderScript : MonoBehaviour
    {
        public Slider HpSlider;
        public LoadLevelDataScript LevelData;

        float timecheck = 0;
        bool Once = false;

        void Start()
        {
            HpSlider = gameObject.GetComponent<Slider>();
            LevelData = FindObjectOfType<LoadLevelDataScript>();
        }

        void Update()
        {
            timecheck += Time.deltaTime;
            if(timecheck >= 0.2f && !Once)
            {
                Set_Total_Hp_Value();
                Once = true;
            }
        }

        public void Set_Total_Hp_Value()
        {
            HpSlider.maxValue = LevelData.getValue(1,2);
            HpSlider.value = HpSlider.maxValue;
        }

        public void UpdateSlider(int type, float value)
        {
            switch (type)
            {
                case 0:
                    HpSlider.value -= value;
                    break;
                case 1:
                    HpSlider.value += value;
                    break;
            }
        }

        public float currentHp()
        {
            return HpSlider.value;
        }
    }
}
