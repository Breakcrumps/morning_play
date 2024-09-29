namespace Morning_Play.NPC;

internal partial class Npc : CharacterBody2D {

  public override void _PhysicsProcess(double delta) {
    MoveAndSlide();
  }

}