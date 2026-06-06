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
    private enum ActionType
    {
        Attack,
        Override,
        Item,
        Escape
        
    }
    
    private NerethosInputActions _inputActions; // store the input actions

    [SerializeField] private TMP_Text currentPatternText;
    [SerializeField] private BattleManager battleManager; // reference to battle manager to send pattern to it

    [SerializeField] private GameObject[] actionSelectionUI; // this will be used to show the player what actions they can do in their turn - attack, defend, overdrive, item, etc. This will be turned on and off depending on the turn action type
    [SerializeField] private ActionType[] actionTypes;
    
    private int _selectedActionIndex;

    private List<WeaponAttackData.InputActionType>
        _currentPattern = new List<WeaponAttackData.InputActionType>(); // create a list of patterns to store in memory


    private void Awake()
    {
        _inputActions = new NerethosInputActions();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        _selectedActionIndex = 0;
        UpdateActionUI();

        //IN-BATTLE INPUTS
        _inputActions.Battle.Right.performed += OnRightPressed();
        _inputActions.Battle.Left.performed += OnLeftPressed();
        _inputActions.Battle.Down.performed += OnDownPressed();
        _inputActions.Battle.Up.performed += OnUpPressed();

        _inputActions.Battle.AButton.performed += OnAPressed();
        _inputActions.Battle.SButton.performed += OnSPressed();
        _inputActions.Battle.ZButton.performed += OnZPressed();
        _inputActions.Battle.XButton.performed += OnXPressed();

        _inputActions.Battle.EButton.performed += OnEPressed;

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

        _inputActions.Battle.Clear.performed -= ClearPattern;
        _inputActions.Battle.Attack.performed -= PerformAttack;
        
        //UI
        
        _inputActions.Battle.EButton.performed -= OnEPressed;

        _inputActions.Disable();

    }


    private void ClearPattern(InputAction.CallbackContext context)
    {
        _currentPattern.Clear();
        currentPatternText.text = "Pattern: ";
        Debug.Log("Current pattern cleared");
    }

    private void PerformAttack(InputAction.CallbackContext context)
    {
        if (battleManager.CurrentTurnActionType != BattleManager.TurnActionType.PatternInput)
        {
            Debug.Log("Attack submit blocked. Current state: " + battleManager.CurrentTurnActionType);
            return;
        }

        Debug.Log("Submitted Pattern: " + string.Join(" + ", _currentPattern));

        List<WeaponAttackData.InputActionType> submittedPattern =
            new List<WeaponAttackData.InputActionType>(_currentPattern);

        battleManager.ReceivedPattern(submittedPattern);

        _currentPattern.Clear();
        currentPatternText.text = "Pattern: ";
    }

    private Action<InputAction.CallbackContext> OnRightPressed()
    {
        return ctx =>
        {
            switch (battleManager.CurrentTurnActionType)
            {
                case BattleManager.TurnActionType.ActionSelection:
                    MoveActionUI(1);
                    break;
                
                case BattleManager.TurnActionType.PointSelection:
                    battleManager.SelectPoint(1);
                    break;
                
                case BattleManager.TurnActionType.PatternInput:
                    AddInput(WeaponAttackData.InputActionType.Right);
                    break;
            }
        };
        
    }

    private Action<InputAction.CallbackContext> OnLeftPressed()
    {
        return ctx =>
        {
            switch (battleManager.CurrentTurnActionType)
            {
                case BattleManager.TurnActionType.ActionSelection:
                    MoveActionUI(-1);
                    break;
                
                case BattleManager.TurnActionType.PointSelection:
                    battleManager.SelectPoint(-1);
                    break;
                
                case BattleManager.TurnActionType.PatternInput:
                    AddInput(WeaponAttackData.InputActionType.Left);
                    break;
            }
        };
        
    }

    private Action<InputAction.CallbackContext> OnDownPressed()
    {
        return ctx =>
        {
            switch (battleManager.CurrentTurnActionType)
            {
                
                case BattleManager.TurnActionType.ActionSelection:
                    MoveActionUI(-1);
                    break;
                case BattleManager.TurnActionType.PatternInput:
                    AddInput(WeaponAttackData.InputActionType.Down);
                    break;
                
            }
        };
    }

    private Action<InputAction.CallbackContext> OnUpPressed()
    {
        return ctx =>
        {
            switch (battleManager.CurrentTurnActionType)
            {
                
                case BattleManager.TurnActionType.ActionSelection:
                    MoveActionUI(1);
                    break;
                case BattleManager.TurnActionType.PatternInput:
                    AddInput(WeaponAttackData.InputActionType.Up);
                    break;
                
            }
        };
    }

    private Action<InputAction.CallbackContext> OnAPressed()
    {
        
        return ctx =>
        {
            switch (battleManager.CurrentTurnActionType)
            {
                case BattleManager.TurnActionType.PatternInput:
                    AddInput(WeaponAttackData.InputActionType.A);
                    break;
            }
        };
        
    }

    private Action<InputAction.CallbackContext> OnSPressed()
    {
        return ctx =>
        {
            switch (battleManager.CurrentTurnActionType)
            {
                case BattleManager.TurnActionType.PatternInput:
                    AddInput(WeaponAttackData.InputActionType.S);
                    break;
            }
        };
    }

    private Action<InputAction.CallbackContext> OnZPressed()
    {
        return ctx =>
        {
            switch (battleManager.CurrentTurnActionType)
            {
                case BattleManager.TurnActionType.PatternInput:
                    AddInput(WeaponAttackData.InputActionType.Z);
                    break;
            }
        };
    }

    private Action<InputAction.CallbackContext> OnXPressed()
    {
        return ctx =>
        {
            switch (battleManager.CurrentTurnActionType)
            {
                case BattleManager.TurnActionType.PatternInput:
                    AddInput(WeaponAttackData.InputActionType.X);
                    break;
            }
        };
    }
    
    private void OnEPressed(InputAction.CallbackContext ctx)
    {
        BattleManager.TurnActionType stateAtInputStart = battleManager.CurrentTurnActionType;

        switch (stateAtInputStart)
        {
            case BattleManager.TurnActionType.ActionSelection:
                ConfirmActionSelection();
                break;

            case BattleManager.TurnActionType.PointSelection:
                battleManager.ConfirmPointSelection();
                break;
        }
        
    }

    private void ConfirmActionSelection()
    { 
        
        ActionType selectedAction = actionTypes[_selectedActionIndex];
        Debug.Log("Confirmed action selection: " + selectedAction);

        switch (selectedAction)
        {
            
            case ActionType.Attack:
                SetButtonsActive(false);
                battleManager.StartPointSelection();
                break;
            
            case ActionType.Override:
                Debug.Log("Override action selected - not implemented yet");
                break;
            
            case ActionType.Item:
                Debug.Log("Item action selected - not implemented yet");
                break;
            
            case ActionType.Escape:
                Debug.Log("Escape action selected - not implemented yet");
                break;
            
        }
        
    }

    private void SetButtonsActive(bool isActive)
    {
        
        for (int i = 0; i < actionSelectionUI.Length; i++)
        {
            actionSelectionUI[i].SetActive(isActive);
        }
        
    }

    public void AddInput(WeaponAttackData.InputActionType inputName)
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
    
    /// <summary>
    ///  Inputs for the "actions selection" phase of the player's turn
    /// this is where the player selects what action they want to perform in their turn - attack, defend, overdrive, item, etc.
    /// This will be turned on and off depending on the turn action type. This will also update the UI to show the player what action they have selected and what actions are available to them.
    /// </summary>

    private void UpdateActionUI()
    {
        for (int i = 0; i < actionSelectionUI.Length; i++)
        {
            if (i == _selectedActionIndex)
            {
                actionSelectionUI[i].transform.localScale = Vector3.one * (float)1.2; // scale up the selected action
            }

            else
            {
                actionSelectionUI[i].transform.localScale = Vector3.one;
            }
        }
    }

    private void MoveActionUI(int direction)
    {

        _selectedActionIndex += direction;

        if (_selectedActionIndex < 0)
        {
            _selectedActionIndex = actionSelectionUI.Length - 1;
        }
        
        if (_selectedActionIndex >= actionSelectionUI.Length)
        { 
            _selectedActionIndex = 0;
        }
        
        UpdateActionUI();
        
        Debug.Log("Selected action index: " + _selectedActionIndex);

    }

}
