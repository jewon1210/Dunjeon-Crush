using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Origin.Scripts
{
    public class LevelData : MonoBehaviour
    {
        public float Power;
        public float Damage;
        public float Hp;
        public float Critical;
        public float NotCritical;
        public float Defense;

        public LevelData()
        {
            Power = 0;
            Damage = 0;
            Hp = 0;
            Critical = 0;
            NotCritical = 100;
            Defense = 0;
        }
    }
    public class MonLevelData : MonoBehaviour
    {
        public float Power;
        public float Damage;
        public float Hp;

        public MonLevelData()
        {
            Power = 0;
            Damage = 0;
            Hp = 0;
        }
}

    public class LevelDesignData : MonoBehaviour
    {
        private List<LevelData> level_datas = null;
        private List<MonLevelData> M_level_datas = null;
        int level = 0;
        int M_level = 0;

        public void initialize()
        {
            this.level_datas = new List<LevelData>();
            this.M_level_datas = new List<MonLevelData>();
        }

        public void LoadLevelData(TextAsset level_data_text)
        {
            string level_texts = level_data_text.text;
            string[] lines = level_texts.Split('\n');

            foreach(var line in lines)
            {
                if(line == "")
                {
                    continue;
                }

                string[] words = line.Split();
                int n = 0;

                LevelData level_data = gameObject.AddComponent<LevelData>();

                foreach(var word in words)
                {
                    if (word.StartsWith("#")) { break; }
                    if(word == "") { continue; }
                    switch (n)
                    {
                        case 0: level_data.Power = float.Parse(word); break;
                        case 1: level_data.Damage = float.Parse(word); break;
                        case 2: level_data.Hp = float.Parse(word); break;
                        case 3: level_data.Critical = float.Parse(word);
                                level_data.NotCritical = (100 - level_data.Critical); break;
                        case 4: level_data.Defense = float.Parse(word); break;
                    }
                    n++;
                }

                if (n >= 5)
                {
                    this.level_datas.Add(level_data);
                }
                else
                {
                    if (n == 0) { }
                    else
                    {
                        Debug.LogError("[LevelData] Out of parameter.\n");
                    }
                }
            }

            if (this.level_datas.Count == 0)
            {
                Debug.LogError("[LevelData] Has no data.\n");
                this.level_datas.Add(new LevelData());
            }
        }

        public void Load_Mon_LevelData(TextAsset level_data_text)
        {
            string level_texts = level_data_text.text;
            string[] lines = level_texts.Split('\n');

            foreach (var line in lines)
            {
                if (line == "")
                {
                    continue;
                }

                string[] words = line.Split();
                int n = 0;

                MonLevelData level_data = gameObject.AddComponent<MonLevelData>();

                foreach (var word in words)
                {
                    if (word.StartsWith("#")) { break; }
                    if (word == "") { continue; }
                    switch (n)
                    {
                        case 0: level_data.Power = float.Parse(word); break;
                        case 1: level_data.Damage = float.Parse(word); break;
                        case 2: level_data.Hp = float.Parse(word); break;
                    }
                    n++;
                }

                if (n >= 3)
                {
                    this.M_level_datas.Add(level_data);
                }
                else
                {
                    if (n == 0) { }
                    else
                    {
                        Debug.LogError("[LevelData] Out of parameter.\n");
                    }
                }
            }

            if (this.level_datas.Count == 0)
            {
                Debug.LogError("[LevelData] Has no data.\n");
                this.level_datas.Add(new LevelData());
            }
        }

        public void currentLevel(int level)
        {
            this.level = level;
        }

        public void currentMobLevel(int level)
        {
            this.M_level = level;
        }

        public LevelData getCurrentLevelData()
        {
            return (this.level_datas[this.level]);
        }

        public MonLevelData getCurrentMobLevelData()
        {
            return (this.M_level_datas[this.M_level]);
        }

    }
}

