using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State_Gun{
    Idel,
    Fire,
    Aim,
}
public class Attribute_Gun{
    public Transform Target;
    public Transform transform;
    public float BulletMoveSpeed;
    public float FireTimer;
    public float Coolingtime;
    public float AttackRange;
    public string BulletType;
    public Transform Muzzle;
    public Animator Ani;
    
}
public class BaseWeapon : MonoBehaviour
{
    protected Dictionary<State_Gun,Istate_Gun> WeaponState = new Dictionary<State_Gun, Istate_Gun>();
    public Attribute_Gun attribute_Gun = new Attribute_Gun();
    Istate_Gun CurrentState;
    protected virtual void Awake() {
        attribute_Gun.Muzzle = transform.Find("Muzzle").transform;
        attribute_Gun.Ani = GetComponent<Animator>();
        attribute_Gun.FireTimer = 0;
        attribute_Gun.Coolingtime = 0.5f;
        attribute_Gun.AttackRange = 5f;
        attribute_Gun.BulletMoveSpeed = 5f;
        attribute_Gun.transform = transform;

        WeaponState.Add(State_Gun.Idel,new Idle_Gun(this,attribute_Gun));
        WeaponState.Add(State_Gun.Aim,new Aim_Gun(this,attribute_Gun));
        WeaponState.Add(State_Gun.Fire,new FireState_Gun(this,attribute_Gun));
        CurrentState = WeaponState[State_Gun.Idel];
        CurrentState.OnEnter();

    }
    protected virtual void Update() {
        if(GetTarget() != null)
        {
            attribute_Gun.Target = GetTarget().transform;//保持目标最近
        }
        
        //attribute_Gun.Target = GameObject.FindGameObjectWithTag("Enemy").transform;
        
        if(attribute_Gun.FireTimer >0)
        {
            attribute_Gun.FireTimer -= Time.deltaTime;
        }
        CurrentState.OnUpdate();
    }

    public virtual void ChangeCurrenState(State_Gun state)
    {
        if(WeaponState.ContainsKey(state))
        {
            CurrentState.OnExit();
            CurrentState = WeaponState[state];
        }
        CurrentState.OnEnter();
    }

    public GameObject GetTarget()
    {
        GameObject minEnemy = null;
        float Mindistance = float.MaxValue;
        List<GameObject> EnemyList = EnemyManage.Instance.GetCurrentEnemy();
        for(int i =0;i<EnemyList.Count;i++)
        {
            float Tempdistance = Vector2.Distance(transform.position,EnemyList[i].transform.position);//距离
            if(Tempdistance < Mindistance)
            {
                Mindistance = Tempdistance;
                minEnemy = EnemyList[i];
            }
        }
        return minEnemy;
    }
    public void InitCfg(WeaponCfg cfg)
    {
        Debug.Log(3);
        attribute_Gun.FireTimer = 0;
        attribute_Gun.Coolingtime = cfg.Coolingtime;
        attribute_Gun.AttackRange = cfg.Attackrange;
        attribute_Gun.BulletMoveSpeed = cfg.BulletMoveSpeed;
        attribute_Gun.BulletType = cfg.bulletType;
        if(cfg.StateList == null)
        {
            return;
        }
        foreach(State_Gun k in cfg.StateList.Keys)
        {
            if(WeaponState.ContainsKey(k))
            {
                WeaponState[k] = (Istate_Gun)WeaponDate.GetWeaponState(cfg.StateList[k],this);
            }else
            {
                WeaponState.Add(k,(Istate_Gun)WeaponDate.GetWeaponState(cfg.StateList[k],this));
            }
        }
    }

    //主要是修改开火
    protected virtual void ChangeState(State_Gun Newstate,Istate_Gun state)
    {
        
    }
    protected virtual void  ChangeAttribute(string name,object T1)
    {

    }
}
