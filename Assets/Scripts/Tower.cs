using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

	void OnTriggerEnter(Collider c) {
		Destroy(c.gameObject);
	}
}
