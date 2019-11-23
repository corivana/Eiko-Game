using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static int numOfItems;
    public Image[] items;
    public Sprite fullItem;
    public Sprite emptyItem;
    public Image energyBar;
    public Text energyText;

    void Update()
    {
        //Player's Energy
        energyBar.fillAmount = PlayerManager.instance.currentEnergy / PlayerManager.instance.maxEnergy; //Divide bc fill amount is from 0-1

        //Energy Text
        //Rounds the displayed health to an int
        energyText.text = Mathf.RoundToInt(PlayerManager.instance.currentEnergy).ToString();  //+ " / " + PlayerManager.instance.maxEnergy.ToString();



        //Handling array for items found
        for (int i = 0; i < items.Length; i++)
        {
            //Empty or Full Sprite based on items found
            if(i < PlayerManager.instance.itemsCollected)
            {
                items[i].sprite = fullItem;
            }
            else
            {
                items[i].sprite = emptyItem;
            }

            //Display the number of items needed for the level
            if (i < numOfItems)
            {
                items[i].enabled = true;
            }
            else
            {
                items[i].enabled = false;
            }
        }
    }
}
