using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds PlayerId and callbacks it's parent view on collision.
/// </summary>
public class PlayerCollisionView : MonoBehaviour
{
    [SerializeField] private GameObject views = null;
    public int playerId = 0;
    private PlayerView playerView = null;

    private void Awake()
    {
        playerView = views.GetComponentInChildren<PlayerView>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerView.CollisionCallback(collision, playerId);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        playerView.CollisionCallback(collision, playerId);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        playerView.TriggerEnterCallback(collider, playerId);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        playerView.TriggerExitCallback(collider, playerId);
    }
}
