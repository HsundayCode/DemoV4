using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotUI : MonoBehaviour,IPointerEnterHandler,IPointerClickHandler,IPointerExitHandler
{
    public Text Count;
    public Image Icon;
    public SlotItem slotItem;

    public void SetSlotItem(SlotItem slotItem)
    {
        this.slotItem = slotItem;
        ChangeUI(slotItem);
    }
    public void ChangeUI(SlotItem slotItem = null)
    {
        if(slotItem == null)
        {
            this.slotItem = null;
            Count.text = "";
            Icon.sprite = null;
            return;
        }
        Count.text = slotItem.CurrentCount.ToString();
        Icon.sprite = ResourceLoading.Instance.Load<Sprite>(slotItem.item.IconPath);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        SlotManage.Instance.CheckPoint = 1;
        if(slotItem == null) return;
        SlotManage.Instance.EnterUI(slotItem);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clik");
        //左键选择
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("left");
            SlotManage.Instance.LeftShowPickInfo(this,slotItem);
        }
        //右键菜单
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("right");
            SlotManage.Instance.RightShowMenu(slotItem);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SlotManage.Instance.CheckPoint = 3;
        if(slotItem == null) return;
        SlotManage.Instance.ExitUI(slotItem);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
