using UnityEngine;

public class ControlsPanelUI : MonoBehaviour
{
    [Header("Controls UI")]
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject openControlsButton;

    [Header("Battle References")]
    [SerializeField] private BattleManager battleManager;
    [SerializeField] private BattleUIManager battleUIManager;
    [SerializeField] private BattleInputManager battleInputManager;

    private float _previousTimeScale = 1f;

    public bool IsOpen { get; private set; }

    private void Start()
    {
        OpenControls();
    }

    public void OpenControls()
    {
        if (IsOpen) return;

        IsOpen = true;
        _previousTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        controlsPanel.SetActive(true);
        openControlsButton.SetActive(false);

        // Prevent battle keyboard input while reading the controls.
        battleUIManager.enabled = false;
        battleInputManager.enabled = false;
    }

    public void CloseControls()
    {
        if (!IsOpen) return;

        IsOpen = false;
        Time.timeScale = _previousTimeScale;

        controlsPanel.SetActive(false);
        openControlsButton.SetActive(true);

        battleUIManager.enabled = true;
        battleInputManager.enabled = true;
        
        if (battleManager.StateMachine.CurrentState is BattleStartState) battleManager.ConfirmBattleState();
        
    }

    private void OnDestroy()
    {
        if (IsOpen) Time.timeScale = _previousTimeScale;
        
    }
}