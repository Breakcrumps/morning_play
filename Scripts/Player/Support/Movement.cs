using System.Threading.Tasks;
public partial class Movement : Node2D {

  [Signal]
  public delegate void IdleAnimationEventHandler();
  [Signal]
  public delegate void WalkAnimationEventHandler();
  
  [ExportGroup("Stats")]
  [Export]
  private int _walkSpeed = 75;
  [Export]
  private int _runSpeed = 100;
  [Export]
  private int _dashVelocity = 500;
  [Export]
  private int _dashTime = 15;

  private int Speed => Controller.Run? _runSpeed : _walkSpeed;

  [ExportGroup("Nodes")]
  [Export]
  private Controller Controller { get; set; }
  private Player Player => GetOwner<Player>();

  public override void _PhysicsProcess(double delta) {
    SetVelocity(Speed);
  }

  private async void SetVelocity(int speed) {

    if (Controller.StopMove) {
      Player.Velocity = Vector2.Zero;
      return;
    }

    if (!Controller.CanControl)
      return;
    
    Vector2 movementDirection = new(Controller.DirX(), Controller.DirY());

    if (movementDirection == Vector2.Zero) {
      EmitSignal("IdleAnimation");
      Player.Velocity = Vector2.Zero;
      return;
    }

    if (Controller.Dash) {
      await Dash(movementDirection, _dashVelocity, _dashTime);
      return;
    }

    EmitSignal("WalkAnimation");
    Player.Velocity = movementDirection.Normalized() * speed;
    
  }

  private async Task Dash(Vector2 movementDirection, int dashVelocity, int dashTime) {

    Controller.CanControl = false;

    Vector2 initVelocity = movementDirection.Normalized() * dashVelocity;
    Player.Velocity = initVelocity;

    for (int i = _dashTime; i > 0; i--) {

      await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
      
      var velocity = Player.Velocity;
      velocity.X -= initVelocity.X / dashTime;
      velocity.Y -= initVelocity.Y / dashTime;
      Player.Velocity = velocity;

    }

    Controller.CanControl = true;

  }

}