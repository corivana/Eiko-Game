using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class RangedEnemyManager : MonoBehaviour
{
    [SerializeField] private Transform trans;
    [SerializeField] private Transform effectTrans;
    public LayerMask aggroLayerMask;
    public float enemyCurrentEnergy;
    public float enemyMaxEnergy;
    private NavMeshAgent navAgent;
    private PlayerManager player;
    private Collider[] withinAggroColliders;
    public float aggroRadius = 20f;
    public Image EnemyEnergyBar;

    public GameObject deathEffectPrefab;
    public Animator anim;

    //Ranged Attack
    public GameObject enemyBulletPrefab;
    //[SerializeField] private float fireRate = 2f;
    //private float fireCountdown = 0f;
    public Transform enemyFirePoint;
    private float turnSpeed = 7f;
    Vector3 targetPosition;


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
            if (!IsInvoking("PerformAttack"))
            {
                InvokeRepeating("PerformAttack", .1f, 1f); //can change float values to change how often enemy attacks
            }
        }
        else
        {
           CancelInvoke("PerformAttack"); //stop attacking if we are too far away
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
            LookAt();
            /*
            if (!IsInvoking("PerformAttack"))
            {
                InvokeRepeating("PerformAttack", .5f, 3f); //can change float values to change how often enemy attacks
            }
            */
        }
        else
        {
           //CancelInvoke("PerformAttack"); //stop attacking if we are too far away
        }
    }

    void LookAt()
    {
        targetPosition = PlayerManager.instance.transform.position;


        Vector3 towards = targetPosition - trans.position;

        Quaternion targetRotation = Quaternion.LookRotation(towards);
        trans.rotation = Quaternion.Lerp(trans.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Enemy damages the player
    /// </summary>
    void PerformAttack()
    {
        GameObject bulletGO = (GameObject)Instantiate(enemyBulletPrefab, enemyFirePoint.position, enemyFirePoint.rotation);
        EnemyBulletManager bullet = bulletGO.GetComponent<EnemyBulletManager>();
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
            player.ChangeEnergy(30); //when an enemy dies add energy to player
        }
    }

    void Die()
    {
        GameObject effectIns = (GameObject)Instantiate(deathEffectPrefab, effectTrans.position, effectTrans.rotation);
        Destroy(effectIns, 1.2f);
        Destroy(gameObject);
    }

}
