﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Champion : LivingEntity, IFollowable
{
    public float speed;
    public float spaceBetween = 0.5f;

    private void Update()
    {
        timeCount += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        Vector3 screenPosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        Vector3 targetPos = new Vector3(worldPosition.x, worldPosition.y, 0);

        FlipBaseOnTargetPos(targetPos);

        if (Vector3.Distance(targetPos, transform.position) > spaceBetween)
        {
            FollowTarget(targetPos);
        }
    }

    void OnNewWave(int waveNumber)
    {
        health = startingHealth;
    }

    public void FollowTarget(Vector3 targetPos)
    {
        transform.position += (targetPos - transform.position).normalized * speed * Time.fixedDeltaTime;
    }

    private void Shoot()
    {
        GameObject i_projectile = Instantiate(projectile, transform.position, Quaternion.identity);

        if (i_projectile != null) { 
            i_projectile.GetComponent<Projectile>().damage = damage;
        }
    }
}
