﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gider : Champion
{
    float closestDistance = 100f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ShootIE(0));
        }

        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        closestTarget = GetClosestTargetInList(targets);
        if (closestTarget != null)
        {
            FlipBaseOnTargetPos(closestTarget.transform.position);
            closestDistance = Vector2.Distance(transform.position, closestTarget.transform.position);
        }
        if (Time.time >= nextAttackTime)
        { 
            if (closestTarget != null && closestDistance <= championData.range && closestTarget)
            {
                StartCoroutine(ShootIE(0));
                nextAttackTime = Time.time + cooldownTime;
                if (playerChampions.barrage_m && Utility.RandomBool(20))
                {
                    StartCoroutine(ShootIE(0.5f));
                }
            }
        }
    }
}
