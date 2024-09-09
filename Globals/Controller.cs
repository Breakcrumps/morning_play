using static Godot.Input;
using Godot;

namespace Morning_Play.Globals {

  partial class Controller : Node2D {

    public static bool Run => IsActionPressed("Run");

    public static Vector2 Velocity => new(DirX(), DirY());

    private static int DirX() {
      if (IsActionPressed("Left") && !IsActionPressed("Right"))  return -1;
      if (IsActionPressed("Right") && !IsActionPressed("Left"))  return 1;
      return 0;
    }
    private static int DirY() {
      if (IsActionPressed("Up") && !IsActionPressed("Down"))  return -1;
      if (IsActionPressed("Down") && !IsActionPressed("Up"))  return 1;
      return 0;
    }
  
  }

}