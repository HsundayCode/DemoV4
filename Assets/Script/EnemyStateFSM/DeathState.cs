using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : Istate
{
    EnemyFSM FSM;
    EnemyAttribute attribute;
    public DeathState(EnemyFSM Controller)
    {
        FSM = Controller;
        attribute = FSM.attribute;
    }
    public void OnEnter()
    {
        attribute.EnemyAni.Play("Death");
        EventCenter.Instance.Trigger<string>("TaskItem",attribute.transform.name);
        EventCenter.Instance.Trigger<int>("AddSource",attribute.Source);
    }


    public void OnUpdate()
    {
        if(attribute.EnemyAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            EnemyManage.Instance.DeathEnemy(attribute.transform.gameObject);
            ObjectPool.Instance.PushObject(attribute.transform.gameObject);
        }
        
    }

    public void OnExit()
    {
        //Debug.Log("退出死亡");
    }
}
