using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class MenuPrincipal : MonoBehaviour
{

    [SerializeField]
    Animator fade;

    static MenuPrincipal Instan;


    void Start()
    {
        if (Instan == null) Instan = this;
        else Destroy(gameObject);
    }

    public void Louda()
    {
        fade.Play("FadeIn");
        GerenciadorDeSom.Play(10);
        Invoke("Louda2", 1f);
    }

    void Louda2()
    {
        GerenciadorDeSom.Play(8);
        GerenciadorDeSom.Stop(9);
        SceneManager.LoadScene(1);
        
    }

   
}

