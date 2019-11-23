using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractableManager : MonoBehaviour

{
    [SerializeField] private Transform trans;
    [SerializeField] private Transform playerTrans;
    [SerializeField] private int totalItems;

    public float detectionRadius = 3f;
    bool withinRadius = false;


    // Update is called once per frame
    void Update()
    {
        //Distance from the player to the object
        float distance = Vector3.Distance(PlayerManager.instance.transform.position, trans.position);

        //Is the distance within raidus
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
        if (withinRadius == true && Input.GetKeyDown(KeyCode.E) && this.gameObject.tag == "item")
        {
            PlayerManager.instance.AddItems();
            Debug.Log("Items: " + PlayerManager.instance.itemsCollected);
            Destroy(gameObject); //Destroy item when we collect it
        }
        if (withinRadius == true && Input.GetKeyDown(KeyCode.E) && this.gameObject.tag == "powercore1")
        {
            if(PlayerManager.instance.itemsCollected == totalItems) //We need to have collected all the items for the level to interact with the powercore
            {
                //set exitLevel true
                LevelTransitionManager.exitLevel = true;
                print("You can leave now!");

                //Give player full energy again
                PlayerManager.instance.currentEnergy = PlayerManager.instance.maxEnergy;

                //Then add 50 to that total energy
                PlayerManager.instance.maxEnergy += 50;
                PlayerManager.instance.currentEnergy += 50;

            }
            else
            {
                //set exitLevel false
                LevelTransitionManager.exitLevel = false;
                print("You can't leave yet!");
            }
        }

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(trans.position, detectionRadius);
    }

}
