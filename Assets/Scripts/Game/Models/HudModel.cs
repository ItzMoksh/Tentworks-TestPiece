using System;
using TMPro;

[Serializable]
public class HudModel
{
    PlayerId playerId;
    public TextMeshProUGUI vegetablesInHand;
    public TextMeshProUGUI vegetablesChopped;
    public TextMeshProUGUI saladInHand;
    public TextMeshProUGUI activityLog;
}

public enum VegetableListType
{
    IN_HAND,
    CHOPPED,
    SALAD
}
