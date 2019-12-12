using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlateModel
{
    public PlayerId playerId;
    [HideInInspector] public VegetableModel vegetable;
    public PlateModel(PlayerId id)
    {
        playerId = id;
    }
}
