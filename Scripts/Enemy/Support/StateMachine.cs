using System.Collections.Generic;
using System.Linq;

[GlobalClass]
public partial class StateMachine : Node2D
{
  private readonly Dictionary<string, State> _states = [];
  private State _currentState;

  [Export] private State _initialState;

  public bool CanManageState { get; set; } = true;

  public override void _Ready()
  {
    foreach (var state in GetChildren().Cast<State>())
    {
      _states.Add(state.Name, state);
      state.Transition += OnTransition;
    }

    _initialState.Enter();
    _currentState = _initialState;
  }

  public override void _Process(double delta)
  {
    if (!CanManageState)
      return;

    _currentState.Process(delta);
  }

  public override void _PhysicsProcess(double delta)
  {
    if (!CanManageState)
      return;

    _currentState.PhysicsProcess(delta);
  }

  private void OnTransition(string newStateName)
  {
    var newState = _states[newStateName];

    _currentState.Exit();
    newState.Enter();

    _currentState = newState;
  }
}