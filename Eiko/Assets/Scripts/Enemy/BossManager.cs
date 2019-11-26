using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    [SerializeField] private Transform trans;
    public LayerMask aggroLayerMask;
    public float enemyCurrentEnergy;
    public float enemyMaxEnergy;
    private NavMeshAgent navAgent;
    private PlayerManager player;
    private Collider[] withinAggroColliders;
    public float aggroRadius = 25f;
    public Image EnemyEnergyBar;

    public GameObject deathEffectPrefab;
    public Animator anim;


    void Start()
    {

        navAgent = GetComponent<NavMeshAgent>();
        enemyMaxEnergy = 100f;
        enemyCurrentEnergy = enemyMaxEnergy;
        EnemyEnergyBar.fillAmount = enemyCurrentEnergy / 100; //divide by 100 because fill amount is a 0-1 value
    }

    private void Update()
    {
        EnemyEnergyBar.fillAmount = enemyCurrentEnergy / 100;
    }

    void FixedUpdate()
    {
        withinAggroColliders = Physics.OverlapSphere(transform.position, aggroRadius, aggroLayerMask);
        if (withinAggroColliders.Length > 0)
        {
            ChasePlayer(withinAggroColliders[0].GetComponent<PlayerManager>());
        }
    }

    void OnDrawGizmosSelected()  //Draws the aggro radius
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(trans.position, aggroRadius);
    }

    void ChasePlayer(PlayerManager player)
    {
        anim.SetInteger(HashIDs.flyingCond_Int, 1);
        navAgent.SetDestination(player.transform.position);
        this.player = player;

        //if we within distance of the player thank invoke attack
        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            anim.SetInteger(HashIDs.flyingCond_Int, 0);
            if (!IsInvoking("PerformAttack"))
            {
                InvokeRepeating("PerformAttack", .5f, 2f); //can change float values to change how often enemy attacks
            }
        }
        else
        {
            anim.SetInteger(HashIDs.attackCond_Int, 0);
            CancelInvoke("PerformAttack"); //stop attacking if we are too far away
        }
    }

    /// <summary>
    /// Enemy damages the player
    /// </summary>
    void PerformAttack()
    {
        anim.SetInteger(HashIDs.attackCond_Int, 1);
    }

    void DoDamage()
    {
        PlayerManager.instance.ChangeEnergy(-15);
    }

    /// <summary>
    /// Increases(+) or Decreases(-) enemy energy
    /// </summary>
    /// <param name="amount"></param>
    public void ChangeEnergy(int amount)
    {
        enemyCurrentEnergy += amount;
        EnemyEnergyBar.fillAmount = enemyCurrentEnergy / 100;

        if (enemyCurrentEnergy <= 0)
        {
            Die();
            PlayerManager.instance.ChangeEnergy(30); //when an enemy dies add energy to player
        }
    }

    void Die()
    {
        GameObject effectIns = (GameObject)Instantiate(deathEffectPrefab, transform.position, transform.rotation);
        Destroy(effectIns, 1.2f);
        Destroy(gameObject);
    }

}

