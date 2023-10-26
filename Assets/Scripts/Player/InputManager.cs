using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public enum Map { Menu, Gameplay }
    public static InputManager Instance;
    public static InputMap InputMap { get; private set; }
    //Define the default input map of each scene.
    [SerializeField] private Map _defaultMap;
    private void Awake()
    {
        if (Instance != this && Instance) Destroy(this);
        else Instance = this;
        InputMap ??= new InputMap();
        ChangeMap(_defaultMap);
    }
    //change input map
    public void ChangeMap( Map map )
    {
        InputMap.Disable();
        switch (map)
        {
        case Map.Menu:
            InputMap.Menu.Enable();
            break;
        case Map.Gameplay:
            InputMap.GamePlay.Enable();
            break;
        }
    }
}
