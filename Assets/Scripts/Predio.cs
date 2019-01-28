using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predio : MonoBehaviour
{
    [SerializeField]
    List<Animator> anim;

    void Start()
    {
        foreach (var a in anim) a.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D ou)
    {
        if (ou.transform == Player.Instan.transform) foreach (var a in anim) a.Play("Sumir");
    }
}
