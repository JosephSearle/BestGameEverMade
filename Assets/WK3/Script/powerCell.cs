using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerCell : MonoBehaviour
{
    public GameObject explode;
    private GameObject tripod, box;
    float removeTime = 3.0f;
    float radius = 5.0f;
    float force = 700f;
    // Use this for initialization
    void Start () {
        tripod = GameObject.Find ("tripod");//find the tripod
        box = GameObject.Find ("crate");
        Destroy(gameObject, removeTime); //destory the object after 2s
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Enemy") {
            //instantiate the explosion
            Instantiate(explode, transform.position, transform.rotation);
            //reduce the tripod's health
            tripod.GetComponent<triPodHealth>().reduceHealth();
            Destroy(gameObject);//destory self
        }
        // Check for boxes to send flying
        else if (other.gameObject.tag == "Box") {
            // explode if you hit a box
            Instantiate(explode, transform.position, transform.rotation);
            // Find all objects around the powercell on collision
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider nearbyObject in colliders) {
                // make boxes fly away with an explosive force.
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if(rb != null) {
                    rb.isKinematic = false;
                    rb.AddExplosionForce(force, transform.position, radius);
                }
            }
            Destroy(gameObject);
        }
    }

    void OnDestroy() {
        Instantiate(explode, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
