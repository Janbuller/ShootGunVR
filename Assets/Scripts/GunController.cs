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

	public void Fire(InputAction.CallbackContext context)
	{
        if (context.performed)
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(MuzzleTrans.position, MuzzleTrans.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                var Obj = hit.transform.gameObject;
                IShootable ShootableComponent;
                if (Obj.TryGetComponent<IShootable>(out ShootableComponent))
                {
                    ShootableComponent.GetShot();
                }

            }
        }
	}
}
