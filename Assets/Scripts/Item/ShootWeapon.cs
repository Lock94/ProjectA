using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 远程武器
/// </summary>
public class ShootWeapon : Item
{
    /// <summary>
    /// 对应的远程武器实例化的ID
    /// </summary>
    public int ShootWeaponID { get; set; }
    /// <summary>
    /// 远程伤害
    /// </summary>
    public int ShootDamage { get; set; }
    /// <summary>
    /// 近战伤害
    /// </summary>
    public int MeleeDamage { get; set; }
    /// <summary>
    /// 攻击间隔
    /// </summary>
    public float AttackInterval { get; set; }
    /// <summary>
    /// 拉栓时间
    /// </summary>
    public float LaShuanTime { get; set; }
    /// <summary>
    /// 换弹时间
    /// </summary>
    public float ReloadTime { get; set; }
    /// <summary>
    /// 衰减射程
    /// </summary>
    public float DamageRange { get; set; }
    /// <summary>
    /// 远程武器类型
    /// </summary>
    public ShootWeaponType SWType { get; set; }
    /// <summary>
    /// 子弹类型
    /// </summary>
    public AmmoType AMType { get; set; }
    /// <summary>
    /// 弹夹容量
    /// </summary>
    public int AmmoCount { get; set; }
    /// <summary>
    /// 精准度
    /// </summary>
    public int Accuracy { get; set; }

    public ShootWeapon(int id, string name,string description, ItemType type, ItemTechnology technology, int value,int maxStark, string sprite,int shootWeaponID,int shootDamage, int meleeDamage, float attackInterval, float lashuanTime, float reloadTime, float damageRange, ShootWeaponType swType, AmmoType amType,int ammoCount,int accuracy)
  : base(id, name,description, type, technology, value, maxStark,sprite)
    {
        this.ShootWeaponID = shootWeaponID;
        this.ShootDamage = shootDamage;
        this.MeleeDamage = meleeDamage;
        this.AttackInterval = attackInterval;
        this.LaShuanTime = lashuanTime;
        this.ReloadTime = reloadTime;
        this.DamageRange = damageRange;
        this.SWType = swType;
        this.AMType = amType;
        this.AmmoCount = ammoCount;
        this.Accuracy = accuracy;
    }

    public enum ShootWeaponType
    {
        弩,燧发枪,霰弹枪,半自动, 拉栓枪,全自动, 损坏
    }

    public enum AmmoType
    {
        弩矢, 遂发枪弹, 米涅弹, 霰弹, 步枪弹, 左轮子弹, 特制步枪弹
    }
    public override string GetToolTipText()
    {
        string text = base.GetToolTipText();
        text += string.Format("\n<size=13>武器类型： {6}</size>" +
                              "\n<size=13>子弹类型： {7}</size>" +
                              "\n<size=13>弹夹容量： {8}</size>" +
                              "\n<size=13>精准度数： {9}</size>" +
                              "\n<size=13>近战伤害： {0}</size>" +
                              "\n<size=13>远程伤害： {1}</size>" +
                              "\n<size=13>攻击间隔： {2}</size>" +
                              "\n<size=13>拉栓时间： {3}</size>" +
                              "\n<size=13>换弹时间： {4}</size>" +
                              "\n<size=13>衰减射程： {5}</size>",
                              MeleeDamage, ShootDamage, AttackInterval,LaShuanTime, ReloadTime,DamageRange,SWType,AMType,AmmoCount,Accuracy);
        return text;
    }
}
