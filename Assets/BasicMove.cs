using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMove : MonoBehaviour
{

    public virtual void Switch()
    {
        enabled = !enabled;
    }
}
