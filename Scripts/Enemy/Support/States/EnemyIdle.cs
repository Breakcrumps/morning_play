public partial class EnemyIdle : State {

  [ExportGroup("Stats")]
  [Export]
  private int _movementSpeed = 10;
  [Export]
  private int _followDistance = 100;
  [Export]
  private int _maxWanderDistance = 20;
  private Vector2 _initPosition;

  [ExportGroup("Nodes")]
  [Export]
  private Enemy Enemy { get; set; }
  private Player Character => (Player)GetTree().GetFirstNodeInGroup("Player");
  
  private Vector2 _movementDirection;
  private double _wanderTime;
  
  public override void _Ready() {
    _initPosition = GlobalPosition;
  }
  
  private void RandomiseWander() {

    var vectorFromSpawn = GlobalPosition - _initPosition;

    if (vectorFromSpawn.X > _maxWanderDistance || vectorFromSpawn.Y > _maxWanderDistance)
      _movementDirection = -vectorFromSpawn.Normalized();
    else 
      _movementDirection = new Vector2(GD.RandRange(-1, 1), GD.RandRange(-1, 1)).Normalized();
    
    _wanderTime = GD.RandRange(1, 4);
    
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