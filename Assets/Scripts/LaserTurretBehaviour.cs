using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LaserTurretBehaviour : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
    void Awake()
    {

    }
    //public variables 
    public GameObject myProjecttile;
	public double rateOfFire = 4;
    public GameObject fireEffect;
    public float damage;
	public double fireDuration = 1.25;

    //private varibales
    private Quaternion fireDirection;
    private Transform myTarget;
    private double nextFireTime;
	private double stopFireTime;
	private double nextDamageTime;
	private List<Transform> targetQueue = new List<Transform>();
    private List<int> targetID = new List<int>();
	private LineRenderer laser;
	

	// Update is called once per frame
	void Update () {
		updateTargets();
        if (!(myTarget == null)) {
			print ("world");
						if (Time.time >= nextFireTime) {
								fireLaser ();
						}
						if (Time.time >= stopFireTime) {
								print ("working kinda");
								stopFiringLaser ();
						} else {
								
								updateLaser();
								
						}
				} 
		else {
			stopFiringLaser();
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
	


	private void fireLaser(){
		audio.Play ();
		laser = gameObject.GetComponent<LineRenderer> ();

		laser.enabled = true;
		nextFireTime = Time.time + rateOfFire + fireDuration;
		stopFireTime = Time.time + fireDuration;



		laser.SetPosition (0, gameObject.transform.position);
		laser.SetPosition (1, myTarget.position);

		}

	private void stopFiringLaser(){
		gameObject.GetComponent<LineRenderer> ().enabled = false;
		}
	private void updateLaser(){
		if (nextDamageTime <= Time.time || nextDamageTime == null) {
			myTarget.gameObject.BroadcastMessage ("takeDamage", damage);
			nextDamageTime = Time.time + 1;
				}
		laser.SetPosition (0, gameObject.transform.position);
		laser.SetPosition (1, myTarget.position);
		}
}
