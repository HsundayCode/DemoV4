using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Attribute_Player
{
    public int Hp;
    public int Mp;
    public int Attack;
    public int Source;
    public float MoveSpeed;
}
public class Player : MonoBehaviour
{
    public int maxHp = 500;
    public int maxMp = 500;
    public int maxMoveSpeed = 8;
    public int maxAttack = 20;
    BoxCollider2D Player_Col2D;//碰撞盒子
    Rigidbody2D Player_Rig2D;//刚体
    Animator Player_Ani;//动画器

    Vector3 Dash_Distance;//冲刺距离
    bool isDash;//冲刺判断
    bool DashCD;//冲刺冷却
    //public float MoveSpeed;
    Vector2 MosePos;//鼠标位置
    float InputX;//输入的x轴
    float InputY;//输入的Y轴
    Vector2 Move_Input;//移动方向
    Attribute_Player attribute_Player;
    private void Awake() 
    {
        Player_Ani = GetComponent<Animator>();
        Player_Col2D = GetComponent<BoxCollider2D>();
        Player_Rig2D = GetComponent<Rigidbody2D>();
        attribute_Player = new Attribute_Player();
        PlayerInit();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public void PlayerInit()
    {
        attribute_Player.Hp = 500;
        attribute_Player.Mp = 500;
        attribute_Player.Source = 500;
        attribute_Player.Attack = 1;
        attribute_Player.MoveSpeed = 5;

    }
    // Update is called once per frame
    void Update()
    {
        InputX = Input.GetAxisRaw("Horizontal");
        InputY = Input.GetAxisRaw("Vertical");
        Move_Input = new Vector2(InputX,InputY);
        MosePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!DashCD)
            {
                DashCD = true;
                StartCoroutine(Dashing());
                isDash = true;
                Dash_DirctinoSeting();
            }
        }
        
        ToDash();
        ToFlipFace();
        if(!isDash)
        {
            ToMove();
        }
        //ToDeath();
    }
    public void ToMove()
    {
        if(Move_Input != Vector2.zero)
        {
            Player_Ani.SetBool("isMove",true);
        }else
        {
            Player_Ani.SetBool("isMove",false);
        }
        Player_Rig2D.velocity = Move_Input.normalized * attribute_Player.MoveSpeed;
    }

    public void ToFlipFace()
    {
        if(MosePos.x >= transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0,0,0);

        }else if(MosePos.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
        }
    }

    // public void ToDeath()
    // {
    //     if(attribute_Player.Hp <= 0)
    //     {
    //         //Debug.Log("角色死亡");
    //         EventCenter.Instance.Trigger("GameOver");
    //     }
    // }

    public void ToDash()
    {
        if(isDash)
        {
            //transform.DOMove(Dash_Distance,1);
            //transform.position = Vector3.Lerp(transform.position,Dash_Distance,1/Time.deltaTime);
            transform.position = Vector2.MoveTowards(transform.position,Dash_Distance,20*Time.deltaTime);//距离=速度*时间
        }
        //需要价格时间限制或者冲刺时不能移动
        if(Vector3.Distance(transform.position,Dash_Distance) <= 0.1f )
        {
            isDash = false;
        }
    }
    public void Dash_DirctinoSeting()
    {   
        Vector3 tem = new Vector3(3,0,0);
        tem *= (Vector2)transform.right.normalized;
        Dash_Distance = transform.position + tem;
        //Debug.Log(Dash_Distance);
        if(InputX > 0)
        {
            Dash_Distance = transform.position + new Vector3(3,0,0);
        }else if(InputX < 0)
        {
            Dash_Distance = transform.position + new Vector3(-3,0,0);
        }
        if(InputY > 0)
        {
            Dash_Distance = transform.position + new Vector3(0,3,0);
        }else if(InputY <0)
        {
            Dash_Distance = transform.position + new Vector3(0,-3,0);
        }
    }

    IEnumerator Dashing()
    {
        yield return new WaitForSeconds(1);
        DashCD = false;
    }

 }
