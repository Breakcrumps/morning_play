using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Morning_Play.NPC {

  partial class StateMachine : Node2D {

    [Export]
    private State InitialState { get; set; }

    private State CurrentState { get; set; }
    private readonly Dictionary<string, State> states = [];

    public override void _Ready() {

      foreach (var state in GetChildren().Cast<State>()) {
        states.Add(state.Name, state);
        state.Transition += OnTransition;
      }

      Console.WriteLine($"{states.Count}");
      foreach (KeyValuePair<string, State> dictItem in states) {
        Console.WriteLine($"{dictItem.Key}: {dictItem.Value}");
      }

      InitialState.Enter();
      CurrentState = InitialState;

    }

    public override void _Process(double delta) {
      CurrentState.Process(delta);
    }
    public override void _PhysicsProcess(double delta) {
      CurrentState.PhysicsProcess(delta);
    }

    private void OnTransition(string newStateName) {
        
      State newState = states[newStateName];
      
      CurrentState.Exit();
      newState.Enter();

      CurrentState = newState;

    }

  }
  
}