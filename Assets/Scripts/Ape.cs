using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ape : MonoBehaviour
{
    [SerializeField]
    bool Casa;

    [SerializeField]
    int preco;

    [SerializeField]
    float disInteragir;

    [SerializeField]
    Sprite Comprado, EmUso;

    bool AplicandoStatus;
    GameObject Sec;
    Collider2D col;
    SpriteRenderer sp;
    Animator anim;

    void Start()
    {
        col = GetComponentInChildren<Collider2D>();
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (transform.childCount > 0) Sec = transform.GetChild(0).gameObject;

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && !Sec.activeInHierarchy)
            if (Player.Instan)
                if ((Player.Instan.transform.position - transform.position).magnitude < disInteragir)
                    if (Player.Instan.Dinheiro > preco)
                    {
                        Player.Instan.Dinheiro -= preco;
                        Sec.SetActive(true);
                    }


        if (Casa) {

            if ((Player.Instan.transform.position - transform.position).magnitude > disInteragir)
            {
                if(AplicandoStatus) anim.Play("A");
                sp.sprite = Comprado;
                AplicandoStatus = false;
                GerenciadorDeSom.StopNow(3);
                StopCoroutine("StatusCasa");
            }
            else
            {
                if (Player.Instan.Dinheiro > 0)
                {
                    Player.Instan.DinheiroEmCasa = Player.Instan.Dinheiro;
                    Player.Instan.Dinheiro = 0;
                    
                }
                anim.enabled = true;
                if (!AplicandoStatus)
                {
                    GerenciadorDeSom.Play(3);
                    anim.Play("Casa");
                    StartCoroutine("StatusCasa");
                }
            }

        }
        else if (Sec.activeInHierarchy)
        {
            if ((Player.Instan.transform.position - transform.position).magnitude > disInteragir)
            {
                sp.sprite = Comprado;
                AplicandoStatus = false;
                GerenciadorDeSom.StopNow(3);
                StopCoroutine("StatusCasa");
            }
            else
            {
                sp.sprite = EmUso;
                if (!AplicandoStatus)
                {
                    GerenciadorDeSom.Play(3);
                    StartCoroutine("StatusCasa");
                }
            }

           
        }
    }

    IEnumerator StatusCasa()
    {
        AplicandoStatus = true;
        while (AplicandoStatus)
        {
            yield return new WaitForSecondsRealtime(2f);
            
            if (Casa)
            {
                if ((Player.Instan.Vida % 3 == 2) && (Player.Instan.Vida < 9)) Player.Instan.AddBlock();
                else Player.Instan.Vida++;

                Player.Instan.Estresse -= 3;

                Player.Instan.Agitacao -= 3;
            }
            else
            {
                Player.Instan.Vida++;

                Player.Instan.Agitacao -= 3;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform == Player.Instan.transform) Player.Instan.Texto.text = preco.ToString();

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform == Player.Instan.transform) Player.Instan.Texto.text = "";
    }
}
