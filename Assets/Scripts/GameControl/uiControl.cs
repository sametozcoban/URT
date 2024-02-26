using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class uiControl : MonoBehaviour
{
    [Header("Kategorilerin olduğu Ui")]
    [SerializeField] private GameObject _activeUi = null;
    [Header("Buton basıldığında hangi Ui açılıp kapanacak")]
    [SerializeField] private GameObject[] _ui = null;
    [Header("Beyaz Text Bloğu")]
    [SerializeField] private TMP_Text _uiText = null;
    [Header("Menu açıldığındaki 3 farklı Buton")]
    [SerializeField] private Button _buttons = null;
    [Header("Settings Button Tıklandığında Açılacak Ui")] 
    [SerializeField] private GameObject[] _settingsUi = null;
    [SerializeField] private GameObject _closeButton = null;
    [Header("SettingsUI içindeki UI kontrol")]
    [SerializeField] private GameObject _audioUi = null;
    [SerializeField] private GameObject _controlUi = null;
    [Header("Level Selection Ekranındaki Menuleri Açma Butonu")] 
    [SerializeField] private GameObject[] _menuAndPlayUi;
    [SerializeField] private RectTransform[] _uiSRect = null;
    private bool isOpen = false;
    private bool settingsIsOpen = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (_uiText != null)
        {
            _uiText.text = "Karakter";
        }
    }
    
    public void OpenInfoUi() // Menu butonuna basıldığında 3 farklı kategorili diğer Ui aç kapa yapılıyor.
    {
        
        if (isOpen)
        {
            foreach (var uiS in _ui)
            {
                uiS.SetActive(false);
                isOpen = false;
            }
        }
        else
        {
            foreach (var uiS in _ui)
            {
                uiS.SetActive(true);
                isOpen = true;
            }
        }
    }
    public void CharacterInfoUiControl() // Karakter, Özellikler ve Market arasında git gel yaparken,
                                         // Ui aç kapa yaptırdığımız fonksiyon.
    {
        _activeUi.SetActive(true);
        foreach (var uiS in _ui)
        {
            uiS.SetActive(false);
        }
    }
    public void ButtonClicked() // Kategori, Özellikler ve Market butonlarına tıkladığımızda,
                                // Beyaz Text Bloğunun içindeki text değişmesini sağlıyor.
                                // Hangi butona tıkladıysam buton içinde yazan text Beyaz Büyük Bloktada yazıyor.
    {
        if (_uiText != null)
        {
            _uiText.text.ToUpper();
            _uiText.text = _buttons.GetComponentInChildren<TMP_Text>().text ;
        }
    }

    public void SettingsOpen()
    {
        if (!settingsIsOpen)
        {
            foreach (GameObject settingsUI in _settingsUi)
            {
                settingsUI.SetActive(true);
                _closeButton.SetActive(true);
                settingsIsOpen = true;
            }
        }
    } //SettingsUi açma.

    public void SettingsClosed()
    {
        if (settingsIsOpen)
        {
            foreach (GameObject settingsUI in _settingsUi)
            {
                settingsUI.SetActive(false);
                _closeButton.SetActive(false);
                settingsIsOpen = false;
                
            }
        }
    } //SettingsUi kapatma

    public void OpenAudio()
    {
        _audioUi.SetActive(true);
        _controlUi.SetActive(false);
    } //SesUi açma

    public void OpenControl()
    {
        _audioUi.SetActive(false);
        _controlUi.SetActive(true);
    } // Kontrol Öğretisi Ui açma

    public void OpenMenuAndPlayButton()
    {
        
        foreach (GameObject go in _menuAndPlayUi)
        {
            if (go.activeSelf)
            {
                go.SetActive(false);
               
                
            }
            else
            { 
                go.SetActive(true);
            }
        }
    } // Menu ve Play Butonunu açma ve kapama için activeSelf kullandık.
}
