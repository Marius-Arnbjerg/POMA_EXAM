using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    string brand;
    float price;
    int topSpeed;

    Car(string theBrand, float thePrice, int theTopSpeed)
    {
        brand = theBrand;
        price = thePrice;
        topSpeed = theTopSpeed;
    }

    public void Start()
    {
        Car bugatti = new Car("buggati", 6.500000f, 220);

        Debug.Log(bugatti.brand);
        Debug.Log(bugatti.price);
        Debug.Log(bugatti.topSpeed);
    }
}
