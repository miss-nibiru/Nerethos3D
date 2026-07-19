using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This one lsitens for navigation buttons and confirm ui
/// </summary>

public class BattleUIManager : MonoBehaviour

{
    public enum ActionType
    {
        Attack,
        Override,
        Item,
        Escape
    }

    [SerializeField] private BattleManager battleManager;
    [SerializeField] private GameObject[] actionSelectionUI;
    [SerializeField] private ActionType[] actionType;
   
    [Header("Battle Result")]
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject defeatPanel;
    
    [Header("Health Bars")]
    [SerializeField] private Image playerHealthFill;
    [SerializeField] private Image enemyHealthFill;
    
    [Header("Attack Announcement")]
    [SerializeField] private GameObject attackAnnouncementUI;
    [SerializeField] private TMP_Text attackAnnouncementText;
    
    [Header("Restrictions")]
    [SerializeField] private GameObject notAvailablePanel;
    
    public ActionType SelectedAction => actionType[_selectedActionIndex];
    private NerethosInputActions _inputActions;
    private int _selectedActionIndex;

    private void Awake()
    {
        InitializeInputActions();
        if (!notAvailablePanel) notAvailablePanel.SetActive(false);
    }

    private void OnEnable()
    {
        InitializeInputActions();
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
        if  (_inputActions == null) return;
        
        //BATTLE
        _inputActions.Battle.Right.performed -= OnRightPressed;
        _inputActions.Battle.Left.performed -= OnLeftPressed;
        
        //UI
        _inputActions.Battle.EButton.performed -= OnConfirmPressed;
        _inputActions.Disable();
    }
    
    
    private void InitializeInputActions()
    {
        _inputActions ??= new NerethosInputActions();
    }


    private void OnConfirmPressed(InputAction.CallbackContext ctx)
    {
        battleManager.ConfirmBattleState();
    }

    private void OnRightPressed(InputAction.CallbackContext ctx)
    {
        //here we start dividing states i think
        
        battleManager.MoveBattleState(1);

    }

    private void OnLeftPressed(InputAction.CallbackContext ctx)
    {
        battleManager.MoveBattleState(-1);
    }

    private void UpdateActionUI()
    {
        for (int i = 0; i < actionSelectionUI.Length; i++)
        {
            if (i == _selectedActionIndex) actionSelectionUI[i].transform.localScale = Vector3.one * (float)1.2; // scale up the selected action
            else actionSelectionUI[i].transform.localScale = Vector3.one;
            
        }
    }

    public void MoveActionUI(int direction)
    {

        _selectedActionIndex += direction;

        if (_selectedActionIndex < 0) _selectedActionIndex = actionSelectionUI.Length - 1;
        
        if (_selectedActionIndex >= actionSelectionUI.Length) _selectedActionIndex = 0;
        UpdateActionUI();
        Debug.Log("Selected action index: " + _selectedActionIndex);

    }

    public void SetActionSelectionUIActive(bool isActive)
    {
        for (int i = 0; i < actionSelectionUI.Length; i++) actionSelectionUI[i].SetActive(isActive);
        
    }
    
    public void UpdateHealthBars()
    {
        float playerHealthPercent =
            (float)battleManager.PlayerController.CurrentHealth /
            battleManager.PlayerController.MaxHealth;

        float enemyHealthPercent =
            (float)battleManager.EnemyController.CurrentHealth /
            battleManager.EnemyController.MaxHealth;

        playerHealthFill.fillAmount = Mathf.Clamp01(playerHealthPercent);
        enemyHealthFill.fillAmount = Mathf.Clamp01(enemyHealthPercent);
    }
    
    public void ShowVictory()
    {
        SetActionSelectionUIActive(false);
        victoryPanel.SetActive(true);
        defeatPanel.SetActive(false);
    }

    public void ShowDefeat()
    {
        SetActionSelectionUIActive(false);
        victoryPanel.SetActive(false);
        defeatPanel.SetActive(true);
    }

    public void RestartBattle()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void ShowAttackAnnouncement(string attackName)
    {
        attackAnnouncementText.text = attackName;
        attackAnnouncementUI.SetActive(true);
    }

    public void HideAttackAnnouncement()
    {
        attackAnnouncementUI.SetActive(false);
    }
    
    public void ShowNotAvailable()
    {
        notAvailablePanel.SetActive(true);
    }

    public void HideNotAvailable()
    {
        notAvailablePanel.SetActive(false);
    }

}