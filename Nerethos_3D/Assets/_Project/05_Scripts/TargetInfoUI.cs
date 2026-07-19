using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetInfoUI : MonoBehaviour
{
    [Header("Target Information")]
    [SerializeField] private TMP_Text pointNameText;
    [SerializeField] private TMP_Text pointDescriptionText;
    [SerializeField] private TMP_Text pointHealthText;
    [SerializeField] private Image pointHealthFill;

    [Header("Position")]
    [SerializeField] private Vector2 screenOffset = new Vector2(-200f, 0f);

    private RectTransform _panelRect;
    private RectTransform _canvasRect;
    private Canvas _battleCanvas;
    private CombatTargetPointController _currentTarget;

    private void Awake()
    {
        _panelRect = GetComponent<RectTransform>();
        _battleCanvas = GetComponentInParent<Canvas>();
        _canvasRect = _battleCanvas.GetComponent<RectTransform>();
    }

    public void ShowTarget(CombatTargetPointController target)
    {
        if (target == null) return;

        _currentTarget = target;
        gameObject.SetActive(true);

        RefreshInformation();
        MoveBesideTarget();
    }

    public void Hide()
    {
        _currentTarget = null;
        gameObject.SetActive(false);
    }

    private void RefreshInformation()
    {
        CombatTargetPointData data = _currentTarget.PointData;

        pointNameText.text = data.TargetPointName.ToUpper();

        string alchemyWeakness = FormatValues(data.Weakness);
        string statusWeakness = FormatValues(data.StatusEffects);
        string statusResistance = FormatValues(data.ResistStatusEffects);

        pointDescriptionText.text =
            "ALCHEMY: " + alchemyWeakness +
            "\nWEAK TO: " + statusWeakness +
            "\nRESISTS: " + statusResistance;

        pointHealthText.text = _currentTarget.CurrentHealth + " / " + data.MaxHealth;
        pointHealthFill.fillAmount = data.MaxHealth > 0 ? Mathf.Clamp01((float)_currentTarget.CurrentHealth / data.MaxHealth) : 0f;
    }

    private string FormatValues<T>(T[] values)
    {
        if (values == null || values.Length == 0) return "NONE";
        return string.Join(", ", values.Select(value => value.ToString()));
    }

    private void MoveBesideTarget()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null) return;

        Vector2 screenPoint =
            mainCamera.WorldToScreenPoint(_currentTarget.transform.position);

        Camera canvasCamera =
            _battleCanvas.renderMode == RenderMode.ScreenSpaceOverlay
                ? null
                : _battleCanvas.worldCamera;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvasRect,
            screenPoint,
            canvasCamera,
            out Vector2 localPoint
        );

        _panelRect.anchoredPosition = localPoint + screenOffset;
    }
}