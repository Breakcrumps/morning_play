namespace Morning_Play.NPC;

abstract partial class State : Node2D {
  
  [Signal]
  public delegate void TransitionEventHandler(string newStateName);

  public abstract void Enter();
  public abstract void Exit();

  public abstract void Process(double delta);
  public abstract void PhysicsProcess(double delta);

}