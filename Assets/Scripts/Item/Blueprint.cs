using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : Item
{
    /// <summary>
    /// 合成ID
    /// </summary>
    public int[] CraftIDs { get; set; }

    public Blueprint(int id, string name, string description, ItemType type, ItemTechnology technology, int value, int maxStark, string sprite,int[] craftIDs)
       : base(id, name, description, type, technology, value, maxStark, sprite)
    {
        this.CraftIDs = craftIDs;
    }
}
