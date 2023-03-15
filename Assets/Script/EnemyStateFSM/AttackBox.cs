using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    public int Damage;
    EnemyFSM enemyFSM;
    private void Awake() {
        enemyFSM = gameObject.GetComponentInParent<EnemyFSM>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Damage = 20;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            //TODO计算伤害 触发事件中心的事件
            //Debug.Log("击中玩家");
            EventCenter.Instance.Trigger<int>("Changehp",-Damage);
        }
    }
}
