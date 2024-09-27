using System.Threading.Tasks;

namespace Morning_Play.ControlledCharacter;

partial class Movement : Node2D {

  [Signal]
  public delegate void IdleAnimationEventHandler();
  [Signal]
  public delegate void WalkAnimationEventHandler();

  private int _accelerationTime = 5;
  private int _currentSpeed = 0;
  private int MaxSpeed => Controller.Run? _runSpeed : _walkSpeed;
  
  [ExportGroup("Stats")]
  [Export]
  private int _walkSpeed = 75;
  [Export]
  private int _runSpeed = 100;
  [Export]
  private int _dashVelocity = 200;
  [Export]
  private int _dashTime = 200;

  [ExportGroup("Nodes")]
  [Export]
  private Controller Controller;
  private PlayableCharacter Character => GetOwner<PlayableCharacter>();

  public override void _PhysicsProcess(double delta) {
    SetVelocity();
  }

  public async void SetVelocity() {

    if (Controller.StopMove) {
      Character.Velocity = Vector2.Zero;
      return;
    }

    if (!Controller.CanControl)
      return;
    
    if (Controller.MovementDirection == Vector2.Zero) {
      EmitSignal("IdleAnimation");
      Character.Velocity = Vector2.Zero;
      return;
    }

    if (Controller.Dash) {
      await Dash();
      return;
    }

    EmitSignal("WalkAnimation");
    if (_currentSpeed < MaxSpeed)
      _currentSpeed += MaxSpeed / _accelerationTime;
    else
      _currentSpeed = MaxSpeed;
    Character.Velocity = Controller.MovementDirection.Normalized() * _currentSpeed;
    
  }

  private async Task Dash() {

    Controller.CanControl = false;

    Vector2 initVelocity = Controller.MovementDirection.Normalized() * _dashVelocity;
    Character.Velocity = initVelocity;

    for (int i = _dashTime; i > 0; i--) {

      await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
      
      var velocity = Character.Velocity;
      velocity.X -= initVelocity.X / _dashTime;
      velocity.Y -= initVelocity.Y / _dashTime;
      Character.Velocity = velocity;

    }

    Controller.CanControl = true;

  }

}