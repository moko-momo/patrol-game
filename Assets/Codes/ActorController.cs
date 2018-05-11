using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class ActorController : MonoBehaviour {

    private Animator ani;
    private AnimatorStateInfo currentBaseState;
    private Rigidbody rig;

    private Vector3 velocity;
   

    private float rotateSpeed = 15f;
    private float runSpeed = 4f;
  
    void Start () {
        ani = GetComponent<Animator>();
		rig = GetComponent<Rigidbody> ();

    }

   
    void FixedUpdate () {
		if (this.transform.localEulerAngles.x != 0 || this.transform.localEulerAngles.z != 0)
		{
			this.transform.localEulerAngles = new Vector3(0, this.transform.localEulerAngles.y, 0);
		}
		if (this.transform.position.y != 0)
		{
			this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
		} 
        
		if (!ani.GetBool ("isLive")) {
			return;
		}
        

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        ani.SetFloat("Speed", Mathf.Max(Mathf.Abs(x), Mathf.Abs(z)));
        
        ani.speed = 1 + ani.GetFloat("Speed") / 3;
       

        velocity = new Vector3(x, 0, z);

        if (x != 0 || z != 0)
        {
            Quaternion rotation = Quaternion.LookRotation(velocity);
            if (transform.rotation != rotation) transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime * rotateSpeed);
        }

        this.transform.position += velocity * Time.fixedDeltaTime * runSpeed;


    
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Area"))
        {
            Publish publish = Publisher.getInstance();
            int patrolType = other.gameObject.name[other.gameObject.name.Length - 1] - '0';
            publish.notify(ActorState.ENTER_AREA, patrolType, this.gameObject);
          
        }
    }
		
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Patrol") && ani.GetBool("isLive"))
        {	
			Debug.Log ("1");
            ani.SetBool("isLive", false);
            ani.SetTrigger("toDie");
            

            Publish publish = Publisher.getInstance();
            publish.notify(ActorState.DEATH, 0, null);
         
        }
    }
}
