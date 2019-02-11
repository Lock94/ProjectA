using UnityEngine;
using System.Collections.Generic;

public class VendorBigPanel : BigPanel
{
    #region 单例模式
    private static VendorBigPanel _instance;
    public static VendorBigPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("VendorBigPanel").GetComponent<VendorBigPanel>();
            }
            return _instance;
        }
    }
    #endregion

    //private CanvasGroup canvasGroup;

    //private float targetAlpha = 0;
    //private float smoothing = 5;

    private VendorPanel vendorStore;
    private BackPack vendorBag;
    private VendorMessage vendorMessage;

    public float YinRate =1.7f;
    public float ChaoRate=0.7f;
    private int yinCountBef;
    private int chaoCountBef;

    private PlayerInventory player;

    public int UseCoinID { get; set; }
    public KeyCode button = KeyCode.O;

    private List<Item> tempList;

    public override void Start()
    {
        base.Start();
        vendorStore = transform.Find("VendorStore").GetComponent<VendorPanel>();
        vendorBag = transform.Find("VendorBag").GetComponent<BackPack>();
        vendorMessage = transform.Find("VendorMessage").GetComponent<VendorMessage>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }
    public override void Show()
    {
        base.Show();
        vendorStore.ClearItems();
        vendorStore.InitVendor();
        vendorMessage.SetMessage(YinRate, ChaoRate, "T1", player.myPublic.YinCoinAmount, player.myPublic.ChaoCoinAmount);

        tempList = BackPack.Instance.AllItems();
        BackPack.Instance.ClearItems();
        BackPack.BackPackID = 2;
        BackPack.Instance.TransferItems(tempList);

        yinCountBef = player.myPublic.YinCoinAmount;
        chaoCountBef = player.myPublic.ChaoCoinAmount;
    }
    public override void Hide()
    {
        base.Hide();

        tempList = BackPack.Instance.AllItems();
        BackPack.Instance.ClearItems();
        BackPack.BackPackID = 1;
        BackPack.Instance.TransferItems(tempList);
    }

    public void SetPrice(int id)
    {
        if (id == 1)
        {
            InventoryManager.Instance.SetItemPrice(YinRate);
        }
        else if (id ==2)
        {
            InventoryManager.Instance.SetItemPrice(ChaoRate);
        }
    }

    public void InitPrice()
    {
        InventoryManager.Instance.InitItemPrice();
    }

    public void UpdateAmount()
    {
        vendorMessage.SetYinAmount(player.myPublic.YinCoinAmount,yinCountBef);
        vendorMessage.SetChaoAmount(player.myPublic.ChaoCoinAmount,chaoCountBef);
    }
    public void EarnCoin(int amount)
    {
        if (UseCoinID == 1)
        {
            player.myPublic.YinCoinAmount += amount;
        }
        else if (UseCoinID==2)
        {
            player.myPublic.ChaoCoinAmount += amount;
        }
        UpdateAmount();
    }
    public bool CostCoin(int amount)
    {
        if (UseCoinID == 1)
        {
            if (player.myPublic.YinCoinAmount >= amount)
            {
                player.myPublic.YinCoinAmount -= amount;
                UpdateAmount();
                return true;
            }
        }
        else if (UseCoinID == 2)
        {
            if (player.myPublic.ChaoCoinAmount >= amount)
            {
                player.myPublic.ChaoCoinAmount -= amount;
                UpdateAmount();
                return true;
            }
        }
        return false;
    }
    public void ResetItem()
    {
        vendorStore.ClearItems();
        vendorStore.InitVendor();
        vendorBag.ClearItems();
        vendorBag.TransferItems(tempList);
        player.myPublic.YinCoinAmount = yinCountBef;
        player.myPublic.ChaoCoinAmount = chaoCountBef;
        vendorMessage.SetYinAmount(player.myPublic.YinCoinAmount, yinCountBef);
        vendorMessage.SetChaoAmount(player.myPublic.ChaoCoinAmount, chaoCountBef);
    }
}
