using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Player : MonoBehaviour
{
    public static Player Instan;
    public static bool EleSeguir;

    // Movimentação
    public Camera main;

    [Space]
    [Header("Movimentação")]
    [SerializeField]
    float acel, MaxVelocidade;
    float maxVel;

    [Space]
    [Header("Tiro")]
    [SerializeField]
    GameObject Tiro;

    [Space]
    [Header("Geral")]
    [SerializeField]
    int MaxVida, framesInv;

    [SerializeField]
    float tempoAumentaStresse, TempoPerdaVida;


    bool PerdendoVida;
    [SerializeField]
    int _vida, _estresse, _agitacao = 6, _dinheiro, Inv;
    Vector2 vec;
    [HideInInspector]
    public Rigidbody2D rb;
    SpriteRenderer sp;

    [HideInInspector]
    public int _dinheiroEmCasa;

    public TMPro.TextMeshPro Texto;

    public int DinheiroEmCasa
    {
        get { return _dinheiroEmCasa; }
        set
        {
            _dinheiroEmCasa = value;
            Interface.AtuDinCasa(value);
        }
    }

    Vector2 DireTiro
    {
        get
        {
            return (main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        }
    }

    public int Vida
    {
        get { return _vida; }
        set {
            if (enabled)
            {
                var v = value - _vida;

                if (v < 0)
                {
                    if (Inv > 0) return;
                    GerenciadorDeSom.Play(7);
                    Inv = framesInv;
                    _vida = value;
                    Estresse++;
                }
                else
                    if ((!(_vida % 3 == 0) && v == 1) && !(value > MaxVida)) _vida = value;

                var n = (_vida == 0) ? 0 : ((_vida < 2) ? 1 : ((_vida < 3) ? 2 : ((_vida < 4) ? 3 : ((_vida < 7) ? 4 : ((_vida < 9) ? 5 : 6)))));

                Interface.AtualizarCoracao(n);

                if (value == 0) Morrer();
            }
        }
    }

    public int Estresse
    {
        get { return _estresse; }

        set
        {
            if (enabled)
            {
                if (value > _estresse && value < 10) Agitacao++;

                if (value <= 0)
                    _estresse = 0;
                else if (value >= 15)
                    _estresse = 15;
                else
                    _estresse = value;

                if (_estresse >= 10) StartCoroutine("PerderVidaEstresse");
                else PerdendoVida = false;

                var n = (_estresse == 0) ? 0 : ((_estresse < 3) ? 1 : ((_estresse < 7) ? 2 : ((_estresse < 9) ? 3 : ((_estresse < 10) ? 4 : ((_estresse < 15) ? 5 : 6)))));

                Interface.AtualizarCerebro(n);
            }
        }
    }

    public int Agitacao
    {
        get { return _agitacao; }
        set
        {
            if (enabled)
            {
                if (value <= 0)
                    _agitacao = 0;
                else if (value >= 21)
                    _agitacao = 21;
                else
                    _agitacao = value;

                maxVel = MaxVelocidade + _agitacao - 6;

                var n = (_agitacao < 6) ? 0 : ((_agitacao < 9) ? 1 : ((_agitacao < 12) ? 2 : ((_agitacao < 15) ? 3 : ((_agitacao < 18) ? 4 : ((_agitacao < 21) ? 5 : 6)))));

                Interface.AtualizarPumao(n);
            }
        }
    }

    public int Dinheiro
    {
        get { return _dinheiro;  }
        set
        {
            _dinheiro = value;
            Interface.AtuDin(value);
        }
    }

    public void AddBlock() { if (_vida < MaxVida) _vida += 1; }

    void Morrer() { enabled = false; Interface.AbrirMenuRanking(); }

    void Awake()
    {
        if (Instan == null) Instan = this;
        else Destroy(gameObject);

        _vida = MaxVida;

        Texto = GetComponentInChildren<TMPro.TextMeshPro>();

        maxVel = MaxVelocidade;

        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();

        StartCoroutine("AumentarStress");
        
    }

    IEnumerator AumentarStress() {
        while (true)
        {
            yield return new WaitForSecondsRealtime(tempoAumentaStresse);
            Estresse++;
        }
    }

    IEnumerator PerderVidaEstresse()
    {
        PerdendoVida = true;
        while (PerdendoVida)
        {
            if(Vida > 1) Vida--;
            Agitacao++;
            yield return new WaitForSecondsRealtime(TempoPerdaVida);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Frames de Invencibilidade

        if (Inv > 0) Inv--;

        if (Inv % 4 == 0) sp.enabled = !sp.enabled;

        if (Inv == 0) sp.enabled = true;

        

        // Movimentação
        int x = 0, y = 0;

        if (Input.GetKey(KeyCode.W)) y++;
        if (Input.GetKey(KeyCode.A)) x--;
        if (Input.GetKey(KeyCode.S)) y--;
        if (Input.GetKey(KeyCode.D)) x++;


        vec = new Vector2(x, y);

        // Tiro
        
        if (Input.GetMouseButtonDown(0)) Atirar();

        if ((main.ScreenToWorldPoint(Input.mousePosition) - transform.position).x > 0) sp.flipX = true;
        else sp.flipX = false;
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude < maxVel) rb.velocity += vec.normalized * acel;
    }


    void Atirar()
    {
        var t = Instantiate(Tiro, transform.position, Quaternion.Euler(0,0, Mathf.Rad2Deg * (Mathf.PI *0.5f + Mathf.Atan2(DireTiro.y, DireTiro.x))));
        var tt = t.GetComponent<Tiro>();

        GerenciadorDeSom.Play(6);

        if (tt) tt.Dire = DireTiro;   
    }

    private void OnTriggerEnter2D(Collider2D ou)
    {
        var c = ou.GetComponent<Moeda>();
        if (c)
        {
            Dinheiro += c.Valor;
            Interface.AtuDin(Dinheiro);
            GerenciadorDeSom.Play(4);
            Destroy(c.gameObject);
        }

        if (ou.CompareTag("Check")) EleSeguir = true;
    }

}
