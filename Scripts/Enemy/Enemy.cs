public partial class Enemy : CharacterBody2D {

  public override void _PhysicsProcess(double delta) {
    MoveAndSlide();
  }

}