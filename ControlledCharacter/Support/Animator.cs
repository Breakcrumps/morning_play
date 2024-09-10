using Godot;

namespace Morning_Play.ControlledCharacter {

  partial class Animator : Node2D {

    [Export]
    private bool WeaponOut { get; set; } = false;
    [Export]
    private Movement Movement { get; set; }
    [Export]
    private AnimationPlayer Player { get; set; }
    [Export]
    private Controller Controller { get; set; }

    public override void _Ready() {
      Movement.IdleAnimation += IdleAnimation;
      Movement.WalkAnimation += WalkAnimation;
      Controller.Unsheethe += WeaponAnimation;
    }

    private void IdleAnimation() {
      if (WeaponOut) {
        Player.Play("Unsheethed_Idle");
        return;
      }
      Player.Play("Idle");
    }
    private void WalkAnimation() {
      if (WeaponOut) {
        Player.Play("Unsheethed_Walk");
        return;
      }
      Player.Play("Walk");
    }
    private void WeaponAnimation() {
      Player.Play("Unsheethe");
      Controller.CanMove = false;
    }

  }

}
