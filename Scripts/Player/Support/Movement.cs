using System.Threading.Tasks;

public partial class Movement : Node2D
{
  [Signal] public delegate void IdleAnimationEventHandler();

  [Signal] public delegate void WalkAnimationEventHandler();

  [ExportGroup("Stats")]
    [Export] private int _walkSpeed = 50;
    [Export] private int _runSpeed = 65;
    
    [Export] private int _dashTime = 15;
    [Export] private int _dashVelocity = 500;
  
  private int Speed => Controller.Run ? _runSpeed : _walkSpeed;

  [ExportGroup("Nodes")]
    [Export] private Controller Controller { get; set; }
    [Export] private Sprite2D Sprite { get; set; }

  private Player Player => GetOwner<Player>();

  public override void _PhysicsProcess(double delta)
  {
    SetVelocity(Speed);
  }

  private async void SetVelocity(int speed)
  {
    if (Controller.StopMove)
    {
      Player.Velocity = Vector2.Zero;
      return;
    }

    if (!Controller.CanControl)
      return;

    Vector2 movementDirection = new(Controller.DirX(), Controller.DirY());

    if (movementDirection == Vector2.Zero)
    {
      EmitSignal("IdleAnimation");
      Player.Velocity = Vector2.Zero;
      return;
    }

    if (movementDirection.X > 0)
      Sprite.FlipH = false;
    else if (movementDirection.X < 0)
      Sprite.FlipH = true;

    if (Controller.Dash)
    {
      await Dash(movementDirection, _dashVelocity, _dashTime);
      return;
    }

    EmitSignal("WalkAnimation");
    Player.Velocity = movementDirection.Normalized() * speed;
  }

  private async Task Dash(Vector2 movementDirection, int dashVelocity, int dashTime)
  {
    Controller.CanControl = false;

    var initVelocity = movementDirection.Normalized() * dashVelocity;
    Player.Velocity = initVelocity;

    for (var i = _dashTime; i > 0; i--)
    {
      await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);

      var velocity = Player.Velocity;
      velocity.X -= initVelocity.X / dashTime;
      velocity.Y -= initVelocity.Y / dashTime;
      Player.Velocity = velocity;
    }

    Controller.CanControl = true;
  }
}