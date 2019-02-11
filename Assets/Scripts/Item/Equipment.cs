using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 装备
/// </summary>
public class Equipment : Item
{
    /// <summary>
    /// 对应的装备实例化ID
    /// </summary>
    public int EquipmentID { get; set; }
    /// <summary>
    /// 装备类型
    /// </summary>
    public EquipmentType EquipType { get; set; }
    /// <summary>
    /// 抗性
    /// </summary>
    public int Resistance { get; set; }
    /// <summary>
    /// 体质
    /// </summary>
    public int Constitution { get; set; }
    /// <summary>
    /// 力量
    /// </summary>
    public int Strength { get; set; }
    /// <summary>
    /// 灵巧
    /// </summary>
    public int Agility { get; set; }
    /// <summary>
    /// 洞察
    /// </summary>
    public int Dexterous { get; set; }
    /// <summary>
    /// 专注
    /// </summary>
    public int Concentration { get; set; }


    public Equipment(int id, string name, string description, ItemType type, ItemTechnology technology, int value, int maxStark, string sprite, int equipmentID, EquipmentType equipmentType, int resistance, int constitution, int strength, int agility, int dexterous, int concentration) : base(id, name, description, type, technology, value, maxStark, sprite)
    {
        this.EquipmentID = equipmentID;
        this.EquipType = equipmentType;
        this.Resistance = resistance;
        this.Constitution = constitution;
        this.Strength = strength;
        this.Agility = agility;
        this.Dexterous = dexterous;
        this.Concentration = concentration;
    }

    public enum EquipmentType
    {
       None, Head, Belt, Cloth, Pants
    }
    public override string GetToolTipText()
    {
        string text = base.GetToolTipText();
        if (Resistance != 0)
        {
            text += string.Format("\n<size=13>抗性提升： {0}</size>", Resistance);
        }
        if (Constitution != 0)
        {
            text += string.Format("\n<size=13>体质提升： {0}</size>", Constitution);
        }
        if (Strength != 0)
        {
            text += string.Format("\n<size=13>力量提升： {0}</size>", Strength);
        }
        if (Agility != 0)
        {
            text += string.Format("\n<size=13>灵巧提升： {0}</size>", Agility);
        }
        if (Dexterous != 0)
        {
            text += string.Format("\n<size=13>洞察提升： {0}</size>", Dexterous);
        }
        if (Concentration != 0)
        {
            text += string.Format("\n<size=13>专注提升： {0}</size>", Concentration);
        }
        return text;
    }
}
