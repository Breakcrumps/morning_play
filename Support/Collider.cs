using Godot;

namespace Morning_Play.Support {

  [GlobalClass]
  partial class Collider : CollisionShape2D {

    public override void _Ready() {
      Disabled = true;
    }

  }

}