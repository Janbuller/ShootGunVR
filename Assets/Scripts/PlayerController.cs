using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Dictionary<Weapons, int> UnlockedWeapons = new Dictionary<Weapons, int>() {
        {Weapons.Handgun, 0},
        {Weapons.Rifle, 0},
        {Weapons.Shotgun, 0},
    };

    public Weapons GetNextDrop() {
        var rand = new System.Random();
        var WeaponCount = Weapons.GetNames(typeof(Weapons)).Length;

        int WeaponIndex = rand.Next(WeaponCount);

        return (Weapons)WeaponIndex;
    }

    public void PickupWeapon(Weapons Weapon) {
        UnlockedWeapons[Weapon]++;
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        SceneManager.LoadScene("DeathScene");
    }
}
