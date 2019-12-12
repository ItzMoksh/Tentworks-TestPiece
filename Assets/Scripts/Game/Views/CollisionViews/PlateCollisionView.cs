using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds Chopping board model and callbacks it's parent view on collision.
/// </summary>
public class PlateCollisionView : MonoBehaviour
{
    [SerializeField] private GameObject Views = null;
    [SerializeField] private PlateModel plateModel = null;

    private PlateView plateView = null;

    private void Awake()
    {
        plateView = Views.GetComponentInChildren<PlateView>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        plateView.TriggerEnterCallback(collider, plateModel);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        plateView.TriggerExitCallback(collider, plateModel);
    }
}
