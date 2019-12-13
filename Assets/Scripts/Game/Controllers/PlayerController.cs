using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject controllers = null;
    [SerializeField] private GameObject views = null;
    [SerializeField] private int vegetablePickLimit = 2;
    [SerializeField] private float choppingTime = 2f;

    private GameController gameController = null;
    private PlateController plateController = null;
    private VegetableController vegetableController = null;
    private ChoppingBoardController choppingBoardController = null;

    private PlayerView playerView = null;
    private PlayerHudView playerHudView = null;
    private ChoppingBoardView chopBoardView = null;

    [HideInInspector] public List<PlayerModel> playerModels;

    private void Awake()
    {
        gameController = controllers.GetComponentInChildren<GameController>();
        vegetableController = controllers.GetComponentInChildren<VegetableController>();
        choppingBoardController = controllers.GetComponentInChildren<ChoppingBoardController>();
        plateController = controllers.GetComponentInChildren<PlateController>();

        playerView = views.GetComponentInChildren<PlayerView>();
        playerHudView = views.GetComponentInChildren<PlayerHudView>();
        chopBoardView = views.GetComponentInChildren<ChoppingBoardView>();
    }

    private void PickupVegetable(VegetableModel vegetableModel, PlayerId playerId)
    {
        var vegetables = playerModels[(int)playerId].vegetablesInHand;
        vegetables.Add(vegetableModel);
        playerHudView.UpdateHud(playerModels[(int)playerId]);
        SetActivityLog((int)playerId, $"{vegetableModel.type}\nPicked!");
        Debug.LogFormat("{0} picked by {1}", vegetableModel.type, playerId);
    }

    private void ChopVegetable(VegetableModel vegetableToChop, PlayerId playerId)
    {
        choppingBoardController.PlaceVegetableToChop(vegetableToChop, playerId);
        StartCoroutine(ChoppingEnumerator(playerId));
        StartCoroutine(playerHudView.ChoppingIndicator(playerId, choppingTime));
        Debug.LogFormat("{0} Started Chopping {1}", (PlayerId)playerId, vegetableToChop.type);
    }

    private IEnumerator ChoppingEnumerator(PlayerId playerId)
    {
        RestrictMovement(playerId, true);
        yield return new WaitForSeconds(choppingTime);
        RestrictMovement(playerId, false);
        playerHudView.UpdateHud(playerModels[(int)playerId]);
        Debug.LogFormat("{0} Finished Chopping", (PlayerId)playerId);
    }

    private void RestrictMovement(PlayerId playerId, bool doRestrict)
    {
        playerView.RestrictMovement(playerId, doRestrict);
    }

    public PlayerModel GetPlayerModel(int playerId)
    {
        return playerModels[playerId];
    }

    public void OnVegetableInteract(PlayerId playerId)
    {
        var playerModel = playerModels[(int)playerId];
        if (playerModel.saladInHand.vegetables.Count == 0 && playerModel.vegetablesInHand.Count < vegetablePickLimit)
        {
            VegetableModel vegetableModel = vegetableController.GetVegetableModelOnPlayer(playerId);
            PickupVegetable(vegetableModel, playerId);
        }
        else
        {
            SetActivityLog((int)playerId, "Hands\nfull!");
            Debug.LogError("Can't pickup, empty your hands!");
        }
    }

    public void PlaceVegetableOnBoard(PlayerId playerId)
    {
        if (choppingBoardController.ValidateChoppingBoardId(playerId))
        {
            if (playerModels[(int)playerId].vegetablesInHand.Count > 0)
            {
                var vegetableModel = playerModels[(int)playerId].vegetablesInHand[0];
                playerModels[(int)playerId].vegetablesInHand.RemoveAt(0);
                playerHudView.UpdateHud(playerModels[(int)playerId]);
                ChopVegetable(vegetableModel, playerId);
            }
            else
            {
                SetActivityLog((int)playerId, "Get a \n Vegetable!");
                Debug.LogError("Get Some Vegetables!");
            }
        }
        else
        {
            SetActivityLog((int)playerId, "Go to \nyour board! ");
            Debug.LogError("Not your board!");
        }
    }

    public void PlaceVegetableOnPlate(PlayerId playerId)
    {
        if (plateController.ValidatePlayerPlate(playerId))
        {
            if (playerModels[(int)playerId].vegetablesInHand.Count > 0)
            {
                var vegetableModel = playerModels[(int)playerId].vegetablesInHand[0];
                playerModels[(int)playerId].vegetablesInHand.RemoveAt(0);
                if(plateController.PutVegetableOnPlate(playerId, vegetableModel))
                {
                playerHudView.SetPlateHud(playerId, vegetableModel);
                SetActivityLog((int)playerId, $"Placed {vegetableModel.type}\non plate!");
                }
                else
                {
                    SetActivityLog((int)playerId, $"Plate is not empty!");
                }
                
            }
            else
            {
                SetActivityLog((int)playerId, "Get a \n Vegetable!");
                Debug.LogError("Get Some Vegetables!");
            }
        }
        else
        {
            SetActivityLog((int)playerId, "Go to \nyour board! ");
            Debug.LogError("Not your board!");
        }
    }

    public void UpdateChoppedVegetables(ChoppingBoardModel choppingBoardModel)
    {
        playerHudView.UpdateHud(choppingBoardModel);
    }

    public void PickSaladFromBoard(PlayerId playerId)
    {
        if (choppingBoardController.ValidateChoppingBoardId(playerId))
        {
            if (playerModels[(int)playerId].vegetablesInHand.Count == 0)
            {
                var salad = choppingBoardController.GetSaladFromBoard(playerId);
                if (salad != null)
                {
                    playerModels[(int)playerId].saladInHand = salad;
                    playerHudView.UpdateHud(playerModels[(int)playerId]);
                    SetActivityLog((int)playerId, "Picked up\n a salad!");
                }
                else
                {
                    SetActivityLog((int)playerId, "There's no\nSalad!");
                    Debug.LogError("Nothing on the board!");
                }
            }
            else
            {
                SetActivityLog((int)playerId, "Hands are\n not empty!");
                Debug.LogError("Your hands need to be empty to pickup the salad!");
            }
        }
        else
        {
            SetActivityLog((int)playerId, "Not your board!");
            Debug.LogError("Not your board!");
        }
    }

    public void PickVegetableFromPlate(PlayerId playerId)
    {
        if (plateController.ValidatePlayerPlate(playerId))
        {
            if (playerModels[(int)playerId].vegetablesInHand.Count < vegetablePickLimit)
            {
                var vegetable = plateController.PickVegetableFromPlate(playerId);
                if (vegetable != null)
                {
                    playerModels[(int)playerId].vegetablesInHand.Add(vegetable);
                    playerHudView.UpdateHud(playerModels[(int)playerId]);
                    playerHudView.ClearPlateHud((int)playerId);
                    SetActivityLog((int)playerId, $"Picked up\n{vegetable.type}");
                }
                else
                {
                    SetActivityLog((int)playerId, "Plate empty!");
                    Debug.LogError("Nothing on the plate!");
                }
            }
            else
            {
                SetActivityLog((int)playerId, "Hands are\n not empty!");
                Debug.LogError("Your hands need to be empty to pickup the vegetable!");
            }
        }
        else
        {
            SetActivityLog((int)playerId, "Not your plate!");
            Debug.LogError("Not your plate!");
        }
    }

    public void RemoveSaladFromPlayer(int playerId)
    {
        playerModels[playerId].saladInHand = new SaladModel();
        playerHudView.UpdateHud(playerModels[playerId]);
    }

    public void SetActivityLog(int playerId, string log)
    {
        playerHudView.SetActivityLog(playerId, log);
    }

    public void SetActivityLog(string log)
    {
        for (int i = 0, l = playerModels.Count; i < l; i++)
        {
            playerHudView.SetActivityLog(i, log);
        }
    }

    public void ThrowSalad(PlayerId playerId)
    {
        if (playerModels[(int)playerId].saladInHand.vegetables.Count == 0)
        {
            SetActivityLog((int)playerId, "No salad\nto throw!");
        }
        else
        {
            RemoveSaladFromPlayer((int)playerId);
            gameController.OnSaladThrow(playerId);
            SetActivityLog((int)playerId, "Salad Thrown!");
        }
    }

    public void OnGameOver(PlayerId playerId)
    {
        RestrictMovement(playerId, true);
        SetActivityLog((int)playerId, "GAME OVER!");
    }

    public void UpdatePlayersModel(List<PlayerModel> playerModels)
    {
        this.playerModels = playerModels;
    }
}
