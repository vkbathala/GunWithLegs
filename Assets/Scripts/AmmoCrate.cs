using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCrate : MonoBehaviour
{
    public GameObject drop;
    public int durability = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (durability <= 0)
        {
            BreakCrate();
        }
    }

    void BreakCrate()
    {
        Destroy(gameObject);
        Instantiate(drop, gameObject.transform.position, drop.transform.rotation);
    }
    
    public void DamageCrate(int dmg)
    {
        durability -= dmg;
    }

    
}
