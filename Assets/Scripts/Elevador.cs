using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevador : MonoBehaviour
{
    [SerializeField]
    Animator Fade;

    [SerializeField]
    int preco;

    float maxY;

    void Start()
    {
        Fade.Play("FadeOut");
    }

    void Update()
    {
        if (Player.Instan)
            if (Player.Instan.transform.position.y > maxY)
            {
                maxY = Player.Instan.transform.position.y;
                if (Player.EleSeguir) transform.position += 5 * Time.deltaTime * Vector3.up * (Player.Instan.transform.position.y - transform.position.y);
                if (Player.Instan.transform.position.y - transform.position.y < 1.5f) Player.EleSeguir = false;
            }

    }

    private void OnTriggerStay2D(Collider2D ou)
    {
        if (Input.GetKeyDown(KeyCode.E))
            if (Player.Instan)
                if (Player.Instan.transform.position.y < 0)
                {
                    Fade.Play("FadeInFadeOut");
                    Invoke("Subir", 1f);
                }
                else
                {
                    Fade.Play("FadeInFadeOut");
                    Invoke("Descer", 1f);
                }

    }

    void Subir() { Mover(maxY); }

    void Descer() {
        if (Player.Instan.Dinheiro >= preco) Player.Instan.Dinheiro -= preco;
        else return;
        GerenciadorDeSom.Play(1);
        Mover(-5f);
    }

    void Mover(float pos)
    {
        Player.Instan.transform.position = new Vector2(Player.Instan.transform.position.x, pos);
        transform.position = new Vector2(transform.position.x, pos);
        
    }
}
