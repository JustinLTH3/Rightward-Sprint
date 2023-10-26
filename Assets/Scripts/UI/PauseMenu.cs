using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private void Start()
    {
        InputManager.InputMap.GamePlay.Esc.performed += Esc_performed;
        gameObject.SetActive(false);
    }

    private void Esc_performed( UnityEngine.InputSystem.InputAction.CallbackContext obj )
    {
        //Activate/Deactivate Pause menu.
        gameObject.SetActive(!gameObject.activeSelf);
        Time.timeScale = 1 - Time.timeScale;
    }

    private void OnDestroy()
    {
        InputManager.InputMap.GamePlay.Esc.performed -= Esc_performed;
    }

}
