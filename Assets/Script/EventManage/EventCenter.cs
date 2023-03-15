using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCenter : BaseSingleton<EventCenter>
{
    Dictionary<string,List<Delegate>> EventContainer = new Dictionary<string, List<Delegate>>();
    public void ShowEvent()
    {
        
        foreach(string s in EventContainer.Keys)
        {
            Debug.Log(s);
        }
    }

    public void AddListenerBase(string name,Delegate callBack)//重载==》？同一方法名不同参数，调用时安装参数进行调用 ，委托做容器
    {
        if(!EventContainer.ContainsKey(name))
        {
            EventContainer.Add(name,new List<Delegate>(){callBack});
        }else
        {
            EventContainer[name].Add(callBack);
        }
    }



    public void AddListener(string name,Action callBack)
    {
        AddListenerBase(name,callBack);
    }

    public void AddListener<T>(string name,Action<T> callBack)
    {
        AddListenerBase(name,callBack);
    }
    public void AddListener<T1,T2>(string name,Action<T1,T2> callBack)
    {
        AddListenerBase(name,callBack);
    }

    public void Trigger(string name)
    {
        if(EventContainer.ContainsKey(name))
        {
            foreach(Delegate callBack in EventContainer[name])
            {
                (callBack as Action).Invoke();
            }
        }
    }

    public void Trigger<T>(string name,T info)
    {
        if(EventContainer.ContainsKey(name))
        {
            foreach(Delegate callBack in EventContainer[name])
            {
                (callBack as Action<T>).Invoke(info);
            }
        }
    }
     public void Trigger<T1,T2>(string name,T1 info,T2 info2)
    {
        if(EventContainer.ContainsKey(name))
        {
            foreach(Delegate callBack in EventContainer[name])
            {
                (callBack as Action<T1,T2>).Invoke(info,info2);
            }
        }
    }


    public void RemoveListenerBase(string name,Delegate callBack)
    {
        if(EventContainer[name].Count>0)
        {
            EventContainer[name].Remove(callBack);
        }
    }
    public void RemoveListener(string name)
    {
        EventContainer.Remove(name);
    }

    public void RemoveListener(string name,Action callBack)
    {
        EventContainer[name].Remove(callBack);
    }

    public void RemoveListener<T>(string name,Action<T> callBack)
    {
        
        EventContainer[name].Remove(callBack);
    }

    public void RemoveListener<T1,T2>(string name,Action<T1,T2> callBack)
    {
        EventContainer[name].Remove(callBack);
    }
    
    public void Clear()
    {
        EventContainer.Clear();
    }
}
