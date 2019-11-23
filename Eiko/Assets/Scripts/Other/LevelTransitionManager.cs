using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionManager : MonoBehaviour
{
    [SerializeField] private Transform trans;
    [SerializeField] private Transform playerTrans;
    public static bool exitLevel = false;
    public static bool inLevel = false;

    public float detectionRadius = 3f;
    bool withinRadius = false;

    // Update is called once per frame
    void Update()
    {
        //The distance from the player to the object
        float distance = Vector3.Distance(PlayerManager.instance.transform.position, trans.position);

        //if distance is within the objects radius
        if (distance < detectionRadius)
        {
            withinRadius = true;
        }
        else
        {
            withinRadius = false;
        }

        //if we are:
        //1. Within Radius
        //2. Pressed E
        //3. And the object has a certain tag (does different stuff based on the tag)
        if (withinRadius == true && Input.GetKeyDown(KeyCode.E) && this.gameObject.tag == "hub")
        {
            
            Debug.Log("Exit Bool: " + exitLevel);
            //If tag is hub, then we are returning to the hub, so we need to check if we can exit the level (collected all the items)
            //exitLevel is changed in in the InteractableManager script (the powercore)
            if (exitLevel == true)
            {
                SceneManager.LoadScene("Hub");
            }
        }

        if (withinRadius == true && Input.GetKeyDown(KeyCode.E) && this.gameObject.tag == "traininglevel")
        {
            SceneManager.LoadScene("Training Level");
        }

        if (withinRadius == true && Input.GetKeyDown(KeyCode.E) && this.gameObject.tag == "level1")
        {
            SceneManager.LoadScene("Level 1");
        }

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(trans.position, detectionRadius);
    }

    /// <summary>
    /// Sets necessary values that are unique to a level, and handles other variables that change when enetering a level
    /// </summary>
    /// <param name="level"></param>
    private void OnLevelWasLoaded(int level)
    {
        /*
        Level 0 = Start Menu
        Level 1 = Hub
        Level 2 = Training Level
        Level 3 = Level 1
        */

        if (level == 1)
        {
            inLevel = false;
            HUDManager.numOfItems = 0; //items displayed on the HUD
            PlayerManager.instance.currentEnergy = PlayerManager.instance.maxEnergy; //restore energy
            PlayerManager.instance.itemsCollected = 0; //reset items collected
        }
        if (level == 2)
        {
            HUDManager.numOfItems = 3;
            inLevel = true;
        }
        if(level == 3)
        {
            HUDManager.numOfItems = 6;
            inLevel = true;
        }
           
    }

}
