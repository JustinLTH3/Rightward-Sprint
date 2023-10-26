using UnityEngine;

[CreateAssetMenu(fileName = "LevelData")]
public class LevelData : ScriptableObject
{
    public int Level { get => _level; }
    [SerializeField] private int _level = 0;

    //a screenshot of the level
    public Sprite Screenshot { get => _screenshot; }
    [SerializeField] Sprite _screenshot;
    public int BuildLevelIndex { get => _buildLevelIndex; }
    [SerializeField] private int _buildLevelIndex;
}
