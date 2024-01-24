using System;
using System.Collections;
using System.Collections.Generic;
using Supinfo.Project.Scripts.Events;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

using UnityEngine;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour
{
    /// <summary>
    /// The GameObject representing the unit associated with this button.
    /// </summary>
    [SerializeField] private GameObject unit;
    
    /// <summary>
    /// Event to be raised when the button is clicked.
    /// </summary>
    [SerializeField] private GameEvent onClick;

    /// <summary>
    /// Reference to the Button component.
    /// </summary>
    private Button _button;

    /// <summary>
    /// Start is called before the first frame update.
    /// It initializes the Button component.
    /// </summary>
    private void Start()
    {
        _button = GetComponentInChildren<Button>();
    }

    /// <summary>
    /// Method to be called when the button is clicked.
    /// Raises the onClick event with the associated unit.
    /// </summary>
    public void OnClick()
    {
        onClick.Raise(this, unit);
    }
}
