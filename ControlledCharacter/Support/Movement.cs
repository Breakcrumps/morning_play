using Godot;

namespace Morning_Play.ControlledCharacter {

  partial class Movement : Node2D {

    [Signal]
    public delegate void IdleAnimationEventHandler();
    [Signal]
    public delegate void WalkAnimationEventHandler();

    private static float AccelerationTime => 5;
    private static float Speed { get; set; } = 0;
    private float MaxSpeed() {
      if (Controller.Run)
        return 100;
      return 75;
    }

    [Export]
    private Controller Controller { get; set; }

    public Vector2 GetVelocity() {

      if (!Controller.CanMove) {
        return Vector2.Zero;
      }
      if (Controller.MovementDirection == Vector2.Zero) {
        EmitSignal("IdleAnimation");
        return Vector2.Zero;
      }

      EmitSignal("WalkAnimation");
      if (Speed < MaxSpeed())
        Speed += MaxSpeed() / AccelerationTime;
      else
        Speed = MaxSpeed();
      return Controller.MovementDirection.Normalized() * Speed;
      
    }

  }

}