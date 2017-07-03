using UnityEngine;
using System.Collections;

public class ChangeCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (GameManager.strikes == 0) {
			gameObject.transform.GetChild (1).gameObject.SetActive (false);
			gameObject.transform.GetChild (2).gameObject.SetActive (false);
			gameObject.transform.GetChild (0).gameObject.SetActive (true);
		}
		if (GameManager.strikes == 1) {
			gameObject.transform.GetChild (0).gameObject.SetActive (false);
			gameObject.transform.GetChild (2).gameObject.SetActive (false);
			gameObject.transform.GetChild (1).gameObject.SetActive (true);
		}
		if (GameManager.strikes == 2) {
			gameObject.transform.GetChild (1).gameObject.SetActive (false);
			gameObject.transform.GetChild (0).gameObject.SetActive (false);
			gameObject.transform.GetChild (2).gameObject.SetActive (true);
		}
		if (GameManager.strikes == 3) {
			
		}

		

	
	}
}
