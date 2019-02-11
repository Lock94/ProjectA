using UnityEngine;
using System.Collections.Generic;

public class PropertyManager : MonoBehaviour
{
    #region 单例模式
    private static PropertyManager _instance;
    public static PropertyManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("InventoryManager").GetComponent<PropertyManager>();
            }
            return _instance;
        }
    }
    #endregion

    //-----------------自己队伍的全体成员-------------- 
    private List<CharacterProperty> allCharaceters;
    public List<CharacterProperty> AllCharacters
    {
        get
        {
            for (int i = 0; i < allCharaceters.Count; i++)
            {
                if (allCharaceters[i].ID==chosenCharBase.ID)
                {
                    allCharaceters[i] = new CharacterProperty(chosenCharBase);
                }
            }
            return allCharaceters;
        }
    }
    //-----------------自己队伍的全体成员-------------- 

    //-----------------当前选择的角色-------------------
    public CharacterProperty chosenCharBase;
    public CharacterProperty chosenCharUsed;
    //-----------------当前选择的角色-------------------

    //-----------------自己的队伍属性-------------------
    private List<CharacterProperty[]> companionCharBase;
    public List<CharacterProperty[]> CompanionCharBase
    {
        get
        {
            CharacterProperty[] playerTeam = TeamProps(AllCharacters[0], null, null, null, null);
            CharacterProperty[] team1 = TeamProps(AllCharacters[1], null, null, null, null);
            companionCharBase = new List<CharacterProperty[]>();
            companionCharBase.Add(playerTeam);
            companionCharBase.Add(team1);
            return companionCharBase;
        }
    }   //战斗时队友小队属性(0 是玩家所在小队，00是玩家)
    //-----------------自己的队伍属性-------------------

    private List<CharacterProperty> charPropList;       //模板属性列表

    private List<CharacterProperty[]> meetEnemyTeam;        
    public List<CharacterProperty[]> MeetEnemyTeam      //战斗是从这个属性中读取
    {
        get { return meetEnemyTeam; }
        set { meetEnemyTeam = value; }
    }

    private void Start()
    {
   
        int[] skills = new int[20];
        for (int i = 0; i < 20; i++)
        {
            skills[0] = 0;
        }
        Item weaponWith = InventoryManager.Instance.GetItemByID(999);
        chosenCharBase = new CharacterProperty(0, "二狗子", CharacterProperty.CharacterFaction.Player, CharacterProperty.CharacterType.Hero, "Sprites/Heads/草原土匪/草原土匪01",CharacterProperty.CharacterKind.无, 1, 1, 1, 1, 1, weaponWith, 5000, 0, skills);
        chosenCharUsed = new CharacterProperty(0, "二狗子", CharacterProperty.CharacterFaction.Player, CharacterProperty.CharacterType.Hero, "Sprites/Heads/草原土匪/草原土匪01", CharacterProperty.CharacterKind.无, 1, 1, 1, 1, 1, weaponWith, 5000, 0, skills, 0, 0, 0, 0, 0);


        CharacterProperty[] playerCompanion1 = new CharacterProperty[5];
        playerCompanion1[0] = new CharacterProperty(1, "狗剩子", CharacterProperty.CharacterFaction.Companion, CharacterProperty.CharacterType.Hero, "Sprites/Heads/草原土匪/草原土匪02", CharacterProperty.CharacterKind.无, 2, 2, 2, 2, 2, weaponWith, 5000, 0, skills);

        allCharaceters = new List<CharacterProperty>();
        allCharaceters.Add(chosenCharBase);
        allCharaceters.Add(playerCompanion1[0]);

        ChangeCharProp(chosenCharBase);

        SetTextShow();
        SetSkillShow();

        ParseCharacterJson();
    }
    /// <summary>
    /// 解析模板角色信息
    /// </summary>
    void ParseCharacterJson()
    {
        charPropList = new List<CharacterProperty>();
        TextAsset charText = Resources.Load<TextAsset>("Characters");
        string charJson = charText.text;
        JSONObject charObjects = new JSONObject(charJson);

        foreach (JSONObject temp in charObjects.list)
        {
            int id = (int)temp["id"].n;
            string name = temp["name"].str;
            CharacterProperty.CharacterFaction faction = (CharacterProperty.CharacterFaction)System.Enum.Parse(typeof(CharacterProperty.CharacterFaction), temp["faction"].str);
            CharacterProperty.CharacterType type = (CharacterProperty.CharacterType)System.Enum.Parse(typeof(CharacterProperty.CharacterType), temp["characterType"].str);
            int consititution = (int)temp["constitution"].n;
            int strength = (int)temp["strength"].n;
            int agility = (int)temp["agility"].n;
            int dexterous = (int)temp["dexterous"].n;
            int concentration = (int)temp["concentration"].n;
            string sprite = temp["sprite"].str;
            int allExperience = (int)temp["allExperience"].n;

            int headID = 0; Item.ItemTechnology headTech = Item.ItemTechnology.T0;
            int clothID = 0; Item.ItemTechnology clothTech = Item.ItemTechnology.T0;
            int pantsID = 0; Item.ItemTechnology pantsTech = Item.ItemTechnology.T0;
            int beltID = 0; Item.ItemTechnology beltTech = Item.ItemTechnology.T0;
            int weaponID = 0; Item.ItemTechnology weaponTech = Item.ItemTechnology.T0;

            if (temp["head"].IsNumber)
            {
                headID = (int)temp["head"].n;
            }
            if (temp["head"].IsString)
            {
                headTech = (Item.ItemTechnology)System.Enum.Parse(typeof(Item.ItemTechnology), temp["head"].str);
            }
            if (temp["cloth"].IsNumber)
            {
                clothID = (int)temp["cloth"].n;
            }
            if (temp["cloth"].IsString)
            {
                clothTech = (Item.ItemTechnology)System.Enum.Parse(typeof(Item.ItemTechnology), temp["cloth"].str);
            }
            if (temp["pants"].IsNumber)
            {
                pantsID = (int)temp["pants"].n;
            }
            if (temp["pants"].IsString)
            {
                pantsTech = (Item.ItemTechnology)System.Enum.Parse(typeof(Item.ItemTechnology), temp["pants"].str);
            }
            if (temp["belt"].IsNumber)
            {
                beltID = (int)temp["belt"].n;
            }
            if (temp["belt"].IsString)
            {
                beltTech = (Item.ItemTechnology)System.Enum.Parse(typeof(Item.ItemTechnology), temp["belt"].str);
            }
            if (temp["weapon"].IsNumber)
            {
                weaponID = (int)temp["weapon"].n;
            }
            if (temp["weapon"].IsString)
            {
                weaponTech = (Item.ItemTechnology)System.Enum.Parse(typeof(Item.ItemTechnology), temp["weapon"].str);
            }

            if (headTech != Item.ItemTechnology.T0)
            {
                List<Item> allRightItems = InventoryManager.Instance.GetItemsByTechAndEquipType(headTech, Equipment.EquipmentType.Head);
                int i = Random.Range(0, allRightItems.Count);
                headID = allRightItems[i].ID;
            }
            if (clothTech != Item.ItemTechnology.T0)
            {
                List<Item> allRightItems = InventoryManager.Instance.GetItemsByTechAndEquipType(clothTech, Equipment.EquipmentType.Cloth);
                int i = Random.Range(0, allRightItems.Count);
                clothID = allRightItems[i].ID;
            }
            if (pantsTech != Item.ItemTechnology.T0)
            {
                List<Item> allRightItems = InventoryManager.Instance.GetItemsByTechAndEquipType(pantsTech, Equipment.EquipmentType.Pants);
                int i = Random.Range(0, allRightItems.Count);
                pantsID = allRightItems[i].ID;
            }
            if (beltTech != Item.ItemTechnology.T0)
            {
                List<Item> allRightItems = InventoryManager.Instance.GetItemsByTechAndEquipType(beltTech, Equipment.EquipmentType.Belt);
                int i = Random.Range(0, allRightItems.Count);
                beltID = allRightItems[i].ID;
            }
            if (weaponTech != Item.ItemTechnology.T0)
            {
                Item.ItemType weaponType = Random.Range(0, 2) == 0 ? Item.ItemType.MeleeWeapon : Item.ItemType.ShootWeapon;
                List<Item> allRightItems = InventoryManager.Instance.GetItemsByTechAndType(weaponTech, weaponType);
                int i = Random.Range(0, allRightItems.Count);
                weaponID = allRightItems[i].ID;
            }
            int[] skills = new int[20];
            for (int i = 0; i < 20; i++)
            {
                skills[0] = 0;
            }
            //TODO 特异值（主体质）
            CharacterProperty.CharacterKind kind = (CharacterProperty.CharacterKind)System.Enum.Parse(typeof(CharacterProperty.CharacterKind), temp["characterKind"].str);
            CharacterProperty charProp = new CharacterProperty(id, name, faction, type, sprite, kind, consititution, strength, agility, dexterous, concentration, InventoryManager.Instance.GetItemByID(weaponID), allExperience, 0, skills, 0, headID, clothID, pantsID, beltID);

            charPropList.Add(charProp);
        }
    }

    public void SetSkillShow()
    {
        int[] skillPoints = new int[25];

       int useSxPoint = 0; int useTzPoint = 0;

        skillPoints[0] = chosenCharUsed.Constitution;
        skillPoints[1] = chosenCharUsed.Strength;
        skillPoints[2] = chosenCharUsed.Agility;
        skillPoints[3] = chosenCharUsed.Dexterous;
        skillPoints[4] = chosenCharUsed.Concentration;

        int[] skills = Skills();
        for (int i = 0; i < 20; i++)
        {
            skillPoints[i + 5] = skills[i];
        }

        for (int i = 0; i < 5; i++)
        {
            useSxPoint += skillPoints[i];
        }
        for (int i = 5; i < 25; i++)
        {
            useTzPoint += skillPoints[i];
        }

        int allSxPoint = chosenCharUsed.Level * 5;
        int allTzPoint = chosenCharUsed.Level;

        allSxPoint += chosenCharUsed.EquipmentProp.AllPoints;
        allSxPoint += chosenCharUsed.BuffItemProp == null ? 0 : chosenCharUsed.BuffItemProp.AllPoints;

        SkillPanel.Instance.SetSkillPoints(skillPoints, allSxPoint - useSxPoint, allTzPoint - useTzPoint);
    }

    private int[] Skills()
    {
        int[] skills = new int[20];

        skills[0] = chosenCharUsed.SkillA;
        skills[1] = chosenCharUsed.SkillB;
        skills[2] = chosenCharUsed.SkillC;
        skills[3] = chosenCharUsed.SkillD;
        skills[4] = chosenCharUsed.SkillE;
        skills[5] = chosenCharUsed.SkillF;
        skills[6] = chosenCharUsed.SkillG;
        skills[7] = chosenCharUsed.SkillH;
        skills[8] = chosenCharUsed.SkillI;
        skills[9] = chosenCharUsed.SkillJ;
        skills[10] = chosenCharUsed.SkillK;
        skills[11] = chosenCharUsed.SkillL;
        skills[12] = chosenCharUsed.SkillM;
        skills[13] = chosenCharUsed.SkillN;
        skills[14] = chosenCharUsed.SkillO;
        skills[15] = chosenCharUsed.SkillP;
        skills[16] = chosenCharUsed.SkillQ;
        skills[17] = chosenCharUsed.SkillR;
        skills[18] = chosenCharUsed.SkillS;
        skills[19] = chosenCharUsed.SkillT;

        return skills;
    }

    //换装
    public void EquipPropChange(Item item, bool putOn)
    {
        if (item is ShootWeapon || item is MeleeWeapon)
        {
            chosenCharBase.WeaponWith = putOn ? item : InventoryManager.Instance.GetItemByID(999);
        }
        else if (item is Equipment)
        {
            Equipment equip = (Equipment)item;
            switch (equip.EquipType)
            {
                case Equipment.EquipmentType.None:
                    break;
                case Equipment.EquipmentType.Head:
                    chosenCharBase.EquipmentProp.HeadID = putOn ? equip.ID : 0;
                    break;
                case Equipment.EquipmentType.Belt:
                    chosenCharBase.EquipmentProp.BeltID = putOn ? equip.ID : 0;
                    break;
                case Equipment.EquipmentType.Cloth:
                    chosenCharBase.EquipmentProp.ClothID = putOn ? equip.ID : 0;
                    break;
                case Equipment.EquipmentType.Pants:
                    chosenCharBase.EquipmentProp.PantsID = putOn ? equip.ID : 0;
                    break;
            }
        }

        //chosenCharUsed.EquipmentProp = new CharacterProperty.EquipProp(chosenCharUsed.EquipmentProp.HeadID, chosenCharUsed.EquipmentProp.ClothID, chosenCharUsed.EquipmentProp.PantsID, chosenCharUsed.EquipmentProp.BeltID);
        //chosenCharUsed.BuffItemProp = new CharacterProperty.BuffProp(chosenCharUsed.BuffItemProp.Buffs);
        chosenCharUsed = new CharacterProperty(
            chosenCharBase.ID, chosenCharBase.Name, chosenCharBase.Faction, chosenCharBase.Type, chosenCharBase.Sprite, chosenCharBase.charKind,
            chosenCharBase.Constitution, chosenCharBase.Strength, chosenCharBase.Agility, chosenCharBase.Dexterous, chosenCharBase.Concentration,
            chosenCharBase.WeaponWith, chosenCharBase.AllExperience, 0, Skills(), 0,
            chosenCharBase.EquipmentProp.HeadID, chosenCharBase.EquipmentProp.ClothID, chosenCharBase.EquipmentProp.PantsID, chosenCharBase.EquipmentProp.BeltID, chosenCharUsed.BuffItemProp);

        chosenCharBase.WeaponWith = chosenCharUsed.WeaponWith;
        chosenCharBase.EquipmentProp = new CharacterProperty.EquipProp(chosenCharUsed.EquipmentProp);
        chosenCharBase.BuffItemProp = new CharacterProperty.BuffProp(chosenCharUsed.BuffItemProp);

        SetTextShow();
        SetSkillShow();
    }

    /// <summary>
    /// 选中一个角色，那么把属性传递给chosenChar
    /// </summary>
    /// <param name="charProp">包括装备，武器，buff</param>
    public void ChangeCharProp(CharacterProperty _charbase)
    {
        //Debug.Log(_charbase.Name + _charbase.ID);
        //for (int i = 0; i < AllCharaceters.Count; i++)
        //{
        //    if (AllCharaceters[i].ID == _charbase.ID)
        //    {
        //        AllCharaceters[i] = new CharacterProperty(chosenCharBase);
        //    }
        //}
        chosenCharBase = new CharacterProperty(_charbase);
        chosenCharUsed = new CharacterProperty(
          chosenCharBase.ID, chosenCharBase.Name, chosenCharBase.Faction, chosenCharBase.Type, chosenCharBase.Sprite, chosenCharBase.charKind,
          chosenCharBase.Constitution, chosenCharBase.Strength, chosenCharBase.Agility, chosenCharBase.Dexterous, chosenCharBase.Concentration,
          chosenCharBase.WeaponWith, chosenCharBase.AllExperience, 0, Skills(), 0,
          chosenCharBase.EquipmentProp.HeadID, chosenCharBase.EquipmentProp.ClothID, chosenCharBase.EquipmentProp.PantsID, chosenCharBase.EquipmentProp.BeltID, chosenCharUsed.BuffItemProp);
        SetTextShow();
    }

    public void SetTextShow()
    {

        CharacterPanel.Instance.ClearText(false);
        CharacterPanel.Instance.AddTextSlot("名字：", chosenCharUsed.Name, false);
        CharacterPanel.Instance.AddTextSlot("等级：", chosenCharUsed.Level.ToString(), false);
        CharacterPanel.Instance.AddTextSlot("生命：", chosenCharUsed.CurrentHealth + "/" + chosenCharUsed.MaxHealth, false);
        CharacterPanel.Instance.AddTextSlot("耐力：", chosenCharUsed.CurrentStamina + "/" + chosenCharUsed.MaxStamina, false);
        CharacterPanel.Instance.AddTextSlot("热量：", chosenCharUsed.CurrentEnergy + "/100", false);
        CharacterPanel.Instance.AddTextSlot("健康：", chosenCharUsed.CurrentHealthLevel + "/100", false);
        if (CharacterPanel.charPanelID == 1)
        {
            CharacterPanel.Instance.ClearText(true);
            CharacterPanel.Instance.AddTextSlot("抗性：", chosenCharUsed.Resistance.ToString(), true);
            CharacterPanel.Instance.AddTextSlot("暴击概率：", chosenCharUsed.Critical.ToString(), true);
            CharacterPanel.Instance.AddTextSlot("角色经验：", chosenCharUsed.AllExperience.ToString(), true);
            CharacterPanel.Instance.AddTextSlot("移动速度：", chosenCharUsed.MoveSpeed.ToString(), true);
            if (chosenCharUsed.WeaponWith is MeleeWeapon)
            {
                //HeroPropertyMelee myMeleeProperty = (HeroPropertyMelee)charUsed;
                CharacterPanel.Instance.AddTextSlot("近战攻击：", chosenCharUsed.MeleeDamage.ToString(), true);
                CharacterPanel.Instance.AddTextSlot("投掷攻击：", chosenCharUsed.ThrowDamage.ToString(), true);
                CharacterPanel.Instance.AddTextSlot("攻击间隔：", chosenCharUsed.AttackInterval.ToString(), true);
                CharacterPanel.Instance.AddTextSlot("脚踢伤害：", chosenCharUsed.KickDamage.ToString(), true);
                CharacterPanel.Instance.AddTextSlot("近战攻击增幅：", chosenCharUsed.MeleeRaise.ToString(), true);
                CharacterPanel.Instance.AddTextSlot("投掷攻击增幅：", chosenCharUsed.ThrowRaise.ToString(), true);
            }
            else if (chosenCharUsed.WeaponWith is ShootWeapon)
            {
                //HeroPropertyShoot myShootProperty = (HeroPropertyShoot)charUsed;
                CharacterPanel.Instance.AddTextSlot("近战攻击：", chosenCharUsed.MeleeDamage.ToString(), true);
                CharacterPanel.Instance.AddTextSlot("远程攻击：", chosenCharUsed.ShootDamage.ToString(), true);
                CharacterPanel.Instance.AddTextSlot("瞄准速度：", chosenCharUsed.AimingSpeed.ToString(), true);
                CharacterPanel.Instance.AddTextSlot("拉栓时间：", chosenCharUsed.PullingTime.ToString(), true);
                CharacterPanel.Instance.AddTextSlot("换弹时间：", chosenCharUsed.ReloadTime.ToString(), true);
                CharacterPanel.Instance.AddTextSlot("衰减距离：", chosenCharUsed.DeclineRange.ToString(), true);
                CharacterPanel.Instance.AddTextSlot("近战攻击增幅：", chosenCharUsed.MeleeRaise.ToString(), true);
                CharacterPanel.Instance.AddTextSlot("远程攻击增幅：", chosenCharUsed.ShootRaise.ToString(), true);
            }
        }
        CharacterPanel.Instance.SetProperty(chosenCharUsed.Constitution.ToString(), chosenCharUsed.Strength.ToString(), chosenCharUsed.Agility.ToString(), chosenCharUsed.Dexterous.ToString(), chosenCharUsed.Concentration.ToString());
    }
    /// <summary>
    /// 保存技能点
    /// </summary>
    /// <param name="points"></param>
    public void SaveSkillPoint(int[] points)
    {
        int[] skills = new int[20];

        for (int i = 5; i < 24; i++)
        {
            skills[i - 5] = points[i];
        }
        int truePoint0 = points[0] - chosenCharUsed.EquipmentProp.Constitution;
        int truePoint1 = points[1] - chosenCharUsed.EquipmentProp.Strength;
        int truePoint2 = points[2] - chosenCharUsed.EquipmentProp.Agility;
        int truePoint3 = points[3] - chosenCharUsed.EquipmentProp.Dexterous;
        int truePoint4 = points[4] - chosenCharUsed.EquipmentProp.Concentration;

        truePoint0 -= chosenCharUsed.BuffItemProp == null ? 0 : chosenCharUsed.BuffItemProp.Constitution;
        truePoint1 -= chosenCharUsed.BuffItemProp == null ? 0 : chosenCharUsed.BuffItemProp.Strength;
        truePoint2 -= chosenCharUsed.BuffItemProp == null ? 0 : chosenCharUsed.BuffItemProp.Agility;
        truePoint3 -= chosenCharUsed.BuffItemProp == null ? 0 : chosenCharUsed.BuffItemProp.Dexterous;
        truePoint4 -= chosenCharUsed.BuffItemProp == null ? 0 : chosenCharUsed.BuffItemProp.Concentration;

        chosenCharBase = new CharacterProperty(
            chosenCharBase.ID, 
            chosenCharBase.Name, 
            chosenCharBase.Faction,
            chosenCharBase.Type,
            chosenCharBase.Sprite,
            chosenCharBase.charKind,
            truePoint0, 
            truePoint1,
            truePoint2,
            truePoint3,
            truePoint4,
            chosenCharBase.WeaponWith,
            chosenCharBase.AllExperience, 
            0, 
            skills);

        //buffProp = new CharacterProperty.BuffProp(buffs);


        chosenCharUsed = new CharacterProperty(
            chosenCharBase.ID, 
            chosenCharBase.Name, 
            chosenCharBase.Faction,
            chosenCharBase.Type,
            chosenCharBase.Sprite,
            chosenCharBase.charKind,
            truePoint0,
            truePoint1,
            truePoint2,
            truePoint3,
            truePoint4,
            chosenCharBase.WeaponWith,
            chosenCharBase.AllExperience, 
            0, skills, 0,
            chosenCharUsed.EquipmentProp.HeadID,
            chosenCharUsed.EquipmentProp.ClothID,
            chosenCharUsed.EquipmentProp.PantsID,
            chosenCharUsed.EquipmentProp.BeltID,
            chosenCharUsed.BuffItemProp);

        chosenCharBase.WeaponWith = chosenCharUsed.WeaponWith;
        chosenCharBase.EquipmentProp = chosenCharUsed.EquipmentProp;
        chosenCharBase.BuffItemProp = chosenCharUsed.BuffItemProp;

        SetSkillShow();
        SetTextShow();
    }
    public void ChangeBuff(Buff buff, bool add)
    {
        if (add)
        {
            if (!chosenCharBase.BuffItemProp.Buffs.Contains(buff))
            {
                chosenCharBase.BuffItemProp.Buffs.Add(buff);
            }
        }
        else
        {
            if (chosenCharBase.BuffItemProp.Buffs.Contains(buff))
            {
                chosenCharBase.BuffItemProp.Buffs.Remove(buff);
            }
        }

        CharacterProperty.BuffProp buffprop = new CharacterProperty.BuffProp(chosenCharBase.BuffItemProp.Buffs);

        chosenCharUsed = new CharacterProperty(
            chosenCharBase.ID,
            chosenCharBase.Name,
            chosenCharBase.Faction, 
            chosenCharBase.Type, 
            chosenCharBase.Sprite,
            chosenCharBase.charKind,
            chosenCharBase.Constitution, 
            chosenCharBase.Strength, 
            chosenCharBase.Agility,
            chosenCharBase.Dexterous, 
            chosenCharBase.Concentration, 
            chosenCharBase.WeaponWith,
            chosenCharBase.AllExperience,
            0, Skills(), 0,
            chosenCharBase.EquipmentProp.HeadID,
            chosenCharBase.EquipmentProp.ClothID,
            chosenCharBase.EquipmentProp.PantsID,
            chosenCharBase.EquipmentProp.BeltID,
            buffprop);

        SetSkillShow();
        SetTextShow();
    }
  
    public CharacterProperty GetCharByID(int id)
    {
        foreach (CharacterProperty charBase in AllCharacters)
        {
            if (charBase.ID == id)
            {
                return charBase;
            }
        }
        return null;
    }
    public void SetPropertyByID(CharacterProperty _charBase)
    {
        for (int i = 0; i < AllCharacters.Count; i++)
        {
            if (AllCharacters[i].ID == _charBase.ID)
            {
                AllCharacters[i] = new CharacterProperty(_charBase);
            }
        }
    }

    public CharacterProperty[] TeamProps(CharacterProperty leader, CharacterProperty member1, CharacterProperty member2, CharacterProperty member3, CharacterProperty member4)
    {
        CharacterProperty[] newTeam = new CharacterProperty[5];
        newTeam[0] = leader;
        newTeam[1] = member1;
        newTeam[2] = member2;
        newTeam[3] = member3;
        newTeam[4] = member4;
        return newTeam;
    }
    //大地图请求根据势力生成1个新的小队
    public CharacterProperty[] BuildTeamOfTemp(CharacterProperty.CharacterKind kind,int LeaderID,int memeberID)
    {
        CharacterProperty[] returnProp = new CharacterProperty[5];
        returnProp[0] = charPropList[LeaderID];
        returnProp[1] = charPropList[memeberID];
        returnProp[2] = charPropList[memeberID];
        returnProp[3] = charPropList[memeberID];
        returnProp[4] = charPropList[memeberID];
        return returnProp;
    }

    
}