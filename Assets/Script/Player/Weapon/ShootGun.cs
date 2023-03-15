using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : BaseWeapon
{
    new private void Awake() {
        base.Awake();
        base.WeaponState[State_Gun.Fire] = new ThreeFireState_Gun(this,base.attribute_Gun);
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();   
    }
}
