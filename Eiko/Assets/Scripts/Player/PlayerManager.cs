using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance; //singleton
    //[SerializeField] private Transform trans;
    public Transform rangedTarget;
    public PlayerMovementManager movementManager;
    public Animator anim;
    public int itemsCollected = 0;

    //Melee Attack
    public bool leftClick = false;

    //Ranged Attack
    public Transform temp;
    [SerializeField] private float fireRate = 2f;
    private float fireCountdown = 0f;
    public GameObject bulletPrefab;
    public Transform firePoint;


    //Health Variables
    [SerializeField] public float currentEnergy;
    [SerializeField] public float maxEnergy;

    //Cheats
    private bool invincible = false;

    public AudioSource ShootSource;
    public AudioClip shootClip;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        // Has the singleton not been created yet
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        ShootSource.clip = shootClip;
        movementManager = GetComponent<PlayerMovementManager>();
        currentEnergy = 100f;
    }

    void Update()
    {

        //If we are in a level then let energy degen over time
        if (LevelTransitionManager.inLevel == true)
        {
            EnergyDegen();
        }

        CheckInvincibility();
        PerformMeleeAttack();

        
        if (fireCountdown <= 0f && Input.GetMouseButtonDown(1))
        {
            anim.SetInteger(HashIDs.shootCond_Int, 1);
            fireCountdown = 1f / fireRate;
        }
        else
        {
            anim.SetInteger(HashIDs.shootCond_Int, 0);
        }

        fireCountdown -= Time.deltaTime;
    }

    void FixedUpdate()
    {
       //Debug.Log("Able to attack: " + leftClick);
    }

    void PerformRangedAttack()
    {
        ShootSource.Play();
        temp.position = rangedTarget.position;
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        BulletManager bullet = bulletGO.GetComponent<BulletManager>();

        if(bullet != null)
        {
            bullet.Seek(temp);
        }
    }

    /// <summary>
    /// When the player left clicks: sets leftClick to true for 3 seconds, then runs DisableAttack which sets it to false.
    /// leftClick bool allows the player to do damage when true
    /// </summary>
    void PerformMeleeAttack()
    {
        if (Input.GetMouseButton(0))
        {
            anim.SetInteger(HashIDs.slashCond_Int, 1);
            leftClick = true;
            Invoke("DisableMeleeAttack", 2f);
        }
        else
        {
            anim.SetInteger(HashIDs.slashCond_Int, 0);
        }
    }

    void DisableMeleeAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            leftClick = false;
        }
    }


    /// <summary>
    /// Toggle able to take damage on and off using "*"
    /// Currently Broken: can't toggle off
    /// </summary>
    void CheckInvincibility()
    {
        //TODO: FIX ME, can't toggle off once toggled on
        bool onoff = false;
        if(Input.GetKeyDown(KeyCode.KeypadMultiply))
        {
            onoff = !onoff;
            if(onoff)
            {
                invincible = true;
            }
            else
            {
                invincible = false;
            }
        }
    }

    /// <summary>
    /// Increase(+) or Decrease energy(-)
    /// </summary>
    /// <param name="amount"></param>
    public void ChangeEnergy(int amount)
    {
        if(invincible == false) //Make sure we aren't invincible
        {
            currentEnergy += amount;  
            if (currentEnergy <= 0)
            {
                Die();
            }
        }
    }

    /// <summary>
    /// Energy decreases over time
    /// </summary>
    public void EnergyDegen()
    {
        //You can change the rate it decreases by changing float value
        currentEnergy -= .2f * Time.deltaTime;
    }

    public void AddItems()
    {
        itemsCollected++;
    }


    private void Die()
    {
        print("Player Dead!");
        Destroy(gameObject);
        //TODO: What do we do when we die?
    }

}
