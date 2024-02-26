using System;
using SnowBoarder.Characters;
using Unity.VisualScripting.FullSerializer.Internal.Converters;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace SnowBoarder.Characters
{
    public class LevelControl : MonoBehaviour
    {
        private bool hasColletable;
        public int collectedGoldInLevel;
        public AudioSource _goldPickUp = null;
        public Progression goldValue;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Collectable")
            { 
                goldValue.totalGoldAmount += 1;
                hasColletable = true;
                collectedGoldInLevel += 1;
                col.gameObject.SetActive(false);
                if (_goldPickUp!=null)
                {
                    _goldPickUp.Play();
                }
            }
        } // Karakter Coin' e değdiğinde,
                                                            // Gold Amount yükseltmek için kullandığımız trigger fonksiyonu
    }
}