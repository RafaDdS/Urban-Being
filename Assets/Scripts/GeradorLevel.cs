using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorLevel : MonoBehaviour
{
    static GeradorLevel Instan;
    static float  DisNovoLevel;

    [SerializeField]
    float limiteX, limiteY;

    [SerializeField]
    List<GameObject> Inimigos;

    [SerializeField]
    List<Vector2> InimigosMinMax;

    [SerializeField]
    GameObject Moeda, Apart, CheckPoint;

    [SerializeField]
    int MoedaMin, MoedaMax;

    [Header("Gerar Novos Leveis")]

    [SerializeField]
    float GerarNovoLevelDist;
    [SerializeField]
    List<GameObject> Levels;

    Vector3 QualLug
    {
        get
        {
            return new Vector3(Random.Range(-limiteX, limiteX), Random.Range(2, limiteY)) + transform.position;
        }
    }

    IEnumerator GerarNovo()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            if (Player.Instan.transform.position.y > DisNovoLevel)
            {
                DisNovoLevel += GerarNovoLevelDist;
                // Mudar ----------------------
                Instantiate(Levels[0], Vector3.up * DisNovoLevel, Quaternion.identity);
            }
        }
    }

    void Start()
    {
        if (Instan == null)
        {
            Instan = this;
            StartCoroutine("GerarNovo");
            return;
        }

        Instantiate(Apart, QualLug, Quaternion.identity, transform);

        Instantiate(CheckPoint, Vector3.up * QualLug.y, Quaternion.identity, transform);

        for (int i = 0; i < Random.Range(MoedaMin, MoedaMax); i++) Instantiate(Moeda, QualLug, Quaternion.identity, transform);

        for (int i = 0; i < Inimigos.Count; i++)
            for (int j = 0; j < Random.Range(InimigosMinMax[i].x, InimigosMinMax[i].y); j++)
                Instantiate(Inimigos[i], QualLug, Quaternion.identity, transform);
    }

}
