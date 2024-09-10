using Godot;

namespace Morning_Play.ControlledCharacter {

  partial class ControlledCharacter : CharacterBody2D {

    [Export]
    private Controller Controller { get; set; }
    [Export]
    private Movement Movement { get; set; }

    public override void _PhysicsProcess(double delta) {
      Velocity = Movement.GetVelocity();
      MoveAndSlide();
    }

  }

}
