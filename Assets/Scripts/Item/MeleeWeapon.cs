using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 近战武器
/// </summary>
public class MeleeWeapon : Item
{
    /// <summary>
    /// 对应的武器实例化的ID
    /// </summary>
    public int MeleeWeaponID { get; set; }
    /// <summary>
    /// 近战伤害
    /// </summary>
    public int MeleeDamage { get; set; }
    /// <summary>
    /// 投掷伤害
    /// </summary>
    public int ThrowDamage { get; set; }
    /// <summary>
    /// 攻击间隔
    /// </summary>
    public float AttackInterval { get; set; }
    /// <summary>
    /// 近战武器类型
    /// </summary>
    public MeleeWeaponType MWType { get; set; }

    public MeleeWeapon(int id, string name, string description, ItemType type, ItemTechnology technology, int value, int maxStark, string sprite, int meleeWeaponID, int meleeDamage, int throwDamage, float attackInterval, MeleeWeaponType mwType)
     : base(id, name, description, type, technology, value, maxStark, sprite)
    {
        this.MeleeWeaponID = meleeWeaponID;
        this.MeleeDamage = meleeDamage;
        this.ThrowDamage = throwDamage;
        this.AttackInterval = attackInterval;
        this.MWType = mwType;
    }

    public enum MeleeWeaponType
    {
        刀, 矛, 投掷
    }
    public override string GetToolTipText()
    {
        string text = base.GetToolTipText();
        text += string.Format("\n<size=13>武器类型： {3}</size>\n<size=13>近战伤害： {0}</size>\n<size=13>投掷伤害： {1}</size>\n<size=13>攻击间隔： {2}</size>", MeleeDamage,ThrowDamage,AttackInterval,MWType);
        return text;
    }
}
