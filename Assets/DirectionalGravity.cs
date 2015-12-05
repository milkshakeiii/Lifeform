using UnityEngine;
using System.Collections;

public class DirectionalGravity : MonoBehaviour
{

    public Vector2 gravity;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetComponent<Rigidbody2D>().AddForce(gravity * Time.deltaTime);
	}
}
