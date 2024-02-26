using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControlSystem : MonoBehaviour
{
   [Header("Text Değerlerine Göre değişen Sliderlar")]
   [SerializeField] public Slider _startSpeedSlider;
   [SerializeField] public Slider _boostSpeedSlider;
   [SerializeField] public Slider _toruqeSlider;

   [Header("Slider Fullenince Kapanacak Butonlar")] 
   [SerializeField] Button StartSpeedButtons;
   [SerializeField] Button BoostSpeedButtons;
   [SerializeField] Button TorqueButtons;

   private void Awake()
   {
      _startSpeedSlider.maxValue = 10;
      _boostSpeedSlider.maxValue = 10;
      _toruqeSlider.maxValue = 10;
   } //Slider Max Level Seviye Belirleme

   public void setStartSpeedSliderValues(int value)
   {
      _startSpeedSlider.value = value;
   } //Slider Level Ayarlama
   public void setBoostSpeedSliderValues(int value)
   {
      _boostSpeedSlider.value = value;
   } //Slider Level Ayarlama
   public void setTorqueSliderValues(int value)
   {
      _toruqeSlider.value = value;
   } //Slider Level Ayarlama
}
