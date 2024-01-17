using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Opsive.Shared;
using Opsive.Shared.Inventory;
using Opsive.UltimateInventorySystem.Core.DataStructures;
using PixelCrushers.DialogueSystem;

public class SeedPlotLogic : MonoBehaviour
{
    public Crop currentCrop;

    public SpriteRenderer currentSprite;

    public Sprite dugHole;

    public bool plotReady = false;
    public bool plotInUse = false;
    public bool plotWatered = false;

    public int daysGrowing = 0;
    public int daysTillFullyGrown;

    public int daysOfRegrowth = 0;
    public int daysTillReharvest = 5;

    public bool fullyGrown;
    public bool readyToHarvest;
    public int fullGrownEXP;

    private PlayerGlobalFunctions player;
    [SerializeField] private SpriteRenderer wateredSpot;
    private bool usingShovel = false;

    //Debug Values
    public Crop debugCropToPlant;
    public bool debugPlantCrop = false;
    public bool debugNextDay = false;
    public bool debugHarvestPlant = false;

    void Start()
    {
        currentSprite = GetComponent<SpriteRenderer>();
        player = GetComponent<PlayerGlobalFunctions>();
    }

    void Update()
    {
        if (debugNextDay){
            debugNextDay = false;
            NextDay();
        }
        if (debugPlantCrop)
        {
            debugPlantCrop = false;
            PlantSeed(debugCropToPlant);
        }
        if (debugHarvestPlant)
        {
            debugHarvestPlant = false;
            TryHarvestPlant();
        }

    }

    public void OnEnable()
    {
        ClockManager.OnDayStart += DayStart;
    }

    private void DayStart()
    {
        NextDay();
    }

    public void OnDisable()
    {
        ClockManager.OnDayStart -= DayStart;
    }

    public void ShovelInteract()
    {
        SetToCurrentSeedPlot();

        if (plotInUse)
        {
            usingShovel = true;
            TryHarvestPlant();
        }
        else if(!plotReady)
        {
            DigHole();
            plotReady = true;
        }
        else
        {
            FillHole();
            plotReady = false;
        }
    }

    public void DigHole()
    {
        player.SetPlayerAnimationState("Shovel");
        currentSprite.sprite = dugHole;
    }

    public void FillHole()
    {
        //TODO: Swap this out with a fill hole shovelling animation like in Animal Crossing
        player.SetPlayerAnimationState("Shovel");
        currentSprite.sprite = null;
    }

    public void PlantSeed(Crop crop)
    {
        SetToCurrentSeedPlot();
        player.SetPlayerAnimationState("player_interact_plantseed");
        currentCrop = crop;
        plotInUse = true;
        fullyGrown = false;
        daysGrowing = 0;
        daysOfRegrowth = 0;
        daysTillFullyGrown = currentCrop.numGrowthStages;
        currentSprite.sprite = currentCrop.justPlantedSprite;
        daysTillReharvest = currentCrop.regrowthTimeInDays;
        fullGrownEXP = currentCrop.fullyGrownEXP;
    }

    public void SetCurrentCrop()
    {
        GameManager.Instance.GetCurrentItem().TryGetAttributeValue<Crop>("CropObject", out Crop crop);
        PlantSeed(crop);
    }

    public void TryHarvestPlant()
    {
        SetToCurrentSeedPlot();

        if (readyToHarvest)
        {
            if (currentCrop.removeOnHarvest)
            {
                plotInUse = false;
                plotReady = false;
                currentSprite.sprite = null;
            }
            else
            {
                currentSprite.sprite = currentCrop.justHarvestedSprite;
                daysOfRegrowth = 0;
            }

            foreach (ItemInfo x in currentCrop.harvestOutputs)
            {
                x.Item.Initialize(true);
                GameManager.Instance.playerInventory.AddItem(x);
            }

            readyToHarvest = false;
        }
        else if(usingShovel)
        {
            //Plant is not ready to be harvested. Display a message and ask player if they want to destroy the plant and reset the plot
            DialogueManager.instance.StartConversation("Tutorials/DestroyOrLeavePlant");
        }
        usingShovel = false;
    }

    

    public void WaterPlant()
    {
        player.SetPlayerAnimationState("player_tool_wateringcan");
        wateredSpot.enabled = true;
        plotWatered = true;
    }

    public void RemovePlant()
    {
        player.SetPlayerAnimationState("Shovel");
        currentSprite.sprite = dugHole;
        fullyGrown = false;
        plotReady = true;
        daysGrowing = 0;
        daysOfRegrowth = 0;
        plotInUse = false;
        wateredSpot.enabled = false;
        plotWatered = false;
    }

    public void NextDay()
    {
        if (plotInUse)
        {
            if((currentCrop.needsWater && plotWatered) || !currentCrop.needsWater)
            {
                daysGrowing++;

                if (!fullyGrown)
                {
                    currentSprite.sprite = currentCrop.growthSprites[daysGrowing - 1];
                }

                if (daysGrowing == daysTillFullyGrown)
                {
                    fullyGrown = true;
                    readyToHarvest = true;
                    currentSprite.sprite = currentCrop.growthSprites[currentCrop.growthSprites.Length - 1];
                    //TODO: Add fullGrownEXP to player's total EXP value
                }

                if (fullyGrown && !readyToHarvest)
                {
                    daysOfRegrowth++;
                }

                if (daysOfRegrowth == daysTillReharvest)
                {
                    readyToHarvest = true;
                    currentSprite.sprite = currentCrop.growthSprites[currentCrop.growthSprites.Length - 1];
                }
            }
            

            wateredSpot.enabled = false;
            plotWatered = false;
        }
    }

    public void SetToCurrentSeedPlot()
    {
        GameManager.Instance.SetCurrentSeedPlot(this);
    }
}
