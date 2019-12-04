using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionView : MonoBehaviour
{
    [SerializeField] private int playerId = 0;
    [SerializeField] private GameObject views = null;
    private PlayerView playerView = null;

    private void Awake()
    {
        playerView = views.GetComponentInChildren<PlayerView>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerView.CollisionCallBack(collision,playerId);
    }
}
