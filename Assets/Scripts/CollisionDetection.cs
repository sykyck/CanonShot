using UnityEngine;
using System.Collections;

public class CollisionDetection : MonoBehaviour {
	public GameObject Strikes;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	 
	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		if ((collision.gameObject.name == "CanonBall") || (collision.gameObject.name == "CoinPrefab"))
		{
			collision.gameObject.GetComponent<Rigidbody2D>().Sleep ();
			collision.gameObject.SetActive (false);
			collision.gameObject.transform.position = CanonRotatiion.Canon.transform.position;
			GameManager.strikes = GameManager.strikes + 1;
			for (int i = 0; i < Strikes.transform.childCount; i++)
			{
				if (!Strikes.transform.GetChild (i).gameObject.activeInHierarchy)
				{
					Strikes.transform.GetChild (i).gameObject.SetActive (true);
					break;
				}
				if (i == Strikes.transform.childCount - 1)
				{
					for (int j = 0; j < Strikes.transform.childCount; j++)
					{
						Strikes.transform.GetChild (j).gameObject.SetActive (false);
					}
				}
			}
		}
	}
}
