using UnityEngine;

/// <summary>
/// this has to be its own separate scrip since each mosnter has own target point behaviour
/// also each target point can be changed depending on the status effect
/// For now just change in materials
/// </summary>
public class CombatTargetPointFeedback : MonoBehaviour
{
    [Header("Sprite")]
    [SerializeField] private SpriteRenderer baseSprite;
    
    [Header("Materials")]
    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material selectedMaterial;
    [SerializeField] private Material brokenMaterial;


    void Awake()
    {
        if (!baseSprite) baseSprite = GetComponent<SpriteRenderer>();
        SetBase();
    }

    public void SetBase()
    {
        if(!baseSprite || !normalMaterial) return;
        baseSprite.material = normalMaterial;
        
    }
    
    public void SetSelected()
    {
        if(!baseSprite || !selectedMaterial) return;
        baseSprite.material = selectedMaterial;
    }
    
    public void HideTargetVisual()
    {
        if (!baseSprite) return;

        baseSprite.enabled = false;
    }
    
}
