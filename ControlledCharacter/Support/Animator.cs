using Godot;

namespace Morning_Play.ControlledCharacter {

  partial class Animator : Node2D {

    [Export]
    private bool WeaponOut { get; set; } = false;
    [Export]
    private Movement Movement { get; set; }
    [Export]
    private AnimationPlayer AnimationPlayer { get; set; }
    [Export]
    private Controller Controller { get; set; }

    public override void _Ready() {
      Movement.IdleAnimation += IdleAnimation;
      Movement.WalkAnimation += WalkAnimation;
      Controller.Unsheethe += WeaponAnimation;
    }

    private void IdleAnimation() {

      if (WeaponOut) {
        AnimationPlayer.Play("Unsheethed_Idle");
        return;
      }

      AnimationPlayer.Play("Idle");
      
    }
    private void WalkAnimation() {

      if (WeaponOut) {
        AnimationPlayer.Play("Unsheethed_Walk");
        return;
      }

      AnimationPlayer.Play("Walk");
      
    }
    private void WeaponAnimation() {
      AnimationPlayer.Play("Unsheethe");
      Controller.CanMove = false;
    }

  }

}
