using Assets.FantasyMonsters.Scripts;
using Assets.HeroEditor.Common.CharacterScripts;
using UnityEngine;

namespace Assets.HeroEditor.FantasyHeroes.MonsterHitted
{
    /// <summary>
    /// Enemy example behaviour.
    /// </summary>
    public class MonsterHitted : MonoBehaviour
    {
        public Character Character;
        public SpriteRenderer Impact;
        bool Hitted = false;
        float timecheck = 0;

        public void Start()
        {
            Character = FindObjectOfType<Character>();

            if (Character != null)
            {

                Character.Animator.GetComponent<AnimationEvents>().OnCustomEvent += OnAnimationEvent;
            }
        }

        private void Update()
        {
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
            if (eventName == "Hit" && Vector3.Distance(Character.MeleeWeapon.Edge.position, transform.position) < 1.5)
            {
                GetComponent<Monster>().Spring();
                Impact.color = Color.gray;
                Hitted = true;
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