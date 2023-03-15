using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFireState : Istate_Gun
{
    BaseWeapon GunFSM;
    Attribute_Gun Attribute_Gun;
    GameObject Bullet;
    public SwordFireState(BaseWeapon gunFSM, Attribute_Gun attribute_Gun)
    {
        GunFSM = gunFSM;
        Attribute_Gun = attribute_Gun;
    }
    public virtual  void OnEnter()
    {

    }
    public virtual void OnUpdate()
    {

    }
    public virtual void OnExit()
    {

    }
      
}
