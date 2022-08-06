﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChampions : MonoBehaviour
{
    [HideInInspector] public GameObject choosedChampion;
    [HideInInspector] public List<GameObject> champions = new List<GameObject>();

    public int championsCount;

    GameObject blobShadow;
    Vector3 offset = new Vector3(-0.75f, 0f, 0f);
    Vector3 shadowOffset = new Vector3(0, -0.6f, 0f);

    private void Start()
    {
        blobShadow = Resources.Load("Prefabs/Shadow") as GameObject; 
    }

    public void Update()
    {
        
    }

    public void AddChampion(GameObject choosedChampion)
    {
        GameObject insChampion;
        insChampion = Instantiate(choosedChampion, transform);
        insChampion.transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
        champions.Add(insChampion);
        UpdateChampions(champions.Count);
    }

    public bool CheckExistedChampion(GameObject choosedChampion)
    {
        string choosedChampionName = choosedChampion.GetComponent<Champion>().championData.name;
        foreach (GameObject champion in champions)
        {
            if (choosedChampionName == champion.GetComponent<Champion>().championData.name)
            {
                UpgradeChampion(champion);
                return true;
            }
        }
        return false;
    }

    public void UpgradeChampion(GameObject champion)
    {
        champion.GetComponent<LivingEntity>().currentLevel++;
        champion.GetComponent<LivingEntity>().SetCharacteristics(5f, 5f, 5f, 0.1f);
    }

    public void RemoveChampion(GameObject choosedChampion)
    {
        champions.Remove(choosedChampion);
        UpdateChampions(champions.Count);
    }

    public void UpdateChampions(int count)
    {
        championsCount = count;
    }
    public void AddBlobShadowForChampion(GameObject champion)
    {
        Instantiate(blobShadow, champion.transform.position + shadowOffset, Quaternion.identity, champion.transform);
    }
}
