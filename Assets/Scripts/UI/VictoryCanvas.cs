using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryCanvas : MonoBehaviour
{
    [SerializeField] Sprite litStar;
    [SerializeField] private Image[] _stars;
    public void Init( int starsCount )
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
        for (int i = 0; i < starsCount; i++)
        {
            _stars[i].sprite = litStar;
        }
    }
}
