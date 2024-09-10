using Godot;

namespace Morning_Play.NPC {

  partial class NPC : CharacterBody2D {

    public override void _PhysicsProcess(double delta) {
      MoveAndSlide();
    }

  }

}