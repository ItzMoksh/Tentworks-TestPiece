using System;
using System.Collections.Generic;

/// <summary>
/// Model to store and pass in game player score and time left info.
/// </summary>
[Serializable]
public class GameModel
{
    public List<PlayerInfo> playersInfo;
}

[Serializable]
public class PlayerInfo
{
    public int score;
    public int timeLeft;
}

[Serializable]
public class ScoreModel
{
    public int correctDeliveryPoints;
    public int incorrectDeliveryPoints;
    public int correctDeliveryTime;
    public int throwPenalty;
    public int scoreBonus;
    public int timeBonus;
    public float speedBonus;
}