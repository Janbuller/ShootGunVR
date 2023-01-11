using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GunController : MonoBehaviour
{

    public GameObject Muzzle;
    private Transform MuzzleTrans;
    public ParticleSystem MuzzleFlash;

    public Weapons CurrentWeapon;

    [Header("0 = Handgun\n1 = Rifle\n2 = Shotgun")]
    public GameObject[] Guns = new GameObject[3];

    public int[] FullAmmo = new int[3];
    public int[] Ammo = new int[3];

    private AudioController GunAudio;

    // Start is called before the first frame update
    void Start()
    {
        GunAudio = GetComponent<AudioController>();

        MuzzleTrans = Muzzle.GetComponent<Transform>();
        Ammo = (int[])FullAmmo.Clone();
    }

    public void Reload(ActivateEventArgs args)
    {
        Ammo[(int)CurrentWeapon] = FullAmmo[(int)CurrentWeapon];
        GunAudio.Play((int)CurrentWeapon + "-reload");
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        if (Ammo[(int)CurrentWeapon] <= 0)
            return;

        if(CurrentWeapon == Weapons.Handgun) {
            Guns[0].GetComponentsInChildren<Animator>()[0].SetTrigger("Fire");
        }

        MuzzleFlash.Play();

        GunAudio.Play((int)CurrentWeapon + "-fire");

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

        Ammo[(int)CurrentWeapon] -= 1;
    }

    void Update()
    {
        foreach (GameObject go in Guns)
        {
            if (go.activeSelf && go != Guns[(int)CurrentWeapon])
                go.SetActive(false);
        }

        Guns[(int)CurrentWeapon].SetActive(true);

        foreach (Transform child in Guns[(int)CurrentWeapon].transform)
        {
            if (child.name == "Muzzle")
            {
                Muzzle = child.gameObject;
                MuzzleTrans = Muzzle.GetComponent<Transform>();
                MuzzleFlash = Muzzle.GetComponentsInChildren<ParticleSystem>()[0];
            }
        }
    }
}
