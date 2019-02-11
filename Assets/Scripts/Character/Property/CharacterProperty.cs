using UnityEngine;
using System.Collections.Generic;

public class CharacterProperty
{
    public int ID { get; set; }
    public string Name { get; set; }
    public CharacterFaction Faction { get; set; }
    public CharacterType Type { get; set; }
    public EquipProp EquipmentProp { get; set; }
    public BuffProp BuffItemProp { get; set; }
    public float AllExperience { get; set; }
    public string Sprite { get; set; }
    /// <summary>
    /// 抗性
    /// </summary>
    public float Resistance { get; set; }
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
    public int HealthLevel { get; set; }



    public int Level { get; set; }
    public float CurrentExperience { get; set; }
    public int MaxHealth { get; set; }
    public int MaxStamina { get; set; }
    public float HealthRecovery { get; set; }
    public float StaminaRecovery { get; set; }
    public float MoveSpeed { get; set; }
    public float AimingSpeed { get; set; }
    public float Critical { get; set; }
    public float MeleeRaise { get; set; }
    public float ShootRaise { get; set; }
    public float ThrowRaise { get; set; }
    public float MaxAngle { get; set; }
    public float KickDamage { get; set; }
    public float PunchDamage { get; set; }

    public float MeleeDamage { get; set; }
    public float ThrowDamage { get; set; }
    public float AttackInterval { get; set; }

    public float MinAngle { get; set; }
    public float Capacity { get; set; }
    public float PullingTime { get; set; }
    public float ReloadTime { get; set; }
    public float ShootDamage { get; set; }
    public float DeclineRange { get; set; }
    public float Power { get; set; }

    public int SkillA { get; set; }
    public int SkillB { get; set; }
    public int SkillC { get; set; }
    public int SkillD { get; set; }
    public int SkillE { get; set; }
    public int SkillF { get; set; }
    public int SkillG { get; set; }
    public int SkillH { get; set; }
    public int SkillI { get; set; }
    public int SkillJ { get; set; }
    public int SkillK { get; set; }
    public int SkillL { get; set; }
    public int SkillM { get; set; }
    public int SkillN { get; set; }
    public int SkillO { get; set; }
    public int SkillP { get; set; }
    public int SkillQ { get; set; }
    public int SkillR { get; set; }
    public int SkillS { get; set; }
    public int SkillT { get; set; }

    public int CurrentHealth { get; set; }
    public int CurrentStamina { get; set; }
    public int CurrentEnergy { get; set; }
    public int CurrentHealthLevel { get; set; }
    public Item WeaponWith { get; set; }
    public CharacterKind charKind { get; set; }

    /// <summary>
    /// 创建角色（base，无装备，有武器）
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="name">名字</param>
    /// <param name="faction">势力（玩家，友军，敌人）</param>
    /// <param name="type">（英雄，普通）</param>
    /// <param name="constitution">体质</param>
    /// <param name="strength">力量</param>
    /// <param name="agility">灵巧</param>
    /// <param name="dexterous">洞察</param>
    /// <param name="concentration">专注</param>
    /// <param name="weapon">武器</param>
    /// <param name="allExperience">全部经验</param>
    /// <param name="skills">技能</param>
    /// <param name="currentExperience">当前经验</param>
    public CharacterProperty(int id, string name, CharacterFaction faction, CharacterType type, string sprite,CharacterKind kind,
        int constitution, int strength, int agility, int dexterous, int concentration, Item weapon, float allExperience, float resistance = 0,
        int[] skills = null, float currentExperience = 0, int headID = 0, int clothID = 0, int pantsID = 0, int beltID = 0, BuffProp buffProp = null)
    {
        #region 装备处理
        if (this.EquipmentProp == null)
        {
            this.EquipmentProp = new EquipProp(headID, clothID, pantsID, beltID);
        }
        if ((headID != 0) || (clothID != 0) || (pantsID != 0) || (beltID != 0))
        {
            this.EquipmentProp = new EquipProp(headID, clothID, pantsID, beltID);
            resistance += EquipmentProp.Resistance;
            constitution += EquipmentProp.Constitution;
            strength += EquipmentProp.Strength;
            dexterous += EquipmentProp.Dexterous;
            agility += EquipmentProp.Agility;
            concentration += EquipmentProp.Concentration;
        }
        #endregion

        #region 加成处理
        if (buffProp != null)
        {
            this.BuffItemProp = buffProp;
            constitution += buffProp.Constitution;
            strength += buffProp.Strength;
            dexterous += buffProp.Dexterous;
            agility += buffProp.Agility;
            concentration += buffProp.Concentration;

            //else ...
        }
        #endregion

        this.ID = id;
        this.Name = name;
        this.Faction = faction;
        this.Type = type;
        this.Sprite = sprite;
        this.AllExperience = allExperience;
        this.Resistance = resistance;
        this.Constitution = constitution;
        this.Strength = strength;
        this.Agility = agility;
        this.Dexterous = dexterous;
        this.Concentration = concentration;
        this.HealthLevel = 100;
        this.charKind = kind;

        this.Level = CountLevel(allExperience, out currentExperience);
        this.CurrentExperience = currentExperience;
        this.Power = constitution + strength + agility + dexterous + concentration;

        this.WeaponWith = weapon;

        //通常角色
        if (type == CharacterType.Common)
        {
            this.MaxHealth = 100 + 3 * constitution;
            this.MaxStamina = 100 + 1 * constitution;
            this.HealthRecovery = 0.2f;
            this.StaminaRecovery = 5f;
            this.MoveSpeed = 1.3f * (1 + 0.01f * agility);
            this.AimingSpeed = 2 * (1 + 0.02f * concentration);
            this.Critical = 0.1f + 0.01f * dexterous;
            this.MeleeRaise = 1.5f;
            this.ShootRaise = 1.5f;
            this.ThrowRaise = 1.5f;
            this.MaxAngle = 10 * (1 - 0.01f * concentration);
            this.KickDamage = 20 + strength;
            this.PunchDamage = 20 + strength;

            if (weapon is MeleeWeapon)
            {
                MeleeWeapon melee = (MeleeWeapon)weapon;
                this.MeleeDamage = melee.MeleeDamage + strength;
                this.ThrowDamage = melee.ThrowDamage + strength;
                this.AttackInterval = melee.AttackInterval * (1 - 0.02f * agility);

            }
            else if (weapon is ShootWeapon)
            {
                ShootWeapon shoot = (ShootWeapon)weapon;
                this.MinAngle = (110 - shoot.Accuracy / 15);
                this.Capacity = shoot.Accuracy * MaxAngle / 10;
                this.PullingTime = shoot.LaShuanTime * (1 - 0.02f * agility);
                this.ReloadTime = shoot.ReloadTime * (1 - 0.01f * agility);
                this.MeleeDamage = shoot.MeleeDamage + strength;
                this.ShootDamage = shoot.ShootDamage;
                this.AttackInterval = shoot.AttackInterval * (1 - 0.01f * agility);
                this.DeclineRange = shoot.DamageRange;
            }
        }
        //英雄角色
        else if (type == CharacterType.Hero)
        {
            this.SkillA = skills[0];
            this.SkillB = skills[1];
            this.SkillC = skills[2];
            this.SkillD = skills[3];
            this.SkillE = skills[4];
            this.SkillF = skills[5];
            this.SkillG = skills[6];
            this.SkillH = skills[7];
            this.SkillI = skills[8];
            this.SkillJ = skills[9];
            this.SkillK = skills[10];
            this.SkillL = skills[11];
            this.SkillM = skills[12];
            this.SkillN = skills[13];
            this.SkillO = skills[14];
            this.SkillP = skills[15];
            this.SkillQ = skills[16];
            this.SkillR = skills[17];
            this.SkillS = skills[18];
            this.SkillT = skills[19];

            this.MaxHealth = 100 + 3 * constitution + 20 * SkillA;
            this.MaxStamina = 100 + 1 * constitution + 20 * SkillF;
            this.HealthRecovery = 0.2f + 2 * SkillM + 100 * SkillN;
            this.StaminaRecovery = 5 + 2 * SkillL;
            this.MoveSpeed = 1.3f * (1 + 0.01f * agility + 0.15f * SkillR);
            this.AimingSpeed = 2f * (1 + 0.02f * concentration + 0.1f * SkillQ);
            this.Critical = 0.1f + 0.01f * dexterous + 0.04f * SkillD;
            this.MeleeRaise = 1.5f + 0.5f * SkillI;
            this.ShootRaise = 1.5f + 0.5f * SkillJ;
            this.ThrowRaise = 1.5f + 1 * SkillT;
            this.MaxAngle = 10 * (1 - 0.01f * concentration - 0.04f * SkillE);
            this.Resistance = this.Resistance + 0.04f * SkillG + 0.1f * SkillN;
            this.KickDamage = 20 + 20 * SkillH;

            if (weapon is MeleeWeapon)
            {
                MeleeWeapon melee = (MeleeWeapon)weapon;
                this.MeleeDamage = (melee.MeleeDamage + strength) * (1 + 0.04f * SkillB);
                this.AttackInterval = melee.AttackInterval * (1 - 0.02f * agility - 0.04f * SkillC);
                this.ThrowDamage = melee.ThrowDamage * (1 + SkillT) + strength;
            }
            else if (weapon is ShootWeapon)
            {
                ShootWeapon shoot = (ShootWeapon)weapon;
                this.MinAngle = (110 - shoot.Accuracy / 15) * (1 - 0.1f * SkillP);
                this.Capacity = shoot.Accuracy * MaxAngle / 10f;
                this.ReloadTime = shoot.ReloadTime * (1 - 0.01f * agility - 0.1f * SkillO);
                this.PullingTime = shoot.LaShuanTime * (1 - 0.02f * agility - 0.08f * SkillK);
                this.MeleeDamage = (shoot.MeleeDamage * (1 + SkillT) + strength) * (1 + 0.04f * SkillB);
                this.ShootDamage = shoot.ShootDamage;
                this.AttackInterval = shoot.AttackInterval * (1 - 0.01f * agility - 0.04f * SkillC);
            }
        }

    }

    public CharacterProperty(CharacterProperty charProp)
    {
        this.ID = charProp.ID;
        this.Name = charProp.Name;
        this.Faction = charProp.Faction;
        this.Type = charProp.Type;
        this.EquipmentProp = new EquipProp(charProp.EquipmentProp);
        this.BuffItemProp = new BuffProp(charProp.BuffItemProp);
        this.AllExperience = charProp.AllExperience;
        this.Sprite = charProp.Sprite;
        this.Resistance = charProp.Resistance;
        this.Constitution = charProp.Constitution;
        this.Strength = charProp.Strength;
        this.Agility = charProp.Agility;
        this.Dexterous = charProp.Dexterous;
        this.Concentration = charProp.Concentration;
        this.HealthLevel = charProp.HealthLevel;
        this.charKind = charProp.charKind;

        this.Level = charProp.Level;
        this.CurrentExperience = charProp.CurrentExperience;
        this.MaxHealth = charProp.MaxHealth;
        this.MaxStamina = charProp.MaxStamina;
        this.HealthRecovery = charProp.HealthRecovery;
        this.StaminaRecovery = charProp.StaminaRecovery;
        this.MoveSpeed = charProp.MoveSpeed;
        this.AimingSpeed = charProp.AimingSpeed;
        this.Critical = charProp.Critical;
        this.MeleeRaise = charProp.MeleeRaise;
        this.ShootRaise = charProp.ShootRaise;
        this.ThrowRaise = charProp.ThrowRaise;
        this.MaxAngle = charProp.MaxAngle;
        this.KickDamage = charProp.KickDamage;
        this.PunchDamage = charProp.PunchDamage;

        this.MeleeDamage = charProp.MeleeDamage;
        this.ThrowDamage = charProp.ThrowDamage;
        this.AttackInterval = charProp.AttackInterval;

        this.MinAngle = charProp.MinAngle;
        this.PullingTime = charProp.PunchDamage;
        this.ReloadTime = charProp.ReloadTime;
        this.ShootDamage = charProp.ShootDamage;
        this.DeclineRange = charProp.DeclineRange;
        this.Power = charProp.Power;

        this.SkillA = charProp.SkillA;
        this.SkillB = charProp.SkillB;
        this.SkillC = charProp.SkillC;
        this.SkillD = charProp.SkillD;
        this.SkillE = charProp.SkillE;
        this.SkillF = charProp.SkillF;
        this.SkillH = charProp.SkillH;
        this.SkillI = charProp.SkillI;
        this.SkillJ = charProp.SkillJ;
        this.SkillK = charProp.SkillK;
        this.SkillL = charProp.SkillL;
        this.SkillM = charProp.SkillM;
        this.SkillN = charProp.SkillN;
        this.SkillO = charProp.SkillO;
        this.SkillP = charProp.SkillP;
        this.SkillQ = charProp.SkillQ;
        this.SkillR = charProp.SkillR;
        this.SkillS = charProp.SkillS;
        this.SkillT = charProp.SkillT;

        this.CurrentHealth = charProp.CurrentHealth;
        this.CurrentStamina = charProp.CurrentStamina;
        this.CurrentEnergy = charProp.CurrentEnergy;
        this.CurrentHealthLevel = charProp.CurrentHealthLevel;
        this.WeaponWith = charProp.WeaponWith;
    }

    private int CountLevel(float experience, out float currentExperience, int level = 0)
    {
        if (experience < ((100 * Mathf.Pow(1.1f, level)) + 50 * level))
        {
            currentExperience = experience;
            return level;
        }
        else
        {
            experience -= (100 * Mathf.Pow(1.1f, level)) + 50 * level;
            level += 1;
            currentExperience = experience;
            return CountLevel(experience, out currentExperience, level);
        }
    }

    public enum CharacterFaction
    {
        Player, Companion, Enemy
    }
    public enum CharacterType
    {
        Common, Hero
    }
    public enum CharacterKind
    {
        无, 日军, 革命军, 哥老会, 俄军, 复辟军, 光明教, 平民, 土匪
    }
    /// <summary>
    /// 换装备修改这个值
    /// </summary>
    public class EquipProp
    {
        public int Constitution { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Dexterous { get; set; }
        public int Concentration { get; set; }

        public int Resistance { get; set; }

        public int AllPoints { get; set; }

        public int HeadID { get; set; }
        public int ClothID { get; set; }
        public int PantsID { get; set; }
        public int BeltID { get; set; }

        public EquipProp(int headID, int clothID, int pantsID, int beltID)
        {
            int constitution = 0;
            int strength = 0;
            int agility = 0;
            int dexterous = 0;
            int concentration = 0;
            int resistance = 0;
            int allPoints = 0;
            Equipment headEquip = (Equipment)InventoryManager.Instance.GetItemByID(headID);
            Equipment clothEquip = (Equipment)InventoryManager.Instance.GetItemByID(clothID);
            Equipment pantsEquip = (Equipment)InventoryManager.Instance.GetItemByID(pantsID);
            Equipment beltEquip = (Equipment)InventoryManager.Instance.GetItemByID(beltID);
            if (headEquip != null)
            {
                constitution += headEquip.Constitution;
                strength += headEquip.Strength;
                agility += headEquip.Agility;
                dexterous += headEquip.Dexterous;
                concentration += headEquip.Concentration;
                resistance += headEquip.Resistance;
            }
            if (clothEquip != null)
            {
                constitution += clothEquip.Constitution;
                strength += clothEquip.Strength;
                agility += clothEquip.Agility;
                dexterous += clothEquip.Dexterous;
                concentration += clothEquip.Concentration;
                resistance += clothEquip.Resistance;
            }
            if (pantsEquip != null)
            {
                constitution += pantsEquip.Constitution;
                strength += pantsEquip.Strength;
                agility += pantsEquip.Agility;
                dexterous += pantsEquip.Dexterous;
                concentration += pantsEquip.Concentration;
                resistance += pantsEquip.Resistance;
            }
            if (beltEquip != null)
            {
                constitution += beltEquip.Constitution;
                strength += beltEquip.Strength;
                agility += beltEquip.Agility;
                dexterous += beltEquip.Dexterous;
                concentration += beltEquip.Concentration;
                resistance += beltEquip.Resistance;
            }
            allPoints = constitution + strength + agility + dexterous + concentration;
            this.Constitution = constitution;
            this.Strength = strength;
            this.Agility = agility;
            this.Dexterous = dexterous;
            this.Concentration = concentration;
            this.Resistance = resistance;
            this.AllPoints = allPoints;

            this.HeadID = headID;
            this.ClothID = clothID;
            this.PantsID = pantsID;
            this.BeltID = beltID;
        }

        public EquipProp(EquipProp equipProp)
        {
            this.Constitution = equipProp.Constitution;
            this.Strength = equipProp.Strength;
            this.Agility = equipProp.Agility;
            this.Dexterous = equipProp.Dexterous;
            this.Concentration = equipProp.Concentration;

            this.Resistance = equipProp.Resistance;
            this.AllPoints = equipProp.AllPoints;
            this.HeadID = equipProp.HeadID;
            this.ClothID = equipProp.ClothID;
            this.PantsID = equipProp.PantsID;
            this.BeltID = equipProp.BeltID;
        }
    }

    /// <summary>
    /// 增删Buff修改这个值
    /// </summary>
    public class BuffProp
    {
        public int Constitution { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Dexterous { get; set; }
        public int Concentration { get; set; }

        public int AllPoints { get; set; }

        public int Energy { get; set; } //每小时增加的负的能量

        public int HealthLevel { get; set; } //每小时增加的负的健康度

        public float HealthRecovery { get; set; } //

        public float StaminaRecovery { get; set; }


        public List<Buff> Buffs { get; set; }

        public BuffProp(List<Buff> buffs)
        {
            int constitution = 0;
            int strength = 0;
            int agility = 0;
            int dexterous = 0;
            int concentration = 0;

            int energy = 0;
            int healthLevel = 0;

            float healthRecovery = 0;

            float staminaRecovery = 0;
            int allPoints = 0;


            foreach (Buff buff in buffs)
            {
                for (int i = 0; i < buff.EffectProp.Length; i++)
                {
                    switch (buff.EffectProp[i])
                    {
                        case "constitution":
                            constitution += buff.EffectValue[i];
                            break;
                        case "strength":
                            strength += buff.EffectValue[i];
                            break;
                        case "agility":
                            agility += buff.EffectValue[i];
                            break;
                        case "dexterous":
                            dexterous += buff.EffectValue[i];
                            break;
                        case "concentration":
                            concentration += buff.EffectValue[i];
                            break;
                        case "energy":
                            energy += buff.EffectValue[i];
                            break;
                        case "healthRecovery":
                            healthRecovery += buff.EffectValue[i];
                            break;
                        case "staminaRecovery":
                            staminaRecovery += buff.EffectValue[i];
                            break;
                        case "healthLevel":
                            healthLevel+= buff.EffectValue[i];
                            break;
                        default:
                            break;
                    }
                }
            }
            allPoints = constitution + strength + agility + dexterous + concentration;

            this.Constitution = constitution;
            this.Strength = strength;
            this.Agility = agility;
            this.Dexterous = dexterous;
            this.Concentration = concentration;

            this.AllPoints = allPoints;

            this.Energy = energy;
            this.HealthLevel = healthLevel;

            this.HealthRecovery = healthRecovery;
            this.StaminaRecovery = staminaRecovery;

            this.Buffs = buffs;
        }

        public BuffProp(BuffProp buffProp)
        {
            if (buffProp == null)
                return;
            this.Constitution = buffProp.Constitution;
            this.Strength = buffProp.Strength;
            this.Agility = buffProp.Agility;
            this.Energy = buffProp.Energy;
            this.Dexterous = buffProp.Dexterous;
            this.Concentration = buffProp.Concentration;

            this.AllPoints = buffProp.AllPoints;
            this.Energy = buffProp.Energy;
            this.HealthLevel = buffProp.HealthLevel;
            this.HealthRecovery = buffProp.HealthRecovery;
            this.StaminaRecovery = buffProp.StaminaRecovery;
        }
    }
}