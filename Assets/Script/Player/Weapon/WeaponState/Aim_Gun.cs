using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim_Gun : Istate_Gun
{
    BaseWeapon GunFSM;
    Attribute_Gun Attribute_Gun;


    public Aim_Gun(BaseWeapon fSM, Attribute_Gun attribute_Gun)
    {
        GunFSM = fSM;
        Attribute_Gun = attribute_Gun;
    }
    //寻找攻击距离内最近距离的怪物，进行瞄准等待cd，进入时的目标瞄准
    public void OnEnter()
    {
        Attribute_Gun.Ani.Play(Attribute_Gun.transform.name+"_Idle");
    }

    public void OnUpdate()
    {
        //武器瞄准
        if(Attribute_Gun.Target.position.x > Attribute_Gun.transform.position.x)
        {
            Attribute_Gun.transform.localScale = new Vector3(1,1,1);
        }else if(Attribute_Gun.Target.position.x < Attribute_Gun.transform.position.x)
        {
            Attribute_Gun.transform.localScale = new Vector3(1,-1,1);
        }
        Vector2 dir = (Attribute_Gun.Target.position - Attribute_Gun.transform.position);
        Attribute_Gun.transform.right = dir;

        //攻击范围--》攻击
        if(Attribute_Gun.FireTimer <= 0 &&Vector2.Distance(Attribute_Gun.transform.position,Attribute_Gun.Target.position) < Attribute_Gun.AttackRange)
        {
            GunFSM.ChangeCurrenState(State_Gun.Fire);
        }
        if(Attribute_Gun.Target == null)
        {
            GunFSM.ChangeCurrenState(State_Gun.Idel);
        }
    }
    public void OnExit()
    {

    }
}
