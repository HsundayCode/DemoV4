using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//物品的功能是吧物品持有的参数暴露出去，让对应的变化
public class Item
{
    public int Id;
    public string Name;
    public string Describe;
    public string IconPath;
    public int Capacity;
    public int Price;

    public Item(int id, string name, string describe, string iconPath, int capacity, int price)
    {
        Id = id;
        Name = name;
        Describe = describe;
        IconPath = iconPath;
        Capacity = capacity;
        Price = price;
    }
}
