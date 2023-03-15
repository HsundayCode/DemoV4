using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : BaseSingleton<ObjectPool>
{
    public Dictionary<string,Queue<GameObject>> ObPool = new Dictionary<string,Queue<GameObject>>();

    GameObject Pool = GameObject.Find("Pool");
    public void Init(){

    }

    public void PushObject(GameObject o)
    {
        string PoolName = o.name+"Pool";
        ContainsPool(PoolName);
        if(ObPool.ContainsKey(PoolName))
        {
            o.SetActive(false);
            ObPool[PoolName].Enqueue(o);
        }
    }
    //获取对象激活
    public GameObject GetObject(string name)
    {
        string PoolName = name+"Pool";//对象池对应的key
        ContainsPool(PoolName);//是否存在
        GameObject o = null;
        if(ObPool.ContainsKey(PoolName))
        {
            if(ObPool[PoolName].Count  == 0)//对象池缓存数量不够
            {
                var temp = ResourceLoading.Instance.Load<GameObject>(name);//加载预制件
                o = GameObject.Instantiate(temp);
                o.name = name;
                o.transform.localScale = Vector3.one;
                o.transform.SetParent(GameObject.Find(PoolName).transform);//把对象实例化（放入场景）
                PushObject(o);//放入池
            }
            o = ObPool[PoolName].Dequeue();//获取对象
            o.transform.localScale = Vector3.one;
            o.SetActive(true);
        }
        return o;
    }

    public void ContainsPool(string name){
        //string PoolName = name+"Pool";
        if(!ObPool.ContainsKey(name))
        {
            GameObject PoolOb = new GameObject(name);
            PoolOb.transform.SetParent(Pool.transform); 
            ObPool.Add(name,new Queue<GameObject>());
        }
    }
}
