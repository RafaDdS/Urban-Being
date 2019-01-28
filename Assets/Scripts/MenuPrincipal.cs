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

    [SerializeField]
    GameObject MenuEntradaNome, LugarDosNomes;

    [SerializeField]
    TextMeshProUGUI TemplateNomeRanking;

    int nPonts;

    List<string> Chaves = new List<string>();
    List<string> Nomes = new List<string>();
    List<int> Ponts = new List<int>();

    bool ft;

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


    public void RankingMenu()
    {
        MenuEntradaNome.gameObject.SetActive(true);
        RecuperarPontuacao();
        EscreverPontuacao();
    }

    void RecuperarPontuacao()
    {
        Chaves = new List<string>();
        Nomes = new List<string>();
        Ponts = new List<int>();

        nPonts = (PlayerPrefs.HasKey("NumeroDePonts")) ? PlayerPrefs.GetInt("NumeroDePonts") : 0;

        for (int i = 0; i < nPonts; i++)
        {
            Chaves.Add(PlayerPrefs.GetString("gjgv" + i.ToString()));
        }

        foreach (var cha in Chaves)
        {
            Nomes.Add(PlayerPrefs.GetString(cha));
            Ponts.Add(PlayerPrefs.GetInt(cha + "i"));
        }

    }

    void EscreverPontuacao()
    {
        if (!ft)
        {
            ft = true;
            for (int i = 0; i < nPonts; i++)
            {
                var o = Instantiate(TemplateNomeRanking.gameObject, LugarDosNomes.transform);

                o.transform.position += i * 50 * Vector3.down;

                var te = o.GetComponent<TextMeshProUGUI>();

                te.text = (i + 1) + ". " + Nomes[i] + " <line-height=1>\n<align=right> " + Ponts[i] + " <line-height=1 > </align>";

            }
            Destroy(TemplateNomeRanking);
        }
    }
}

