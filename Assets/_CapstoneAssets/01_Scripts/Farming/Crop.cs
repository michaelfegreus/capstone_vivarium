using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Opsive.UltimateInventorySystem;
using Opsive.Shared.Inventory;
using Opsive.UltimateInventorySystem.Core.DataStructures;

[CreateAssetMenu(fileName = "MyCrop", menuName = "ScriptableObjects/CropObject", order = 1)]

public class Crop : ScriptableObject
{
    public int numGrowthStages = 3;

    public int regrowthTimeInDays = 3;

    public Sprite justPlantedSprite;

    public Sprite[] growthSprites = new Sprite[2];

    public Sprite justHarvestedSprite;

    public ItemInfo[] harvestOutputs = new ItemInfo[1];

    public bool removeOnHarvest = false;

    public int fullyGrownEXP;

    public bool needsWater = true;
}
