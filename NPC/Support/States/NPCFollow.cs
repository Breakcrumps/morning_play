using Godot;
using Morning_Play.ControlledCharacter;

namespace Morning_Play.NPC;

partial class NPCFollow : State {

  [Export]
  private NPC NPC { get; set; }
  [Export]
  private int MovementSpeed { get; set; } = 10;
  private PlayableCharacter Character => (PlayableCharacter)GetTree().GetFirstNodeInGroup("MC");

  public override void Enter() {
    
  }
  public override void Exit() {

  }

  public override void Process(double delta) {

    Vector2 direction = Character.GlobalPosition - NPC.GlobalPosition;

    if (direction.Length() > 70) {
      EmitSignal("Transition", "Idle");
      return;
    }

    if (direction.Length() < 30) {
      NPC.Velocity = Vector2.Zero;
      return;
    }

    NPC.Velocity = direction.Normalized() * MovementSpeed;

  }
  public override void PhysicsProcess(double delta) {

  }

}