using UnityEngine;
using System.Collections;

public class ProjectileBehaviour : MonoBehaviour {

    public float speed = 20;//projectile speed
    public float myRange = 10;//projectiles range 
    private float myDist;//distance the obecjt has traviled used in range calculations 


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //move object forward by speed
        transform.Translate(Vector3.forward *Time.deltaTime * speed);
        //distance traviled
        myDist += Time.deltaTime * speed;
        //if distance traviled is equal or the same as range distroy projectile 
        if(myDist >= myRange){
            Destroy(gameObject);
        }
         

        
    }

    //this is called when the rigid body collieds with anything 
    void OnTriggerEnter(Collider impact)
    {
        if (impact.transform.tag == "Enemy")//if the collision was with enemy 
        {
            //the prjectile is destroyed 
            Destroy(gameObject);
        }
    }
}
