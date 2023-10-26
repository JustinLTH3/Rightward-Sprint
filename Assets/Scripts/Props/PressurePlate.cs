using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    //can be activated by player or box.
    public bool IsActivated { get => _count > 0; }
    private int _count = 0;
    private void OnTriggerEnter2D( Collider2D collision )
    {
        if (collision.GetComponent<Rigidbody2D>())
            _count++;
    }
    private void OnTriggerExit2D( Collider2D collision )
    {
        if (collision.GetComponent<Rigidbody2D>())
            _count--;
    }
}
