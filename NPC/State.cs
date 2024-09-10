using Godot;

namespace Morning_Play.NPC {

  abstract partial class State : Node2D {
    
    [Signal]
    public delegate void TransitionEventHandler(State state, string newStateName);

    abstract public void Enter();
    abstract public void Exit();

  }

}