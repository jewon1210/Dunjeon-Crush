﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Origin.Scripts;
using Assets.HeroEditor.FantasyHeroes.MonsterHitted;

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
        public PlayerStatus PS;
        public int Mobvalue = 0;
        int Playerlevel;

        void Start()
        {
            MeshRenderer mesh = GetComponent<MeshRenderer>();
            mesh.sortingLayerName = sortingLayerName;
            mesh.sortingOrder = sortingOrder;

            PS = FindObjectOfType<PlayerStatus>();
            Playerlevel = PS.getLevel(1);


            leveldata = FindObjectOfType<LoadLevelDataScript>();
            Hp = FindObjectOfType<HealthSliderScript>();
            this.Textmesh = this.gameObject.GetComponent<TextMesh>();

            TextUpdate();
        }

        void Update()
        {
            float alpha = this.Textmesh.color.a;

            alpha -= Time.deltaTime;

            this.Textmesh.color = new Color(this.Textmesh.color.r, this.Textmesh.color.g, this.Textmesh.color.b, alpha);

            if (alpha <= 0)
                Destroy(this.gameObject);
        }

        //public void AlphaColorReset()
        //{
        //    this.Textmesh.color = new Color(0.6784314f, 0, 0, 1);
        //}

        public void TextUpdate()
        {
            float value = 0;

            switch (type)
            {
                case 0:
                    value = leveldata.getValue(Playerlevel, 1);
                    Textmesh.text = value.ToString();
                    break;
                case 1:
                    value = leveldata.getMobValue(Mobvalue, 1);
                    float Defense = leveldata.getValue(Playerlevel, 4);
                    Textmesh.text = (value-Defense).ToString();
                    Hp.UpdateSlider(0, value - Defense);
                    break;
            }
        }

    }
}
