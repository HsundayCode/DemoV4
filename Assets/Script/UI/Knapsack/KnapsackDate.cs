using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnapsackDate : BaseSingleton<KnapsackDate>
{
    Dictionary<string,SlotItem> Knapsack = new Dictionary<string, SlotItem>();
    public void AddItem(Item item,int count = 1)
    {
        if(Knapsack.ContainsKey(item.Name))
        {
            Knapsack[item.Name].CurrentCount += count;
        }else
        {
            SlotItem temp = new SlotItem(item,count);
            Knapsack.Add(item.Name,temp);
        }
    }

    public SlotItem GetSlotItem(string name)
    {
        if(Knapsack.ContainsKey(name))
        {
            return Knapsack[name];
        }
        return null;
    }

    public bool ContainsItem(string name)
    {
        if(Knapsack.ContainsKey(name))
        {
            return true;
        }
        return false;
    }
}

public class SlotItem
{
    public Item item;
    public int CurrentCount;

    public SlotItem(Item item, int currentCount)
    {
        this.item = item;
        CurrentCount = currentCount;
    }
}
