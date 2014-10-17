using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

    public float health;//enemys health 
	public Transform killed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)//if health is 0 or below 
        {
			GameObject expl = Instantiate(killed, transform.position, transform.rotation) as GameObject;
            Destroy(gameObject);//destroy this enemy
        }
	
	}

    public void takeDamage(float damage)//method used by other scripts to damage enemy 
    {
        health -= damage;
    }
}
