using static Godot.Input;
using Godot;

namespace Morning_Play.Globals {

  partial class McController : Node2D {

    public bool Unsheethe => IsActionJustPressed("Unsheethe");
    public bool Run => IsActionPressed("Run");

    public Vector2 Velocity => new(DirX(), DirY());

    private int DirX() {
      if (IsActionPressed("Left") && !IsActionPressed("Right"))  return -1;
      if (IsActionPressed("Right") && !IsActionPressed("Left"))  return 1;
      return 0;
    }
    private int DirY() {
      if (IsActionPressed("Up") && !IsActionPressed("Down"))  return -1;
      if (IsActionPressed("Down") && !IsActionPressed("Up"))  return 1;
      return 0;
    }
  
  }

}