using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Model class for storing and passing Player related data.
/// </summary>
[Serializable]
public class PlayerModel
{
    public Rigidbody2D playerRb;
    public int id;
    public float speed;
    [HideInInspector] public State state;
    [HideInInspector] public List<VegetableModel> vegetablesInHand;
    [HideInInspector] public SaladModel saladInHand;
}

[Serializable]
public enum State
{
    ROAMING,
    IDLE,
    CHOPPING
}

[Serializable]
public enum PlayerId
{
    PLAYER_ONE,
    PLAYER_TWO
}