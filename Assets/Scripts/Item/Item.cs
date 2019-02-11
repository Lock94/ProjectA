using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 物品基类
/// </summary>
public class Item
{

    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ItemType Type { get; set; }
    public ItemTechnology Technology { get; set; }
    public int Value { get; set; }
    public int MaxStark { get; set; }
    public string Sprite { get; set; }

    public int SellPrice { get; set; }
    public int BuyPrice { get; set; }

    public Item(int id, string name, string description,ItemType type, ItemTechnology technology, int value,int maxStark,string sprite)
    {
        this.ID = id;
        this.Name = name;
        this.Description = description;
        this.Type = type;
        this.Technology = technology;
        this.Value = value;
        this.MaxStark = maxStark;
        this.Sprite = sprite;
    }
    /// <summary>
    /// 物品类型
    /// </summary>
    public enum ItemType
    {
        None,
        Consumable,
        Package,
        Equipment,
        MeleeWeapon,
        ShootWeapon,
        Unuseable,
        Blueprint,
        Ammo,
        Buff,
        Skill
    }
    /// <summary>
    /// 物品等级
    /// </summary>
    public enum ItemTechnology
    {
        T0, T1, T2, T3, T4, T5
    }
    /// <summary>
    /// 面板显示信息
    /// </summary>
    /// <returns></returns>
    public virtual string GetToolTipText()
    {
        string color = "";
        switch (Technology)
        {
            case ItemTechnology.T0:
                color = "orange";
                break;
            case ItemTechnology.T1:
                color = "white";
                break;
            case ItemTechnology.T2:
                color = "green";
                break;
            case ItemTechnology.T3:
                color = "blue";
                break;
            case ItemTechnology.T4:
                color = "red";
                break;
            case ItemTechnology.T5:
                color = "magenta";
                break;
        }
        string text = string.Format("<size=18><color={0}>{1}</color></size>\n<size=10>物品价值： {2}</size>\n" +
                                    "------------------------------\n" +
                                    "<size=13>{3}</size>\n" +
                                     "------------------------------\n" ,
                                    color, Name, Value, Description);
        if (SellPrice!=0&&BuyPrice!=0)
        {
            text += string.Format("<size=13>购买价格： {0}</size>\n<size=13>出售价格： {1}</size>\n", BuyPrice, SellPrice);
        }
        return text;
    }
}
