using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IShootable
{

    [SerializeField]
    private PlayerController PlayerCtrl;

    [Header("The chance for a weapon to drop")]
    [SerializeField]
    private double WeaponDropChance = 0.1;

    [Header("0 = Handgun\n1 = Rifle\n2 = Shotgun")]
    [SerializeField]
    private GameObject[] WeaponParts = new GameObject[3];

    public bool Die = false;

    public void Update() {

        if(Die) {
            Die = false;
            GetShot();
        }
    }

    public void GetShot()
    {
        var rand = new System.Random();

        if(rand.NextDouble() < WeaponDropChance) {
            DropWeapon();
        }

        Destroy(gameObject);
    }

    public void DropWeapon() {
        var NextDrop = PlayerCtrl.GetNextDrop();

        Instantiate(WeaponParts[(int)NextDrop], transform.position, transform.rotation).GetComponent<DroppedWeapon>().PlayerCtrl = PlayerCtrl;
    }
}
