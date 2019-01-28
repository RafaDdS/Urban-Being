using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Depre : Inimigo
{

    [SerializeField]
    Vector2 limites;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("Ati", 2 * TempoTiro, TempoTiro);
    }

    void Ati()
    {
        Atirar(0, Random.value < ProbAdiantar, false, Random.insideUnitCircle);
        Atirar(0, Random.value < ProbAdiantar, false, Random.insideUnitCircle);
    }

    void Update()
    {
        var vec = Player.Instan.transform.position - transform.position;
        if (vec.magnitude > limites.x && vec.magnitude < limites.y) rb.velocity = vec.normalized * Velocidade;
    }
}
