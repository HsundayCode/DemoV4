using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanleManage : MonoSingleton<PanleManage>
{
    BasePanle[] BasePanles;
    Dictionary<string,BasePanle> PanleDic;
    GameObject canvas;
    private void Awake() {
        Debug.Log("UI管理启动");
        Init();
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void Init()
    {
        canvas = GameObject.Find("Canvas");
        PanleDic = new Dictionary<string, BasePanle>();
        BasePanles = GetComponentsInChildren<BasePanle>(true);
        foreach(BasePanle t in BasePanles)
        {
            PanleDic.Add(t.name,t);
        }
    }

    public void AddPanle(string name)
    {
        var t = ResourceLoading.Instance.Load<GameObject>(name);
        GameObject panle = GameObject.Instantiate(t);
        panle.transform.SetParent(canvas.transform);
        panle.transform.localScale = Vector3.one;
        panle.name = name;
        panle.SetActive(false);
        PanleDic.Add(name,panle.GetComponent<BasePanle>());
        
    }

    public void ShowPanle(string name)
    {
        if(!PanleDic.ContainsKey(name))
        {
            AddPanle(name);
        }
        PanleDic[name].Show();
    }

    public void HidePanle(string name)
    {
        if(PanleDic.ContainsKey(name))
        PanleDic[name].Hide();
    }

    public void HideAllPanle()
    {
        foreach(BasePanle b in PanleDic.Values)
        {
            b.Hide();
        }
    }
}
