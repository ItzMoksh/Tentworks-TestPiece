using System.Runtime.InteropServices.ComTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHudView : MonoBehaviour
{
    [SerializeField] private List<HudModel> playersHud = null;
    [SerializeField] private float choppingBlinkDelay = 0.1f;

    private void Start()
    {
        for (int i = 0, l = playersHud.Count; i < l; i++)
        {
            playersHud[i].vegetablesChopped.text =
            playersHud[i].vegetablesInHand.text =
            playersHud[i].saladInHand.text =
            playersHud[i].activityLog.text =
            "";
        }
    }

    public void UpdateHud(PlayerModel playerModel)
    {
        SetHud(playerModel.id, playerModel.vegetablesInHand, VegetableListType.IN_HAND);
        SetHud(playerModel.id, playerModel.saladInHand.vegetables, VegetableListType.SALAD);
    }

    public void UpdateHud(ChoppingBoardModel choppingBoardModel)
    {
        SetHud((int)choppingBoardModel.playerId, choppingBoardModel.vegetablesOnBoard, VegetableListType.CHOPPED);
    }

    public void SetHud(int playerId, List<VegetableModel> vegetables, VegetableListType type)
    {
        string vegetablesString = "";
        for (int i = 0, l = vegetables.Count; i < l; i++)
        {
            vegetablesString += $"{vegetables[i].type}\n";
        }

        switch (type)
        {
            case VegetableListType.IN_HAND:
                {
                    playersHud[(int)playerId].vegetablesInHand.text = vegetablesString;
                }
                break;
            case VegetableListType.CHOPPED:
                {
                    playersHud[(int)playerId].vegetablesChopped.text = vegetablesString;
                }
                break;
            case VegetableListType.SALAD:
                {
                    playersHud[(int)playerId].saladInHand.text = vegetablesString;
                }
                break;
        }

    }

    public void SetActivityLog(int playerId, string log)
    {
        playersHud[playerId].activityLog.text = log;
    }

    public IEnumerator ChoppingIndicator(PlayerId playerId, float duration)
    {

        if (choppingBlinkDelay * 8 > 1f)
        {
            choppingBlinkDelay = 0.1f;
        }

        float remaingTime = 1 - (choppingBlinkDelay * 8);

        while (duration > 0)
        {
            playersHud[(int)playerId].activityLog.text = "C";
            yield return new WaitForSeconds(choppingBlinkDelay);

            playersHud[(int)playerId].activityLog.text = "CH";
            yield return new WaitForSeconds(choppingBlinkDelay);

            playersHud[(int)playerId].activityLog.text = "CHO";
            yield return new WaitForSeconds(choppingBlinkDelay);

            playersHud[(int)playerId].activityLog.text = "CHOP";
            yield return new WaitForSeconds(choppingBlinkDelay);

            playersHud[(int)playerId].activityLog.text = "CHOPP";
            yield return new WaitForSeconds(choppingBlinkDelay);

            playersHud[(int)playerId].activityLog.text = "CHOPPI";
            yield return new WaitForSeconds(choppingBlinkDelay);

            playersHud[(int)playerId].activityLog.text = "CHOPPIN";
            yield return new WaitForSeconds(choppingBlinkDelay);

            playersHud[(int)playerId].activityLog.text = "CHOPPING";
            yield return new WaitForSeconds(choppingBlinkDelay);

            yield return new WaitForSeconds(remaingTime);

            duration--;

        }
        playersHud[(int)playerId].activityLog.text = "";
    }
}
