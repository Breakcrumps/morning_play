namespace Morning_Play.ControlledCharacter;

internal partial class PlayableCharacter : CharacterBody2D {

  public override void _PhysicsProcess(double delta) {
    MoveAndSlide();
  }

}