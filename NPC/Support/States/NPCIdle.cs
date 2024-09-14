using Godot;
using Morning_Play.ControlledCharacter;

namespace Morning_Play.NPC;

partial class NPCIdle : State {

  [Export]
  private NPC NPC { get; set; }
  private PlayableCharacter Character =>
      (PlayableCharacter)GetTree().GetFirstNodeInGroup("MC");

  [Export]
  private int MovementSpeed { get; set; } = 10;
  private Vector2 MovementDirection { get; set; }
  private double WanderTime { get; set; }

  private void RandomiseWander() {
    MovementDirection = 
        new Vector2((float)GD.RandRange(-1.0, 1.0), (float)GD.RandRange(-1.0, 1.0));
    WanderTime = GD.RandRange(0, 5);
  }

  public override void Enter() {
    RandomiseWander();
  }
  public override void Process(double delta) {
    if (WanderTime < 0) {
      RandomiseWander();
      return;
    }
    WanderTime -= delta;
  }
  public override void PhysicsProcess(double delta) {

    NPC.Velocity = MovementDirection * MovementSpeed;

    Vector2 direction = Character.GlobalPosition - NPC.GlobalPosition;
    
    if (direction.Length() < 50) {
      EmitSignal("Transition", "Follow");
      return;
    }

  }
  public override void Exit() {
    
  }

}