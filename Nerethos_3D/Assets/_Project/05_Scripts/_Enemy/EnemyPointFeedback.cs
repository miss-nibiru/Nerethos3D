using UnityEngine;

/// <summary>
/// this has to be its own separate scrip since each mosnter has own target point behaviour
/// also each target point can be changed depending on the status effect
/// For now just change in materials
/// </summary>
public class EnemyPointFeedback : MonoBehaviour
{
    [Header("Sprite")]
    [SerializeField] private SpriteRenderer baseSprite;
    
    [Header("Materials")]
    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material hoverMaterial;


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
        if(!baseSprite || !hoverMaterial) return;
        baseSprite.material = hoverMaterial;
    }
    
}
