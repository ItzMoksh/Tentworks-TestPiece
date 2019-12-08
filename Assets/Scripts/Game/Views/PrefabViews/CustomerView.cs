using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerView : MonoBehaviour
{
    [SerializeField] private CustomerModel customerModel;

    private CustomerController customerController;

    private void SetTimeLeft(float time)
    {
        customerModel.timeLeft = time;
        customerModel.timeLeftText.text = time.ToString();
    }

    private void SetSalad(SaladModel salad)
    {
        customerModel.salad = salad;
        for (int i = 0, l = salad.vegetables.Count; i < l; i++)
        {
            customerModel.saladText.text += salad.vegetables[i] + "\n";
        }
    }

    public void Init(int id,CustomerController customerController, float time, SaladModel salad)
    {
        this.customerController = customerController;
        SetTimeLeft(time);
        SetSalad(salad);
    }



}
