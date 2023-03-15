using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCfg 
{
    public string name;//名称
    public string bulletType;//发射的物品名字
    public float Coolingtime;
    public string SpritePath;//2d资源路径
    public float Attackrange;//范围
    public float Damage;//伤害
    public float BulletMoveSpeed;
    public Dictionary<State_Gun,string> StateList;

    public WeaponCfg(string name, string bulletType, float coolingtime, string spritePath, float attackrange, float damage, float bulletMoveSpeed, Dictionary<State_Gun, string> stateList)
    {
        this.name = name;
        this.bulletType = bulletType;
        Coolingtime = coolingtime;
        SpritePath = spritePath;
        Attackrange = attackrange;
        Damage = damage;
        BulletMoveSpeed = bulletMoveSpeed;
        StateList = stateList;
    }
}

public enum WeaponType{
    
}
