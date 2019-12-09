using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Model to store and pass Chopping board related data.
/// </summary>
[Serializable]
public class ChoppingBoardModel
{
    public PlayerId playerId;
    public GameObject choppingBoard;
    public ChoppingBoardState state;
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