using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Core.Infrastructure.States
{
    public class StateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        
        private IState _currentState;

        public StateMachine()
        {
            _states = new Dictionary<Type, IState>();
        }

        public void Register(IState state)
        {
            _states[state.GetType()] = state;
        }
        
        public void Enter<TState>()
            where TState : IndependentState
        {
            if (_currentState is IExitableState exitableState)
                exitableState?.Exit();

            TState newState = _states[typeof(TState)] as TState;
            
            Debug.LogError($"=== Enter {typeof(TState).Name} ===");
            newState.Enter();

            _currentState = newState;
        }
        
        public void Enter<TState, TDependency>(TDependency dependency)
            where TState : DependentState<TDependency>
        {
            if (_currentState is IExitableState exitableState)
                exitableState?.Exit();

            TState newState = _states[typeof(TState)] as TState;
            
            Debug.LogError($"=== Enter {typeof(TState).Name} ===");
            newState.Enter(dependency);

            _currentState = newState;
        }
    }
}