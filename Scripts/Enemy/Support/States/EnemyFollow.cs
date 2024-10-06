public partial class EnemyFollow : State
{
  [ExportGroup("Stats")]
    [Export] private int _loseDistance = 130;
    [Export] private int _movementSpeed = 70;
    [Export] private int _stopAndAttackDistance = 30;

  [ExportGroup("Nodes")] 
    [Export] private Enemy Enemy { get; set; }
    [Export] private AnimationPlayer Animator { get; set; }

  private Player Player => (Player)GetTree().GetFirstNodeInGroup("Player");

  public override void Enter() { }
  public override void Exit() { }

  public override void Process(double delta) { }

  public override void PhysicsProcess(double delta)
  {
    if (Player is null)
      return;

    var vectorToPlayer = Player.GlobalPosition - Enemy.GlobalPosition;

    if (vectorToPlayer.Length() > _loseDistance)
    {
      EmitSignal("Transition", "Idle");
      return;
    }

    if (vectorToPlayer.Length() < _stopAndAttackDistance)
    {
      Enemy.Velocity = Vector2.Zero;
      Animator.Play("SwordAttack");
      return;
    }

    Enemy.Velocity = vectorToPlayer.Normalized() * _movementSpeed;
  }
}