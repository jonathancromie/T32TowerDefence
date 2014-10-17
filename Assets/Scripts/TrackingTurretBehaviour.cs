using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TrackingTurretBehaviour : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
    void Awake()
    {

    }
    //public variables 
    public GameObject myProjecttile;
    public double rateOfFire = 1.25;
    public GameObject fireEffect;
    public int damage;

    //private varibales
    private Quaternion fireDirection;
    private Transform myTarget;
    private double nextFireTime;
    private List<Transform> targetQueue = new List<Transform>();
    private List<int> targetID = new List<int>();


	
	// Update is called once per frame
	void Update () {
		updateTargets ();
        if(!(myTarget == null)){
            if (Time.time >= nextFireTime)
            {
                CalculateFirePosition(myTarget.position);
                fireProjectile();

            }
        }
	}



	private void updateTargets(){
		Collider[] objectsInRange = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius);
		targetQueue.Clear ();
		foreach (Collider col in objectsInRange) {
			if(col.tag == "Enemy" && col != null){
				targetQueue.Add(col.transform);
			}
		}
		if (myTarget != null && targetQueue.Count > 0) {
			if(targetQueue.Contains(myTarget)){
				return;
			}
			else{
				myTarget = targetQueue[0];
			}
			
		}
		
		else if(targetQueue.Count > 0){
			myTarget = targetQueue[0];
		}
		else{
			myTarget = null;
		}
	}

    private void CalculateFirePosition(Vector3 target)
    {
        //calculate fire diretion from turret to the enemy
        Vector3 relativePos = target - transform.position;
        fireDirection = Quaternion.LookRotation(relativePos);
    }

    private void fireProjectile()
    {
        audio.Play();
        //set next fire time 
        nextFireTime = Time.time + rateOfFire;
        //create projectile with z axis alinged with taget 
        GameObject projectile = Instantiate(myProjecttile, gameObject.transform.position, fireDirection) as GameObject;
        //call to damage method in enemy script
		projectile.gameObject.BroadcastMessage ("trackingTargetVector", myTarget);

    }
}
