using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManage : BaseSingleton<ItemManage>
{
    Dictionary<string,Item> ItemDate = new Dictionary<string, Item>();

    public Item GetItem(string name)
    {
        if(!ItemDate.ContainsKey(name))
        {
            return null; 
        }
        return ItemDate[name];
    }

    public void AddItem(Item item)
    {
        if(ItemDate.ContainsKey(item.Name))
        {
            return;
        }
        ItemDate.Add(item.Name,item);
    }

}
