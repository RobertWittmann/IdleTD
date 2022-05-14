
using UnityEngine;


[CreateAssetMenu(fileName = "Float Variable", menuName = "Variables/Float")]
public class FloatVariable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif
    public float DefaultValue;

    [SerializeField] float currentValue;

    public float CurrentValue
    {
        get { return currentValue; }
        set { currentValue = value; }
    }

    private void OnEnable()
    {
        currentValue = DefaultValue;
    }

    public void SetValue(float value)
    {
        currentValue = value;
    }

    public void SetValue(FloatVariable value)
    {
        currentValue = value.currentValue;
    }

    public void ApplyChange(float amount)
    {
        currentValue += amount;
    }

    public void ApplyChange(FloatVariable amount)
    {
        currentValue += amount.currentValue;
    }
}