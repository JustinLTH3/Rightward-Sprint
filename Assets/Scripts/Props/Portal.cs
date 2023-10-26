using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    //this is a two way portal.

    public bool IsTeleporting { get; private set; }

    //Not instantly teleported to destination and prevent teleport loop.
    readonly WaitForSeconds _delay = new(.4f);
    //the other portal as the destination.
    [SerializeField] Portal _target;
    public IEnumerator Teleport()
    {
        IsTeleporting = true;
        Player.Instance.gameObject.SetActive(false);
        yield return _delay;
        Player.Instance.transform.position = _target.transform.position;
        Player.Instance.gameObject.SetActive(true);
        yield return _delay;
        IsTeleporting = false;
    }
    private void OnTriggerEnter2D( Collider2D collision )
    {
        if (_target.IsTeleporting) return;
        if (collision.GetComponent<Player>()) StartCoroutine(Teleport());
    }
}
