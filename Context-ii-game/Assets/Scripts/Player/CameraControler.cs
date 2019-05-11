using UnityEngine;
using System.Collections;

public class CameraControler : MonoBehaviour 
{
	public GameObject followTarget;
	private Vector3 targetPos;
	public float moveSpeed;



	// Update is called once per frame
	void Update () {
		targetPos = new Vector3 (followTarget.transform.position.x, followTarget.transform.position.y + 15, followTarget.transform.position.z -10);
		//lerp = (where you are, where you want to be,speed )
		transform.position = Vector3.Lerp (transform.position, targetPos, moveSpeed * Time.deltaTime);
	}
}
