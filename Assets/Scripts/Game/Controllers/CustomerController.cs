using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private Transform customerSpawnPoint;
    [SerializeField] private List<Transform> customerTables;
    private int noOfCustomers = 0;

    private void SpawnCustomer(int id,float time,SaladModel salad)
    {
        var customer = Instantiate(customerPrefab,customerSpawnPoint);
        CustomerView customerView = customer.GetComponent<CustomerView>();
        customerView.Init()
    }
}
