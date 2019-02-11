using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 材料
/// </summary>
public class Unuseable : Item
{
    public Unuseable(int id, string name,string description, ItemType type, ItemTechnology technology, int value, int maxStark,string sprite) :base(id, name, description, type, technology, value, maxStark,sprite)
    {

    }
}
