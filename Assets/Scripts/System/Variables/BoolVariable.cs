
using UnityEngine;


[CreateAssetMenu(fileName = "Bool Variable", menuName = "Variables/Bool")]
public class BoolVariable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif
    public bool DefaultValue = true;

    private bool currentValue;

    public bool CurrentValue
    {
        get { return currentValue; }
        set { currentValue = value; }
    }

    private void OnEnable()
    {
        currentValue = DefaultValue;
    }

    public void SetValue(bool value)
    {
        currentValue = value;
    }

    public void SetValue(BoolVariable value)
    {
        currentValue = value.currentValue;
    }
}