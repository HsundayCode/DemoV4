using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventCenter_Func : BaseSingleton<EventCenter_Func>
{
    Dictionary<string,Delegate> FuncDic = new Dictionary<string, Delegate>();

    public void AddListener<T1,T2>(string name,Func<T1,T2> callBack)
    {
        FuncDic.Add(name,callBack);
    }

    public T2 Trigger<T1,T2>(string name,T1 info)
    {

        Delegate callBack = FuncDic[name];
        return (callBack as Func<T1,T2>).Invoke(info);
    }
}
