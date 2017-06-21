using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour
{
	public bool detectCanonBalls;
	public static bool IsTouchValid;

	void Start ()
	{
		detectCanonBalls = false;
		IsTouchValid = false;
	}

	void Update ()
	{
		if (Input.GetMouseButtonDown (0))
		{
			CanonBall.CheckIfTouchIsValid (Input.mousePosition.x, Input.mousePosition.y);
			if (IsTouchValid) 
			{
				gameObject.GetComponent<PolygonCollider2D> ().enabled = true;
				gameObject.transform.GetChild(0).gameObject.SetActive(true);
				gameObject.transform.position = new Vector3 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y, -2f);
				detectCanonBalls = true;
			}
		}
		if (Input.GetMouseButtonUp (0))
		{
			gameObject.GetComponent<PolygonCollider2D> ().enabled = false;
			gameObject.transform.GetChild(0).gameObject.SetActive(false);
			detectCanonBalls = false;
			IsTouchValid = false;
		}
		if (Input.touchCount > 0)
		{
			CanonBall.CheckIfTouchIsValid (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y);
			if (IsTouchValid) 
			{
				switch (Input.GetTouch (0).phase) 
				{
				    case TouchPhase.Began:
					    gameObject.GetComponent<PolygonCollider2D> ().enabled = true;
					    gameObject.transform.GetChild(0).gameObject.SetActive(true);
					    gameObject.transform.position = new Vector3 (Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position).x, Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position).y, -2f);
						detectCanonBalls = true;
						break;
				    case TouchPhase.Moved:
						break;
					case TouchPhase.Ended:
						gameObject.GetComponent<PolygonCollider2D> ().enabled = false;
					    gameObject.transform.GetChild(0).gameObject.SetActive(false);
						detectCanonBalls = false;
						IsTouchValid = false;
						break;
				}
			}
		}
	
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		if ((detectCanonBalls) && ((collision.gameObject.name == "CanonBall") || (collision.gameObject.name == "CoinPrefab"))) 
		{
			collision.gameObject.GetComponent<Rigidbody2D>().Sleep ();
			collision.gameObject.SetActive (false);
			collision.gameObject.transform.position = CanonRotatiion.Canon.transform.position;
			if (collision.gameObject.name == "CanonBall") {
				GameManager.score = GameManager.score + 1;
			}
			if (collision.gameObject.name == "CoinPrefab") {
				GameManager.coinsCollected = GameManager.coinsCollected + 1;
			}
		}
	}

	public void UseCoinsToIncreaseVortex()
	{
		if (GameManager.coinsCollected > 20)
		{
			GameManager.coinsCollected = GameManager.coinsCollected - 20;
			gameObject.transform.localScale = new Vector3 (gameObject.transform.localScale.x + (float)0.1, gameObject.transform.localScale.y + (float)0.1, gameObject.transform.localScale.z);
			gameObject.transform.GetChild (0).gameObject.GetComponent<ParticleSystem> ().startSize += 1;
		}
	}
		
}
