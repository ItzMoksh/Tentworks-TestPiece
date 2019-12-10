using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Model to store and pass in game player score and time left info.
/// </summary>
[Serializable]
public class GameModel
{
    List<PlayerInfo> playersInfo;
}

public class PlayerInfo
{
    int score;
    int timeLeft;
}
