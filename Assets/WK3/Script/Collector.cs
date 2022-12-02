using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    GameObject player;
    public AudioClip pickUpSound;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player Camera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Pick up the cells
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Pick Up")) {
            player.GetComponent<Shooter>().no_cell++;
            //deactivate the other object
            AudioSource.PlayClipAtPoint(pickUpSound, transform.position);
            other.gameObject.SetActive(false);
        }
    }
}
