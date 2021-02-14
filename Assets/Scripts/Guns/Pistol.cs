using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : RaycastGun
{
    private IEnumerator coroutine;
    public Pistol(StatePlayerController player, Transform firePoint, GameObject hitEffect, AudioClip fireSound, LineRenderer renderer, RuntimeAnimatorController animatorController) {
        this.size = Size.LIGHT;
        this.shotCost = 1;
        this.damage = 10f;
        this.fireRate = 1.0f;
        this.firePt = firePoint;
        this.bulletTrail = renderer;
        this.hitEffect = hitEffect;
        this.fireSound = fireSound;
        this.player = player;
        this.animController = animatorController;
    }

    public override void Shoot()
    {
        Vector3 temp = new Vector3();
        if (player.playerManager.GetStateInput().spriteRenderer.flipX) {
            temp = new Vector3(firePt.position.x - 5f, firePt.position.y, firePt.position.z);
        } else {
            temp = firePt.right;
        }
        temp.y += Random.Range(-0.1f,0.1f);

        RaycastHit2D hitInfo = Physics2D.Raycast(firePt.position, temp);
        Physics2D.IgnoreLayerCollision(8, Physics2D.IgnoreRaycastLayer);
        Debug.Log(hitInfo.collider.gameObject.layer);
        if (hitInfo)
        {
            // Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            // if (enemy != null)
            // {
            //     enemy.TakeDamage((int)(damage));
            // }
            bulletTrail.SetPosition(0,firePt.position);
            bulletTrail.SetPosition(1,hitInfo.point);

            GameObject new_hit = (GameObject)Instantiate(hitEffect, hitInfo.point, Quaternion.identity);
            Destroy(new_hit, 0.267f);

        } else 
        {
            bulletTrail.SetPosition(0,firePt.position);
            bulletTrail.SetPosition(1,firePt.position + temp*100);
        }
        player.playSound(fireSound);
        player.showBulletTrail(bulletTrail);
    }
}
