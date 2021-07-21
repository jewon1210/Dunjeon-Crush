using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.CharacterScripts;
using HeroEditor.Common.Enums;


public class PlayerAttack : MonoBehaviour
{
    public Character Character;
    public Transform ArmL;
    public Transform ArmR;
    public KeyCode Attack;

    void Start()
    {
        
    }

    void Update()
    {
        if (Character.Animator.GetInteger("State") >= (int)CharacterState.DeathB) return;

        switch (Character.WeaponType)
        {
            case WeaponType.Melee1H:
            case WeaponType.Melee2H:
            case WeaponType.MeleePaired:
                if (Input.GetKeyDown(Attack))
                {
                    Character.Slash();
                }
                break;
        }
    }
}
