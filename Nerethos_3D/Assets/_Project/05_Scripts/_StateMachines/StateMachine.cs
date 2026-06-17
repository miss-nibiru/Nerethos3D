using UnityEngine;

/// <summary>
/// controls all the states for the battle
/// received old state, then changes to new one and then performs it
/// </summary>

public class StateMachine : MonoBehaviour

{ 
    private IBattleState _currentState;
    
    public IBattleState CurrentState => _currentState;


    private void Update()
    {
        
        PerformState();
        
    }

    public void ChangeState(IBattleState newState)
    {
        _currentState?.ExitState();        
        _currentState = newState;
        _currentState?.EnterState();     
        
    }


    private void PerformState()
    {
        
        _currentState?.PerformState();
        
    }
    
    
}
