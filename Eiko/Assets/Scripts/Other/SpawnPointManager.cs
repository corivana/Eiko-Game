using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPointManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    /*
    Level 0 = Start Menu
    Level 1 = Hub
    Level 2 = Training Level
    Level 3 = Level 1
    */

    private void OnLevelWasLoaded(int level)
    {
        if(level == 1)
        {
            PlayerManager.instance.transform.position = spawnPoint.position;
        }
        if(level == 2)
        {
            PlayerManager.instance.transform.position = spawnPoint.position;
        }
        if(level == 3)
        {
            PlayerManager.instance.transform.position = spawnPoint.position;
        }
    }



}
