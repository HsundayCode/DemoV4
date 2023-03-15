using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeFireState_Gun : FireState_Gun
{
    BaseWeapon GunFSM;
    Attribute_Gun Attribute_Gun;
    GameObject Bullet;
    public ThreeFireState_Gun(BaseWeapon gunFSM, Attribute_Gun attribute_Gun) : base(gunFSM, attribute_Gun)
    {
        base.count = 3;
        GunFSM = gunFSM;
        Attribute_Gun = attribute_Gun;
    }
    public new void OnEnter()
    {
        base.OnEnter();
    }
    public new void OnUpdate()
    {
        base.OnUpdate();
    }
    public new void OnExit()
    {
        base.OnExit();
    }
}
