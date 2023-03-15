using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickItem : MonoBehaviour
{
    public SlotItem item;
    public Text Count;
    public Image Icon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(item != null)
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButtonDown(0) && item != null && gameObject.activeSelf)
        {
            SlotManage.Instance.CancelPick(item);
        }
    }

    public void SetSlotItem(SlotItem item = null)
    {
        if(item == null)
        {
            this.item = null;
            Count.text = "";
            Icon.sprite = null;
            gameObject.SetActive(false);
            return;
        }
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.item = item;
        Count.text = item.CurrentCount.ToString();
        Icon.sprite = ResourceLoading.Instance.Load<Sprite>(item.item.IconPath);
    }
}
