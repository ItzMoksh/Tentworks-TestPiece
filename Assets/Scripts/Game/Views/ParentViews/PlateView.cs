using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateView : MonoBehaviour
{
    [SerializeField] private GameObject controllers = null;
    private PlateController plateController = null;

    private void Awake()
    {
        plateController = controllers.GetComponentInChildren<PlateController>();
    }

    public void TriggerEnterCallback(Collider2D collider, PlateModel plateModel)
    {
        if (collider.tag == "Player")
        {
            int playerId = collider.GetComponent<PlayerCollisionView>().playerId;
            plateController.OnPlayerCollisionEnter((PlayerId)playerId, plateModel);
        }
    }

    public void TriggerExitCallback(Collider2D collider, PlateModel plateModel)
    {
        if (collider.tag == "Player")
        {
            int playerId = collider.GetComponent<PlayerCollisionView>().playerId;
            plateController.OnPlayerCollisionExit((PlayerId)playerId, plateModel);
        }
    }
}
