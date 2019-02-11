using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{

    public PlayerPublicProperty myPublic;

    private void Start()
    {
        myPublic = new PlayerPublicProperty(1000, 1000, null, null);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            BackPack.Instance.SetItem(Random.Range(0, 324));
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            BackPack.Instance.Toggle();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CharacterPanel.Instance.Toggle();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            InventoryManager.Instance.LearnCraft(35);
            CraftPanel.Instance.CraftChangeMethod();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            CraftPanel.Instance.Toggle();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            VendorBigPanel.Instance.Toggle();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            TeamBigPanel.Instance.Toggle();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            SettingPanel.Instance.Toggle();
        }
    }
   
    /// <summary>
    /// 消费
    /// </summary>
    public bool CostCoin(int amount)
    {
        return VendorBigPanel.Instance.CostCoin(amount);
    }
    /// <summary>
    /// 赚钱
    /// </summary>
    /// <param name="amount"></param>
    public void EarnCoin(int amount)
    {
        VendorBigPanel.Instance.EarnCoin(amount);
    }

   
}
