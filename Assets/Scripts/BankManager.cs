using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankManager : MonoBehaviour
{
    [SerializeField] private FloatReference bankCurrent;
    [SerializeField] private FloatReference unitValue;
    [SerializeField] private GameEvent updateBankUI;

    public void IncreaseBank()
    {
        bankCurrent.Variable.ApplyChange(unitValue);
        updateBankUI.Raise();
    }
}
