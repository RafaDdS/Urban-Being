using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDeSom : MonoBehaviour
{

    public static GerenciadorDeSom Instan;

    [SerializeField]
    List<AudioClip> Sons;

    List< AudioSource> Aso = new List<AudioSource>();

    void Start()
    {
        Instan = this;
        DontDestroyOnLoad(this);
        Aso.AddRange( GetComponentsInChildren<AudioSource>());
        for (int i = 0; i < Sons.Count; i++) Aso[i].clip = Sons[i];

        PlayLoop(9);
    }

    public static void Play(int n)
    {
        Instan.Aso[n].Play();
    }

    public static void PlayLoop(int n)
    {
        Instan.Aso[n].loop = true;
        Instan.Aso[n].Play();
    }

    public static void Stop(int n)
    {
        Instan.Aso[n].loop = false;
    }

    public static void StopNow(int n)
    {
        Instan.Aso[n].loop = false;
        Instan.Aso[n].Stop();
    }
}
