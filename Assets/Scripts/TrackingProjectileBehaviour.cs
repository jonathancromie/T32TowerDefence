using UnityEngine;
using System.Collections;

public class TrackingProjectileBehaviour : MonoBehaviour {

    public float speed = 20;//projectile speed
    public float myRange = 10;//projectiles range 
	public float damge = 10;
	public float radius = 10;
	public Transform explosion;
    private float myDist;//distance the obecjt has traviled used in range calculations 
	private Quaternion stepDirection;
	private Transform lockedTarget;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //move object forward by speed
		trackingTargetVectorCalculation();
		transform.rotation = stepDirection;
        transform.Translate(Vector3.forward *Time.deltaTime * speed);
        //distance traviled
        myDist += Time.deltaTime * speed;
        //if distance traviled is equal or the same as range distroy projectile 
        if(myDist >= myRange){
            Destroy(gameObject);
        }
         

        
    }

    //this is called when the rigid body collieds with anything 
    void OnCollisionEnter(Collision impact)
    {
        if (impact.transform.tag == "Enemy")//if the collision was with enemy 
        {
            //the prjectile is destroyed 
			AreaDamageEnemies();
			GameObject expl = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
            Destroy(gameObject);

        }
    }

	void trackingTargetVectorCalculation(){
		Vector3 targetPos = lockedTarget.position;
		
		Vector3 relativePos = targetPos - transform.position;
		stepDirection = Quaternion.LookRotation(relativePos);
		}

	void trackingTargetVector(Transform target){
		lockedTarget = target;

	}



	void AreaDamageEnemies()
	{
		Collider[] objectsInRange = Physics.OverlapSphere(transform.position, radius);
		foreach (Collider col in objectsInRange)
		{
			Collider enemy = col;
			if (enemy != null && enemy.tag == "Enemy" )
			{
				// linear falloff of effect
				float proximity = (transform.position - enemy.transform.position).magnitude;
				float effect = 1 - (proximity / radius);

				enemy.BroadcastMessage("takeDamage", (damge * effect));
			}
		}
	}
}

