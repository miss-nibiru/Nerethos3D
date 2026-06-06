
public class EnemyTargetPointInstance 

{
    
    private EnemyTargetPointData _pointData;
    private int _currentHealth;
    private bool _isDestroyed;
    
    public EnemyTargetPointData pointData => _pointData;
    public int currentHealth => _currentHealth;
    public bool isDestroyed => _isDestroyed;
    
    
    public EnemyTargetPointInstance(EnemyTargetPointData pointData)
    {
        _pointData = pointData;
        _currentHealth = pointData.Health;
        _isDestroyed = false;
    }
    
    // each point also has to take individual damage that is reduced from total max health
    
    public void TakeDamage(int damage)
    {
        if (_isDestroyed)
        {
            return;
        }
        
        _currentHealth -= damage;
        
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            _isDestroyed = true;
        }
    }
    

}
