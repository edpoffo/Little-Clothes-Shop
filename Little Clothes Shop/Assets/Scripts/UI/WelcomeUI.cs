using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WelcomeUI : MonoBehaviour
{
    public static WelcomeUI UI;
    public static string PlaceName;

    private Animator _animator;
    [SerializeField] private Text placeNameText;
    private static readonly int Welcome = Animator.StringToHash("Welcome");

    private void Awake()
    {
        UI = this;
        _animator = GetComponent<Animator>();

        gameObject.SetActive(false);
    }

    public static void Play(string name)
    {
        PlaceName = name;
        UI.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        placeNameText.text = PlaceName;
        _animator.SetTrigger(Welcome);
    }
}