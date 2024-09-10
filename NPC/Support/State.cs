using Godot;

namespace Morning_Play.NPC {

  abstract partial class State : Node2D {
    
    [Signal]
    public delegate void TransitionEventHandler(string newStateName);

    abstract public void Enter();
    abstract public void Exit();

    abstract public void Process(double delta);
    abstract public void PhysicsProcess(double delta);

  }

}