using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    [SerializeField] private GameObject customerPrefab = null;
    [SerializeField] private Transform customerSpawnPoint = null;
    [SerializeField] private List<CustomerTableModel> customerTables = new List<CustomerTableModel>();
    [SerializeField] private float spawnDelay = 10f;
    [SerializeField] private int saladItemTimeMultiplier = 5;
    [SerializeField] private int minItemsInSalad = 3;
    [SerializeField] private int maxItemsInSalad = 5;
    private List<VegetableType> allVegetables = new List<VegetableType>(6);
    private List<CustomerTableModel> emptyTables = new List<CustomerTableModel>();
    private int noOfCustomers = 0;

    private void Start()
    {
        InitTables();
        InitVegetableList();
    }

    private void InitTables()
    {
        emptyTables = customerTables;
    }

    private void InitVegetableList()
    {
        for (int i = 0, l = allVegetables.Count; i < l; i++)
        {
            allVegetables[i] = (VegetableType)i;
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
        var allVegetables = this.allVegetables;
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
}
