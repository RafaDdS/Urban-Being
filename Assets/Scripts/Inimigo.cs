using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    [SerializeField]
    int _vida;

    [SerializeField]
    protected List<GameObject> Tirou;

    [SerializeField]
    protected float TempoTiro, ProbAdiantar;

    [SerializeField]
    List<GameObject> Drop;

    protected bool AtiraEne = false;

    protected Rigidbody2D rb;

    [Space]
    [SerializeField]
    protected float Velocidade;

    public int Vida
    {
        get { return _vida; }
        set {
            _vida = value;

            if (value == 0) Morrer();
        }
    }

    protected Vector2 DireTiro
    {
        get
        {
            if (Player.Instan) return (Player.Instan.transform.position - transform.position).normalized;
            else return Vector3.zero;
        }
    }

    protected Vector2[] DireTiroAdiantado
    {
        get
        {
            var pl = Player.Instan;
            Vector2[] s = new Vector2[Tirou.Count];
            if (pl) {
                int i = 0;
                foreach (var Tirinho in Tirou)
                {
                    var t = Tirinho.GetComponent<Tiro>();
                    s[i] = (pl.transform.position - transform.position + (Vector3)(pl.rb.velocity * (transform.position - pl.transform.position).magnitude / t.VelTiro)).normalized;
                    i++;
                }
                return s;
            }
            else return new Vector2[] { Vector2.zero };

        }
    }

    public void Morrer()
    {
        foreach (var i in Drop) Instantiate(i, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }

    public void Atirar(int TiroNum ,bool Antecipar, bool Girar = false, Vector3 ajuste = new Vector3())
    {
        if (AtiraEne)
        {
            var t = Instantiate(Tirou[TiroNum], transform.position + ajuste, (Girar) ? Quaternion.Euler(0, 0, Mathf.Rad2Deg * (-Mathf.PI * 0.25f + Mathf.Atan2(DireTiro.y, DireTiro.x))) : Quaternion.identity);
            var tt = t.GetComponent<Tiro>();

            if (tt) tt.Dire = (Antecipar) ? DireTiroAdiantado[TiroNum] : DireTiro;
        }
    }

    private void OnBecameVisible()
    {
        AtiraEne = true;
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        AtiraEne = false;
    }
}
