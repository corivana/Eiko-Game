using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletManager : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    private float range = 25f;
    public GameObject impactEffectPrefab;
    private bool destroy = false;

    void Update()
    {
       MoveBullet();
        BulletDie();
    }

    
    private void MoveBullet()
    {
        transform.position += transform.forward * Time.deltaTime * bulletSpeed;
        Vector3 distance = transform.position - PlayerManager.instance.transform.position;
        float distanceThisFrame = bulletSpeed * Time.deltaTime;

        if (distance.magnitude > range)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        GameObject objectCollided = other.gameObject;
        if (other.gameObject.tag == "Player")
        {
            destroy = true;
            //Debug.Log("Collision with bullet!");
            PlayerManager.instance.ChangeEnergy(-1);
            //Destroy(gameObject);
        }
        if(other.gameObject.tag == "meleeEnemy")
        {
            destroy = true;
            //Destroy(gameObject);
        }
        if(other.gameObject.tag == "rangedEnemy")
        {
            destroy = true;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        destroy = true;
        //Destroy(gameObject);
    }

    private void BulletDie()
    {
        if (destroy == true)
        {
            GameObject effectIns = (GameObject)Instantiate(impactEffectPrefab, transform.position, transform.rotation);
            Destroy(effectIns, 0.5f);
            Destroy(gameObject);
        }
    }


}
