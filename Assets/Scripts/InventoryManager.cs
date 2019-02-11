using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    #region 单例模式
    private static InventoryManager _instance;
    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
            }
            return _instance;
        }
    }
    #endregion

    /// <summary>
    /// 物品库
    /// </summary>
    private List<Item> itemList;

    /// <summary>
    /// 蓝图库
    /// </summary>
    private List<CraftRecipe> craftList;
    /// <summary>
    /// 蓝图按种类排序的库（全部）
    /// </summary>
    private List<List<CraftRecipe>> craftRecipos;
    /// <summary>
    /// 蓝图按种类排序的库（已学习）
    /// </summary>
    private List<List<CraftRecipe>> craftLearned;

    #region ToolTip
    private ToolTip toolTip;

    private Canvas canvas;

    private bool isToolTipShow = false;

    private Vector2 toolTipPositionOffset = new Vector2(15, -15);
    #endregion

    private bool isPickedItem = false;
    public bool IsPickedItem
    {
        get { return isPickedItem; }
    }

    private ItemUI pickedItem;
    public ItemUI PickedItem
    {
        get { return pickedItem; }
    }

    private Slot pickedSlot;
    public Slot PickedSlot
    {
        get { return pickedSlot; }
    }

    private bool isControl;
    public bool IsControl
    {
        get { return isControl; }
    }

    private void Awake()
    {
        ParseItemJson();
        ParseCraftJson();
    }

    private void Start()
    {
        toolTip = GameObject.FindObjectOfType<ToolTip>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        pickedItem = GameObject.Find("PickedItem").GetComponent<ItemUI>();
        pickedItem.Hide();
    }

    private void Update()
    {
        if (isPickedItem)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
            pickedItem.SetLocalPosition(position);
        }
        if (isToolTipShow)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
            toolTip.SetLocalPostion(position + toolTipPositionOffset);
        }
        //丢弃物品
        if (isPickedItem && Input.GetMouseButtonDown(0) && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1) == false)//鼠标下面没有组件   
        {
            isPickedItem = false;
            pickedItem.Hide();
        }
        isControl = Input.GetKey(KeyCode.LeftControl) ? true : false;
    }

    /// <summary>
    /// 解析物品信息
    /// </summary>
    void ParseItemJson()
    {
        itemList = new List<Item>();
        TextAsset itemText = Resources.Load<TextAsset>("Items");
        string itemJson = itemText.text;
        JSONObject itemObjects = new JSONObject(itemJson);

        foreach (JSONObject temp in itemObjects.list)
        {
            Item.ItemType type = (Item.ItemType)System.Enum.Parse(typeof(Item.ItemType), temp["type"].str);

            int id = (int)temp["id"].n;
            string name = temp["name"].str;
            string description = temp["description"].str;
            Item.ItemTechnology technology = (Item.ItemTechnology)System.Enum.Parse(typeof(Item.ItemTechnology), temp["technology"].str);
            int value = (int)temp["value"].n;
            string sprite = temp["sprite"].str;
            int maxStark = (int)temp["maxStark"].n;

            Item item = null;
            switch (type)
            {
                #region 解析消耗品
                case Item.ItemType.Consumable:
                    int hp = temp.HasField("hp") ? (int)temp["hp"].n : 0;
                    int eg = temp.HasField("eg") ? (int)temp["eg"].n : 0;
                    int hl = temp.HasField("hl") ? (int)temp["hl"].n : 0;
                    int tempConstitution = temp.HasField("tempConstitution") ? (int)temp["tempConstitution"].n : 0;
                    int tempStrength = temp.HasField("tempStrength") ? (int)temp["tempStrength"].n : 0;
                    int tempAgility = temp.HasField("tempAgility") ? (int)temp["tempAgility"].n : 0;
                    int tempDexterous = temp.HasField("tempDexterous") ? (int)temp["tempDexterous"].n : 0;
                    int tempConcentration = temp.HasField("tempConcentration") ? (int)temp["tempConcentration"].n : 0;
                    int continuedTime = temp.HasField("continuedTime") ? (int)temp["continuedTime"].n : 0;
                    int con_buffID = temp.HasField("buffID") ? (int)temp["buffID"].n : 0;
                    item = new Consumable(id, name, description, type, technology, value, maxStark, sprite, hp, eg, hl, tempConstitution, tempStrength, tempAgility, tempDexterous, tempConcentration, continuedTime, con_buffID);
                    break;
                #endregion

                #region 解析包裹
                case Item.ItemType.Package:
                    bool isGetOne = temp["isGetOne"].n == 1 ? true : false;
                    int[] itemIDs = temp.HasField("itemIDs") ? new int[temp["itemIDs"].list.Count] : null;
                    if (itemIDs != null)
                    {
                        for (int i = 0; i < temp["itemIDs"].list.Count; i++)
                        {
                            itemIDs[i] = int.Parse(temp["itemIDs"].list[i].str);
                        }
                    }
                    Item.ItemType[] itemTypes = temp.HasField("itemTypes") ? new Item.ItemType[temp["itemTypes"].list.Count] : null;
                    if (itemTypes != null)
                    {
                        for (int i = 0; i < temp["itemTypes"].list.Count; i++)
                        {
                            itemTypes[i] = (Item.ItemType)System.Enum.Parse(typeof(Item.ItemType), temp["itemTypes"].list[i].str);
                        }
                    }
                    Item.ItemTechnology[] itemTechnologies = temp.HasField("itemTechnologies") ? new Item.ItemTechnology[temp["itemTechnologies"].list.Count] : null;
                    if (itemTechnologies != null)
                    {
                        for (int i = 0; i < temp["itemTechnologies"].list.Count; i++)
                        {
                            itemTechnologies[i] = (Item.ItemTechnology)System.Enum.Parse(typeof(Item.ItemTechnology), temp["itemTechnologies"].list[i].str);
                        }
                    }
                    int[] itemCount = new int[temp["itemCount"].list.Count];
                    for (int i = 0; i < temp["itemCount"].list.Count; i++)
                    {
                        itemCount[i] = int.Parse(temp["itemCount"].list[i].str);
                    }
                    float[] itemProbabilities = new float[temp["itemProbabilities"].list.Count];
                    for (int i = 0; i < temp["itemProbabilities"].list.Count; i++)
                    {
                        itemProbabilities[i] = float.Parse(temp["itemProbabilities"].list[i].str);
                    }
                    item = new Package(id, name, description, type, technology, value, maxStark, sprite, isGetOne, itemIDs, itemTypes, itemTechnologies, itemCount, itemProbabilities);
                    break;
                #endregion

                #region 解析装备
                case Item.ItemType.Equipment:
                    int equipmentID = (int)temp["equipmentID"].n;
                    Equipment.EquipmentType equipmentType = (Equipment.EquipmentType)System.Enum.Parse(typeof(Equipment.EquipmentType), temp["equipmentType"].str);
                    int resistance = (int)temp["resistance"].n;
                    int constitution = (int)temp["constitution"].n;
                    int strength = (int)temp["strength"].n;
                    int agility = (int)temp["agility"].n;
                    int dexterous = (int)temp["dexterous"].n;
                    int concentration = (int)temp["concentration"].n;
                    item = new Equipment(id, name, description, type, technology, value, maxStark, sprite, equipmentID, equipmentType, resistance, constitution, strength, agility, dexterous, concentration);
                    break;
                #endregion

                #region 解析武器
                case Item.ItemType.MeleeWeapon:
                    int meleeWeaponID = (int)temp["meleeWeaponID"].n;
                    int meleeDamage = (int)temp["meleeDamage"].n;
                    int throwDamage = (int)temp["throwDamage"].n;
                    float attackInterval = temp["attackInterval"].n;
                    MeleeWeapon.MeleeWeaponType mwType = (MeleeWeapon.MeleeWeaponType)System.Enum.Parse(typeof(MeleeWeapon.MeleeWeaponType), temp["mwType"].str);
                    item = new MeleeWeapon(id, name, description, type, technology, value, maxStark, sprite, meleeWeaponID, meleeDamage, throwDamage, attackInterval, mwType);
                    break;
                case Item.ItemType.ShootWeapon:
                    int shootWeaponID = (int)temp["shootWeaponID"].n;
                    int shootDamage = (int)temp["shootDamage"].n;
                    int s_meleeDamage = (int)temp["meleeDamage"].n;
                    float s_attackInterval = temp["attackInterval"].n;
                    float lashuanTime = temp["lashuanTime"].n;
                    float reloadTime = temp["reloadTime"].n;
                    float damageRange = temp["damageRange"].n;
                    ShootWeapon.ShootWeaponType swType = (ShootWeapon.ShootWeaponType)System.Enum.Parse(typeof(ShootWeapon.ShootWeaponType), temp["swType"].str);
                    ShootWeapon.AmmoType amType = (ShootWeapon.AmmoType)System.Enum.Parse(typeof(ShootWeapon.AmmoType), temp["amType"].str);
                    int ammoCount = (int)temp["ammoCount"].n;
                    int accuracy = (int)temp["accuracy"].n;
                    item = new ShootWeapon(id, name, description, type, technology, value, maxStark, sprite, shootWeaponID, shootDamage, s_meleeDamage, s_attackInterval, lashuanTime, reloadTime, damageRange, swType, amType, ammoCount, accuracy);
                    break;
                #endregion

                #region 解析材料，蓝图，子弹
                case Item.ItemType.Unuseable:
                    item = new Unuseable(id, name, description, type, technology, value, maxStark, sprite);
                    break;
                case Item.ItemType.Blueprint:
                    int[] craftIDs = new int[temp["craftIDs"].list.Count];
                    for (int i = 0; i < temp["craftIDs"].list.Count; i++)
                    {
                        craftIDs[i] = int.Parse(temp["craftIDs"].list[i].str);
                    }
                    item = new Blueprint(id, name, description, type, technology, value, maxStark, sprite, craftIDs);
                    break;
                case Item.ItemType.Ammo:
                    item = new Ammo(id, name, description, type, technology, value, maxStark, sprite);
                    break;
                #endregion

                #region 解析BUFF，技能
                case Item.ItemType.Buff:
                    int buffID = (int)temp["buffID"].n;
                    int durationTime = (int)temp["durationTime"].n;
                    string effectDescription = temp["effectDescription"].str;
                    string[] effectProp = new string[temp["effectProp"].list.Count];
                    int[] effectValue = new int[temp["effectValue"].list.Count];
                    for (int i = 0; i < temp["effectProp"].list.Count; i++)
                    {
                        effectProp[i] = temp["effectProp"].list[i].str;
                    }
                    for (int i = 0; i < temp["effectValue"].list.Count; i++)
                    {
                        effectValue[i] = int.Parse(temp["effectValue"].list[i].str);
                    }
                    item = new Buff(id, name, description, type, technology, value, maxStark, sprite, buffID, durationTime, effectDescription, effectProp, effectValue);
                    break;
                case Item.ItemType.Skill:
                    int skillID = (int)temp["skillID"].n;
                    int maxPoint = (int)temp["maxPoint"].n;
                    string skillDescription = temp["skillDescription"].str;
                    int[] preSkillID = new int[temp["preSkillID"].list.Count];
                    for (int i = 0; i < temp["preSkillID"].list.Count; i++)
                    {
                        preSkillID[i] = int.Parse(temp["preSkillID"].list[i].str);
                    }
                    int[] eachSkillNeed = new int[temp["eachSkillNeed"].list.Count];
                    for (int i = 0; i < temp["eachSkillNeed"].list.Count; i++)
                    {
                        eachSkillNeed[i] = int.Parse(temp["eachSkillNeed"].list[i].str);
                    }
                    item = new Skill(id, name, description, type, technology, value, maxPoint, sprite, skillID, maxPoint, skillDescription, preSkillID, eachSkillNeed);
                    break;
                    #endregion
            }
            itemList.Add(item);
        }
        //拳头，只用于属性调整
        MeleeWeapon hand = new MeleeWeapon(999, "拳头", " ", Item.ItemType.MeleeWeapon, Item.ItemTechnology.T0, 0, 1, " ", 0, 20, 0, 2, MeleeWeapon.MeleeWeaponType.刀);
        itemList.Add(hand);
    }

    void ParseCraftJson()
    {
        craftList = new List<CraftRecipe>();
        TextAsset craftText = Resources.Load<TextAsset>("Crafts");
        string craftJson = craftText.text;
        JSONObject craftObjects = new JSONObject(craftJson);

        foreach (JSONObject temp in craftObjects.list)
        {
            int id = (int)temp["id"].n;
            string name = temp["name"].str;
            bool isNeedLearn = temp["isNeedLearn"].n == 0 ? false : true;
            CraftRecipe.CraftType type = (CraftRecipe.CraftType)System.Enum.Parse(typeof(CraftRecipe.CraftType), temp["type"].str);
            int[] needItemIDs = new int[temp["needItemIDs"].list.Count];
            for (int i = 0; i < temp["needItemIDs"].list.Count; i++)
            {
                needItemIDs[i] = int.Parse(temp["needItemIDs"].list[i].str);
            }
            int[] needItemCount = new int[temp["needItemCount"].list.Count];
            for (int i = 0; i < temp["needItemCount"].list.Count; i++)
            {
                needItemCount[i] = int.Parse(temp["needItemCount"].list[i].str);
            }
            int[] getItemIDs = new int[temp["getItemIDs"].list.Count];
            for (int i = 0; i < temp["getItemIDs"].list.Count; i++)
            {
                getItemIDs[i] = int.Parse(temp["getItemIDs"].list[i].str);
            }
            int[] getItemCount = new int[temp["getItemCount"].list.Count];
            for (int i = 0; i < temp["getItemCount"].list.Count; i++)
            {
                getItemCount[i] = int.Parse(temp["getItemCount"].list[i].str);
            }
            CraftRecipe craft = new CraftRecipe(id, name, isNeedLearn, type, needItemIDs, needItemCount, getItemIDs, getItemCount);
            craftList.Add(craft);
        }
        craftRecipos = SetCraftList(true);
        craftLearned = SetCraftList(false);
    }

    public Item GetItemByID(int id)
    {
        foreach (Item item in itemList)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        return null;
    }

    public void ShowToolTip(string content)
    {
        if (this.isPickedItem) return;
        isToolTipShow = true;
        toolTip.Show(content);
    }

    public void HideToolTip()
    {
        isToolTipShow = false;
        toolTip.Hide();
    }
    public void PickedUpSlot(Slot slot)
    {
        pickedSlot = slot;
    }
    public void PickUpItem(Item item, int amount)
    {
        PickedItem.SetItemUI(item, amount);
        isPickedItem = true;
        PickedItem.Show();
        this.toolTip.Hide();
    }
    public void RemoveItem(int amount = 1)
    {
        PickedItem.ReduceAmount(amount);
        if (PickedItem.Amount <= 0)
        {
            isPickedItem = false;
            PickedItem.Hide();
        }
    }

    public List<List<CraftRecipe>> SetCraftList(bool isAll)
    {
        string[] typeNames = System.Enum.GetNames(typeof(CraftRecipe.CraftType));
        List<List<CraftRecipe>> returnCraftList = new List<List<CraftRecipe>>();
        for (int i = 0; i < typeNames.Length; i++)
        {
            List<CraftRecipe> oneType = new List<CraftRecipe>();
            returnCraftList.Add(oneType);
        }
        foreach (CraftRecipe craft in craftList)
        {
            if (!craft.IsNeedLearn || isAll)
            {
                switch (craft.Type)
                {
                    case CraftRecipe.CraftType.生存:
                        returnCraftList[0].Add(craft);
                        break;
                    case CraftRecipe.CraftType.药物:
                        returnCraftList[1].Add(craft);
                        break;
                    case CraftRecipe.CraftType.武器:
                        returnCraftList[2].Add(craft);
                        break;
                    case CraftRecipe.CraftType.维修:
                        returnCraftList[3].Add(craft);
                        break;
                    case CraftRecipe.CraftType.拆解:
                        returnCraftList[4].Add(craft);
                        break;
                }
            }

        }
        return returnCraftList;
    }
    public void LearnCraft(int craftID)
    {
        foreach (CraftRecipe craft in craftList)
        {
            if (craft.ID == craftID)
            {
                switch (craft.Type)
                {
                    case CraftRecipe.CraftType.生存:
                        if (!craftLearned[0].Contains(craft))
                            craftLearned[0].Add(craft);
                        break;
                    case CraftRecipe.CraftType.药物:
                        if (!craftLearned[1].Contains(craft))
                            craftLearned[1].Add(craft);
                        break;
                    case CraftRecipe.CraftType.武器:
                        if (!craftLearned[2].Contains(craft))
                            craftLearned[2].Add(craft);
                        break;
                    case CraftRecipe.CraftType.维修:
                        if (!craftLearned[3].Contains(craft))
                            craftLearned[3].Add(craft);
                        break;
                    case CraftRecipe.CraftType.拆解:
                        if (!craftLearned[4].Contains(craft))
                            craftLearned[4].Add(craft);
                        break;
                }
            }
        }
        CraftPanel.Instance.CraftChangeMethod();
    }
    public List<CraftRecipe> GetCraftsOfType(int typeValue)
    {
        return craftLearned[typeValue];
    }
    public List<Item> FindItemsWithCondition(Item.ItemType type, Item.ItemTechnology tech)
    {
        List<Item> items = new List<Item>();
        foreach (Item item in itemList)
        {
            if (item.Type == type && item.Technology == tech)
            {
                items.Add(item);
            }
        }
        return items;
    }

    public void SetItemPrice(float rate)
    {
        foreach (Item item in itemList)
        {
            item.SellPrice = (int)(item.Value * 0.9f * rate);
            item.BuyPrice = (int)(item.Value * 1.1f * rate);
        }
    }

    public void InitItemPrice()
    {
        foreach (Item item in itemList)
        {
            item.SellPrice = 0;
            item.BuyPrice = 0;
        }
    }
    public List<Item> GetItemsByTechAndEquipType(Item.ItemTechnology tech,Equipment.EquipmentType equipmentType)
    {
        List<Item> returnItems = new List<Item>();
        foreach (Item item in itemList)
        {
            if (item.Technology == tech&& item.Type ==Item.ItemType.Equipment)
            {
                Equipment equipment = (Equipment)item;
                if (equipment.EquipType == equipmentType)
                {
                    returnItems.Add(item);
                }
            }
        }
        return returnItems;
    }
    public List<Item> GetItemsByTechAndType(Item.ItemTechnology tech,Item.ItemType type)
    {
        List<Item> returnItems = new List<Item>();
        foreach (Item item in itemList)
        {
            if (item.Technology == tech && item.Type == type)
            {
                returnItems.Add(item);
            }
        }
        return returnItems;
    }
}
