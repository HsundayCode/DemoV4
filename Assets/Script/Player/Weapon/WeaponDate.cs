using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponDate :BaseSingleton<WeaponDate>
{
    public Dictionary<string,WeaponCfg> weaponContainer;

    public WeaponDate()
    {
         weaponContainer = new Dictionary<string, WeaponCfg>();
         weaponContainer.Add("ShootGun",new WeaponCfg("ShootGun","ShootGun_Bullet",1f,null,5f,10f,5f,new Dictionary<State_Gun, string>(){{State_Gun.Fire,"ThreeFire"}}));
    }

    public void addCompoment(string name,GameObject weapon)
    {
        if(name == "Pistor")
        {
            weapon.AddComponent<Pistor>();
        }
    }

    public static object GetWeaponState(string name,BaseWeapon fsm)
    {
        if(name == "Idle")
        {
            return new Idle_Gun(fsm,fsm.attribute_Gun);
        }
        if(name == "ThreeFire")
        {
            return new ThreeFireState_Gun(fsm,fsm.attribute_Gun);
        }
        return new Idle_Gun(fsm,fsm.attribute_Gun);
    }
}
