using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class slime : MonoBehaviour
{
    public enum States 
    {
        Idle,
        Chase,
        Attack
    }
    States currentState = States.Idle;
    private bool hit;
    public float health;
    public float maxHealth;
    public int str = 10;
    public float currentEnemyHealth;
    private float spawnPos;

    float time = 0f;
    float timeDelay = 1f;

    Animator anim;
    Transform playerPos;
    NavMeshAgent nav;
    GameObject player;

    public Slider slider;

    void Awake () 
    {
        // Set up the references
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        health = maxHealth;
        slider.value = CalculateHealth();
        player = GameObject.Find("FirstPerson-AIO");
        currentEnemyHealth = player.GetComponent<FirstPersonAIO>().getHealth();
    }

    // Damage the enemy
    public void reduceHealth(float damage) 
    {
        health -= damage;
        if (health > 0) {
            nav.speed = 0.5f;
            hit = true;
            hitAnimation();
            hit = false;
            Invoke("hitAnimation", 2.0f);
            nav.speed = 2.5f;
        }
    }

    // Perform the attack animation
    void attackAnimation()
    {
        if (currentEnemyHealth > 0) {
            anim.SetBool("willAttack", true);
        }else {
            anim.SetBool("willAttack", false);
        }
        
    }

    // Performs the hit animation
    void hitAnimation() 
    {
        anim.SetBool("isHit", hit);
    }

    // Performs the death animation
    void dieAnimation(float h) 
    {
        bool death = h <= 0;
        anim.SetBool("isDead", death);
    }

    // Perform walk animation
    void walkAnimation() 
    {
        if (nav.enabled)
        {
            anim.SetBool("isMoving", true);
        } else
        {
            anim.SetBool("isMoving", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = CalculateHealth();
        time = time + 1f * Time.deltaTime;

        if (health <= 0) {
            dieAnimation(health);
            Destroy(gameObject, 4.0f);
        }

        if (health > maxHealth) {
            health = maxHealth;
        }

        ChangeState();
        Debug.Log(currentState);

        switch(currentState)
        {
            case States.Idle:
                nav.enabled = false;
                walkAnimation();
                break;
            case States.Chase:
                nav.enabled = true;
                walkAnimation();
                nav.SetDestination(playerPos.position);
                break;
            case States.Attack:
                if (time >= timeDelay) 
                {
                    time = 0f;
                    attackAnimation();
                    player.GetComponent<FirstPersonAIO>().getHit(str);
                }
                break;

        }
    }

    // change state of slime
    void ChangeState()
    {
        float distance = Vector3.Distance(transform.position, playerPos.position);

        // The Slime should pursue the player
        if(distance < 50 && distance > 5 && currentEnemyHealth > 0)
        {
            switch(currentState)
            {
                case States.Attack:
                    currentState = States.Chase;
                    break;
                case States.Idle:
                    currentState = States.Chase;
                    break;
                case States.Chase:
                    break;
            }
        } 
        // If the player is within distance to get attacked
        else if(distance <= 5 && currentEnemyHealth > 0) 
        {
            switch(currentState)
            {
                case States.Attack:
                    break;
                case States.Idle:
                    currentState = States.Attack;
                    break;
                case States.Chase:
                    currentState = States.Attack;
                    break;
            }
        }
        // Slime is idle
        else
        {
            switch(currentState)
            {
                case States.Attack:
                    currentState = States.Idle;
                    break;
                case States.Idle:
                    break;
                case States.Chase:
                    currentState = States.Idle;
                    break;
            }
        }
    }

    float CalculateHealth() 
    {
        return health / maxHealth;
    }

    void spawnSmallSlimes() 
    {
        
    }
}
