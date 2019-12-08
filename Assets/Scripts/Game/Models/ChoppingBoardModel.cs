using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChoppingBoardModel
{
    public PlayerId playerId;
    public GameObject choppingBoard;
    [HideInInspector] public List<VegetableModel> vegetablesOnBoard;
    public ChoppingBoardModel(PlayerId id)
    {
        playerId = id;
    }
}

[Serializable]
public enum ChoppingBoardState
{
    EMTPY,
    OCCUPIED
}