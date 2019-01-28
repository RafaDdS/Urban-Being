using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailDestroy : MonoBehaviour
{
    TrailRenderer tr;
    EdgeCollider2D ec;

    private void Start()
    {
        tr = GetComponentInChildren<TrailRenderer>();
        ec = GetComponentInChildren<EdgeCollider2D>();
    }

    private void Update()
    {
        Vector2[] gh = new Vector2[tr.positionCount];
        for (int i = 0; i < tr.positionCount; i++) gh[i] = transform.InverseTransformPoint(tr.GetPosition(i));

        ec.points = gh;

        if (!transform.parent)
            if (tr.positionCount < 5)
                Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D ou)
    {
  
        if (ou.transform == Player.Instan.transform) { Player.Instan.Vida--; Destroy(gameObject); }
    }
}
