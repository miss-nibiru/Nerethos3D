using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using Cache = UnityEngine.Cache;

public class StateMachine

{
    [SerializeField] private IState _currentState;
    [SerializeField] private PlayerController _playerController;
    public IState CurrentState => _currentState;
    
    
    
    
}
