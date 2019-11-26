using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JunAI : MonoBehaviour
{
    public LayerMask rangeLayerMask;
    private NavMeshAgent navAgent;
    private Collider[] withinRangeColliders;
    public float rangeRadius = 15f;
    [SerializeField] private Transform JunPosition;

    private Light lt;
    float duration  = 2.0f;
    float originalRange;
//    private float moveDistance;
    private Vector3 currentPos;
//    private float speed = 3.0f;
//    private float step;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        lt = GameObject.Find("Jun").GetComponent<Light>();
        originalRange = lt.range;
    }

    void Update()
    {
        transform.position = JunPosition.position;
        
        var amplitude = Mathf.PingPong(Time.time, duration);

        // Transform from 0..duration to 0.5..1 range.
        amplitude = amplitude / duration * 0.5f + 1f;

        // Set light range.
        lt.range = originalRange * amplitude;

        withinRangeColliders = Physics.OverlapSphere(transform.position, rangeRadius, rangeLayerMask);
        if (withinRangeColliders.Length > 0)
        {
            ChangeColorToRed();
            //Relocation Failure
            //RelocateJun();

        }
        else
        {
            ChangeColorToWhite();
        }
    }

    void ChangeColorToRed()
    {
        lt.color = Color.green;
    }
    
/*    void RelocateJun()
    {
        currentPos = GameObject.Find("Jun").transform.position;
        step = speed * Time.deltaTime;
        GameObject closestItem = null;
        float closestItemDist = 0;
        
        foreach (var item in withinRangeColliders)
        {
            float itemDistanceFromJun = Vector3.Distance(item.gameObject.transform.position, currentPos);
            if (itemDistanceFromJun < closestItemDist)
            {
                closestItemDist = itemDistanceFromJun;
                closestItem = item.gameObject;
            }
        }
        
        transform.position = Vector3.MoveTowards(currentPos, closestItem.transform.position, step);
    }*/
    void ChangeColorToWhite()
    {
        lt.color = Color.white;
    }


    void OnDrawGizmosSelected()  //Draws the aggro radius
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeRadius);
    }


}
