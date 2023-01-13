using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodEnemyController : MonoBehaviour, IShootable
{
    [SerializeField]
    private GameObject BadVersion;

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
        Instantiate(BadVersion, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }
}
