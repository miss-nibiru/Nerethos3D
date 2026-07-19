using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem;

/// <summary>
///  this script receives the players input in battle to know what button was pressed
/// Stores the current button pressed and the pattern before attacking
/// </summary>

public class BattleInputManager : MonoBehaviour
{
    [Header("Pattern Icons")]
    [SerializeField] private Image[] inputSlots = new Image[5];

    [SerializeField] private Sprite arrowIcon;
    [SerializeField] private Sprite aIcon;
    [SerializeField] private Sprite sIcon;
    [SerializeField] private Sprite zIcon;
    [SerializeField] private Sprite xIcon;
    
    private NerethosInputActions _inputActions; // store the input actions
    [SerializeField] private BattleManager battleManager; // reference to battle manager to send pattern to it
    
    private bool _canReceivePatternInput;
    private List<PlayerAttackData.InputActionType> _currentPattern = new List<PlayerAttackData.InputActionType>(); // create a list of patterns to store in memory


    private void Awake()
    {
        InitializeInputActions();
        RefreshPatternIcons();
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
        gameObject.SetActive(true);
        _currentPattern.Clear();
        RefreshPatternIcons();

        _canReceivePatternInput = true;
        Debug.Log("Pattern input enabled.");
    }

    public void DisablePatternInput()
    {
        _canReceivePatternInput = false;
        _currentPattern.Clear();
        RefreshPatternIcons();

        gameObject.SetActive(false);
        Debug.Log("Pattern input disabled.");
    }


    private void ClearPattern(InputAction.CallbackContext context)
    {
        _currentPattern.Clear();
        RefreshPatternIcons();
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
        RefreshPatternIcons();
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
        RefreshPatternIcons();
        Debug.Log("Current pattern: " + string.Join(" + ", _currentPattern));

    }
    
    private void RefreshPatternIcons()
    {
        for (int i = 0; i < inputSlots.Length; i++)
        {
            if (inputSlots[i] == null) continue;

            bool hasInput = i < _currentPattern.Count;
            inputSlots[i].gameObject.SetActive(hasInput);

            if (!hasInput) continue;
            PlayerAttackData.InputActionType input = _currentPattern[i];
            inputSlots[i].sprite = GetInputIcon(input);
            inputSlots[i].rectTransform.localRotation = Quaternion.Euler(0f, 0f, GetInputRotation(input));
        }
    }

    private Sprite GetInputIcon(PlayerAttackData.InputActionType input)
    {
        switch (input)
        {
            case PlayerAttackData.InputActionType.A: return aIcon;
            case PlayerAttackData.InputActionType.S: return sIcon;
            case PlayerAttackData.InputActionType.Z: return zIcon;
            case PlayerAttackData.InputActionType.X: return xIcon;
            default: return arrowIcon;
        }
    }

    private float GetInputRotation(PlayerAttackData.InputActionType input)
    {
        switch (input)
        {
            case PlayerAttackData.InputActionType.Right: return -90f;
            case PlayerAttackData.InputActionType.Down: return 180f;
            case PlayerAttackData.InputActionType.Left: return 90f;
            default: return 0f;
        }
    }
    

}
