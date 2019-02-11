using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : Inventory
{
    public static int charPanelID=1;
    #region 单例模式
    private static CharacterPanel _instance1;
    private static CharacterPanel _instance2;
    public static CharacterPanel Instance
    {
        get
        {
            if (_instance1 == null)
            {
                _instance1 = GameObject.Find("CharacterPanel").GetComponent<CharacterPanel>();
                _instance2 = GameObject.Find("TeamBigPanel").transform.Find("TeamCharPanel").GetComponent<CharacterPanel>();
            }
            if (charPanelID ==1)
            {
                return _instance1;
            }
            else if(charPanelID ==2)
            {
                return _instance2;
            }
            return _instance1;
        }
    }
    #endregion

    public KeyCode button = KeyCode.C;


    private Transform message;
    private Transform property;
    
    private Transform detailProperty;
    private Transform buff;

    public GameObject TextSlotPrefab;
    public GameObject BuffSlotPrefab;

    private List<BuffSlot> buffSlots;
    private Button backPackButton;
    

    private Button skillButton;

    public void InitTransfrom()
    {
        message = transform.Find("Message");
        property = transform.Find("Property").Find("Prop");
        detailProperty = transform.Find("DetailProperty");
        buffSlots = new List<BuffSlot>();
        buff = transform.Find("Buff").Find("Content");
        if (charPanelID == 1)
        {
            backPackButton = transform.Find("BackPack").GetComponent<Button>();
            skillButton = property.Find("Skill").GetComponent<Button>();

            backPackButton.onClick.AddListener(delegate { BackPack.Instance.Toggle(); });
            skillButton.onClick.AddListener(delegate { SkillPanel.Instance.Toggle(); });
        }  
    }


    public void Equip(Item item)
    {
        //从物品栏直接点击装备物品
        Item exitItem = null;
        foreach (Slot slot in SlotList)
        {
            EquipmentSlot equipmentSlot = (EquipmentSlot)slot;
            if (equipmentSlot.IsRightItem(item))
            {
                if (equipmentSlot.transform.childCount>0)
                {
                    ItemUI currentItem = equipmentSlot.transform.GetChild(0).GetComponent<ItemUI>();
                    exitItem = currentItem.Item;
                    currentItem.SetItemUI(item, 1);
                }
                else
                {
                    equipmentSlot.SetItem(item);
                }
                EquipItem(item, true);
                equipmentSlot.CheckEquipSlot();
                break;
            }
        }
        if (exitItem!=null )
        {
            BackPack.Instance.SetItem(exitItem);
        }
        
    }
    public void UnEquip(Item item)
    {
        //直接右键点击卸载装备
        BackPack.Instance.SetItem(item);
        CheckAllSlot();
        EquipItem(item, false);
    }
    public void CheckAllSlot()
    {
        foreach (Slot slot in SlotList)
        {
            if (slot is EquipmentSlot)
            {
                EquipmentSlot equipmentSlot = (EquipmentSlot)slot;
                equipmentSlot.CheckEquipSlot();
            }
        }
    }

    public void EquipItem(Item item,bool putOn)
    {
        PropertyManager.Instance.EquipPropChange(item, putOn);
    }


    public void AddTextSlot(string title, string value,bool isDetail)
    {
        if (message == null) InitTransfrom();
        GameObject textGo= Instantiate(TextSlotPrefab);
        textGo.GetComponent<TextSlot>().SetTextSlot(title, value);
        if (isDetail)
        {
            if (charPanelID == 1)
                textGo.transform.SetParent(detailProperty);
        }
        else
            textGo.transform.SetParent(message);
    }
    public void ClearText(bool isDetail)
    {
        if (message == null) InitTransfrom();
        if (isDetail)
        {
            if (charPanelID == 1)
            {
                for (int i = 0; i < detailProperty.childCount; i++)
                {
                    Destroy(detailProperty.GetChild(i).gameObject);
                }
            }
        }
        else
        {
            for (int i = 0; i < message.childCount; i++)
            {
                Destroy(message.GetChild(i).gameObject);
            }
        }
      
    }
    public void SetProperty(string constitution, string strength,string agility,string dexterous,string concentration)
    {
        if (message == null) InitTransfrom();
        property.GetChild(0).GetComponent<TextSlot>().SetTextSlot("体质:", constitution);
        property.GetChild(1).GetComponent<TextSlot>().SetTextSlot("力量:", strength);
        property.GetChild(2).GetComponent<TextSlot>().SetTextSlot("灵巧:", agility);
        property.GetChild(3).GetComponent<TextSlot>().SetTextSlot("洞察:", dexterous);
        property.GetChild(4).GetComponent<TextSlot>().SetTextSlot("专注:", concentration);
    }

    public void AddBuff(int itemID)
    {
        Item item = InventoryManager.Instance.GetItemByID(itemID);
        if (!(item is Buff))
        {
            Debug.LogWarning("添加的不是Buff类型");
            return;
        }
        GameObject BuffGo = Instantiate(BuffSlotPrefab) as GameObject;
        BuffGo.transform.SetParent(buff);
        BuffGo.GetComponent<BuffSlot>().SetItem(item);

        Image coldDown = BuffGo.GetComponent<BuffSlot>().Coldown;
        Buff buffItem = (Buff)item;
        int buffID = buffItem.ID;
        RemoveBuff(buffID);
        PropertyManager.Instance.ChangeBuff(buffItem, true);
        buffSlots.Add(BuffGo.GetComponent<BuffSlot>());
        coldDown.fillAmount = buffItem.DurationTime == 0 ? 0 : 1;
    }

    public bool RemoveBuff(int itemID)
    {
        for (int i = 0; i < buffSlots.Count; i++)
        {
            Buff buff = (Buff)buffSlots[i].GetItem();
            if (buff != null && buff.ID == itemID)
            {
                PropertyManager.Instance.ChangeBuff(buff, false);
                buffSlots[i].DestroyBuff();
                buffSlots.Remove(buffSlots[i]);
                return true;
            }
        }
        return false;
    }
    //将角色随身的物品直接显示在UI上
    public void SetEquipments(int headID, int clothID, int pantsID, int beltID, Item weapon)
    {
        //TODO clear
        ClearEquipments();
        //TODO setEquipment
        if (headID != 0)
        {
            Item head = InventoryManager.Instance.GetItemByID(headID);
            SetEquipment(head);
        }
        if (clothID != 0)
        {
            Item cloth = InventoryManager.Instance.GetItemByID(clothID);
            SetEquipment(cloth);
        }
        if (pantsID != 0)
        {
            Item pants = InventoryManager.Instance.GetItemByID(pantsID);
            SetEquipment(pants);
        }
        if (beltID != 0)
        {
            Item belt = InventoryManager.Instance.GetItemByID(beltID);
            SetEquipment(belt);
        }
        if (weapon.ID !=999)
        {
            SetEquipment(weapon);
        }
    }

    public void SetEquipment(Item item)
    {
        foreach (Slot slot in SlotList)
        {
            if (slot is EquipmentSlot)
            {
                EquipmentSlot equipmentSlot = (EquipmentSlot)slot;
                if (equipmentSlot.IsRightItem(item))
                {
                    if (equipmentSlot.transform.childCount > 0)
                    {
                        ItemUI currentItem = equipmentSlot.transform.GetChild(0).GetComponent<ItemUI>();
                        currentItem.SetItemUI(item, 1);
                    }
                    else
                    {
                        equipmentSlot.SetItem(item);
                    }
                    equipmentSlot.CheckEquipSlot();
                    break;
                }
            }
        }
    }
    public void ClearEquipments()
    {
        //清理装备
        foreach (Slot slot in SlotList)
        {
            if (slot is EquipmentSlot)
            {
                if (slot.transform.childCount > 0)
                {
                    slot.RemoveItem();
                }
            }       
        }
    }
}
