using Morning_Play.ControlledCharacter;

namespace Morning_Play.NPC;

internal partial class NpcFollow : State {

  [ExportGroup("Stats")]
  [Export]
  private int _movementSpeed = 70;
  [Export]
  private int _loseDistance = 130;
  [Export]
  private int _stopAndAttackDistance = 30;
  
  [ExportGroup("Nodes")]
  [Export]
  private Npc Npc { get; set; }
  [Export]
  private AnimationPlayer Animator { get; set; }
  private PlayableCharacter Character => (PlayableCharacter)GetTree().GetFirstNodeInGroup("MC");

  public override void Enter() { }
  public override void Exit() { }

  public override void Process(double delta) { }
  public override void PhysicsProcess(double delta) {
    
    if (Character is null) {
      return;
    }

    var vectorToPlayer = Character.GlobalPosition - Npc.GlobalPosition;

    if (vectorToPlayer.Length() > _loseDistance) {
      EmitSignal("Transition", "Idle");
      return;
    }

    if (vectorToPlayer.Length() < _stopAndAttackDistance) {
      Npc.Velocity = Vector2.Zero;
      Animator.Play("SwordAttack");
      return;
    }

    Npc.Velocity = vectorToPlayer.Normalized() * _movementSpeed;
    
  }

}