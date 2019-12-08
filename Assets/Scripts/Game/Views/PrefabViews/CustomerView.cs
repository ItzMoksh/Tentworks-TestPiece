using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerView : MonoBehaviour
{
    private CustomerController customerController;
    private Coroutine countDown;
    public CustomerModel customerModel = null;


    private void SetTimeLeft(int time)
    {
        customerModel.timeLeftText.text = time.ToString();
    }

    private void SetSalad(SaladModel salad)
    {
        for (int i = 0, l = salad.vegetables.Count; i < l; i++)
        {
            customerModel.saladText.text += salad.vegetables[i] + "\n";
        }
    }

    public void Init(CustomerController customerController, CustomerModel customerModel)
    {
        this.customerController = customerController;
        this.customerModel = customerModel;
        SetTimeLeft(customerModel.time);
        SetSalad(customerModel.salad);
        countDown = StartCoroutine(CountDown());
    }

    public IEnumerator CountDown()
    {
        for (int i = customerModel.time; i >= 0; i--)
        {
            SetTimeLeft(i);
            yield return new WaitForSeconds(1);
        }
        customerController.OnTimeOver(this, customerModel.customerId);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        StopCoroutine(countDown);
    }

}
