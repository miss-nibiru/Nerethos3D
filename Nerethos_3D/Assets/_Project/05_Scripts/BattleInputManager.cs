using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.InputSystem;

/// <summary>
///  this script receives the players input in battle to know what button was pressed
/// Stores the current button pressed and the pattern before attacking
/// </summary>

public class BattleInputManager : MonoBehaviour
{
    
    private NerethosInputActions _inputActions; // store the input actions

    [SerializeField] private TMP_Text currentPatternText;
    [SerializeField] private BattleManager battleManager; // reference to battle manager to send pattern to it
    private bool _canReceivePatternInput;

    private List<PlayerAttackData.InputActionType>
        _currentPattern = new List<PlayerAttackData.InputActionType>(); // create a list of patterns to store in memory


    private void Awake()
    {
        InitializeInputActions();
    }

    private void OnEnable()
    {
        InitializeInputActions();
        _inputActions.Enable();

        //IN-BATTLE INPUTS
       
        _inputActions.Battle.Right.performed += OnRightPressed;
        _inputActions.Battle.Left.performed += OnLeftPressed;
        _inputActions.Battle.Down.performed += OnDownPressed;
        _inputActions.Battle.Up.performed += OnUpPressed;

        _inputActions.Battle.AButton.performed += OnAPressed;
        _inputActions.Battle.SButton.performed += OnSPressed;
        _inputActions.Battle.ZButton.performed += OnZPressed;
        _inputActions.Battle.XButton.performed += OnXPressed;

        //UI INPUTS - player needs access to ui at all times - remember to map this for controller!

        _inputActions.Battle.Clear.performed += ClearPattern;
        _inputActions.Battle.Attack.performed += PerformAttack;
        
    }

    private void OnDisable()
    {
        if (_inputActions == null) return;
        
        _inputActions.Battle.Right.performed -= OnRightPressed;
        _inputActions.Battle.Left.performed -= OnLeftPressed;
        _inputActions.Battle.Up.performed -= OnUpPressed;
        _inputActions.Battle.Down.performed -= OnDownPressed;
        _inputActions.Battle.AButton.performed -= OnAPressed;
        _inputActions.Battle.SButton.performed -= OnSPressed;
        _inputActions.Battle.ZButton.performed -= OnZPressed;
        _inputActions.Battle.XButton.performed -= OnXPressed;

        _inputActions.Battle.Clear.performed -= ClearPattern;
        _inputActions.Battle.Attack.performed -= PerformAttack;

        _inputActions.Disable();

    }

    private void InitializeInputActions()
    {
        _inputActions ??= new NerethosInputActions();
    }
    
    public void EnablePatternInput()
    {
        _canReceivePatternInput = true;
        Debug.Log("Pattern input enabled.");
    }

    public void DisablePatternInput()
    {
        _canReceivePatternInput = false;
        Debug.Log("Pattern input disabled.");
    }


    private void ClearPattern(InputAction.CallbackContext context)
    {
        _currentPattern.Clear();
        currentPatternText.text = "Pattern: ";
        Debug.Log("Current pattern cleared");
    }

    private void PerformAttack(InputAction.CallbackContext context)
    {
        if(!_canReceivePatternInput) return; //dont be alive if cant receive pattern
        
        Debug.Log("Submitted Pattern: " + string.Join(" + ", _currentPattern));

        List<PlayerAttackData.InputActionType> submittedPattern =
            new List<PlayerAttackData.InputActionType>(_currentPattern);

        battleManager.ReceivedPattern(submittedPattern);

        _currentPattern.Clear();
        currentPatternText.text = "Pattern: ";
    }

    private void OnRightPressed(InputAction.CallbackContext ctx)
    {
        AddInput(PlayerAttackData.InputActionType.Right);
    }

    private void OnLeftPressed(InputAction.CallbackContext ctx)
    {
        AddInput(PlayerAttackData.InputActionType.Left);
    }

    private void OnUpPressed(InputAction.CallbackContext ctx)
    {
        AddInput(PlayerAttackData.InputActionType.Up);
    }

    private void OnDownPressed(InputAction.CallbackContext ctx)
    {
        AddInput(PlayerAttackData.InputActionType.Down);
    }

    private void OnAPressed(InputAction.CallbackContext ctx)
    {
        AddInput(PlayerAttackData.InputActionType.A);
    }
    
    private void OnSPressed(InputAction.CallbackContext ctx)
    {
        AddInput(PlayerAttackData.InputActionType.S);
    }
    
    private void OnZPressed(InputAction.CallbackContext ctx)
    {
        AddInput(PlayerAttackData.InputActionType.Z);
    }

    private void OnXPressed(InputAction.CallbackContext ctx)
    {
        AddInput(PlayerAttackData.InputActionType.X);
    }
    

    public void AddInput(PlayerAttackData.InputActionType inputName)
    {
        
        if (!_canReceivePatternInput) return;
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
