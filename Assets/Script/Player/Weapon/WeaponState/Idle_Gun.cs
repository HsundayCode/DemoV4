using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_Gun : Istate_Gun
{
    BaseWeapon GunFSM;
    Attribute_Gun Attribute_Gun;

    public Idle_Gun(BaseWeapon gunFSM, Attribute_Gun attribute_Gun)
    {
        GunFSM = gunFSM;
        Attribute_Gun = attribute_Gun;
    }

    public void OnEnter()
    {
        
    }

    public void OnUpdate()
    {
        if(Attribute_Gun.Target != null)
        {
            GunFSM.ChangeCurrenState(State_Gun.Aim);
        }
    }
    public void OnExit()
    {

    }
}
