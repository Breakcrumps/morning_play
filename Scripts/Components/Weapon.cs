[GlobalClass]
internal partial class Weapon : Area2D
{
  [ExportGroup("Stats")]
    [Export] private int _attackDamage = 10;
    [Export] private int _knockbackForce = 500;
    [Export] private int _stunTime = 15;

  public override void _Ready()
  {
    AreaEntered += DealDamage;
  }

  private void DealDamage(Area2D area)
  {
    if (area is not HurtboxComponent hurtbox)
      return;

    Attack attack = new()
    {
      AttackDamage = _attackDamage,
      KnockbackForce = _knockbackForce,
      StunTime = _stunTime,
      AttackPosition = GlobalPosition
    };

    hurtbox.TakeDamage(attack);
  }
}