using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : Item
{
    /// <summary>
    /// buff的ID，对应效果
    /// </summary>
    public int BuffID;
    /// <summary>
    /// buff持续的时间
    /// </summary>
    public int DurationTime;
    /// <summary>
    /// 效果描述
    /// </summary>
    public string EffectDescription;
    /// <summary>
    /// 影响的属性
    /// </summary>
    public string[] EffectProp;
    /// <summary>
    /// 影响的值
    /// </summary>
    public int[] EffectValue;

    public Buff(int id, string name, string description, ItemType type, ItemTechnology technology, int value, int maxStark, string sprite,int buffID,int durationTime,string effectDescription,string[] effectProp,int[] effectValue) : base(id, name, description, type, technology, value, maxStark, sprite)
    {
        this.BuffID = buffID;
        this.DurationTime = durationTime;
        this.EffectDescription = effectDescription;
        this.EffectProp = effectProp;
        this.EffectValue = effectValue;
    }
    public override string GetToolTipText()
    {
        string color = "white";
        string text = string.Format("<size=18><color={0}>{1}</color></size>\n" +
                                    "------------------------------\n" +
                                    "<size=13>{2}</size>\n" +
                                     "------------------------------\n" +
                                     "<size=13>{3}</size>\n",
                                    color, Name, Description,EffectDescription);
        return text;
    }
}
