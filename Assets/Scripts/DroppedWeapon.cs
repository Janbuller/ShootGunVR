using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class DroppedWeapon : MonoBehaviour
{
    [System.NonSerialized]
    public PlayerController PlayerCtrl;

    [Header("0 = Handgun\n1 = Rifle\n2 = Shotgun")]
    public int WeaponType;

    private Weapons Type;

    public void Start() {
        Type = (Weapons)WeaponType;
    }

    public void OnHover(HoverEnterEventArgs args) {
        PlayerCtrl.PickupWeapon(Type);
    }
}
