using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBall : MonoBehaviour
{
    public GameObject explode;
    private GameObject enemy;
    private float damage = 5f;
    //private float cost = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2.0f);
    }

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Slime") {
            enemy = GameObject.Find("Slime");
            Instantiate(explode, transform.position, transform.rotation);
            enemy.GetComponent<slime>().reduceHealth(damage);
            Destroy(gameObject);
        }
    }

    bool useFireBall() {
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
