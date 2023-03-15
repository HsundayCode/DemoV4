using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : Istate
{
    EnemyFSM FSM;
    EnemyAttribute attribute;
    float timer;
    public AttackState(EnemyFSM Controller)
    {
        FSM = Controller;
        attribute = FSM.attribute;
    }

    public void OnEnter()
    {
        timer = 1.5f;
        attribute.EnemyAni.Play("Attack");
        AudioManage.Instance.PlaySound(attribute.transform.gameObject,"EnemyHit");
    }


    public void OnUpdate()
    {
        if(attribute.EnemyAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            attribute.EnemyAni.Play("Idle");
            if(timer < 0)
            {
                OnEnter();
            }
        }

        if(timer >= 0)
        {
            timer -= Time.deltaTime;
        }

        if(Vector2.Distance(attribute.transform.position,attribute.TargetPos.position)>1 && attribute.EnemyAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            FSM.ChangeState(State.Run);
        }
        if(attribute.HP <= 0)
        {
            FSM.ChangeState(State.Death);
        }
        if(attribute.TakeHit)
        {
            FSM.ChangeState(State.TakeHit);
        }
    }

    public void OnExit()
    {
        //Debug.Log("退出攻击");
    }
}
