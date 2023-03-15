using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistor : BaseWeapon
{
    new private void Awake() {
        base.attribute_Gun.BulletType = "Pistor_Bullet";
        base.Awake();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();   
    }

    protected override void ChangeState(State_Gun state, Istate_Gun Newstate)
    {
        if(WeaponState.ContainsKey(state))
        {
            WeaponState[state] = Newstate;
        }else
        {
            WeaponState.Add(state,Newstate);
        }
    }

    protected override void ChangeAttribute(string name,object T1)
    {
        if(name == "AttackRange")
        {
            attribute_Gun.AttackRange = (float)T1;
        }
    }
}
