using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManage : MonoSingleton<EnemyManage>
{
    public float Timer = 2;
    int end;
    List<string> EnemyList;
    bool isCreate;
    int Count;
    Vector2 PlayerPos;

    List<GameObject> EnemyContainer;

    // Start is called before the first frame update
    void Start()
    {
        Count = 100;
        isCreate = true;    
        EnemyContainer = new List<GameObject>();
        EnemyList = new List<string>(){"Flying_eye","Goblin","Skeleton"};
        EventCenter.Instance.AddListener("CleanEnemy",Clean);
        EventCenter.Instance.AddListener<int>("CreateEnemy",CreateEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        if(Timer <= 0 && isCreate && Count >0 )
        {
            Timer = 2; 
            RandomCreate();
        }else
        {
            Timer -= Time.deltaTime;
        }
    }


    //随机生成
    public void RandomCreate()
    {
        Count--;
        if(Count > 0)
        {
            GameObject enemy =  ObjectPool.Instance.GetObject(EnemyList[Random.Range(0,EnemyList.Count)]);
            enemy.transform.position = PlayerPos + RandomPos();
            enemy.transform.localScale = Vector3.one;
            enemy.GetComponent<EnemyFSM>().Init();
            EnemyContainer.Add(enemy);
        }

    }
    public void DeathEnemy(GameObject enemy)
    {
        EnemyContainer.Remove(enemy);
    }
    public List<GameObject> GetCurrentEnemy()
    {
        return EnemyContainer;
    }
    public void CreateEnemy(int Count)
    {
        this.Count = Count;
        isCreate = true;
    }
    //清除所有怪物 暂停生成
    public void Clean()
    {
        for(int i = 0;i<EnemyContainer.Count;i++)
        {
            if(EnemyContainer[i].activeSelf)
            {
                ObjectPool.Instance.PushObject(EnemyContainer[i]);
            }
        }
        Count = 0;
        isCreate = false;
    }
    //计算以角色圆心半径为3的圆，在这个圆的面积内不生生成怪物，半径为8的圆 -半径3的圆的面积里生成 
    //再角色周围随机在8-3的距离内选取一个点进行怪物生成 ,
    //判断一个点在不在圆内 -8,8,剔除-3,3 
    public Vector2 RandomPos()
    {
        Vector2 Pos = new Vector2(Random.Range(-8,8),Random.Range(-8,8));
        if(Pos.x > -2 && Pos.x < 2)
        {
            Pos.x = Pos.x >0?-4:4;
        }
        if(Pos.y > -2 && Pos.y < 2)
        {
            Pos.y = Pos.y >0?-4:4;
        }
        return Pos;
    }
}
