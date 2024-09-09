using Godot;
using Morning_Play.Globals;

namespace Morning_Play.MC {

  partial class Mc : CharacterBody2D {
    
    private static float AccelerationTime => 5;
    private static float Speed { get; set; } = 0;
    private static float MaxSpeed() {
      if (Controller.Run)
        return 100;
      return 75;
    }

    private Controller Controller => GetNode<Controller>("Controller");
    private AnimationPlayer Player => GetNode<AnimationPlayer>("AnimationPlayer");

    public override void _PhysicsProcess(double delta) {
      ApplyVelocity(Controller.Velocity);
      MoveAndSlide();
    }

    private void ApplyVelocity(Vector2 velocity) {

      if (velocity == Vector2.Zero) {
        Player.Play("Idle");
        Speed = 0;
      }
      else {
        Player.Play("Walk");
        if (Speed < MaxSpeed())
          Speed += MaxSpeed() / AccelerationTime;
        else
          Speed = MaxSpeed();
      }

      Velocity = velocity.Normalized() * Speed;
      
    }
  
  }

}
