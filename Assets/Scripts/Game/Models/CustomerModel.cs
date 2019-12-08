using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[Serializable]
public class CustomerModel
{
    public int customerId;
    [SerializeField] public TextMeshPro timeLeftText;
    [SerializeField] public TextMeshPro saladText;
    [HideInInspector] public float timeLeft;
    [HideInInspector] public SaladModel salad;
    [HideInInspector] public Emotion emotion;
}

public enum Emotion
{
    NORMAL,
    SATISFIED,
    DISATISFIED,
    ANGRY,
    WILL_REWARD
}