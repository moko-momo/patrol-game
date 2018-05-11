using UnityEngine;
using System.Collections;


public class MoveCamera : MonoBehaviour {

	public int cameraMoveSpeed = 10;


	float originalPositionX;
	float originalPositionY;


	void Start()
	{
		originalPositionX = this.transform.position.x;
		originalPositionY = this.transform.position.y;

	}


	void OnGUI()
	{
		if (Event.current.type == EventType.MouseDrag) {
			float x;
			float y;
			x = Input.GetAxis ("Mouse X");
			y = Input.GetAxis ("Mouse Y");
			transform.Translate (new Vector3 (-x, 0, 0) * Time.deltaTime * cameraMoveSpeed);
			transform.Translate (new Vector3 (0, -y, 0) * Time.deltaTime * cameraMoveSpeed);
		}
	}
	
	void Update()  
	{  
		 
		if( Input.GetAxis("Mouse ScrollWheel") != 0 )
		{
			this.gameObject.transform.Translate(new Vector3(0,0,Input.GetAxis("Mouse ScrollWheel")*Time.deltaTime*500));
		}


	}  
}  

