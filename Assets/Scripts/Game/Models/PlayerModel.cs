using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerModel
{
    public Rigidbody2D playerRb;
    public int id;
    public float speed;
    [HideInInspector] public float score;
    [HideInInspector] public State state;
}

[Serializable]
public enum State
{
    ROAMING,
    IDLE,
    CHOPPING
}