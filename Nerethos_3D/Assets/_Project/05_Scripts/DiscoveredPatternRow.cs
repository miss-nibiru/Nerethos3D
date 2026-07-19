using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiscoveredPatternRow : MonoBehaviour
{
    [Header("Row")]
    [SerializeField] private TMP_Text attackNameText;
    [SerializeField] private Image[] inputSlots = new Image[5];

    [Header("Icons")]
    [SerializeField] private Sprite unknownIcon;
    [SerializeField] private Sprite emptyIcon;
    [SerializeField] private Sprite arrowIcon;
    [SerializeField] private Sprite aIcon;
    [SerializeField] private Sprite sIcon;
    [SerializeField] private Sprite zIcon;
    [SerializeField] private Sprite xIcon;
    

    private PlayerAttackData _attack;
    private int _continuationDepth;

    private void OnEnable()
    {
        PatternDiscovery.PatternDiscovered += OnPatternDiscovered;
    }

    private void OnDisable()
    {
        PatternDiscovery.PatternDiscovered -= OnPatternDiscovered;
    }

    public void Setup(PlayerAttackData attack, int continuationDepth)
    {
        _attack = attack;
        _continuationDepth = continuationDepth;
        Refresh();
    }

    public void Refresh()
    {
        if (_attack == null) return;

        bool discovered = PatternDiscovery.IsDiscovered(_attack);
        string prefix = _continuationDepth > 0 ? new string(' ', _continuationDepth * 2) + "-- " : "";

        attackNameText.text = discovered ? prefix + _attack.AttackName : prefix + "???";
        for (int i = 0; i < inputSlots.Length; i++)
        {
            if (inputSlots[i] == null) continue;
            inputSlots[i].gameObject.SetActive(true);
            bool hasInput = i < _attack.InputPatterns.Count;

            if (!hasInput)
            {
                inputSlots[i].sprite = emptyIcon;
                inputSlots[i].rectTransform.localRotation = Quaternion.identity;
                continue;
            }

            if (!discovered)
            {
                inputSlots[i].sprite = unknownIcon;
                inputSlots[i].rectTransform.localRotation = Quaternion.identity;
                continue;
            }

            PlayerAttackData.InputActionType input = _attack.InputPatterns[i];
            inputSlots[i].sprite = GetInputIcon(input);
            inputSlots[i].rectTransform.localRotation = Quaternion.Euler(0f, 0f, GetInputRotation(input));
        }
    }

    private void OnPatternDiscovered(PlayerAttackData discoveredAttack)
    {
        if (discoveredAttack == _attack) Refresh();
        
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