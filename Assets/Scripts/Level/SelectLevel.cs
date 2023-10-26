using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevel : MonoBehaviour
{
    public static SelectLevel Instance;
    [SerializeField] Transform _frame;
    [SerializeField] GameObject _levelBtn;
    [SerializeField] Transform _levelBtnStartPoint;
    [SerializeField] GameObject _mainMenu;
    readonly float _offset = 400;
    private void Start()
    {
        for (int i = 0; i < LevelManager.Instance.Levels.Count; i++)
        {
            //Instantiate a LevelBtn for Player to click to select the level.
            Instantiate(_levelBtn, _frame)
                .GetComponent<LevelBtn>()
                .Init(LevelManager.Instance.Levels[i],
                new Vector3(_levelBtnStartPoint.localPosition.x + _offset * (i % 3) + 50, _levelBtnStartPoint.localPosition.y + 175 * ((i / 3) > 0 ? -1 : 1), 0));
        }
        InputManager.InputMap.Menu.Esc.performed += Esc_performed;
    }
    private void Esc_performed( UnityEngine.InputSystem.InputAction.CallbackContext obj )
    {
        if (gameObject.activeSelf)
        {
            _mainMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        InputManager.InputMap.Menu.Esc.performed -= Esc_performed;
    }
}
