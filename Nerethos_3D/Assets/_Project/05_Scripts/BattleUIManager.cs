using UnityEngine;
using UnityEngine.InputSystem;

public class BattleUIManager : MonoBehaviour
{
    public enum ActionType
    {
        Attack,
        Override,
        Item,
        Escape
        
    }

    [SerializeField] private GameObject[] actionSelectionUI;
    [SerializeField] private ActionType[] actionType;
    private int _selectedActionIndex;
    
    public ActionType SelectedAction => actionType[_selectedActionIndex];
    
    private NerethosInputActions _inputActions;
    [SerializeField] private BattleManager battleManager;

    private void Awake()
    {
        _inputActions = new NerethosInputActions();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        _selectedActionIndex = 0; // to start on the first index always
        UpdateActionUI();
        
        //IN-BATTLE INPUTS
        _inputActions.Battle.Right.performed += OnRightPressed;
        _inputActions.Battle.Left.performed += OnLeftPressed;
        
        //UI INPUTS
        
        _inputActions.Battle.EButton.performed += OnConfirmPressed;
    }

    private void OnDisable()
    {
        //BATTLE
        _inputActions.Battle.Right.performed -= OnRightPressed;
        _inputActions.Battle.Left.performed -= OnLeftPressed;
        
        
        //UI
        _inputActions.Battle.EButton.performed -= OnConfirmPressed;
        _inputActions.Disable();
    }


    private void OnConfirmPressed(InputAction.CallbackContext ctx)
    {
        battleManager.ConfirmBattleState();
    }

    private void OnRightPressed(InputAction.CallbackContext ctx)
    {
        
        //here we start dividing states i think
        
        if (battleManager.CurrentTurnActionType == BattleManager.TurnActionType.ActionSelection)
            MoveActionUI(1);
        // if (battleManager.CurrentTurnActionType == BattleManager.TurnActionType.PatternInput)
        //PatternInput();


    }

    private void OnLeftPressed(InputAction.CallbackContext ctx)
    {
        
        
        if (battleManager.CurrentTurnActionType == BattleManager.TurnActionType.ActionSelection)
            MoveActionUI(-1);
        //if (battleManager.CurrentTurnActionType == BattleManager.TurnActionType.PatternInput)
            //somethingsomething
            
            
    }

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