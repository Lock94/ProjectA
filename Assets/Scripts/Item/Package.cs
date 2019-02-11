using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 包裹
/// </summary>
public class Package : Item
{
    /// <summary>
    /// 判断是从包裹中获取一种还是分别计算，0是分别计算，1是获取其中一个
    /// </summary>
    public bool IsGetOne { get; set; }
    /// <summary>
    /// 以ID获取物品
    /// </summary>
    public int[] ItemIDs { get; set; }
    /// <summary>
    /// 以种类获取物品
    /// </summary>
    public ItemType[] ItemTypes { get; set; }
    /// <summary>
    /// 以科技等级获取物品
    /// </summary>
    public ItemTechnology[] ItemTechnologies { get; set; }
    /// <summary>
    /// 物品获得的数量，与上面相对应
    /// </summary>
    public int[] ItemCount { get; set; }
    /// <summary>
    /// 物品获得的概率，与上面相对应，范围是0-1
    /// </summary>
    public float[] ItemProbabilities { get; set; }

    public Package(int id, string name, string description, ItemType type, ItemTechnology technology, int value, int maxStark, string sprite, bool isGetOne, int[] itemIDs, ItemType[] itemTypes, ItemTechnology[] itemTechnologies, int[] itemCount,float[] itemProbabilities)
        : base(id, name, description, type, technology, value, maxStark, sprite)
    {
        this.IsGetOne = isGetOne;
        this.ItemIDs = itemIDs;
        this.ItemTypes = itemTypes;
        this.ItemTechnologies = itemTechnologies;
        this.ItemCount = itemCount;
        this.ItemProbabilities = itemProbabilities;
    }
}
