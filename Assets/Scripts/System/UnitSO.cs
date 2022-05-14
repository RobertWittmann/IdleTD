using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "Unit/New Unit", order = 0)]
public class UnitSO : ScriptableObject
{
    public string _unitName;
    public GameObject _unitPrefab;
    public Sprite _sprite;
    public float _moveInterval = 0.5f;
    public Vector2Int _spawnLocation;
}