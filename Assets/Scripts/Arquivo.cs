using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arquivo : Inimigo
{

    [SerializeField]
    Transform Padro;

    List<Transform> pos = new List<Transform>();

    Vector3 posIni, Lpos;

    int i = 0;

    private void Start()
    {
        var a = Padro.GetChild(Random.Range(0, Padro.childCount));

        for (int i = 0; i < a.childCount; i++)
        {
            pos.Add(a.GetChild(i));
        }
        

        posIni = transform.position;
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("Ati", 1f + TempoTiro, TempoTiro);
    }

    void Ati()
    {
        Atirar(Random.Range(0,2), Random.value < ProbAdiantar, true);
    }

    private void Update()
    {
        if (i < pos.Count)
        {


            var p = pos[i];
            Vector3 vec = p.position + posIni - transform.position;
            rb.velocity = vec.normalized * Velocidade;
            if (vec.magnitude < 1f) i++;

            if (Time.frameCount % 20 == 0)
            {
                if (Mathf.Approximately((Lpos - transform.position).magnitude, 0)) i++;
                Lpos = transform.position;
            }
        }
        else
        {
            i = 0;
        }

    }

}
