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
    public GameObject vegetableObject;
    public Sprite vegetableSprite;
    public VegetableType type;
    VegetableState state;
}
public enum VegetableType
{
    CUCUMBER,
    BELL_PEPPER,
    TOMATO,
    CABBAGE,
    BROCCOLI,
    RADISH
}

public enum VegetableState
{
    UNSLICED,
    SLICED
}