using UnityEngine;
using System.Collections;

public class CheckCollision : MonoBehaviour 
{
	Vector3 VortexCollidedPos;
	public GameObject CoinParticleSystem;
	System.Collections.Generic.List<GameObject> CollidedObj;
	public static bool objcollided;
	public static int noofcollisions;
	Vector3 initPos;
	// Use this for initialization
	void Start () 
	{
	  noofcollisions = 0;
	  CollidedObj = new System.Collections.Generic.List<GameObject> ();
	  initPos = GameManager.Canon_GameObj.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
	void OnCollisionEnter2D (Collision2D collision)
	{
		if ((collision.gameObject.name == "CanonBall") || (collision.gameObject.name == "CoinPrefab"))
		{
			noofcollisions += 1;
			collision.gameObject.GetComponent<Rigidbody2D>().Sleep ();
			collision.gameObject.GetComponent<PolygonCollider2D> ().enabled = false;
			CollidedObj.Add (collision.gameObject);
			objcollided = true;
			for(int i=1;i<=5;i++)
			{
				Invoke ("scaleDownCollidedObject", (i*0.1f));
			}
			Invoke ("makeCollidedObjectDisappear", 0.7f);
			if (collision.gameObject.name == "CanonBall")
			{
				GameManager.score = GameManager.score + 1;
				CanonBall.currentBallsNumInScene = CanonBall.currentBallsNumInScene - 1;
				CanonBall.currentTotalNum = CanonBall.currentTotalNum - 1;
			}
			if (collision.gameObject.name == "CoinPrefab") 
			{
				CoinParticleSystem.SetActive (false);
				GameManager.coinsCollected = GameManager.coinsCollected + 1;
				CanonBall.currentCoinsNumInScene = CanonBall.currentCoinsNumInScene - 1;
				CanonBall.currentTotalNum = CanonBall.currentTotalNum - 1;
			}
		}
	}
	void scaleDownCollidedObject()
	{
		if ((CollidedObj[0].name == "CanonBall")&&(CollidedObj[0].transform.localScale.x>0)&&(CollidedObj[0].transform.localScale.y>0)&&(CollidedObj[0].transform.localScale.z>0)) {
			CollidedObj[0].transform.localScale -= new Vector3 (0.01f, 0.01f, 0.01f);
		}
		if ((CollidedObj[0].name == "CoinPrefab")&&(CollidedObj[0].transform.localScale.x>0)&&(CollidedObj[0].transform.localScale.y>0)&&(CollidedObj[0].transform.localScale.z>0)) {
			CollidedObj[0].transform.localScale -= new Vector3 (0.2f, 0.2f, 0.2f);
		}
		CollidedObj[0].transform.position = Vector3.MoveTowards (CollidedObj[0].transform.position, gameObject.transform.position, (Vector3.Distance (CollidedObj[0].transform.position, gameObject.transform.position)/5f));
	}
	void makeCollidedObjectDisappear()
	{
		objcollided = false;
		CollidedObj [0].transform.position = initPos;
		CollidedObj[0].SetActive (false);
		CollidedObj[0].GetComponent<PolygonCollider2D> ().enabled = true;
		if (CollidedObj[0].name == "CanonBall") {
			CollidedObj[0].transform.localScale = new Vector3 (0.05f, 0.05f, 0.05f);
		}
		if (CollidedObj[0].name == "CoinPrefab") {
			CollidedObj[0].transform.localScale = new Vector3 (1f, 1f, 1f);
		}
		CollidedObj.RemoveAt (0);
	}
}
