using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppingBoardCollisionView : MonoBehaviour
{
    [SerializeField] private GameObject Views = null;
    [SerializeField] private ChoppingBoardModel choppingBoardModel = null;

    private ChoppingBoardView choppingBoardView = null;

    private void Awake()
    {
        choppingBoardView = Views.GetComponentInChildren<ChoppingBoardView>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        choppingBoardView.OnTriggerEnterCallback(collider, choppingBoardModel);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        choppingBoardView.OnTriggerExitCallback(collider, choppingBoardModel);
    }
}
