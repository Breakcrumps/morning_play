public partial class EnemyIdle : State {

  [ExportGroup("Stats")]
  [Export]
  private int _movementSpeed = 10;
  [Export]
  private int _followDistance = 100;
  private Vector2 _movementDirection;
  private double _wanderTime;

  [ExportGroup("Nodes")]
  [Export]
  private Enemy Enemy { get; set; }
  private Player Character => (Player)GetTree().GetFirstNodeInGroup("Player");

  private void RandomiseWander() {
    _movementDirection = new Vector2((float)GD.RandRange(-1.0, 1.0), (float)GD.RandRange(-1.0, 1.0));
    _wanderTime = GD.RandRange(0, 5);
  }

  public override void Enter() {
    RandomiseWander();
  }
  public override void Exit() { }
  
  public override void Process(double delta) {
    
    if (_wanderTime < 0) {
      RandomiseWander();
      return;
    }
    
    _wanderTime -= delta;
    
  }
  public override void PhysicsProcess(double delta) {

    Enemy.Velocity = _movementDirection * _movementSpeed;

    var vectorToPlayer = Character.GlobalPosition - Enemy.GlobalPosition;

    if (vectorToPlayer.Length() < _followDistance)
      EmitSignal("Transition", "Follow");

  }

}