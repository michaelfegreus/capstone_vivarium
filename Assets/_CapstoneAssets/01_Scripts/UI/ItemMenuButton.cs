using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemMenuButton : MonoBehaviour
{
    // The current item affiliated with this button.
    public Item myItemRef;   

    // Button component
    Button myButton;

    // To check when highlighted.
    EventSystem sceneEventSystem;

    // Button text
    public SuperTextMesh myText;
    // What effects will be added via rich text onto the currently highlighted option.
    string effectTagSTM = "<w>";
    // What to display if there is no item
    string itemEmptyText = "---Empty---";
    // Color to apply to the highlighted string.
    public Color highlightTextColor;
    // Default color
    public Color defaultTextColor;

    private void Start()
    {
        myButton = GetComponent<Button>();
        sceneEventSystem = EventSystem.current;
    }

    private void OnEnable()
    {
        if (myItemRef != null)
        {
            myText.text = myItemRef.inGameItemName;
        }
        else
        {
            myText.Text = itemEmptyText;
        }
    }

    /*private void Update()
    {
        // Check to see if this is being highlighted:
        if (gameObject == sceneEventSystem.currentSelectedGameObject)
        {
            // Check if it's set up by color
            if (myText.color == defaultTextColor)
            {
                if (myItemRef != null)
                {
                    myText.text = effectTagSTM + myItemRef.inGameItemName;
                }
                else
                {
                    myText.text = effectTagSTM + itemEmptyText;
                }
                myText.color = highlightTextColor;
            }
        }
        else
        {
            // Check if it's set up by color
            if (myText.color != defaultTextColor)
            {
                if (myItemRef != null)
                {
                    myText.text = myItemRef.inGameItemName;
                }
                else
                {
                    myText.text = itemEmptyText;
                }
                myText.color = defaultTextColor;
            }
        }
    }*/

    /// <summary>
    /// Sets the Player's equipped item and closes the item menu.
    /// </summary>
    public void SelectItemButtonInput()
    {
        GAME_manager.Instance.inventoryManager.SetEquipItem(myItemRef);
    }
}
