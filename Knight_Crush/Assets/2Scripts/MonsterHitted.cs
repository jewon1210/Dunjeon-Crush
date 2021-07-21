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

        public void Start()
        {
            Character = FindObjectOfType<Character>();

            if (Character != null)
            {

                Character.Animator.GetComponent<AnimationEvents>().OnCustomEvent += OnAnimationEvent;
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
            }
        }
    }
}