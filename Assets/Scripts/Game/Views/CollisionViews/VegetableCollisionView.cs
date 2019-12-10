using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds vegetable model and call backs its parent view on collision.
/// </summary>
public class VegetableCollisionView : MonoBehaviour
{
    [SerializeField] private GameObject Views = null;
    [SerializeField] private VegetableModel vegetableModel = null;
    private VegetableView vegetableView = null;

    private void Awake()
    {
        vegetableView = Views.GetComponentInChildren<VegetableView>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        vegetableView.TriggerEnterCallback(collider,vegetableModel);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        vegetableView.TriggerExitCallback(collider,vegetableModel);
    }
}
