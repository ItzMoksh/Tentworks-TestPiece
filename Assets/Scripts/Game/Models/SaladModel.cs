using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaladModel 
{
    string name;
    List<VegetableModel> vegetables;
}

[Serializable]
public class VegetableModel
{
    VegetableType type;
    // State state;
}

enum VegetableType
{
    CUCUMBER,
    BELL_PEPPER,
    TOMATO,
    CABBAGE,    
    BROCCOLI,
    RADISH
}