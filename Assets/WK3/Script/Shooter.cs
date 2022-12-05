using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject fire; // link to fireball prefab
    public GameObject ice; // link to iceball prefab
    GameObject spell;
    public int no_cell = 0; // number of 
    public AudioClip throwSound; //throw sound
    public float throwSpeed= 20;//throw speed
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("FirstPerson-AIO");
    }
    
    // Update is called once per frame
    void Update () {
        // if the player has the right amount of mana to cast a spell
        if (player.GetComponent<FirstPersonAIO>().getMana() >= 20) 
        {
            if (Input.GetButtonDown ("Fire1")) {
                player.GetComponent<FirstPersonAIO>().useSpell(20);
                castFire();
            } else if (Input.GetButtonDown ("Fire2")) {
                player.GetComponent<FirstPersonAIO>().useSpell(20);
                castIce();
            }   
        }
    }

    // Cast spell of users choice
    void castFire () {
        no_cell --; //reduce the cell
        //play throw sound
        AudioSource.PlayClipAtPoint(throwSound, transform.position);
        //instantaite the power cel as game object
        spell = Instantiate(fire, transform.position, transform.rotation) as GameObject; //ask physics engine to ignore collison between
        //power cell and our FPSControler
        Physics.IgnoreCollision(transform.root.GetComponent<Collider>(),
        spell.GetComponent<Collider>(), true);
        //give the powerCell a velocity so that it moves forward
        spell.GetComponent<Rigidbody>().velocity = transform.forward * throwSpeed;
    }

    // Cast spell of users choice
    void castIce () {
        no_cell --; //reduce the cell
        //play throw sound
        AudioSource.PlayClipAtPoint(throwSound, transform.position);
        //instantaite the power cel as game object
        spell = Instantiate(ice, transform.position, transform.rotation) as GameObject; //ask physics engine to ignore collison between
        //power cell and our FPSControler
        Physics.IgnoreCollision(transform.root.GetComponent<Collider>(),
        spell.GetComponent<Collider>(), true);
        //give the powerCell a velocity so that it moves forward
        spell.GetComponent<Rigidbody>().velocity = transform.forward * throwSpeed;
    }
}
