using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 消耗品
/// </summary>
public class Consumable : Item
{
    /// <summary>
    /// 恢复血量
    /// </summary>
    public int HP { get; set; }
    /// <summary>
    /// 恢复热量
    /// </summary>
    public int EG { get; set; }
    /// <summary>
    /// 恢复健康度
    /// </summary>
    public int HL { get; set; }
    /// <summary>
    /// 暂时体质点
    /// </summary>
    public int TempConstitution { get; set; }
    /// <summary> 
    /// 暂时力量点
    /// </summary>
    public int TempStrength { get; set; }
    /// <summary>
    /// 暂时灵巧点
    /// </summary>
    public int TempAgility { get; set; }
    /// <summary>
    /// 暂时洞察点
    /// </summary>
    public int TempDexterous { get; set; }
    /// <summary>
    /// 暂时专注点
    /// </summary>
    public int TempConcentration { get; set; }
    /// <summary>
    /// 药效时间(秒)
    /// </summary>
    public int ContinuedTime { get; set; }
    /// <summary>
    /// 获得的BUFF的ID
    /// </summary>
    public int BuffID { get; set; }

    public Consumable(int id, string name,string description ,ItemType type, ItemTechnology technology, int value,int maxStark, string sprite,int hp,int eg,int hl,
        int tempConstitution,int tempStrength,int tempAgility,int tempDexterous,int tempConcentration,int continuedTime,int buffID)
        :base(id,name,description,type,technology,value, maxStark,sprite)
    {
        this.HP = hp;
        this.EG = eg;
        this.HL = hl;
        this.TempConstitution = tempConstitution;
        this.TempStrength = TempStrength;
        this.TempAgility = tempAgility;
        this.TempDexterous = tempDexterous;
        this.TempConcentration = tempConcentration;
        this.ContinuedTime = continuedTime;
        this.BuffID = buffID;
    }

    public override string GetToolTipText()
    {
        string text= base.GetToolTipText();
        if (HP!=0)
        {
            text += string.Format("\n<size=13>恢复血量： {0}</size>", HP);
        }
        if (EG !=0)
        {
            text += string.Format("\n<size=13>恢复热量： {0}</size>", EG);
        }
        if (HL!=0)
        {
            text += string.Format("\n<size=13>恢复健康度： {0}</size>", HL);
        }
        if (TempConstitution != 0)
        {
            text += string.Format("\n<size=13>体质增加： {0}</size>", TempConstitution);
        }
        if (TempStrength != 0)
        {
            text += string.Format("\n<size=13>力量增加： {0}</size>", TempStrength);
        }
        if (TempAgility != 0)
        {
            text += string.Format("\n<size=13>灵巧增加： {0}</size>", TempAgility);
        }
        if (TempDexterous != 0)
        {
            text += string.Format("\n<size=13>洞察增加： {0}</size>", TempDexterous);
        }
        if (TempConcentration != 0)
        {
            text += string.Format("\n<size=13>专注增加： {0}</size>", TempConcentration);
        }
        return text;
    }
}
