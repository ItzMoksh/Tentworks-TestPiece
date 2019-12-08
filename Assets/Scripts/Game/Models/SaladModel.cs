﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaladModel
{
    public string name;
    public List<VegetableModel> vegetables;

    public SaladModel()
    {
        
    }

    public SaladModel(List<VegetableModel> vegetables)
    {
        this.vegetables = vegetables;
    }
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