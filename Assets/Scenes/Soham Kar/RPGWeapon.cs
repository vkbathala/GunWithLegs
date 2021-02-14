using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGWeapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject rocketPrefab;
    public float shotDelay = 2.0f;
    bool shotFired = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !shotFired) {
            shotFired = true;
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        Instantiate(rocketPrefab, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(shotDelay);
        shotFired = false;
    }
}
