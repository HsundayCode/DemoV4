using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//怪物状态集合
public enum State{
    Idle,Attack,Death,TakeHit,Run,
}
//怪物属性用于根据条件改变状态
public class EnemyAttribute
{
    public int HP;
    public float moveSpeed;
    public int Source;
    public Animator EnemyAni;
    public BoxCollider2D collider2D;
    public bool TakeHit;
    public Rigidbody2D rigidbody2D;
    public Vector3 Pos;//当前位置
    public Transform transform;//当前
    public Transform TargetPos;//目标 人物
    public CircleCollider2D AttackBox;

}
public class EnemyFSM : MonoBehaviour
{

    protected Istate CurrentState;
    public Dictionary<State,Istate> Enemystates = new Dictionary<State, Istate>();
    public EnemyAttribute attribute;
    public void Init()
    {
    }
    public virtual void Awake() {
                
        attribute = new EnemyAttribute();
        attribute.EnemyAni = GetComponent<Animator>();
        attribute.collider2D = GetComponent<BoxCollider2D>();
        attribute.Pos = transform.localPosition;
        attribute.transform = transform;
        attribute.TargetPos = GameObject.FindGameObjectWithTag("Player").transform;
        attribute.AttackBox = transform.Find("AttackBox").GetComponent<CircleCollider2D>();
        attribute.moveSpeed = 1.5f;
        attribute.Source = 20;
        
        Enemystates.Add(State.Idle,new IdelState(this));
        Enemystates.Add(State.Run,new RunState(this));
        Enemystates.Add(State.Attack,new AttackState(this));
        Enemystates.Add(State.Death,new DeathState(this));
        Enemystates.Add(State.TakeHit,new TakeHitState(this));
        CurrentState = Enemystates[State.Idle];
        //EventCenter.Instance.AddListener<int>(gameObject.name+"HP",ReduceHP);
    }
    public virtual void OnEnable() {
        
        attribute.HP = 100;
        attribute.TakeHit = false;
        CurrentState = Enemystates[State.Idle];//为了对象池 报错是因为启动的时候还没初始化
        attribute.collider2D.enabled = true;
    }
    // Start is called before the first frame update
    public virtual void Start()
    {

        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        CurrentState.OnUpdate();
        FlipFace();
    }
    //面向玩家
    public virtual void FlipFace()
    {
        if(transform.position.x >= attribute.TargetPos.position.x)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
        }else if(transform.position.x < attribute.TargetPos.position.x)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
    }

    //切换状态
    public virtual void ChangeState(State state)
    {
        //Debug.Log("进入"+state);
        CurrentState.OnExit();
        if(Enemystates.ContainsKey(state))
        {
            CurrentState = Enemystates[state];
        }
        CurrentState.OnEnter();
    }
    //进入被击状态 有bug 没有碰撞 却触发了
    public virtual void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Bullet")
        {
            attribute.TakeHit = true;
            AudioManage.Instance.PlaySound(gameObject,"Enemy_BulletHit");
        }
        if(other.gameObject.tag == "SwordBullet")
        {
            attribute.TakeHit = true;
            AudioManage.Instance.PlaySound(gameObject,"SwordHit");
        }
    }
    //被击减血量
    public virtual void ReduceHP(int num)
    { 
        Debug.Log("伤害"+num);
        if(attribute.HP > 0)
        {
            attribute.HP -= num;
        }else if(attribute.HP <= 0 && CurrentState != Enemystates[State.Death])
        {
            attribute.collider2D.enabled = false;
            ChangeState(State.Death);
        }
    }

    //攻击碰撞盒子
    public virtual void AttackBox_AniEventActive()
    {
        attribute.AttackBox.gameObject.SetActive(true);
    }
        public void AttackBox_AniEventExit()
    {
        attribute.AttackBox.gameObject.SetActive(false);
    }
}
