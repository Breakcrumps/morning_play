using Morning_Play.Types;

namespace Morning_Play.Components;

[GlobalClass]
partial class Weapon : Area2D {

  [ExportGroup("Stats")]
  [Export]
  private byte _attackDamage = 10;
  [Export]
  private byte _knockbackForce = 10;
  [Export]
  private byte _stunTime = 5;

  public override void _Ready() {
    AreaEntered += DealDamage;
  }

  private void DealDamage(Area2D area) {

    if (area is not HurtboxComponent)
      return;

    var hurtbox = (HurtboxComponent)area;

    Attack attack = new() {
      AttackDamage = _attackDamage,
      KnockbackForce = _knockbackForce,
      StunTime = _stunTime,
      AttackPosition = GlobalPosition
    };
    
    attack.AttackDamage = _attackDamage;
    hurtbox.TakeDamage(attack);

  }

}