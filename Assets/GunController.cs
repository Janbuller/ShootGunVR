using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{

	public GameObject Muzzle;
	private Transform MuzzleTrans;

	// Start is called before the first frame update
    	void Start()
    	{
		MuzzleTrans = Muzzle.GetComponent<Transform>();
    	}

    	// Update is called once per frame
    	void Update()
    	{
    	    
    	}

	public void Fire(InputAction.CallbackContext context)
	{

		Debug.Log("Fire!");
	}
}
