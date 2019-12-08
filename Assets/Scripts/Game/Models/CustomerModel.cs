using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[Serializable]
public class CustomerModel
{
    public int customerId;
    public CustomerTableModel tableAssigned;
    [SerializeField] public TMP_Text timeLeftText;
    [SerializeField] public TMP_Text saladText;
    [HideInInspector] public int time;
    [HideInInspector] public SaladModel salad;
    [HideInInspector] public Emotion emotion;

    public CustomerModel(int id, int time, SaladModel salad)
    {
        this.customerId = id;
        this.time = time;
        this.salad = salad;
    }
}

[Serializable]
public class CustomerTableModel
{
    public int tableId;
    public Transform tableTransform;
    public TableState state;
}

public enum Emotion
{
    NORMAL,
    SATISFIED,
    DISATISFIED,
    ANGRY,
    WILL_REWARD
}

public enum TableState
{
    EMPTY,
    OCCUPIED
}