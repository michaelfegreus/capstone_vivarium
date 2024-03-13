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
        StartCoroutine("ShowHole");
    }

    public void FillHole()
    {
        //TODO: Swap this out with a fill hole shovelling animation like in Animal Crossing
        player.SetPlayerAnimationState("Shovel");
        StartCoroutine("RemoveHole");

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
        daysTillReharvest = currentCrop.regrowthTimeInDays;
        fullGrownEXP = currentCrop.fullyGrownEXP;
        StartCoroutine("SetCrop");
        player.SetPlayerFreeState();
    }

    IEnumerator ShowHole()
    {
        yield return new WaitForSeconds(0.4f);
        currentSprite.sprite = dugHole;
    }

    IEnumerator RemoveHole()
    {
        yield return new WaitForSeconds(0.4f);
        currentSprite.sprite = null;
    }

    IEnumerator SetCrop()
    {
        yield return new WaitForSeconds(0.6f);
        currentSprite.sprite = currentCrop.justPlantedSprite;

    }

    public void SetCurrentCrop()
    {
        if (plotReady && !plotInUse)
        {
            Opsive.UltimateInventorySystem.Core.ItemDefinition item;
            item = GameManager.Instance.GetCurrentItem();
            item.TryGetAttributeValue<Crop>("CropObject", out Crop crop);
            GameManager.Instance.playerInventory.RemoveItem(item, 1);
            PlantSeed(crop);
        }
        else
        {
            if (GameManager.Instance.notebookMenuManager.menuOpen)
            {
                PlayerManager.Instance.playerAnimation.PlayAnimationState("WrongItemShrug");
            }
            else
            {
                PlayerManager.Instance.playerAnimation.PlayAnimationState("WrongItemShrugHotbar");
            }
            StartCoroutine(SetFreeState());
        }
    }

    IEnumerator SetFreeState()
    {
        yield return new WaitForSeconds(1.5f);
        PlayerManager.Instance.EnterFreeState();
    }

    public void TryHarvestPlant()
    {
        SetToCurrentSeedPlot();

        if (readyToHarvest)
        {
            player.SetPlayerAnimationState("Item Pickup");
            PlayerManager.Instance.EnterMenuState();

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
        StartCoroutine("ShowWetSpot");
        DialogueManager.instance.PlaySequence("MAPlaySound(WateringCanWatering,,,,,PlayerMovement,);");
        plotWatered = true;
    }

    IEnumerator ShowWetSpot()
    {
        if(wateredSpot.enabled!= true)
        {
            float alpha = 0f;
            yield return new WaitForSeconds(0.2f);
            wateredSpot.color = new Color(1f, 1f, 1f, alpha);
            wateredSpot.enabled = true;
            while (alpha < 1)
            {
                alpha += 0.05f;
                wateredSpot.color = new Color(1f, 1f, 1f, alpha);
                yield return new WaitForSeconds(0.1f);
            }
        }
        
    }

    public void RemovePlant()
    {
        player.SetPlayerAnimationState("Shovel");
        StartCoroutine("RemoveSprite");
    }


    IEnumerator RemoveSprite()
    {
        yield return new WaitForSeconds(0.4f);
        currentSprite.sprite = null;
        fullyGrown = false;
        plotReady = false;
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
