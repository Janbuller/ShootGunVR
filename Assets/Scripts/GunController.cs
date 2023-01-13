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
    private int[] Ammo = new int[3];

    public double[] ShootTime = new double[3];
    public double[] ReloadTime = new double[3];

    public double ShootTimer;

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
        int CurWeaponIdx = (int)CurrentWeapon;
        Ammo[CurWeaponIdx] = FullAmmo[(int)CurrentWeapon];
        GunAudio.Play(CurWeaponIdx + "-reload");

        ShootTimer = ReloadTime[CurWeaponIdx];
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

        if(ShootTimer > 0)
            return;

        MuzzleFlash.Play();
        GunAudio.Play((int)CurrentWeapon + "-fire");
        ShootTimer = ShootTime[(int)CurrentWeapon];

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(MuzzleTrans.position, MuzzleTrans.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            var Obj = hit.transform.gameObject;
            IShootable ShootableComponent;
            if (Obj.TryGetComponent<IShootable>(out ShootableComponent))
            {
                // If CurrentWeapon is a handgun, deal 1 damage. Else deal 2.
                int Damage = CurrentWeapon == Weapons.Handgun ? 1 : 2;
                ShootableComponent.GetShot(Damage);
            }
        }

        Ammo[(int)CurrentWeapon] -= 1;
    }

    void Update()
    {
        ShootTimer -= Time.deltaTime;

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
