using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public Transform target;
	public GameObject enemy;
	
	public float spawnTime = 3f;
	float spawnTimeLeft = .1f;
	
	// Update is called once per frame
	void Update () {
		if(spawnTimeLeft <= 0) {
			GameObject go = (GameObject)Instantiate(enemy, transform.position, transform.rotation);
			go.GetComponent<AstarAI>().target = target;
			spawnTimeLeft = spawnTime;
		}
		else {
			spawnTimeLeft -= Time.deltaTime;
		}
	}
}
