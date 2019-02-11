using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackPack : Inventory
{
    /// <summary>
    /// true 默认背包，false 商人状态下的背包
    /// </summary>
    public static int BackPackID = 1;
    #region 单例模式
    private static BackPack _instance1;
    private static BackPack _instance2;
    private static BackPack _instance3;
    public static BackPack Instance
    {
        get
        {
            if (_instance1 ==null||_instance2 ==null|| _instance3==null)
            {
                _instance1 = GameObject.Find("Canvas").transform.Find("BackPackPanel").GetComponent<BackPack>();
                _instance2 = GameObject.Find("Canvas").transform.Find("VendorBigPanel").Find("VendorBag").GetComponent<BackPack>();
                _instance3 = GameObject.Find("Canvas").transform.Find("TeamBigPanel").Find("TeamBag").GetComponent<BackPack>();
            }
            if (BackPackID == 1)
            {
                return _instance1;
            }
            else if (BackPackID == 2)
            {
                return _instance2;
            }
            else if (BackPackID == 3)
            {
                return _instance3;
            }
            return _instance1;
        }
    }
    #endregion

    public KeyCode button = KeyCode.I;
    private Button sort;


    public void TransferItems(List<Item> tempList)
    {
        if (tempList != null)
        {
            foreach (var item in tempList)
            {
                SetItem(item);
            }
        }
    }

    public override void Start()
    {
        base.Start();
        sort = transform.Find("Sort").GetComponent<Button>();
        sort.onClick.AddListener(delegate { SortItems(); });
    }

    public int  GetCountByID(int id)
    {
        int allAmount = 0;
        foreach (Slot slot in SlotList)
        {
            if (slot.HasItem())
            {
                if (slot.GetItem().ID ==id)
                {
                    allAmount += slot.GetAmount();
                }
            }
        }
        return allAmount;
    }
    public void RemoveItem(int id, int amount = 1)
    {
        int needAmount = amount;

        foreach (Slot slot in SlotList)
        {
            if (needAmount > 0)
            {
                if (slot.HasItem())
                {
                    if (slot.GetItem().ID == id)
                    {
                        if (slot.GetAmount() >= amount)
                        {
                            slot.RemoveItem(amount);
                        }
                        else
                        {
                            slot.RemoveItem(slot.GetAmount());
                            needAmount -= slot.GetAmount();
                        }
                    }
                }
            }
            else
            {
                break;
            }
        }
    }
    private List<Item> newItemList = new List<Item>();
    /// <summary>
    /// 物品排序算法
    /// </summary>
    public void SortItems()
    {
        List<Item> itemList = new List<Item>();
        List<int> amountList = new List<int>();

        foreach (Slot slot in SlotList)
        {
            if (slot.HasItem())
            {
                itemList.Add(slot.GetItem());
                amountList.Add(slot.GetAmount());
            } 
        }

        newItemList = new List<Item>();

        AddItemByType(itemList, amountList, Item.ItemType.Consumable);
        AddItemByType(itemList, amountList, Item.ItemType.Package);
        AddItemByType(itemList, amountList, Item.ItemType.Ammo);
        AddItemByType(itemList, amountList, Item.ItemType.Unuseable);
        AddItemByType(itemList, amountList, Item.ItemType.ShootWeapon);
        AddItemByType(itemList, amountList, Item.ItemType.MeleeWeapon);
        AddItemByType(itemList, amountList, Item.ItemType.Equipment);
        AddItemByType(itemList, amountList, Item.ItemType.Blueprint);

       // Debug.Log(newItemList.Count);

        for (int i = 0; i < itemList.Count; i++)
        {
            for (int j = 0; j < amountList.Count; j++)
            {
                RemoveItem(itemList[i].ID);
            }
        }
        
        foreach (Item item in newItemList)
        {
            SetItem(item);
        }
    }
    void AddItemByType(List<Item> oldItemList, List<int> amountList, Item.ItemType type)
    {
        for (int i = 0; i < oldItemList.Count; i++)
        {
            if (oldItemList[i].Type == type)
            {
                for (int j = 0; j < amountList[i]; j++)
                {
                    newItemList.Add(oldItemList[i]);
                }
            }
        }
    }
    public List<Item> AllItems()
    {
        List<Item> itemList = new List<Item>();

        foreach (Slot slot in SlotList)
        {
            if (slot.HasItem())
            {
                for (int i = 0; i < slot.GetAmount(); i++)
                {
                    itemList.Add(slot.GetItem());
                }
            }
        }

        return itemList;
    }
    public void ClearItems()
    {
        foreach (Item item in AllItems())
        {
            RemoveItem(item.ID);
        }
    }
}
