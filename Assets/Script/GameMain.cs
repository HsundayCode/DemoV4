using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ItemManage.Instance.AddItem(new Item(1,"apple","这是一个苹果","apple",99,10));
        ItemManage.Instance.AddItem(new Item(2,"book","这是一本魔法书","book",99,10));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(1);
            SlotManage.Instance.AddToKnapsack(ItemManage.Instance.GetItem("apple"));
        }
    }
}
