using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Origin.Scripts;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.HeroEditor.FantasyHeroes.MonsterHitted;

namespace Assets.Origin.Scripts
{
    public class TutorialFightSceneScript : MonoBehaviour
    {
        public Character character;
        public Image Blackout;
        public SpawnMonsterScript Spawner;
        public MovementScript MoveScript;
        public PlayerAttack PAttack;
        CharacterController Controller;
        public HealthSliderScript PlayerHp;

        public GameObject[] Bubbles;
        public GameObject Panel;
        public MonsterHitted Mob;
        public GameObject[] WinBubbles;
        public GameObject TextofRestart;
        public Transform playerTrans;
        public Transform RevivePos;
        public MonsterMovingScript MobMoveScript;

        int SceneNum = 0;
        float timecheck = 0;
        bool start = false, once = false;
        bool spawn = false, findMob = false;
        bool MobKill = false, clear = false;
        bool over = false;

        void Start()
        {
            character = FindObjectOfType<Character>();
            FadeOut();
            MoveScript.StopM();
            PAttack.StopAct();
            foreach (GameObject go in Bubbles)
            {
                go.SetActive(false);
            }
            Panel.SetActive(false);
            foreach (GameObject go in WinBubbles)
            {
                go.SetActive(false);
            }
            PlayerHp = FindObjectOfType<HealthSliderScript>();
            TextofRestart.SetActive(false);
        }

        void Update()
        {
            if (PlayerHp.currentHp() <= 0)
            {
                Panel.SetActive(true);
                TextofRestart.SetActive(true);
                if (Input.GetKeyDown(KeyCode.R))
                {
                    PlayerHp.FullHealth();
                    Panel.SetActive(false);
                    TextofRestart.SetActive(false);                   
                    character.SetState(CharacterState.Idle);
                    MobMoveScript.PlayerRevive();
                }
            }
            else
            {
                if (!once)
                {
                    timecheck += Time.deltaTime;

                    if (timecheck >= 0.1f && !start)
                    {
                        Controller = MoveScript.CharController();
                        if (timecheck >= 0.5f)
                            Move();
                    }

                    if (timecheck >= 1.5f && !start)
                    {
                        start = true;
                        character.SetState(CharacterState.Idle);
                        BubbleControl(SceneNum);
                        timecheck = 0;
                    }

                    if (Input.GetKeyDown(KeyCode.Return) && timecheck >= 0.2f && start)
                    {
                        timecheck = 0;
                        SceneNum++;
                        BubbleControl(SceneNum);
                        Debug.Log("SceneNum : " + SceneNum.ToString());
                    }
                }
                else
                {
                    if (!spawn)
                    {
                        Spawner.Spawn(Spawner.MobData(1));
                        spawn = true;
                        MoveScript.StartM();
                        PAttack.StartAct();
                    }
                }

                if (findMob && !MobKill && SceneNum == 6)
                {
                    if (Mob.curHp() <= 0)
                    {
                        StartCoroutine(Delay(1.0f));
                    }
                }

                if (MobKill && !clear && SceneNum == 6)
                {
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        WinBubbles[1].SetActive(true);
                        WinBubbles[0].SetActive(false);
                        clear = true;
                        SceneNum++;
                    }
                }

                else if (clear && Input.GetKeyDown(KeyCode.Return) && !over && SceneNum == 7)
                {
                    over = true;
                    WinBubbles[1].SetActive(false);
                    WinBubbles[0].SetActive(false);
                    FadeIn();
                    SceneNum++;
                }

                else if (over)
                {
                    Move();
                }
            }
        }

        private void LateUpdate()
        {
            if (spawn && !findMob)
            {
                Mob = FindObjectOfType<MonsterHitted>();
                MobMoveScript = FindObjectOfType<MonsterMovingScript>();
                findMob = true;
            }
        }

        public void FadeOut()//화면 밝아지기
        {
            StartCoroutine(FadeOutCoroutine());
        }
        public void FadeIn()
        {
            StartCoroutine(FadeInCoroutine());
        }
        IEnumerator FadeOutCoroutine()
        {
            float fadeCount = 1;
            while (fadeCount > 0.0f)
            {
                fadeCount -= 0.01f;
                yield return new WaitForSeconds(0.01f);
                Blackout.color = new Color(0, 0, 0, fadeCount);
            }
        }
        IEnumerator FadeInCoroutine()//화면 어둡게하기
        {
            float fadeCount = 0;
            while (fadeCount < 1.0f)
            {
                fadeCount += 0.01f;
                yield return new WaitForSeconds(0.01f);
                Blackout.color = new Color(0, 0, 0, fadeCount);
            }
        }
        IEnumerator Delay(float time)
        {
            yield return new WaitForSeconds(time);
            MoveScript.StopM();
            PAttack.StopAct();
            Panel.SetActive(true);
            MobKill = true;
            WinBubbles[0].SetActive(true);
            character.SetState(CharacterState.Idle);
        }

        public void Move()
        {
            var direction = Vector3.zero;
            direction.x = 0.8f;
            Vector3 speed = new Vector3(3f * direction.x, 0, 6 * direction.z);

            if (direction != Vector3.zero)
                character.SetState(CharacterState.Run);
            else if (character.GetState() < CharacterState.DeathB)
                character.SetState(CharacterState.Idle);

            speed.y -= 1000 * Time.deltaTime;
            Controller.Move(speed * Time.deltaTime);
        }

        public void BubbleControl(int i)
        {
            switch (i)
            {
                case 0:
                    Bubbles[i].SetActive(true);
                    break;
                case 1:
                    Bubbles[i].SetActive(true);
                    Bubbles[i-1].SetActive(false);
                    break;
                case 2:
                    Bubbles[i].SetActive(true);
                    Bubbles[i - 1].SetActive(false);
                    Panel.SetActive(true);
                    break;
                case 3:
                    Bubbles[i].SetActive(true);
                    Bubbles[i - 1].SetActive(false);
                    break;
                case 4:
                    Bubbles[i].SetActive(true);
                    Bubbles[i - 1].SetActive(false);
                    Panel.SetActive(false);
                    break;
                case 5:
                    Bubbles[i].SetActive(true);
                    Bubbles[i - 1].SetActive(false);
                    break;
                default:
                    foreach (GameObject go in Bubbles)
                    {
                        go.SetActive(false);
                    }
                    if (SceneNum >= 6)
                        once = true;
                    break;
            }
        }
    }
}