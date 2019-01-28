using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class Interface : MonoBehaviour
{
    static Interface Instan;

    [SerializeField]
    GameObject MenuEntradaNome, PlanoRecordes, LugarDosNomes;

    [SerializeField]
    TMP_InputField RankingInput;

    [SerializeField]
    TextMeshProUGUI TemplateNomeRanking;

    //[SerializeField]
    //Button ;

    [Space]

    [SerializeField]
    List<Sprite> Coracao, Cerebro, Pumao;

    [SerializeField]
    Sprite Morto;

    [SerializeField]
    Image CoracaoGO, CerebroGO, PumaoGO, Direita;

    int nPonts;

    List<string> Chaves = new List<string>();
    List<string> Nomes = new List<string>();
    List<int> Ponts = new List<int>();

    [Space]
    [Space]
    [Space]
    [SerializeField]
    Image VidaBarra, EstrBarra, AgitBarra;

    [SerializeField]
    List<Sprite> VidaBarrasp, EstrBarrasp, AgitBarrasp;

    [SerializeField]
    TextMeshProUGUI Din, Dincasa;

    void Awake()
    {
        if (Instan == null) Instan = this;
        else Destroy(gameObject);
        //PlayerPrefs.DeleteAll();
    }

    public void Restart() {
        GerenciadorDeSom.Play(10);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void AtualizarCoracao(int n) {
        Instan.CoracaoGO.sprite = Instan.Coracao[n];
        if (n == 0) Instan.Direita.sprite = Instan.Morto;

        Instan.VidaBarra.sprite = Instan.VidaBarrasp[Player.Instan.Vida];
    }
    public static void AtualizarCerebro(int n) { Instan.CerebroGO.sprite = Instan.Cerebro[n];

        Instan.EstrBarra.sprite = Instan.EstrBarrasp[Player.Instan.Estresse];
    }
    public static void AtualizarPumao(int n) { Instan.PumaoGO.sprite = Instan.Pumao[n];

        Instan.AgitBarra.sprite = Instan.AgitBarrasp[Player.Instan.Agitacao];
    }

    public static void AtuDin(int n)
    {
        Instan.Din.text = n.ToString();
    }
    public static void AtuDinCasa(int n)
    {
        Instan.Dincasa.text = n.ToString();

    }

    public static void AbrirMenuRanking()
    {    
        Instan.MenuEntradaNome.SetActive(true);
        Instan.RankingInput.Select();
    }


    public void BotEntradaNome(string no)
    {
        if (no.Length < 3) return;

        RankingInput.gameObject.SetActive(false);
        PlanoRecordes.gameObject.SetActive(true);
        RecuperarPontuacao();
        SalvarPontuacao(no);
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

        //Nomes.Reverse();
        //Ponts.Reverse();
    }

    void SalvarPontuacao(string Nome)
    {
        var k = (Time.time * (1 + Random.value)).ToString();

        var Pos = Ponts.Where(a => a >= Player.Instan.DinheiroEmCasa ).Count();

        nPonts++;
        Chaves.Insert(Pos, k);
        Nomes.Insert(Pos, Nome);
        Ponts.Insert(Pos, Player.Instan.DinheiroEmCasa);

        PlayerPrefs.SetInt("NumeroDePonts", nPonts);

        for (int i = 0; i < nPonts; i++)
        {
            PlayerPrefs.SetString("gjgv" + i.ToString(), Chaves[i]);
            PlayerPrefs.SetString(Chaves[i], Nomes[i]);
            PlayerPrefs.SetInt(Chaves[i] + "i", Ponts[i]);
        }

        PlayerPrefs.Save();
    }

    void EscreverPontuacao()
    {
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
