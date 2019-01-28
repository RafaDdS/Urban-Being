using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moeda : MonoBehaviour
{

    public int Valor;

    private void Start()
    {
        var rb = GetComponent<Rigidbody2D>();
        if (rb) rb.AddForce(Random.insideUnitCircle * 25);
    }
}
