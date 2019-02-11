using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorPanel : Inventory
{
    #region 单例模式
    private static VendorPanel _instance;
    public static VendorPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("VendorBigPanel").transform.Find("VendorStore").GetComponent<VendorPanel>();
            }
            return _instance;
        }
    }
    #endregion

    public int[] itemIDArray;
    private PlayerInventory player;


    public override void Start()
    {
        base.Start();
        //InitVendor();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }
    public void InitVendor()
    {
        foreach (int  itemID in itemIDArray)
        {
            SetItem(itemID);
        }
    }
    public void BuyItem(Item item,bool rightClick, int count=1)
    {
        if (rightClick)
        {
            for (int i = 0; i < count; i++)
            {
                BackPack.Instance.SetItem(item);
            }
         
        }  
    }
    public void SellItem()
    {
        int sellAmout = 1;
        if (InventoryManager.Instance.IsControl)
        {
            sellAmout = 1;
        }
        else
        {
            sellAmout = InventoryManager.Instance.PickedItem.Amount;
        }
        int coinAmount = InventoryManager.Instance.PickedItem.Item.Value * sellAmout;
        player.EarnCoin(coinAmount);
        InventoryManager.Instance.RemoveItem(sellAmout);
    }

    public void SellItem(Item item,bool rightClick,int count=1)
    {
        if (!(InventoryManager.Instance.PickedSlot is SoldSlot)&& !rightClick) return;
        int coinAmount = item.SellPrice*count;
        player.EarnCoin(coinAmount);
        Debug.Log(count);
        if (rightClick)
        {
            for (int i = 0; i < count; i++)
            {
                SetItem(item);
            }
        }      
    }

    public bool CanBuyItem(Item item,int count=1,bool rightClick=false)
    {
        if (!(InventoryManager.Instance.PickedSlot is VendorSlot)&& !rightClick) return false;
        int coinAmount = item.BuyPrice * count;
        return player.CostCoin(coinAmount);
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
}
