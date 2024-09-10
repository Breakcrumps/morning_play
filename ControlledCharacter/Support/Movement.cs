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
    private ControlledCharacter Character => GetParent<ControlledCharacter>();

    public override void _PhysicsProcess(double delta) {
      SetVelocity();
    }

    public void SetVelocity() {

      if (!Controller.CanMove) {
        Character.Velocity = Vector2.Zero;
        return;
      }
      if (Controller.MovementDirection == Vector2.Zero) {
        EmitSignal("IdleAnimation");
        Character.Velocity = Vector2.Zero;
        return;
      }

      EmitSignal("WalkAnimation");
      if (Speed < MaxSpeed())
        Speed += MaxSpeed() / AccelerationTime;
      else
        Speed = MaxSpeed();
      Character.Velocity = Controller.MovementDirection.Normalized() * Speed;
      return;
      
    }

  }

}