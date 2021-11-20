using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Origin.Scripts;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.HeroEditor.FantasyHeroes.MonsterHitted;

namespace Assets.Origin.Scripts
{
    public class StageManagerScript : MonoBehaviour
    {
        public Character character;
        public Transform PlayerTrans;
        public bool InfinityMode = false;
        public SpawnMonsterScript Spawn;
        public MonsterHitted[] Mobs;
        public MonsterHitted EliteMobs;
        public MonsterMovingScript[] MobMove;
        public HealthSliderScript PlayerHp;
        public Transform RevivePos;

        bool EliteMobDied = false;
        bool AllMobsDied = false;
        bool IMStart = false;
        bool once = false;

        float timecheck;
        int MonsterCountForIM = 0;
        int IMStage = 0, IMSubStage = 0;

        void Start()
        {
            character = FindObjectOfType<Character>();
            PlayerTrans = character.gameObject.transform;
            PlayerHp = FindObjectOfType<HealthSliderScript>();
        }
        
        void Update()
        {
            if (InfinityMode && IMStage != 13)
            {
                InfinityModeController();


                if (PlayerHp.currentHp() <= 0)
                {
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        //PlayerTrans.Translate(RevivePos.position);
                        PlayerTrans.position = RevivePos.position;
                        PlayerHp.FullHealth();
                        character.SetState(CharacterState.Idle);

                        foreach (MonsterMovingScript MM in MobMove)
                        {
                            MM.PlayerRevive();
                        }
                    }
                }

                if (Input.GetKeyDown(KeyCode.K))
                {
                    foreach (MonsterHitted MH in Mobs)
                    {
                        MH.MobDie();
                    }
                    EliteMobs.MobDie();
                }
            }

            if (!InfinityMode)
            {

            }
        }

        public void InfinityModeController()
        {
            if (!IMStart && MonsterCountForIM == 0)
            {
                if (IMSubStage == 0 || IMSubStage >= 4)
                {
                    IMStage++;
                    Debug.Log(IMStage + "stage");
                    IMSubStage = 1;
                }

                if (IMStage == 13)
                    return;

                switch (IMSubStage)
                {
                    case 1:
                        IMSpawnMobs(3, IMStage);
                        EliteMobDied = true;
                        break;
                    case 2:
                        IMSpawnMobs(4, IMStage);
                        EliteMobDied = true;
                        break;
                    case 3:
                        IMSpawnMobs(4, IMStage);
                        IMSpawnEliteMob(IMStage);
                        EliteMobDied = false;
                        break;
                    default:
                        break;
                }
                IMStart = true;
            }

            else if(IMStart || MonsterCountForIM >= 1)
            {
                int count = 0;
                if (!AllMobsDied)
                {
                    for (int i = 0; i < MonsterCountForIM; i++)
                    {
                        if (Mobs[i].curHp() <= 0)
                        {
                            count++;
                        }

                        if (count >= MonsterCountForIM)
                        {
                            AllMobsDied = true;
                        }
                    }
                }

                if (EliteMobs == null ||EliteMobs.curHp() <= 0)
                    EliteMobDied = true;
            }

            if (AllMobsDied && EliteMobDied && !once)
            {
                once = true;
                StartCoroutine(IMStartDelay());
            }
        }

        public void IMSpawnMobs(int SpawnNum, int StageNum)
        {
            AllMobsDied = false;
            for (int i = 0; i < SpawnNum; i++)
            {
                Spawn.Spawn(Spawn.MobData(StageNum - 1));
                MonsterCountForIM++;
            }
            Mobs = FindObjectsOfType<MonsterHitted>();
            MobMove = FindObjectsOfType<MonsterMovingScript>();
        }
        public void IMSpawnEliteMob(int StageNum)
        {
            Spawn.Spawn(Spawn.MobData(StageNum));
            EliteMobs = FindObjectOfType<MonsterHitted>();
        }

        public IEnumerator IMStartDelay()
        {
            yield return new WaitForSeconds(1.0f);
            foreach (MonsterHitted Mob in Mobs)
            {
                Destroy(Mob.gameObject);
            }
            if (EliteMobs != null)
                Destroy(EliteMobs.gameObject);
            MonsterCountForIM = 0;
            yield return new WaitForSeconds(2.0f);
            IMStart = false;
            AllMobsDied = false;
            once = false;
            IMSubStage++;
        }
    }
}