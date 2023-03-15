using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHitState : Istate
{
    EnemyFSM FSM;
    EnemyAttribute attribute;
    float TakeHitSpeed;
    Vector3 Backwarddistance;
    Vector3 BackPos;//最后位置
    Vector2 backDirection;
    public TakeHitState(EnemyFSM Controller)
    {
        FSM = Controller;
        attribute = Controller.attribute;
    }

    public void OnEnter()
    {
       attribute.EnemyAni.Play("TakeHit");
       Backwarddistance = new Vector3(1,1,0);
       backDirection = attribute.transform.position - attribute.TargetPos.position;//方向
       backDirection.Normalize();
       BackPos = (Vector2)attribute.transform.position + Backwarddistance * backDirection;//目标位置=当前位置+距离*方向
       
    }


    public void OnUpdate()
    {
        //击退 不是距离是一个位置
        attribute.transform.position = Vector3.Lerp(attribute.transform.position,BackPos,0.08f);
        if(Vector3.Distance(attribute.transform.position,BackPos) <=0.1f)
        {
            FSM.ChangeState(State.Idle);
        }
        if(attribute.HP <= 0)
        {
            FSM.ChangeState(State.Death);
        }
    }

    public void OnExit()
    {
        //Debug.Log("退出被击");
        attribute.TakeHit = false;
    }
}
