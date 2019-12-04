using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private List<PlayerModel> players = null;
    private Vector2 playerOnePos = Vector2.zero;
    private Vector2 playerTwoPos = Vector2.zero;
    private float moveHorizontal;
    private float moveVertical;
    private Vector2 movementP1;
    private Vector2 movementP2;

    private void Start()
    {
        playerOnePos = players[0].playerRb.position;
        playerTwoPos = players[1].playerRb.position;
    }

    private void FixedUpdate()
    {
        CheckForPlayerOneInput();
        CheckForPlayerTwoInput();
    }

    private void CheckForPlayerOneInput()
    {
        moveHorizontal = Input.GetAxis("HorizontalOne") * players[0].speed;
        moveVertical = Input.GetAxis("VerticalOne") * players[0].speed;

        movementP1 = new Vector2(moveHorizontal, moveVertical);
        movementP1.Normalize();
        players[0].playerRb.velocity = movementP1 * players[0].speed;
    }

    private void CheckForPlayerTwoInput()
    {
        moveHorizontal = Input.GetAxis("HorizontalTwo") * players[0].speed;
        moveVertical = Input.GetAxis("VerticalTwo") * players[0].speed;

        movementP2 = new Vector2(moveHorizontal, moveVertical);
        movementP2.Normalize();
        players[1].playerRb.velocity = movementP2 * players[0].speed;

    }

    private void StopPlayerMovement()
    {
        Debug.Log("Stop");
        players.ForEach(player =>
        {
            player.playerRb.velocity = Vector2.zero;
        });
    }

    private void StopPlayerMovement(int playerId)
    {

    }

    public void CollisionCallBack(Collision2D collision, int playerId)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Vector2 positionRelative = transform.InverseTransformPoint(collision.transform.position);
            // float moveRelative = Vector2.Distance(positionRelative, new Vector2(moveHorizontal,moveVertical));
            // if (moveRelative > 1.0f)
            // {

            // }
            // else
            //     StopPlayerMovement();
        }
    }
}
