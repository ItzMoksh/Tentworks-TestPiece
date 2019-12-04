using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerModel
{
    float timeLeft;
    SaladModel salad;
    Emotion emotion;
}

public enum Emotion
{
    NORMAL,
    SATISFIED,
    DISATISFIED,
    ANGRY,
    WILL_REWARD
}