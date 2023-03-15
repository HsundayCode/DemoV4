using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    SlotItem slotItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && slotItem != null)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetSlotItem(SlotItem item,SlotUI ui = null)
    {
        gameObject.SetActive(true);
        this.slotItem = item;
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector2(1,0);
    }
}
