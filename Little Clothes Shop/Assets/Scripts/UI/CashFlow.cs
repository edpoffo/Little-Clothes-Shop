using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CashFlow : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator _animator;
    [SerializeField] private Text flowText;
    private static readonly int Value = Animator.StringToHash("Value");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Flow(float value)
    {
        flowText.text = value.ToString("n2");
        _animator.SetFloat(Value, value);
    }
}