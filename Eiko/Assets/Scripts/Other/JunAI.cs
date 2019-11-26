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

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        transform.position = JunPosition.position;

        withinRangeColliders = Physics.OverlapSphere(transform.position, rangeRadius, rangeLayerMask);
        if (withinRangeColliders.Length > 0)
        {
            ChangeColorToRed();
        }
        else
        {
            ChangeColorToWhite();
        }
    }

    void ChangeColorToRed()
    {
        GameObject.Find("Jun").GetComponent<Light>().color = Color.green;
    }
    void ChangeColorToWhite()
    {
        GameObject.Find("Jun").GetComponent<Light>().color = Color.white;
    }


    void OnDrawGizmosSelected()  //Draws the aggro radius
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeRadius);
    }


}
