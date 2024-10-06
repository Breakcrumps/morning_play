public partial class Player : CharacterBody2D
{
  public override void _PhysicsProcess(double delta)
  {
    MoveAndSlide();
  }
}