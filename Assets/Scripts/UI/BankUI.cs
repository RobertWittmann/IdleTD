using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BankUI : MonoBehaviour
{
    [SerializeField] private FloatReference moneyCurrent;
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateBankUI()
    {
        text.text = $"{moneyCurrent.Value}$";
    }
}
