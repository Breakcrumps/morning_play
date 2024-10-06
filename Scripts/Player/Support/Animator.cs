public partial class Animator : Node2D
{

  [ExportGroup("Flags (ignore)")]
    [Export] private bool _canAnimate = true;
    [Export] private bool _weaponOut;

  [ExportGroup("Nodes")]
    [Export] private Movement Movement { get; set; }
    [Export] private AnimationPlayer AnimationPlayer { get; set; }
    [Export] private Controller Controller { get; set; }

  public override void _Ready()
  {
    Movement.IdleAnimation += IdleAnimation;
    Movement.WalkAnimation += WalkAnimation;
    Controller.Unsheathe += WeaponAnimation;
    Controller.Attack += AttackAnimation;
  }

  private void IdleAnimation()
  {
    if (!_canAnimate)
      return;

    if (_weaponOut)
    {
      AnimationPlayer.Play("Unsheathed_Idle");
      return;
    }

    AnimationPlayer.Play("Idle");
  }

  private void WalkAnimation()
  {
    if (!_canAnimate)
      return;

    if (_weaponOut)
    {
      AnimationPlayer.Play("Unsheathed_Walk");
      return;
    }

    AnimationPlayer.Play("Walk");
  }

  private void WeaponAnimation()
  {
    if (_weaponOut)
    {
      _weaponOut = false;
      return;
    }

    AnimationPlayer.Play("Unsheathe");
    Controller.StopMove = true;
  }

  private void AttackAnimation()
  {
    if (!_weaponOut)
      return;
    AnimationPlayer.Play("SwordAttack");
    _canAnimate = false;
  }
}