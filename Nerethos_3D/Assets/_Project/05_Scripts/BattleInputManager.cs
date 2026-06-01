using System;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.InputSystem;

/// <summary>
///  this script receives the players input in battle to know what button was pressed
/// Stores the current button pressed and the pattern before attacking
/// Updates the current pattern text game object
/// It sends the finished pattern to battle manager script to continue battle
/// </summary>

public class BattleInputManager : MonoBehaviour
{
    private NerethosInputActions _inputActions; // store the input actions
    
    [SerializeField] private TMP_Text currentPatternText;
    [SerializeField] private BattleManager battleManager; // reference to battle manager to send pattern to it
    
    
    private List<string> _currentPattern = new List<string>(); // create a list of patterns to store in memory
    

    private void Awake()
    {
        _inputActions = new NerethosInputActions();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        
        //IN-BATTLE INPUTS
        _inputActions.Battle.Right.performed += OnRightPressed();
        _inputActions.Battle.Left.performed += OnLeftPressed();
        _inputActions.Battle.Down.performed += OnDownPressed();
        _inputActions.Battle.Up.performed += OnUpPressed();
        
        _inputActions.Battle.AButton.performed += OnAPressed();
        _inputActions.Battle.SButton.performed += OnSPressed();
        _inputActions.Battle.ZButton.performed += OnZPressed();
        _inputActions.Battle.XButton.performed += OnXPressed();
        
        //UI INPUTS - player needs access to ui at all times - remember to map this for controller!
        
        _inputActions.Battle.Clear.performed += ClearPattern;
        _inputActions.Battle.Attack.performed += PerformAttack;
    }

    private void OnDisable()
    {
        _inputActions.Battle.Right.performed -= OnRightPressed();
        _inputActions.Battle.Left.performed -= OnLeftPressed();
        _inputActions.Battle.Up.performed -= OnUpPressed();
        _inputActions.Battle.Down.performed -= OnDownPressed();
        _inputActions.Battle.AButton.performed -= OnAPressed();
        _inputActions.Battle.SButton.performed -= OnSPressed();
        _inputActions.Battle.ZButton.performed -= OnZPressed();
        _inputActions.Battle.XButton.performed -= OnXPressed();
        
        //UI
        
        _inputActions.Battle.Clear.performed -= ClearPattern;
        _inputActions.Battle.Attack.performed -= PerformAttack;
        
        _inputActions.Disable();
        
    }
    
    
    private void ClearPattern(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _currentPattern.Clear();
        currentPatternText.text = "Pattern: ";
        Debug.Log("Current pattern cleared");
    }

    private void PerformAttack(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Debug.Log("Submitted Pattern: " + string.Join(" + ", _currentPattern));
        
        List<string> submittedPattern = new List<string>(_currentPattern); // creates a copy of the list to send to battle manager to avoid mess ups later
        battleManager.ReceivedPattern(submittedPattern);
        
        _currentPattern.Clear();
        currentPatternText.text = "Pattern: ";
        
        //turn ends
        
    }
    
     private Action<InputAction.CallbackContext> OnRightPressed()
    {
        return ctx => AddInput("Right");
    }
     
    private Action<InputAction.CallbackContext> OnLeftPressed()
    {
        return ctx => AddInput("Left");
    }

    private Action<InputAction.CallbackContext> OnDownPressed()
    {
        return ctx => AddInput("Down");
    }

    private Action<InputAction.CallbackContext> OnUpPressed()
    {
        return ctx => AddInput("Up");
    }

    private Action<InputAction.CallbackContext> OnAPressed()
    {
        return ctx => AddInput("A");
    }

    private Action<InputAction.CallbackContext> OnSPressed()
    {
        return ctx => AddInput("S");
    }

    private Action<InputAction.CallbackContext> OnZPressed()
    {
        return ctx => AddInput("Z");
    }

    private Action<InputAction.CallbackContext> OnXPressed()
    {
        return ctx => AddInput("X");
    }
  
    public void AddInput(string inputName)
    {
        int maxPatternLength = battleManager.GetCurrentPatternLength();

        if (_currentPattern.Count >= maxPatternLength)
        {
            Debug.LogWarning("Max pattern length is reached");
            return;
        }
        
        _currentPattern.Add(inputName);
        currentPatternText.text = "Pattern: " + string.Join(" + ", _currentPattern);
        Debug.Log("Current pattern: " + string.Join(" + ", _currentPattern));
        
    }

    
}
