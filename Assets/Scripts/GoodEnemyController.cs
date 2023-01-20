using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodEnemyController : MonoBehaviour, IShootable
{
    [SerializeField]
    private GameObject BadVersion;

    public PlayerController PlayerCtrl;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetShot(int Damage)
    {
        var Spawned = Instantiate(BadVersion, gameObject.transform.position, gameObject.transform.rotation);
            Spawned.GetComponent<EnemyController>().PlayerCtrl = PlayerCtrl;
        Destroy(gameObject);
    }
}
