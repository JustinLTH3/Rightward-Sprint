using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] float _speed;
    Camera _camera;
    readonly Vector3 DEFAULTPOS = new(0, 0, -10);

    //Camera Movement when player dies
    //Default size for camera.
    const float _orthSizeDefault = 5;
    //Time for camera to zoom out.
    const float t = 1f;
    //Time after player dies for interpolate when camera zooming out.
    float tempT = 0;

    //For camera have a smooth transformation when focus on the player 
    //the position of player when player dies.
    Vector3? _playerDiePos = null;
    //the position of the camera when player dies.
    Vector3? _playerDieCamPos = null;

    bool _gameoverCanvasShow = false;
    [SerializeField] Canvas _gameoverCanvas;

    bool _playerOutsideScreen = false;

    //Coroutine started when player is outside the screen.
    //Use for Stop the coroutine when the player get back inside the screen.
    Coroutine cor_DamageOutsideScreen;


    void Start()
    {
        transform.position = DEFAULTPOS;
        _camera = Camera.main;
        _camera.orthographicSize = _orthSizeDefault;
    }
    void Update()
    {
        if (Player.Instance.IsAlive)
        {
            MoveCamera();
            /* Start Coroutine DamageOutside() if 
             * player is not inside the screen and the coroutine has not started yet.
             */
            Vector3 playerPosOnScreen = _camera.WorldToScreenPoint(Player.Instance.transform.position);
            if (!_playerOutsideScreen
            && (playerPosOnScreen.x < -1 || playerPosOnScreen.y < -1))
            {
                _playerOutsideScreen = true;
                cor_DamageOutsideScreen = StartCoroutine(DamageOutside());
                return;
            }
            //When player has returned, stop damaging player.
            if (playerPosOnScreen.x >= -1 && playerPosOnScreen.y >= 1
                && _playerOutsideScreen)
            {
                _playerOutsideScreen = false;
                StopCoroutine(cor_DamageOutsideScreen);
            }
            return;
        }
        if (tempT < t)
        {
            _playerDieCamPos ??= new(_camera.transform.position.x, _camera.transform.position.y, -10);
            _playerDiePos ??= new(Player.Instance.transform.position.x, Player.Instance.transform.position.y, -10);
            tempT += Time.deltaTime;
            if (tempT > t)
            {
                tempT = t;
            }
            _camera.orthographicSize = _orthSizeDefault + (Mathf.Cos(0.5f * Mathf.PI - Mathf.PI * tempT / 2)) * 10;
            _camera.transform.position = Vector3.Lerp((Vector3)_playerDieCamPos, (Vector3)_playerDiePos, tempT / t); ;
            return;
        }
        if (!_gameoverCanvasShow)
        {
            _gameoverCanvasShow = true;
            _gameoverCanvas.gameObject.SetActive(true);
            InputManager.Instance.ChangeMap(InputManager.Map.Menu);
        }
    }
    //1 damage every second when player is outside the Screen and is alive.
    IEnumerator DamageOutside()
    {
        while (_playerOutsideScreen && Player.Instance.IsAlive)
        {
            yield return new WaitForSeconds(1);
            Player.Instance.Damage(1);
        }
    }
    void MoveCamera()
    {
        //Camera keep moving to right.
        transform.position += _speed * Time.deltaTime * transform.right;
    }
}
