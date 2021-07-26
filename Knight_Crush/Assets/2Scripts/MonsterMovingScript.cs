using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Assets.FantasyMonsters.Scripts;

namespace Assets.Origin.Scripts
{
    public class MonsterMovingScript : MonoBehaviour
    {
        public Monster Monster;
        public Transform PlayerTrans;
        public Transform MonsterTrans;
        public CharacterController Controller;
        public GameObject DetectedIcon;
        Vector3 speed;

        bool detected;
        float timecheck;

        void Start()
        {
            PlayerTrans = GameObject.FindWithTag("Player").GetComponent<Transform>();

            if (Controller == null)
            {
                Controller = Monster.gameObject.AddComponent<CharacterController>();
                Controller.center = new Vector3(0, 1.27f);
                Controller.height = 1.0f;
                Controller.radius = 0.75f;
                Controller.minMoveDistance = 0;
            }
            Monster.Animator.SetBool("Action", true);

            timecheck = 0;

            detected = false;

            DetectedIcon.SetActive(false);
        }

        Vector3 direction;

        void Update()
        {
            if (detected)
            {
                timecheck += Time.deltaTime;
                ChaseMove();
                if (timecheck >= 1.0f)
                    DetectedIcon.SetActive(false);
                return;
            }

            timecheck += Time.deltaTime;
            float RandomDelay = Random.Range(0.6f, 1.5f);

            if (direction == Vector3.zero)
                RandomDelay = 2.1f;

            if (timecheck > RandomDelay)
            {
                direction = Vector3.zero;

                RandomDir();

                timecheck = 0;
            }

            Monster.Animator.SetBool("Action", false);
            Move(direction);

            if (Vector3.Distance(PlayerTrans.position, MonsterTrans.position) < 5.0f)
            {
                detected = true;
                timecheck = 0;
                DetectedIcon.SetActive(true);
            }
        }

        public void RandomDir()
        {
            int range = 20;
            int randomValue = Random.Range(0, range);

            if (randomValue < 2 && randomValue >= 0) { direction.x = 0; direction.z = 0; }

            if (randomValue < 4 && randomValue >= 2) { direction.x = -0.4f; direction.z = 0.4f; }
            if (randomValue < 6 && randomValue >= 4) { direction.x = -0.4f; direction.z = -0.4f; }
            if (randomValue < 8 && randomValue >= 6) { direction.x = 0.4f; direction.z = -0.4f; }
            if (randomValue < 10 && randomValue >= 8) { direction.x = 0.4f; direction.z = 0.4f; }

            if (randomValue < 12 && randomValue >= 10) { direction.x = -0.4f; direction.z = 0; }
            if (randomValue < 14 && randomValue >= 12) { direction.x = 0.4f; direction.z = 0; }
            if (randomValue < 16 && randomValue >= 14) { direction.x = 0; direction.z = -0.4f; }
            if (randomValue < 18 && randomValue >= 16) { direction.x = 0; direction.z = 0.4f; }

            if (randomValue <= 20 && randomValue >= 18) { direction.x = 0; direction.z = 0; }
        }

        public void Move(Vector3 direction)
        {
            speed = new Vector3(5 * direction.x, 0, 10 * direction.z);

            if (direction != Vector3.zero)
            {
                Turn(speed.x);
            }

            if (direction != Vector3.zero)
            { Monster.SetState(MonsterState.Walk); }
            else if (Monster.GetState() < MonsterState.Death)
            { Monster.SetState(MonsterState.Idle); }

            speed.y -= 1000 * Time.deltaTime;

            Controller.Move(speed * Time.deltaTime);
        }

        public void ChaseMove()
        {
            Vector3 vectorValue;
            vectorValue = PlayerTrans.position - MonsterTrans.position;
            speed = new Vector3(vectorValue.x, 0, 2*vectorValue.z);

            Turn(speed.x);
            
            Monster.SetState(MonsterState.Walk);

            speed.y -= 1000 * Time.deltaTime;

            Controller.Move(speed * Time.deltaTime);
        }

        public void Turn(float direction)
        {
            Monster.transform.localScale = new Vector3(Mathf.Sign(direction)*-0.4f, 0.4f, 1);
        }

    }
}