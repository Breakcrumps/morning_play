using Godot;


namespace Morning_Play.MC {

  public partial class Mc : CharacterBody2D {

    static int Speed => 20;

    Controller Controller => GetNode<Controller>("Controller");
    AnimationPlayer Player => GetNode<AnimationPlayer>("AnimationPlayer");

    public override void _PhysicsProcess(double delta) {
      ApplyVelocity(Controller.Velocity);
      MoveAndSlide();
    }

    void ApplyVelocity(Vector2 velocity) {
      if (velocity == Vector2.Zero)
        Player.Play("Idle");
      else
        Player.Play("Walk");
      Velocity = velocity * Speed;
    }
  }
}
