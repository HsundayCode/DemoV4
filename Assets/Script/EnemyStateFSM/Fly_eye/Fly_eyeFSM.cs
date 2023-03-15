using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly_eyeFSM : EnemyFSM
{
    override public void Awake() {
        base.Awake();
    }

    // Update is called once per frame
    override public void Update()
    {
        CurrentState.OnUpdate();
        FlipFace();
    }
}
