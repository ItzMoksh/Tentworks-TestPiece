using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    [SerializeField] private GameObject controllers = null;
    [SerializeField] private GameObject customerPrefab = null;
    [SerializeField] private Transform customerSpawnPoint = null;
    [SerializeField] private List<CustomerTableModel> customerTables = new List<CustomerTableModel>();
    [SerializeField] private float spawnDelay = 10f;
    [SerializeField] private int saladItemTimeMultiplier = 5;
    [SerializeField] private int minItemsInSalad = 3;
    [SerializeField] private int maxItemsInSalad = 5;

    private PlayerController playerController = null;
    private List<VegetableType> allVegetables = new List<VegetableType>();
    private List<CustomerTableModel> emptyTables = new List<CustomerTableModel>();
    private int noOfCustomers = 0;

    private void Awake()
    {
        playerController = controllers.GetComponentInChildren<PlayerController>();
    }

    private void Start()
    {
        InitTables();
        InitVegetableList();
        StartCoroutine(Spawner());
    }

    private void InitTables()
    {
        emptyTables = customerTables;
    }

    private void InitVegetableList()
    {
        allVegetables = new List<VegetableType>();
        for (int i = 0; i < 6; i++)
        {
            allVegetables.Add((VegetableType)i);
        }
    }

    private IEnumerator Spawner()
    {
        int time;
        while (true)
        {
            var salad = GetRandomSaladCombination();
            time = salad.vegetables.Count * saladItemTimeMultiplier;
            SpawnCustomer(noOfCustomers++, time, salad);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnCustomer(int id, int time, SaladModel salad)
    {
        var customer = Instantiate(customerPrefab, customerSpawnPoint);
        CustomerView customerView = customer.GetComponent<CustomerView>();
        CustomerModel customerModel = new CustomerModel(id, time, salad);
        AssignTable(customerModel);
        customerView.Init(this, customerModel);
    }

    private void AssignTable(CustomerModel customerModel)
    {
        int random = Random.Range(0, emptyTables.Count);
        customerModel.tableAssigned = emptyTables[random];
        emptyTables.RemoveAt(random);
    }

    private void AddToEmptyTables(CustomerTableModel table)
    {
        emptyTables.Add(table);
    }

    private SaladModel GetRandomSaladCombination()
    {
        InitVegetableList();
        int numberOfItems = Random.Range(minItemsInSalad, maxItemsInSalad);
        SaladModel salad = new SaladModel();
        for (int i = 0; i < numberOfItems; i++)
        {
            int random = Random.Range(0, allVegetables.Count);
            var vegetable = new VegetableModel();
            vegetable.type = allVegetables[random];
            salad.vegetables.Add(vegetable);
            allVegetables.RemoveAt(random);
        }
        return salad;
    }

    public void OnTimeOver(CustomerView customerView, int id)
    {
        //Deduct Score
        Debug.LogErrorFormat("Time Over! Customer {0} left", id);
        AddToEmptyTables(customerView.customerModel.tableAssigned);
        Destroy(customerView.gameObject); //Need object pooling
    }

    public void OnSaladeReceived(CustomerView customerView, int playerId)
    {
        AddToEmptyTables(customerView.customerModel.tableAssigned);

        var playerModel = playerController.GetPlayerModel(playerId);
        if (playerModel.saladInHand.vegetables.Count == 0)
        {
            Debug.LogError("No salad in hand!");
            return;
        }

        var customerSalad = customerView.customerModel.salad.vegetables;
        var playerSalad = playerModel.saladInHand.vegetables;

        if (customerSalad.Count == playerSalad.Count)
        {
            List<VegetableType> customerVegetableList = new List<VegetableType>();
            List<VegetableType> playerVegetableList = new List<VegetableType>();

            for (int i = 0, l = customerVegetableList.Count; i < l; i++)
            {
                customerVegetableList.Add(customerSalad[i].type);
                playerVegetableList.Add(playerSalad[i].type);
            }

            int diff = customerVegetableList.Except(playerVegetableList).ToList().Count;

            Debug.Log(diff);
            if (diff > 0)
            {
                Debug.LogErrorFormat("{0} Delivered Incorrect Salad!", (PlayerId)playerId);
            }
            else
            {
                Debug.LogWarningFormat("{0} Delivered Correct Salad!", (PlayerId)playerId);
            }
        }
        else
        {
            Debug.LogWarningFormat("{0} Delivered Incorrect Salad!", (PlayerId)playerId);
        }

        Destroy(customerView.gameObject);
        playerController.RemoveSaladFromPlayer(playerId);
    }

}
