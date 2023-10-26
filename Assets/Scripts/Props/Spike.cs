using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] int Damage = 1;
    bool _playerIn = false;
    private void OnTriggerEnter2D( Collider2D collider )
    {
        if (collider.GetComponent<Player>() && !_playerIn)
        {
            _playerIn = true;
            StartCoroutine(DamagePlayer());
        }
    }
    private void OnTriggerExit2D( Collider2D collider )
    {
        if (collider.GetComponent<Player>())
        {
            _playerIn = false;
            StopAllCoroutines();
        }
    }
    //damage player every second when player steps on the spike.
    IEnumerator DamagePlayer()
    {
        while (_playerIn)
        {
            Player.Instance.Damage(Damage);
            yield return new WaitForSeconds(1);
        }
    }
}
