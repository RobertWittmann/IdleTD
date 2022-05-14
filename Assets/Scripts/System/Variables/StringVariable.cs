
using UnityEngine;


[CreateAssetMenu(fileName = "String Variable", menuName = "Variables/String")]
public class StringVariable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif
    public string DefaultValue;

    private string currentValue;

    public string CurrentValue
    {
        get { return currentValue; }
        set { currentValue = value; }
    }

    private void OnEnable()
    {
        currentValue = DefaultValue;
    }

    public void SetValue(string value)
    {
        currentValue = value;
    }

    public void SetValue(StringVariable value)
    {
        currentValue = value.currentValue;
    }

    public void ApplyChange(string amount)
    {
        currentValue += amount;
    }

    public void ApplyChange(StringVariable amount)
    {
        currentValue += amount.currentValue;
    }
}