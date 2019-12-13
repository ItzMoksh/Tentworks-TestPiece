using System;
using UnityEngine;

[Serializable]
public class PowerUpModel
{
    public PowerUpType type;
    [HideInInspector] public PlayerId playerId;
}

[Serializable]
public enum PowerUpType
{
    SPEED,
    TIME,
    SCORE
}
