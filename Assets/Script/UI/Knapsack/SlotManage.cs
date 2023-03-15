using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotManage : MonoSingleton<SlotManage>
{
    public PickInfo ShowInfo;
    public MenuUI Menu;
    public PickItem pickItem;
    public int CheckPoint;
    SlotItem PickSlotItem;
    SlotUI PreSlotUI;
    List<GameObject> SlotList;
    Dictionary<SlotItem,GameObject> SlotDic;
    private void Awake() {
        SlotList = new List<GameObject>();
        SlotDic = new Dictionary<SlotItem, GameObject>();
        InitList(SlotList);
    }

    public void AddToKnapsack(Item item)
    {
        if(KnapsackDate.Instance.ContainsItem(item.Name))
        {
            SlotItem temp = KnapsackDate.Instance.GetSlotItem(item.Name);
            if(temp.CurrentCount +1 > temp.item.Capacity)
            {
                return;
            }
        }
        KnapsackDate.Instance.AddItem(item);
        SetSlot(KnapsackDate.Instance.GetSlotItem(item.Name));
    }


    public void SetSlot(SlotItem slotItem)
    {
        if(SlotDic.ContainsKey(slotItem))
        {
            SlotDic[slotItem].GetComponent<SlotUI>().SetSlotItem(slotItem);
            return;
        }

        foreach(GameObject g in SlotList)
        {
            if(g.GetComponent<SlotUI>().slotItem == null)
            {
                SlotDic.Add(slotItem,g);
                g.GetComponent<SlotUI>().SetSlotItem(slotItem);
                return;
            }
        }
    }
    public void InitList(List<GameObject> SlotList)
    {
        foreach(Transform t in transform)
        {
            SlotList.Add(t.gameObject);
        }
    }
    //右键菜单
    public void RightShowMenu(SlotItem slotItem)
    {
        Menu.SetSlotItem(slotItem);
    }
    //左键选择
    public void LeftShowPickInfo(SlotUI ui,SlotItem slotItem = null)
    {
        //手上有东西  格子里没东西==>把手上的东西放到格子里，并且手上的东西消失
        if(PickSlotItem != null && ui.slotItem == null)
        {
            PickSlotItem = null;//记录手上状态
            ui.SetSlotItem(pickItem.item);//把手上的东西放到格子里
            SlotDic.Add(pickItem.item,ui.gameObject);
            pickItem.SetSlotItem();//并且手上的东西消失 设为null
            return;
        }
        //手上没有东西  格子里有东西==> 把格子里的东西放到手上，并且格子里的东西消失
        if(PickSlotItem == null && ui.slotItem != null)
        {
            ui.ChangeUI();//格子里取出来
            SlotDic.Remove(slotItem);
            PickSlotItem = slotItem;//记录手上状态
            
            PreSlotUI = ui;//记录上一个ui 用于取消移动
            pickItem.gameObject.SetActive(true);//跟随鼠标移动
            pickItem.SetSlotItem(slotItem);//格子里东西放到手上
            return;
        }
    }
    //点击ui之外的地方取消选取
    public void CancelPick(SlotItem item)
    {
        if(CheckPoint != 3) return;
        PickSlotItem = null;//记录手上状态
        PreSlotUI.SetSlotItem(item);//取消后放回原位
        SlotDic.Add(item,PreSlotUI.gameObject);
        pickItem.gameObject.SetActive(false);
    }

    public void EnterUI(SlotItem slotItem)
    {
        string info = slotItem.item.Name+" "+slotItem.item.Describe+" "+slotItem.item.Price;
        Vector2 Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ShowInfo.SetInfo(info,Pos);
    }

    public void ExitUI(SlotItem slotItem)
    {
        ShowInfo.HideInfo();
    }

    public void UseBtn(SlotItem slotItem)
    {

    }
    public void RemoveBtn(SlotItem slotItem)
    {

    }
}
