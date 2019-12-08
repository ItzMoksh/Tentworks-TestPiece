using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CustomerModel
{
    public int customerId;
    public CustomerTableModel tableAssigned;
    public int time;
    public SaladModel salad;
    public Emotion emotion;

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