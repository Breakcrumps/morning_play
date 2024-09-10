using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Morning_Play.NPC {

  partial class StateMachine : Node2D {

    [Export]
    private State InitialState { get; set; }

    private State CurrentState { get; set; }
    private Dictionary<string, State> States => [];
    public override void _Ready() {

      foreach (State state in GetChildren().Cast<State>()) {
        States[state.Name] = state;
        state.Transition += OnTransition;
      }

      InitialState.Enter();
      CurrentState = InitialState;

    }
    public override void _Process(double delta) {
      CurrentState._Process(delta);
    }
    public override void _PhysicsProcess(double delta) {
      CurrentState._PhysicsProcess(delta);
    }

    private void OnTransition(State state, string newStateName) {

      if (state != CurrentState)
        return;
        
      State newState = States[newStateName];
      
      CurrentState.Exit();
      newState.Enter();

      CurrentState = newState;

    }

  }
  
}