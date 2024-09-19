namespace Morning_Play.ControlledCharacter;

partial class PlayableCharacter : CharacterBody2D {

  public override void _PhysicsProcess(double delta) {
    MoveAndSlide();
  }

}