using Godot;

namespace Morning_Play.ControlledCharacter;

partial class Animator : Node2D {

  [Export]
  private bool WeaponOut { get; set; } = false;
  [Export]
  private Movement Movement { get; set; }
  [Export]
  private AnimationPlayer AnimationPlayer { get; set; }
  [Export]
  private Controller Controller { get; set; }
  [Export]
  private bool CanAnimate { get; set; } = true;

  public override void _Ready() {
    Movement.IdleAnimation += IdleAnimation;
    Movement.WalkAnimation += WalkAnimation;
    Controller.Unsheethe += WeaponAnimation;
    Controller.Attack += AttackAnimation;
  }

  private void IdleAnimation() {

    if (!CanAnimate)
      return;

    if (WeaponOut) {
      AnimationPlayer.Play("Unsheethed_Idle");
      return;
    }

    AnimationPlayer.Play("Idle");
    
  }
  private void WalkAnimation() {

    if (!CanAnimate)
      return;

    if (WeaponOut) {
      AnimationPlayer.Play("Unsheethed_Walk");
      return;
    }

    AnimationPlayer.Play("Walk");
    
  }
  private void WeaponAnimation() {

    if (WeaponOut) {
      WeaponOut = false;
      return;
    }

    AnimationPlayer.Play("Unsheethe");
    Controller.CanMove = false;

  }
  private void AttackAnimation() {
    if (!WeaponOut)
      return;
    AnimationPlayer.Play("SwordAttack");
    CanAnimate = false;
  }

}