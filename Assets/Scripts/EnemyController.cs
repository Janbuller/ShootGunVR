using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IShootable
{

    [SerializeField]
    public PlayerController PlayerCtrl;

    [Header("The chance for a weapon to drop")]
    [SerializeField]
    private double WeaponDropChance = 0.1;

    [Header("0 = Handgun\n1 = Rifle\n2 = Shotgun")]
    [SerializeField]
    private GameObject[] WeaponParts = new GameObject[3];

    public float Speed = 2.5f;

    public bool Die = false;

    private int Health = 2;

    public void Update() {

        if(Die) {
            Die = false;
            GetShot(2);
        }

        var PlayerPos = PlayerCtrl.transform.position + PlayerCtrl.gameObject.GetComponent<CharacterController>().center;
        var Direction = PlayerPos - transform.position;
        Direction = Direction.normalized;

        transform.position += new Vector3(Direction.x, 0, Direction.z) * Speed * Time.deltaTime;
        transform.LookAt(new Vector3(PlayerPos.x, transform.position.y, PlayerPos.z));
    }

    public void GetShot(int Damage)
    {
        var rand = new System.Random();

        if(rand.NextDouble() < WeaponDropChance) {
            DropWeapon();
        }

        Health -= Damage;

        if(Health <= 0)
            Destroy(gameObject);
    }

    public void DropWeapon() {
        var NextDrop = PlayerCtrl.GetNextDrop();

        Instantiate(WeaponParts[(int)NextDrop], transform.position, transform.rotation).GetComponent<DroppedWeapon>().PlayerCtrl = PlayerCtrl;
    }
}
