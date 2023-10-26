using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    List<LevelData> _levels;
    public List<LevelData> Levels { get => _levels; }

    [SerializeField] GameObject _loadLevelCanvas;

    private void Awake()
    {
        if (Instance != this && Instance) Destroy(gameObject);
        else Instance = this;
        DontDestroyOnLoad(gameObject);
        _levels = Resources.LoadAll<LevelData>("Levels/").ToList();
    }
    public static void LoadLevel( LevelData level )
    {
        Instance.StartCoroutine(LoadLevel(SceneManager.LoadSceneAsync(level.BuildLevelIndex)));
        InputManager.Instance.ChangeMap(InputManager.Map.Gameplay);
    }
    private static IEnumerator LoadLevel( AsyncOperation loading )
    {
        //The loading screen when loading.
        GameObject loadLevelCanvas = null;
        Slider loadingBar = null;
        
        while (!loading.isDone) //when loading
        {
            if (!loadLevelCanvas)//Instantiate the canvas if it is not yet instantiated.
            {
                loadLevelCanvas = Instantiate(Instance._loadLevelCanvas, Vector3.zero, Quaternion.identity);
                loadingBar = loadLevelCanvas.transform.Find("LoadingBar").GetComponent<Slider>();
            }
            //The state of the loading progress. 1 - loading.progress made the bar increase from the left.
            loadingBar.value = 1 - loading.progress;
            yield return null;
        }
        //destroy it after the level is loaded.
        if (loadLevelCanvas) Destroy(loadLevelCanvas);
    }
    public static void LoadMainmenu()
    {
        Instance.StartCoroutine(LoadMainmenuCoroutine());
        Time.timeScale = 1.0f;
    }
    private static IEnumerator LoadMainmenuCoroutine()
    {
        yield return Instance.StartCoroutine(LoadLevel(SceneManager.LoadSceneAsync(0)));
        InputManager.Instance.ChangeMap(InputManager.Map.Menu);
    }
}
