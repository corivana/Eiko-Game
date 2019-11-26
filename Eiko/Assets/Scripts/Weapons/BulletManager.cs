using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private Transform target;
    private Transform trans;
    [SerializeField] private float bulletSpeed;
    // private float range = 20f;
    private bool destroy = false;
    public GameObject impactEffectPrefab;


    public void Seek(Transform _target)
    {
        target = _target;
    }

    private void Start()
    {
       // MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
       // Material material = renderer.material;

       // material.SetFloat("_Mode", 1f);
    }

    void Update()
    {
        BulletDie();
        
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = bulletSpeed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        
    }

    /*
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
    */
    private void OnTriggerEnter(Collider other)
    {
     
        GameObject objectCollided = other.gameObject;
        if (other.gameObject.tag == "meleeEnemy")
        {
            destroy = true;
            //Debug.Log("Collision with bullet!");
            objectCollided.GetComponent<MeleeEnemyManager>().ChangeEnergy(-20);
            //Destroy(gameObject);
        }
        if (other.gameObject.tag == "rangedEnemy")
        {
            destroy = true;
            //Debug.Log("Collision with bullet!");
            objectCollided.GetComponent<RangedEnemyManager>().ChangeEnergy(-20);
            //Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        destroy = true;
        //Destroy(gameObject);
    }

    void HitTarget()
    {
        destroy = true;
        //GameObject effectIns = (GameObject)Instantiate(impactEffectPrefab, transform.position, transform.rotation);
        //Destroy(effectIns, 1f);
        //Destroy(gameObject);

    }

    private void BulletDie()
    {
        if(destroy == true)
        {
            GameObject effectIns = (GameObject)Instantiate(impactEffectPrefab, transform.position, transform.rotation);
            Destroy(effectIns, 0.5f);
            Destroy(gameObject);
        }
    }
    

}
