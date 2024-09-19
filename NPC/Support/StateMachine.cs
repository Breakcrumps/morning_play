using System.Collections.Generic;
using System.Linq;

namespace Morning_Play.NPC;

[GlobalClass]
partial class StateMachine : Node2D {


  public bool CanManageState { get; set; } = true;

  [Export]
  private State InitialState { get; set; }
  private State CurrentState { get; set; }
  private readonly Dictionary<string, State> states = [];

  public override void _Ready() {

    foreach (var state in GetChildren().Cast<State>()) {
      states.Add(state.Name, state);
      state.Transition += OnTransition;
    }

    InitialState.Enter();
    CurrentState = InitialState;

  }

  public override void _Process(double delta) {
    
    if (!CanManageState)
      return;

    CurrentState.Process(delta);

  }
  public override void _PhysicsProcess(double delta) {

    if (!CanManageState)
      return;

    CurrentState.PhysicsProcess(delta);

  }

  private void OnTransition(string newStateName) {
      
    State newState = states[newStateName];
    
    CurrentState.Exit();
    newState.Enter();

    CurrentState = newState;

  }

}