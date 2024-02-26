using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Upgrade" , menuName = "UpgradeSkılls")]
public class Progression : ScriptableObject
{
    [Header("Karakter Değerlerinin Kontrol")]
    public int[] StartSpeedValue;
    public int[] boostSpeedValue;
    public int[] torqueSpeedValue;
    
    [Header("Budget Kontrol")]
    public int totalGoldAmount;
    public int totalUpgrade;
    
    [Header("Slider Level Kontrol")]
    public int BaseSpeedSliderLevel;
    public int BoostSpeedSliderLevel;
    public int TorqueSliderLevel;
}
