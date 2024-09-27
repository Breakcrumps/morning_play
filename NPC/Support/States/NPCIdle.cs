using Morning_Play.ControlledCharacter;

namespace Morning_Play.NPC;

partial class NPCIdle : State {

  [ExportGroup("Stats")]
  [Export]
  private int _movementSpeed = 10;
  [Export]
  private int _followDistance = 60;
  private Vector2 _movementDirection;
  private double wanderTime;

  [ExportGroup("Nodes")]
  [Export]
  private NPC NPC { get; set; }
  private PlayableCharacter Character =>
      (PlayableCharacter)GetTree().GetFirstNodeInGroup("MC");

  private void RandomiseWander() {
    _movementDirection = 
        new Vector2((float)GD.RandRange(-1.0, 1.0), (float)GD.RandRange(-1.0, 1.0));
    wanderTime = GD.RandRange(0, 5);
  }

  public override void Enter() {
    RandomiseWander();
  }
  public override void Process(double delta) {
    if (wanderTime < 0) {
      RandomiseWander();
      return;
    }
    wanderTime -= delta;
  }
  public override void PhysicsProcess(double delta) {

    NPC.Velocity = _movementDirection * _movementSpeed;

    Vector2 direction = Character.GlobalPosition - NPC.GlobalPosition;
    
    if (direction.Length() < _followDistance) {
      EmitSignal("Transition", "Follow");
      return;
    }

  }
  public override void Exit() {
    
  }

}