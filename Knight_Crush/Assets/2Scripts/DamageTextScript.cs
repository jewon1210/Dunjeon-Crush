using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Origin.Scripts;

namespace Assets.Origin.Scripts
{
    public class DamageTextScript : MonoBehaviour
    {
        public int type; // 0 = Hero, 1 = Mob;
        TextMesh Textmesh;
        public LoadLevelDataScript leveldata;
        public HealthSliderScript Hp;

        public string sortingLayerName;
        public int sortingOrder;

        int levelvalue = 0;

        void Start()
        {
            MeshRenderer mesh = GetComponent<MeshRenderer>();
            mesh.sortingLayerName = sortingLayerName;
            mesh.sortingOrder = sortingOrder;

            leveldata = FindObjectOfType<LoadLevelDataScript>();
            Hp = FindObjectOfType<HealthSliderScript>();
            this.Textmesh = this.gameObject.GetComponent<TextMesh>();
            TextUpdate();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && levelvalue >= 0)
                levelvalue--;
            if (Input.GetKeyDown(KeyCode.Alpha2) && levelvalue < 20)
                levelvalue++;

            float alpha = this.Textmesh.color.a;

            alpha -= Time.deltaTime;

            this.Textmesh.color = new Color(this.Textmesh.color.r, this.Textmesh.color.g, this.Textmesh.color.b, alpha);

            if (alpha <= 0)
                Destroy(this.gameObject);
        }

        public void AlphaColorReset()
        {
            this.Textmesh.color = new Color(0.6784314f, 0, 0, 1);
        }

        public void TextUpdate()
        {
            float value = 0;

            switch (type)
            {
                case 0:
                    value = leveldata.getValue(levelvalue, 1);
                    Textmesh.text = value.ToString();
                    break;
                case 1:
                    value = leveldata.getMobValue(levelvalue, 1);
                    Textmesh.text = value.ToString();
                    Hp.UpdateSlider(0, value);
                    break;
            }
        }
    }
}
