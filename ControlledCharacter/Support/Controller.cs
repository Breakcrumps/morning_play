using static Godot.Input;
using Godot;

namespace Morning_Play.ControlledCharacter {

  partial class Controller : Node2D {

    [Export]
    public bool CanMove { get; set; } = true;

    [Signal]
    public delegate void UnsheetheEventHandler();
    [Signal]
    public delegate void AttackEventHandler();

    private bool UnsheetheButton => IsActionJustPressed("Unsheethe");
    private bool AttackButton => IsActionJustPressed("Attack");
    public bool Run => IsActionPressed("Run");

    public Vector2 MovementDirection => new(DirX(), DirY());

    private int DirX() {
      if (!CanMove)  return 0;
      if (IsActionPressed("Left") && !IsActionPressed("Right"))  return -1;
      if (IsActionPressed("Right") && !IsActionPressed("Left"))  return 1;
      return 0;
    }
    private int DirY() {
      if (!CanMove)  return 0;
      if (IsActionPressed("Up") && !IsActionPressed("Down"))  return -1;
      if (IsActionPressed("Down") && !IsActionPressed("Up"))  return 1;
      return 0;
    }

    public override void _PhysicsProcess(double delta) {
      if (UnsheetheButton)
        EmitSignal("Unsheethe");
      if (AttackButton) {
        EmitSignal("Attack");
      }
    }
  
  }

}