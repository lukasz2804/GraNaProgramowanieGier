using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosDrawLine : MonoBehaviour
{
    public Transform src;
    public Transform trg;

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(src.position, trg.position);
    }
}
