
using UnityEngine;


[CreateAssetMenu(fileName = "Int Variable", menuName = "Variables/Int")]
public class IntVariable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif
    public int DefaultValue;

    [SerializeField] int currentValue;

    public int CurrentValue
    {
        get { return currentValue; }
        set { currentValue = value; }
    }

    private void OnEnable()
    {
        currentValue = DefaultValue;
    }

    public void SetValue(int value)
    {
        currentValue = value;
    }

    public void SetValue(IntVariable value)
    {
        currentValue = value.currentValue;
    }

    public void ApplyChange(int amount)
    {
        currentValue += amount;
    }

    public void ApplyChange(IntVariable amount)
    {
        currentValue += amount.currentValue;
    }
}