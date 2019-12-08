using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppingBoardView : MonoBehaviour
{
    [SerializeField] private GameObject controllers = null;
    [SerializeField] private List<ChoppingBoardModel> choppingBoardModels = new List<ChoppingBoardModel>(2);

    private ChoppingBoardController choppingBoardController = null;

    private void Awake()
    {
        choppingBoardController = controllers.GetComponentInChildren<ChoppingBoardController>();
    }

    private void Start()
    {
        ModelsInit();
    }

    private void ModelsInit()
    {
        choppingBoardController.UpdateChoppingBoardModels(choppingBoardModels);
    }

    private void UpdateChoppingBoardModels()
    {

    }

    public void OnTriggerEnterCallback(Collider2D collider, ChoppingBoardModel choppingBoardModel)
    {
        if (collider.tag == "Player")
        {
            var playerColl = collider.GetComponent<PlayerCollisionView>();
            int playerId = playerColl.playerId;
            choppingBoardController.UpdateChoppingBoardOnPlayer(playerId, choppingBoardModel);
        }
    }

    public void OnTriggerExitCallback(Collider2D collider, ChoppingBoardModel choppingBoardModel)
    {

    }
}
