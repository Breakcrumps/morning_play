namespace Morning_Play.ControlledCharacter;

partial class Animator : Node2D {

  [ExportGroup("Flags (ignore)")]
  [Export]
  private bool _weaponOut = false;
  [Export]
  private bool _canAnimate = true;
  
  [ExportGroup("Nodes")]
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
    Controller.Attack += AttackAnimation;
  }

  private void IdleAnimation() {

    if (!_canAnimate)
      return;

    if (_weaponOut) {
      AnimationPlayer.Play("Unsheethed_Idle");
      return;
    }

    AnimationPlayer.Play("Idle");
    
  }
  private void WalkAnimation() {

    if (!_canAnimate)
      return;

    if (_weaponOut) {
      AnimationPlayer.Play("Unsheethed_Walk");
      return;
    }

    AnimationPlayer.Play("Walk");
    
  }
  private void WeaponAnimation() {

    if (_weaponOut) {
      _weaponOut = false;
      return;
    }

    AnimationPlayer.Play("Unsheethe");
    Controller.StopMove = true;

  }
  private void AttackAnimation() {
    if (!_weaponOut)
      return;
    AnimationPlayer.Play("SwordAttack");
    _canAnimate = false;
  }

}