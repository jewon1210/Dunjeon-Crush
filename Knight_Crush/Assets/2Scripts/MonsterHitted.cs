using Assets.FantasyMonsters.Scripts;
using Assets.HeroEditor.Common.CharacterScripts;
using UnityEngine;
using Assets.Origin.Scripts;

namespace Assets.HeroEditor.FantasyHeroes.MonsterHitted
{
    /// <summary>
    /// Enemy example behaviour.
    /// </summary>
    public class MonsterHitted : MonoBehaviour
    {
        public Character Character;
        public SpriteRenderer Impact;
        bool Hitted = false, died = false;
        float timecheck = 0;

        int hp = 100; //나중에 바꾸기

        public GameObject DamageTxt;
        MonsterMovingScript MMoving;

        public void Start()
        {
            Character = FindObjectOfType<Character>();

            if (Character != null)
            {
                Character.Animator.GetComponent<AnimationEvents>().OnCustomEvent += OnAnimationEvent;
            }

            MMoving = this.gameObject.GetComponent<MonsterMovingScript>();
        }

        private void Update()
        {
            if (hp <= 0)
            {
                died = true;
                Impact.color = Color.white;
                GetComponent<Monster>().Die();
                MMoving.MonsterDie();
                return;
            }

            if (Hitted)
            {
                ImpactFuc();
                if(count >= 10)
                {
                    Impact.color = Color.white;
                    count = 0;
                    Hitted = false;
                }
            }
        }

        public void OnDestroy()
        {
            if (Character != null)
            {
                Character.Animator.GetComponent<AnimationEvents>().OnCustomEvent -= OnAnimationEvent;
            }
        }

        private void OnAnimationEvent(string eventName)
        {
            if (eventName == "Hit" && Vector3.Distance(Character.MeleeWeapon.Edge.position, transform.position) < 1.5f)
            {
                if (died)
                    return;
                MMoving.Detected();
                Vector3 y = this.transform.position + new Vector3(Random.Range(-0.7f, 0.7f), 2 + Random.Range(0, 0.3f), 0);
                Instantiate(DamageTxt, y, Quaternion.identity);

                GetComponent<Monster>().Spring();

                Debug.Log(hp);
                //if(hp <= 0)
                //{
                //    GetComponent<Monster>().Die();
                //    MMoving.MonsterDie();
                //    return;
                //}

                
                Impact.color = Color.gray;
                Hitted = true;
                hp -= 50; // 나중에 바꾸기
            }
        }

        int count = 0;
        public void ImpactFuc()
        {
            Color white = Color.white;
            Color gray = Color.gray;

            timecheck += Time.deltaTime;

            if(timecheck >= 0.1f)
            {
                if(Impact.color == white)
                {
                    timecheck = 0;
                    Impact.color = gray;
                    count++;
                }
                else if(Impact.color == gray)
                {
                    timecheck = 0;
                    Impact.color = white;
                    count++;
                }
            }
        }
    }
}