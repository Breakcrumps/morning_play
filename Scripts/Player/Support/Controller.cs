public partial class Controller : Node2D
{
  [Signal] public delegate void AttackEventHandler();
  [Signal] public delegate void UnsheatheEventHandler();

  [ExportGroup("Flags (ignore)")]
    [Export] public bool CanControl { get; set; } = true;
    [Export] public bool StopMove { get; set; } = false;

  private bool UnsheatheButton => IsActionJustPressed("Unsheathe");
  private bool AttackButton => IsActionJustPressed("Attack");
  public bool Run => IsActionPressed("Run");
  public bool Dash => IsActionPressed("Dash");

  public int DirX()
  {
    if (IsActionPressed("Left") && !IsActionPressed("Right"))  return -1;
    if (IsActionPressed("Right") && !IsActionPressed("Left"))  return 1;
    return 0;
  }

  public int DirY()
  {
    if (IsActionPressed("Up") && !IsActionPressed("Down"))  return -1;
    if (IsActionPressed("Down") && !IsActionPressed("Up"))  return 1;
    return 0;
  }

  public override void _PhysicsProcess(double delta)
  {
    if (UnsheatheButton)
      EmitSignal("Unsheathe");
    if (AttackButton)
      EmitSignal("Attack");
  }
}