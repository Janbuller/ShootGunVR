using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodEnemyController : MonoBehaviour, IShootable
{
    [SerializeField]
    private GameObject BadVersion;

    public PlayerController PlayerCtrl;

    public void GetShot(int Damage)
    {
        var Spawned = Instantiate(BadVersion, gameObject.transform.position, gameObject.transform.rotation);
        Spawned.GetComponent<EnemyController>().PlayerCtrl = PlayerCtrl;
        Destroy(gameObject);
    }
}
