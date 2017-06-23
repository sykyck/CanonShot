using UnityEngine;
using System.Collections;

public class CanonRotatiion : MonoBehaviour {
	public float speed;
	public float rotationSpeedFactor;
	public static GameObject Canon;
	float angleRotated;
	int timesCanonCanShot;
	public static bool canCanonShoot;

	// Use this for initialization
	void Start ()
	{
		rotationSpeedFactor = 50;
		timesCanonCanShot = 1;
		canCanonShoot = true;
		Canon = gameObject;
		InvokeRepeating ("IncreaseRotationSpeedFactor", 0f, 15f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		speed = Time.deltaTime * rotationSpeedFactor;
		transform.Rotate(Vector3.forward * speed);
		angleRotated += 1 * speed;
		if (((int)angleRotated / 180) > (timesCanonCanShot-1)) 
		{
			Debug.Log ("inside if");
			canCanonShoot = true;
			timesCanonCanShot = ((int)angleRotated / 180)+1;
		}
	}
	public void DecreaseCannonRotationSpeed()
	{
		if (GameManager.coinsCollected > 30) {
			GameManager.coinsCollected = GameManager.coinsCollected - 30;
			rotationSpeedFactor = rotationSpeedFactor - (rotationSpeedFactor / 10);
		}
	}
	void IncreaseRotationSpeedFactor()
	{
		rotationSpeedFactor += 30;
	}
}
