using static Godot.Input;
using Godot;


public partial class Controller : Node2D {

  public Vector2 Velocity => new(DirX(), DirY());

  int DirX() {
    if (IsActionPressed("Left") && !IsActionPressed("Right"))  return -1;
    if (IsActionPressed("Right") && !IsActionPressed("Left"))  return 1;
    return 0;
  }
  int DirY() {
    if (IsActionPressed("Up") && !IsActionPressed("Down"))  return -1;
    if (IsActionPressed("Down") && !IsActionPressed("Up"))  return 1;
    return 0;
  }
}