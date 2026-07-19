using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoveredPatternsUI : MonoBehaviour
{
    [Header("Pattern Rows")]
    [SerializeField] private Transform rowsParent;
    [SerializeField] private DiscoveredPatternRow rowPrefab;
    [SerializeField] private List<PlayerAttackData> displayedPatterns = new();

    [Header("Sliding Panel")]
    [SerializeField] private RectTransform slidingPanel;
    [SerializeField] private GameObject blackOverlay;
    [SerializeField] private float visibleTabWidth = 100f;
    [SerializeField] private float slideDuration = 0.3f;

    private Vector2 _openPosition;
    private Vector2 _closedPosition;
    private Coroutine _slideCoroutine;
    private bool _isOpen;

    private void Awake()
    {
        BuildRows();

        if (slidingPanel == null)
        {
            Debug.LogError("Sliding Panel has not been assigned.");
            return;
        }

        _openPosition = slidingPanel.anchoredPosition;

        float hiddenDistance = Mathf.Max(0f, slidingPanel.rect.width - visibleTabWidth);
        _closedPosition = _openPosition + Vector2.left * hiddenDistance;

        slidingPanel.anchoredPosition = _closedPosition;
        _isOpen = false;

        if (blackOverlay != null) blackOverlay.SetActive(false);
    }

    public void TogglePanel()
    {
        SetPanelOpen(!_isOpen);
    }

    public void ClosePanel()
    {
        SetPanelOpen(false);
    }

    private void SetPanelOpen(bool isOpen)
    {
        if (slidingPanel == null) return;

        _isOpen = isOpen;

        if (_slideCoroutine != null) StopCoroutine(_slideCoroutine);
        if (blackOverlay != null) blackOverlay.SetActive(isOpen);

        Vector2 targetPosition = isOpen ? _openPosition : _closedPosition;
        _slideCoroutine = StartCoroutine(SlidePanel(targetPosition));
    }

    private IEnumerator SlidePanel(Vector2 targetPosition)
    {
        Vector2 startingPosition = slidingPanel.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < slideDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;

            float progress =
                Mathf.Clamp01(elapsedTime / slideDuration);

            float smoothProgress =
                Mathf.SmoothStep(0f, 1f, progress);

            slidingPanel.anchoredPosition =
                Vector2.Lerp(
                    startingPosition,
                    targetPosition,
                    smoothProgress
                );

            yield return null;
        }

        slidingPanel.anchoredPosition = targetPosition;
        _slideCoroutine = null;
    }

    private void BuildRows()
    {
        foreach (PlayerAttackData attack in displayedPatterns)
        {
            if (attack == null) continue;

            DiscoveredPatternRow row = Instantiate(rowPrefab, rowsParent);
            row.Setup(attack, GetContinuationDepth(attack));
        }
    }

    private int GetContinuationDepth(PlayerAttackData attack)
    {
        int depth = 0;
        PlayerAttackData currentAttack = attack;

        while (currentAttack.RequiresPreviousAttack &&
               currentAttack.RequiredPreviousAttack != null &&
               depth < 10)
        {
            depth++;
            currentAttack = currentAttack.RequiredPreviousAttack;
        }

        return depth;
    }
}