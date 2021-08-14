using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Origin.Scripts;

namespace Assets.Origin.Scripts
{
    public class LoadLevelDataScript : MonoBehaviour
    {
        [Header("Text Files")]
        public TextAsset MobLevelDataText;
        public TextAsset LevelDataText;

        public LevelDesignData level_data;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            this.level_data.initialize();
            this.level_data.LoadLevelData(this.LevelDataText);
            this.level_data.Load_Mon_LevelData(this.MobLevelDataText);
        }

        public void getAllStatusValue(int level)
        {
            this.level_data.currentLevel(level);
            for (int i = 0; i < 5; i++)
            {
                getValue(level, i);
            }

            this.level_data.currentMobLevel(level);
            for (int i = 0; i < 5; i++)
            {
                getValue(level, i);
            }
        }

        public float getValue(int level, int type)
        {
            this.level_data.currentLevel(level);
            LevelData leveldata = level_data.getCurrentLevelData();

            switch (type)
            {
                case 0:
                    return leveldata.Power;
                case 1:
                    return leveldata.Damage;
                case 2:
                    return leveldata.Hp;
                case 3:
                    return leveldata.Critical;
                case 4:
                    return leveldata.Defense;
            }

            return 0;
        }

        public float getMobValue(int level, int type)
        {
            this.level_data.currentMobLevel(level);
            MonLevelData leveldata = level_data.getCurrentMobLevelData();

            switch (type)
            {
                case 0:
                    return leveldata.Power;
                case 1:
                    return leveldata.Damage;
                case 2:
                    return leveldata.Hp;
            }

            return 0;
        }
    }
}
