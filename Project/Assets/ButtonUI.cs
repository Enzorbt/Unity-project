using System;
using System.Collections;
using System.Collections.Generic;
using Supinfo.Project.Scripts.Events;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour
{
    [SerializeField] private GameObject unit;
    [SerializeField] private GameEvent onClick;
    private Button _button;

    private void Start()
    {
        _button = GetComponentInChildren<Button>();
    }

    public void OnClick()
    {
        onClick.Raise(this, unit);
    }
}