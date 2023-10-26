using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelBtn : MonoBehaviour
{
    LevelData _level;
    [SerializeField] private Image _cover;
    [SerializeField] private Image[] _stars;
    [SerializeField] private TMP_Text _levelName;
    [SerializeField] private Sprite _litStar;
    [SerializeField] private GameObject _lock;
    Button _button;
    public void Init( LevelData level, Vector3 pos )
    {
        _level = level;
        gameObject.name = _level.name + "Btn";
        Reload();
        transform.localPosition = pos;
    }
    //The level will be locked if the player doesn't gain any star in previous level
    bool IsLevelLocked()
    {
        if (_level.Level == 0) return false;
        return PlayerPrefs.GetInt(LevelManager.Instance.Levels[_level.Level - 1].name + "Stars", 0) == 0;
    }
    public void Reload()
    {
        _cover.sprite = _level.Screenshot;
        _levelName.text = _level.name;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(delegate
        {
            if (!IsLevelLocked()) LevelManager.LoadLevel(_level);
        });
        for (int i = 0; i < PlayerPrefs.GetInt(_level.name + "Stars", 0); i++)
        {
            _stars[i].sprite = _litStar;
        }
        if (IsLevelLocked()) _lock.SetActive(true);
        else _lock.SetActive(false);
    }
}