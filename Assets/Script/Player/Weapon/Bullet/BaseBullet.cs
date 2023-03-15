using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    public float MoveSpeed;//速
    public float Distance;//距离
    public int PierceCount;
    public float Duration;
    public Vector3 dir;
    public Vector2 EndPos;
    private void Awake() {
        MoveSpeed = 5f;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += dir * Time.deltaTime * MoveSpeed;
        if(Vector2.Distance(EndPos,transform.position) <= 0.1f)
        {
            ObjectPool.Instance.PushObject(gameObject);
        }
    }

    public void SetDir(Vector3 dir)
    {
        EndPos = transform.position+ dir * Distance;
        this.dir = dir;
    }
}
