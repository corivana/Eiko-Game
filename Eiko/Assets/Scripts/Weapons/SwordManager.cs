using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    [SerializeField] private Transform trans;
    bool attackEnabled;

    // Update is called once per frame
    void Update()
    {
        //if left click is true then we can attack
        //left click if true for a certain amount of seconds after the player clicks, and then returns to false *See: PlayerManager script
        attackEnabled = PlayerManager.instance.leftClick;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject objectCollided = other.gameObject;
        if (other.gameObject.tag == "meleeEnemy")
        {
            //Debug.Log("Collision with sword!");
            if (attackEnabled == true)
            {
                objectCollided.GetComponent<MeleeEnemyManager>().ChangeEnergy(-5);
            }
            
        }
        if (other.gameObject.tag == "rangedEnemy")
        {
            //Debug.Log("Collision with sword!");
            if (attackEnabled == true)
            {
                objectCollided.GetComponent<RangedEnemyManager>().ChangeEnergy(-5);
            }

        }
        if (other.gameObject.tag == "Boss")
        {
            //Debug.Log("Collision with sword!");
            if (attackEnabled == true)
            {
                objectCollided.GetComponent<BossManager>().ChangeEnergy(-5);
            }

        }
    }

    /*
    private void OnCollisionEnter(Collision col)
    {
        GameObject objectCollided = col.gameObject;
        if (col.gameObject.tag == "enemy")
        {
            Debug.Log("Collision with sword!");
            objectCollided.GetComponent<EnemyManager>().ChangeEnergy(-10);
        }
    }
    */
}
