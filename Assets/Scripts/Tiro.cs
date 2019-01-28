using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiro : MonoBehaviour
{
    [SerializeField]
    bool Trail, Seguir;

    [HideInInspector]
    public Vector2 Dire;

    [SerializeField]
    private List<Tiro> subTiro;

    Collider2D col;

    public float VelTiro, tempVida;

    int n;
    TrailRenderer tr;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if(rb) rb.velocity = Dire.normalized * VelTiro;

        col = GetComponent<Collider2D>();

        if (Trail) tr = GetComponentInChildren<TrailRenderer>();

        Invoke("Destruir", tempVida);

        n = Time.frameCount;
    }

    void Update() {
        if (Trail)
        {
            if (!tr)
            {
                Destruir();
                return;
            }



            // Seguir
            if (Seguir)
            {
                if (Player.Instan) rb.velocity += (Vector2)(0.5f * (Player.Instan.transform.position - transform.position).normalized);
                rb.velocity = rb.velocity.normalized * VelTiro;
            }
        }

        if (col.enabled) return;

        if (n + 2 < Time.frameCount)
        if (!col.IsTouchingLayers(0)) col.enabled = true;

        

        
    }

    private void OnCollisionEnter2D(Collision2D ou)
    {
       
        
        var ini = ou.transform.GetComponent<Inimigo>();
        if (ini) ini.Vida--;
        
        
        var pl = ou.transform.GetComponent<Player>();
        if (pl) if (pl)
            {
                pl.Vida--;
                Destroy(gameObject);
                return;
            }
            
        
        Destruir();
    }

    void Destruir() {
        if (Trail)
            if (transform.childCount > 0)
                transform.GetChild(0).SetParent(null);
        Expandir();
        Destroy(gameObject);
    }

    private void Expandir()
    {
        var ang = Mathf.PI*2 / subTiro.Count;
        int i = 0;
        foreach (var tir in subTiro)
        {
            var t = Instantiate(tir, transform.position, Quaternion.identity);
            var tt = t.GetComponent<Tiro>();

            if (tt) tt.Dire = new Vector2(Mathf.Cos(ang*i), Mathf.Sin(ang * i));
            i++;
        }
    }


}
