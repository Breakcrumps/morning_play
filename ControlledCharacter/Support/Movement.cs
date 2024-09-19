using System.Threading.Tasks;

namespace Morning_Play.ControlledCharacter;

partial class Movement : Node2D {

  [Signal]
  public delegate void IdleAnimationEventHandler();
  [Signal]
  public delegate void WalkAnimationEventHandler();

  private static int AccelerationTime => 5;
  private static int Speed { get; set; } = 0;
  private int MaxSpeed => Controller.Run? RunSpeed : WalkSpeed;
  
  [ExportGroup("Stats")]
  [Export]
  private int WalkSpeed { get; set; } = 75;
  [Export]
  private int RunSpeed { get; set; } = 100;
  [Export]
  private int DashVelocity { get; set; } = 200;
  [Export]
  private int DashTime { get; set; } = 200;

  [ExportGroup("Nodes")]
  [Export]
  private Controller Controller { get; set; }
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
    if (Speed < MaxSpeed)
      Speed += MaxSpeed / AccelerationTime;
    else
      Speed = MaxSpeed;
    Character.Velocity = Controller.MovementDirection.Normalized() * Speed;
    
  }

  private async Task Dash() {

    Controller.CanControl = false;

    Vector2 initVelocity = Controller.MovementDirection.Normalized() * DashVelocity;
    Character.Velocity = initVelocity;

    for (int i = DashTime; i > 0; i--) {

      await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
      
      var velocity = Character.Velocity;
      velocity.X -= initVelocity.X / DashTime;
      velocity.Y -= initVelocity.Y / DashTime;
      Character.Velocity = velocity;

    }

    Controller.CanControl = true;

  }

}