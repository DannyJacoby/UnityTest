using System;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
