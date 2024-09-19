using Morning_Play.ControlledCharacter;

namespace Morning_Play.NPC;

partial class NPCFollow : State {

  [ExportGroup("Stats")]
  [Export]
  private int MovementSpeed { get; set; } = 10;
  [Export]
  private int LoseDistance { get; set; } = 70;
  [Export]
  private int StopAndAttackDistance { get; set; } = 30;
  [ExportGroup("Nodes")]
  [Export]
  private NPC NPC { get; set; }
  [Export]
  private AnimationPlayer Animator { get; set; }
  private PlayableCharacter Character => (PlayableCharacter)GetTree().GetFirstNodeInGroup("MC");

  public override void Enter() {
    
  }
  public override void Exit() {

  }

  public override void Process(double delta) {

    if (Character is null) {
      return;
    }

    Vector2 direction = Character.GlobalPosition - NPC.GlobalPosition;

    if (direction.Length() > LoseDistance) {
      EmitSignal("Transition", "Idle");
      return;
    }

    if (direction.Length() < StopAndAttackDistance) {
      NPC.Velocity = Vector2.Zero;
      Animator.Play("SwordAttack");
      return;
    }

    NPC.Velocity = direction.Normalized() * MovementSpeed;

  }
  public override void PhysicsProcess(double delta) {

  }

}