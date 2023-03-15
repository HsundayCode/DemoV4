using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdelState : Istate
{
    EnemyFSM FSM;
    EnemyAttribute attribute;
    public IdelState(EnemyFSM Controller)
    {
        FSM = Controller;
        attribute = Controller.attribute;
    }

    public void OnEnter()
    {
        attribute.EnemyAni.Play("Idle");
    }


    public void OnUpdate()
    {
        if(attribute.TargetPos != null)
        {
            FSM.ChangeState(State.Run);
        }
        if(attribute.HP <= 0)
        {
            FSM.ChangeState(State.Death);
        }
    }

    public void OnExit()
    {
        //Debug.Log("发现目标");
    }
}
