using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Dictionary<Weapons, int> UnlockedWeapons = new Dictionary<Weapons, int>() {
        {Weapons.Handgun, 0},
        {Weapons.Rifle, 0},
        {Weapons.Shotgun, 0},
    };

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Weapons GetNextDrop() {
        var rand = new System.Random();
        var WeaponCount = Weapons.GetNames(typeof(Weapons)).Length;

        int WeaponIndex = rand.Next(WeaponCount);

        return (Weapons)WeaponIndex;
    }

    public void PickupWeapon(Weapons Weapon) {
        UnlockedWeapons[Weapon]++;
    }
}
