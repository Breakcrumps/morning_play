using Morning_Play.ControlledCharacter;

namespace Morning_Play.NPC;

partial class NPCFollow : State {

  [ExportGroup("Stats")]
  [Export]
  private int _movementSpeed = 10;
  [Export]
  private int _loseDistance = 70;
  [Export]
  private int _stopAndAttackDistance = 30;
  
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

    if (direction.Length() > _loseDistance) {
      EmitSignal("Transition", "Idle");
      return;
    }

    if (direction.Length() < _stopAndAttackDistance) {
      NPC.Velocity = Vector2.Zero;
      Animator.Play("SwordAttack");
      return;
    }

    NPC.Velocity = direction.Normalized() * _movementSpeed;

  }
  public override void PhysicsProcess(double delta) {

  }

}