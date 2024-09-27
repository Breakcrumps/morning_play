using System.Collections.Generic;
using System.Linq;

namespace Morning_Play.NPC;

[GlobalClass]
partial class StateMachine : Node2D {


  public bool CanManageState { get; set; } = true;

  [Export]
  private State _initialState;
  private State _currentState;
  private readonly Dictionary<string, State> _states = [];

  public override void _Ready() {

    foreach (var state in GetChildren().Cast<State>()) {
      _states.Add(state.Name, state);
      state.Transition += OnTransition;
    }

    _initialState.Enter();
    _currentState = _initialState;

  }

  public override void _Process(double delta) {
    
    if (!CanManageState)
      return;

    _currentState.Process(delta);

  }
  public override void _PhysicsProcess(double delta) {

    if (!CanManageState)
      return;

    _currentState.PhysicsProcess(delta);

  }

  private void OnTransition(string newStateName) {
      
    State newState = _states[newStateName];
    
    _currentState.Exit();
    newState.Enter();

    _currentState = newState;

  }

}