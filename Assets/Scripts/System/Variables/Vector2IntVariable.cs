
using UnityEngine;


[CreateAssetMenu(fileName = "Vector2Int Variable", menuName = "Variables/Vector2Int")]
public class Vector2IntVariable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif
    public Vector2Int DefaultValue;

    [SerializeField] Vector2Int currentValue;

    public Vector2Int CurrentValue
    {
        get { return currentValue; }
        set { currentValue = value; }
    }

    private void OnEnable()
    {
        currentValue = DefaultValue;
    }

    public void SetValue(Vector2Int value)
    {
        currentValue = value;
    }

    public void SetValue(Vector2IntVariable value)
    {
        currentValue = value.currentValue;
    }

    public void ApplyChange(Vector2Int amount)
    {
        currentValue += amount;
    }

    public void ApplyChange(Vector2IntVariable amount)
    {
        currentValue += amount.currentValue;
    }
}