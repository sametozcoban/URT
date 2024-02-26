using System;
using System.Collections;
using System.Collections.Generic;
using SnowBoarder.Characters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextControl : MonoBehaviour
{
    [SerializeField] public TMP_Text collectedGoldTxt;

    private LevelControl goldControl;

    private void Start()
    {
        goldControl = FindObjectOfType<LevelControl>();
    }
    void Update() 
    {
        collectedGoldTxt.text = goldControl.collectedGoldInLevel.ToString(); // Kaç Altın Topladık bölümde.
    }
}
