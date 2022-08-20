﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CardHandler : MonoBehaviour
{
    public PlayerChampions pc;
    public TMP_Text _name;
    public TMP_Text _description;
    public TMP_Text _damage;
    public TMP_Text _defense;
    public TMP_Text _tier;

    public MoneyUI moneyUI;
    public UnitsUI unitsUI;
    public ClassesUI classesUI;

    public float maxUnits;

    Image[] images;
    [HideInInspector] public Vector2 originalScale;
    private void Start()
    {
        images = GetComponentsInChildren<Image>();
        originalScale = transform.localScale;
    }

    public void SelectChampion()
    {
        GameObject choosedChampion = Resources.Load("Champion Prefabs/" + _name.text) as GameObject;

        if (pc.GetChampionLevel(choosedChampion) == 3)
            return;

        if (moneyUI.startingMoney >= int.Parse(_tier.text))
        {
            // Fix max level 3
            if (!pc.CheckExistedChampion(choosedChampion))
            {
                if (pc.champions.Count < maxUnits)
                {
                    pc.AddChampion(choosedChampion);
                    moneyUI.startingMoney -= int.Parse(_tier.text);
                    moneyUI.isChanged = true;
                    gameObject.SetActive(false);
                }
            }
            else
            {
                moneyUI.startingMoney -= int.Parse(_tier.text);
                moneyUI.isChanged = true;
                gameObject.SetActive(false);
            }
        }
        unitsUI.SetChampionsToUnit();
        classesUI.SetClassToUnit();
    }

    public void SetCardData(ChampionData champion)
    {
        CardColorData cardColorData = champion.cardColor;

        // Load Card Color Data
        images[0].color = cardColorData.color; // Body's color
        images[1].color = cardColorData.championBoxColor; // Champion Box's color
        images[2].sprite = champion.sprite; // Champion's image
        LoadChampionImage(images[2]);
        images[3].color = cardColorData.nameBoxColor; // Name Box's color
        images[4].color = cardColorData.descriptionBoxColor; // Description Box's color

        // Class Circle
        images[5].gameObject.SetActive(false);
        images[7].gameObject.SetActive(false);

        // Class Image
        for (int i = 0; i < champion.classes.Count; i++)
        {
            images[2 * i + 5].gameObject.SetActive(true);
            images[2 * i + 6].sprite = Resources.Load<Sprite>("Sprites/" + champion.classes[i].ToString().ToLower());
        }

        // Load Champion Data
        _name.text = champion.name;
        _description.text = champion.description;
        _defense.text = champion.defense.ToString();
        _damage.text = champion.damage.ToString();
        _tier.text = champion.tier.ToString();
    }

    public void DoneSelected()
    {   
        GameManager.Instance.UpdateGameState(GameState.GameStart);
    }

    private void LoadChampionImage(Image image)
    {
        image.SetNativeSize();
        RectTransform imageRT = image.GetComponent<RectTransform>();
        Vector2 originalSize = imageRT.sizeDelta;
        imageRT.sizeDelta = new Vector2(100 * originalSize.x / originalSize.y, 100);
    }

    public void ScaleBiggerCard()
    {
        transform.DOScale(originalScale * 1.25f, 0.2f).SetEase(Ease.OutCubic);
    }
    public void ScaleSmallerCard()
    {
        transform.DOScale(originalScale, 0.2f).SetEase(Ease.OutCubic);
    }
}
