using UnityEngine;
using System.Collections;

public class MoveFire : MonoBehaviour {

	Vector3 forwardVec;
	Vector3 attakPos;


	// Use this for initialization

	private Vector3 attakPosition
	{
		get {
			Vector3 ran = Random.insideUnitCircle.normalized;
			//Debug.Log ();
			return new Vector3(ran.x, ran.y, 0.0f) * (0.5f * MyController.gc.AttackWorld.transform.localScale.x) + MyController.gc.AttackWorld.transform.position;
		}
	}

	void OnEnable() {
		forwardVec = this.transform.forward;
		//attakPos = MyController.gc.AttackWorld.transform.position;
		attakPos = attakPosition;
	
	}

	// Update is called once per frame
	void Update () {
		attakPos = attakPosition;

		transform.LookAt (MyController.gc.AttackWorld.transform);
		transform.position = Vector3.Slerp(transform.position, attakPos, 0.3f * Time.deltaTime);
		transform.Rotate(Vector3.Slerp(transform.position, attakPos, 0.3f * Time.deltaTime));

	
	}
	/*
	void OnTriggerEnter(Collider other)
	{
		
	}

	void OnColissionEnter(Collision collison)
	{
	}
*/

}
