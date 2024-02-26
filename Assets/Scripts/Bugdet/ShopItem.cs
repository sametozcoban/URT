using TMPro;
using UnityEngine;
using DG.Tweening;

namespace Bugdet
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private string itemName;
        [SerializeField] public TMP_Text _priceText;
        [SerializeField] private AudioSource _buyFeatureEffect;
        [SerializeField] private RectTransform _totalValueTextRect;
        public void BuyItem()
        {
            if (GameManager.Instance.SpendGold(int.Parse(_priceText.text)))
            {
                UpdateTotalUpgradeValue();
            }
            else
            {
                Debug.LogWarning("Paran yok Dayıcım" + itemName);
            }
            _buyFeatureEffect.Play();
        } // Item Alındığında Çalışacak Fonksiyon
        private void UpdateTotalUpgradeValue()
        {
            if (int.Parse(_priceText.text) == 25)
            {
                GameManager.Instance.totalUpgradeValue+=1;
                GameManager.Instance.UptadeUpgradeText();
                _totalValueTextRect.DOScale(Vector3.zero, 1f).From();
            }
            else if (int.Parse(_priceText.text) == 50)
            {
                GameManager.Instance.totalUpgradeValue+= 2;
                GameManager.Instance.UptadeUpgradeText();
                _totalValueTextRect.DOScale(Vector3.zero, 1f).From();
            }
            else if (int.Parse(_priceText.text) == 75)
            {
                GameManager.Instance.totalUpgradeValue+= 3;
                GameManager.Instance.UptadeUpgradeText();
                _totalValueTextRect.DOScale(Vector3.zero, 1f).From();
            }
        } // Satın Alma yapıldığında para ve upgrade value değişmesi.
    }
}