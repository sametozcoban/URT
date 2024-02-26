using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SnowBoarder.Characters;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("Toplam Bakiye")]
    [SerializeField] public int totalGoldAmount = 0;
    
    [Header("Toplam Kullanılabilecek Geliştirme Sayısı")]
    [SerializeField] public int totalUpgradeValue = 0;
    
    [Header("Geliştirme ve Bakiye Değerlerinin Textleri")]
    [SerializeField] public TMP_Text _upgradeText;
    [SerializeField] public TMP_Text _goldText;
    
    [Header("Özellikler Ui içindeki butonlar")]
    [SerializeField] public Button[] _buttons;

    [Header("Market Ui içindeki Butonlar")] 
    [SerializeField] public Button[] _marketButtons;
    
    [Header("Max Geliştirilebilecek Level Text")] 
    [SerializeField] public TMP_Text _startSpeedTxt;
    [SerializeField] public TMP_Text _boostSpeedTxt;
    [SerializeField] public TMP_Text _torqueTxt;
    [SerializeField] public int _startSpeedTxtValue ;
    [SerializeField] public int _boostSpeedTxtValue ;
    [SerializeField] public int _torqueTxtValue ;

    [Header("Özellik Değeri Textleri")]
    [SerializeField] public TMP_Text _startBoostPlayerValueTxt;
    [SerializeField] public TMP_Text _boostBoostPlayerValueTxt;
    [SerializeField] public TMP_Text _torquePlayerValueTxt;
    
    [Header("Butona Basıldığında Verilecek Effect")] 
    [SerializeField] public AudioSource _upgradeEffect = null;
    [SerializeField] public ParticleSystem _upgradeParticleSystem;
    
    [SerializeField] public Progression upgradeValues;
    private SliderControlSystem sliderControl;
    private PlayerController player;
    public GameObject playerPrefab;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
        CheckProgressionValue();
        UpdateGoldText(); 
        UptadeUpgradeText();
        ResetPlayerTxtValues();
        sliderControl = FindObjectOfType<SliderControlSystem>();
        player = playerPrefab.GetComponent<PlayerController>();
        PlayerValueChangeControl();
    }
    private void Start()
    {
        foreach (Button button in _marketButtons)
        {
            button.interactable = false;
        }
    }
    private void FixedUpdate()
    {
        UpdateSlider();
    }
    private void Update()
    {
        InteractableControl();
        CheckGoldAmount(); 
        UpdateUiValues();
        //totalUpgradeValue = upgradeValues.totalUpgrade;
    }
    private void CheckProgressionValue()//ScriptableObject içerisindeki değerleri oyundaki değişkenlere eşitledik.
    {
        totalGoldAmount = upgradeValues.totalGoldAmount;
        totalUpgradeValue = upgradeValues.totalUpgrade;
        _startSpeedTxtValue = upgradeValues.BaseSpeedSliderLevel;
        _boostSpeedTxtValue = upgradeValues.BoostSpeedSliderLevel;
        _torqueTxtValue = upgradeValues.TorqueSliderLevel;
    }
    public void AddGold(int amount) //Karaterimiz gold objesine değdiğinde bakiyeye gold ekleme.
    {
        totalGoldAmount += amount;
        UpdateGoldText();
    }
    private void UpdateUiValues()
    {
        upgradeValues.totalGoldAmount = totalGoldAmount;
        upgradeValues.BaseSpeedSliderLevel = _startSpeedTxtValue;
        upgradeValues.BoostSpeedSliderLevel = _boostSpeedTxtValue;
        upgradeValues.TorqueSliderLevel = _torqueTxtValue;
    }//Ara yüzde kullanılan değerlerin güncellenmesi.

    private void CheckGoldAmount()
    {
        if (totalGoldAmount <= 0)
        {
            foreach (Button button in _marketButtons)
            {
                button.interactable = false;
            } 
        }
        else if(totalGoldAmount > 0)
        {
            for (int i = 0; i < _marketButtons.Length; i++)
            {
                Debug.Log("Paramız yok");
                switch (i)
                {
               
                    case 0: if (totalGoldAmount >= 25 && totalGoldAmount < 50)
                    {
                        _marketButtons[0].interactable = true;
                    } break;
                    case 1: if (totalGoldAmount >= 50 && totalGoldAmount < 75)
                    {
                        _marketButtons[0].interactable = true;
                        _marketButtons[1].interactable = true;
                    } break;
                    case 2: if (totalGoldAmount >=75)
                    {
                        foreach (Button button in _marketButtons)
                        {
                            button.interactable = true;
                        }
                    } break;
                    default:
                        break;
                }
            }
        }
    }//Gold Değerine göre Market Butonlarının durumlarının kontrolleri.

    public bool SpendGold(int amount) //Butona tıklandığında bakiye düşmesi.
    {
        StartCoroutine(ButtonInteractableControl());
        if (totalGoldAmount >= amount)
        {
            totalGoldAmount -= amount;
            UpdateGoldText();
            UptadeUpgradeText();
            return true;
        }
        else
        {
            Debug.LogWarning("Not Enough Money" + amount + "gold");
            return false;
        }
    }
    private void UpdateGoldText() //Ui üzerindeki Gold değerinin güncellenmesi.
    {
        _goldText.text = totalGoldAmount.ToString() ;
    }
    public void UptadeUpgradeText()//Ui üzerindeki Upgrade değerinin güncellenmesi.
    {
        _upgradeText.text = totalUpgradeValue.ToString();
    }

    private void InteractableControl() //Özellikler ui içerisinde yer alan butonların kontrollerinin sağlandığı fonksiyon.
    {
        if (totalUpgradeValue > 0)
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                bool isInteractable = true;

                // Her bir buton için kontrol
                switch (i)
                {
                    case 0: // Başlangıç hızı kontrolü
                        if (_startSpeedTxtValue >= sliderControl._startSpeedSlider.maxValue)
                            isInteractable = false;
                        break;
                    case 1: // Boost hızı kontrolü
                        if (_boostSpeedTxtValue >= sliderControl._boostSpeedSlider.maxValue)
                            isInteractable = false;
                        break;
                    case 2: // Tork kontrolü
                        if (_torqueTxtValue >= sliderControl._toruqeSlider.maxValue)
                            isInteractable = false;
                        break;
                    default:
                        break;
                }

                // Butonun etkinliğini ayarla
                _buttons[i].interactable = isInteractable;
            }
        }
        else if (totalUpgradeValue <= 0 )
        {
            foreach (Button button in _buttons)
            {
                button.interactable = false;
            }
        }
    }
    
    public void ImproveFeature() //Özellikler butonlarına tıklandığında gerçekleşen durum.
    { 
        totalUpgradeValue-=1;
        UptadeUpgradeText();
        _upgradeParticleSystem.Play();
        _upgradeEffect.Play();
        StartCoroutine(ParticleControl()); // Uı arkasında çok hızlı çalıştı. 
    }

    public void StarSpeedLevelTxtChange()
    {
        if (_startSpeedTxtValue != upgradeValues.StartSpeedValue.Length )
        {
            _startBoostPlayerValueTxt.text = upgradeValues.StartSpeedValue[upgradeValues.BaseSpeedSliderLevel].ToString();
        }
        _startSpeedTxtValue += 1;
        _startSpeedTxt.text = _startSpeedTxtValue.ToString();
        sliderControl.setStartSpeedSliderValues(upgradeValues.BaseSpeedSliderLevel);
        player.basedSpeed = int.Parse(_startBoostPlayerValueTxt.text);
    } //Image İçerisindeki yer alan level 1/20 değişimi,
                                              //PlayerDeğerlerinin text değişimleri
    public void BoostSpeedLevelTxtChange()
    {
        if (_boostSpeedTxtValue != upgradeValues.StartSpeedValue.Length )
        {
            _boostBoostPlayerValueTxt.text = upgradeValues.boostSpeedValue[upgradeValues.BoostSpeedSliderLevel].ToString();
        }
        _boostSpeedTxtValue += 1;
        _boostSpeedTxt.text = _boostSpeedTxtValue.ToString();
        sliderControl.setBoostSpeedSliderValues(upgradeValues.BoostSpeedSliderLevel);
        player.boostSpeed = int.Parse(_boostBoostPlayerValueTxt.text);
    } //Image İçerisindeki yer alan level 1/20 değişimi.
    //PlayerDeğerlerinin text değişimleri
    public void TorqueLevelTxtChange()
    {
        if (_torqueTxtValue != upgradeValues.StartSpeedValue.Length )
        {
            _torquePlayerValueTxt.text = upgradeValues.torqueSpeedValue[_torqueTxtValue].ToString();
        }
        _torqueTxtValue += 1;
        _torqueTxt.text = _torqueTxtValue.ToString();
        sliderControl.setTorqueSliderValues(upgradeValues.TorqueSliderLevel);
        player.torqueAmount = int.Parse(_torquePlayerValueTxt.text);
    } //Image İçerisindeki yer alan level 1/20 değişimi,
    //PlayerDeğerlerinin text değişimleri
    private void PlayerValueChangeControl() // Karakterimizdeki değerlerin değiştirilmesini sağlıyoruz.
    {
        player.basedSpeed = int.Parse(_startBoostPlayerValueTxt.text);
        player.boostSpeed = int.Parse(_boostBoostPlayerValueTxt.text);
        player.torqueAmount = int.Parse(_torquePlayerValueTxt.text);
    }
    private void ResetPlayerTxtValues() // Özellikler tablosundaki UpgradeTxt değerlerini SO ya göre resetledik.
    {
        _startBoostPlayerValueTxt.text = upgradeValues.StartSpeedValue[_startSpeedTxtValue - 1].ToString();
        _boostBoostPlayerValueTxt.text = upgradeValues.boostSpeedValue[_boostSpeedTxtValue - 1].ToString();
        _torquePlayerValueTxt.text = upgradeValues.torqueSpeedValue[_torqueTxtValue - 1].ToString();
    }

    void UpdateSlider()
    {
        sliderControl.setStartSpeedSliderValues(upgradeValues.BaseSpeedSliderLevel);
        sliderControl.setBoostSpeedSliderValues(upgradeValues.BoostSpeedSliderLevel);
        sliderControl.setTorqueSliderValues(upgradeValues.TorqueSliderLevel);
    } //Başlangıçta Slider güncellemesi yapıyoruz.
    public void MarketButtonControl()
    {
        StartCoroutine(ButtonInteractableControl());
    }
    IEnumerator ParticleControl()
    {
        yield return new WaitForSeconds(0.5f);
        _upgradeParticleSystem.Stop();
    }
    IEnumerator ButtonInteractableControl()
    {
        foreach (Button button in _marketButtons)
        {
            button.interactable = false;
        }
        yield return new WaitForSeconds(0.5f);
        
        foreach (Button button in _marketButtons)
        {
            button.interactable = true;
        }
    }

}
