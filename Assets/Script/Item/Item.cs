using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 物品基类
/// </summary>
public class Item {

    public int ID { get; set; }

    public string Name { get; set; }

    public ItemType Type { get; set; }//物品类型 

    public string Description { get; set; }//描述

    public int Capacity { get; set; }//数量

    public string Sprite { get; set; }//物品的精灵


    public Item(int id,string name,ItemType type,string des,int capacity,string sprite)//物品类的构造方法
    {
        this.ID = id;
        this.Name = name;
        this.Type = type;
        this.Description = des;
        this.Capacity = capacity; 
        this.Sprite = sprite;
    }

    public enum ItemType
    {
        Consumable,//消耗品（食物或是药水）
        Equipment,//装备（防具或是装饰）
        Tool//工具
    }
    public override string ToString()
    {
        string s = "";
        s += ID;
        s += Name;
        s += Type;
        s += Description;
        s += Capacity;
        s += Sprite;
        return s;
    }

    public virtual string GetTipText()
    {
        string text = "";
        text += Name + "\n";
        text += "描述：" + Description;
        return text;
    }
}
     