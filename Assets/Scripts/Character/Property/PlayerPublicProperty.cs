using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPublicProperty
{
   // public int CoinAmount { get; set; }
    public int YinCoinAmount { get; set; }
    public int ChaoCoinAmount { get; set; }
    public List<int> ItemIDWith { get; set; }
    public List<int> LearnedCraftID { get; set; }

    public PlayerPublicProperty(int yinCoinAmount, int chaoCoinAmount, List<int> itemIDWith, List<int> learnedCraftID)
    {
        //this.CoinAmount = cointAmount;
        this.YinCoinAmount = yinCoinAmount;
        this.ChaoCoinAmount = chaoCoinAmount;
        this.ItemIDWith = itemIDWith;
        this.LearnedCraftID = learnedCraftID;
    }
}
