using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    //heart icon prefab for instantiate
    [SerializeField] GameObject _heart;
    //parent of intantiated heart.
    [SerializeField] Transform _heartDisplay;
    //position to start instantiating hearts icon. 
    [SerializeField] Transform _heartStartPoint;
    [SerializeField] int _offset = 100;
    //for changing colour when take damage.
    List<Image> _heartImage = new();

    //original color
    [SerializeField] Color _heartColor;
    //damaged
    [SerializeField] Color _heartColorDarken;
    
    private IEnumerator Start()
    {
        for (int i = 0; i < Player.MaxHealth; i++)
        {
            GameObject temp = Instantiate(_heart, _heartDisplay);
            temp.transform.localPosition = new Vector3(_heartStartPoint.localPosition.x + _offset * i, _heartStartPoint.localPosition.y, 0);
            _heartImage.Add(temp.GetComponent<Image>());
        }
        while (!Player.Instance) yield return null;
        Player.Instance.DamageEvent.AddListener(OnDamage);
    }
    void OnDamage()
    {
        for (int i = 0; i < Player.MaxHealth; i++)
        {
            if (i < Player.Instance.Health - 1) _heartImage[i].color = _heartColor;
            else _heartImage[i].color = _heartColorDarken;
        }
    }
}
