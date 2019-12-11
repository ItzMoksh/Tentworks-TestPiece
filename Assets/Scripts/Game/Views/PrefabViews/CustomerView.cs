using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomerView : MonoBehaviour
{

    [SerializeField] public TextMeshProUGUI timeLeftText;
    [SerializeField] public TextMeshProUGUI saladText;
    private CustomerController customerController;
    private Coroutine countDown;
    [HideInInspector] public CustomerModel customerModel = null;


    private void SetTimeLeft(int time)
    {
        timeLeftText.text = time.ToString();
    }

    private void SetSalad(SaladModel salad)
    {
        string saladString = "";
        for (int i = 0, l = salad.vegetables.Count; i < l; i++)
        {
            saladString += salad.vegetables[i].type + "\n";
        }
        saladText.SetText(saladString);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            customerController.OnSaladeReceived(this, collider.GetComponent<PlayerCollisionView>().playerId);
        }
    }

    public void Init(CustomerController customerController, CustomerModel customerModel)
    {
        this.customerController = customerController;
        this.customerModel = customerModel;
        SetTimeLeft(customerModel.time);
        SetSalad(customerModel.salad);
        GetComponent<Rigidbody2D>().MovePosition(this.customerModel.tableAssigned.tableTransform.position);
        countDown = StartCoroutine(CountDown());
    }

    public IEnumerator CountDown()
    {
        for (int i = customerModel.time; i >= 0; i--)
        {
            SetTimeLeft(i);
            yield return new WaitForSeconds(1f);
        }
        customerController.OnTimeOver(this, customerModel.customerId);
    }

}
