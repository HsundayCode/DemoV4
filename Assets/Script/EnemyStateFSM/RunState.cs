using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : Istate
{
    EnemyFSM FSM;
    EnemyAttribute attribute;
    public RunState(EnemyFSM Controller)
    {
        FSM = Controller;
        attribute = Controller.attribute;
    }

    public void OnEnter()
    {
        attribute.EnemyAni.Play("Run");
    }

    public void OnUpdate()
    {
        //Vector2.MoveTowards(attribute.transform.position,attribute.TargetPos.position,attribute.moveSpeed * Time.deltaTime);
        attribute.transform.position += (attribute.TargetPos.position - attribute.transform.position).normalized * attribute.moveSpeed * Time.deltaTime;
        if(Vector3.Distance(attribute.transform.position,attribute.TargetPos.position) <=0.1f)
        {
            FSM.ChangeState(State.Attack);
        }
        if(attribute.TakeHit)
        {
            FSM.ChangeState(State.TakeHit);
        }
        if(attribute.HP <= 0)
        {
            FSM.ChangeState(State.Death);
        }
    }

    public void OnExit()
    {
        //Debug.Log("退出追击");
        //FSM.attribute.EnemyAni.SetBool("isRun",false);
    }
}
