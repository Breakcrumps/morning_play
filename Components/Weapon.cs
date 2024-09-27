using Morning_Play.Types;

namespace Morning_Play.Components;

[GlobalClass]
partial class Weapon : Area2D {

  [ExportGroup("Stats")]
  [Export]
  private byte AttackDamage { get; set; } = 10;
  [Export]
  private byte KnockbackForce { get; set; } = 10;
  [Export]
  private byte StunTime { get; set; } = 5;

  public override void _Ready() {
    AreaEntered += DealDamage;
  }

  private void DealDamage(Area2D area) {
    if (area is not HurtboxComponent)
      return;
    var hurtbox = (HurtboxComponent)area;
    Attack attack = new() {
      AttackDamage = AttackDamage,
      KnockbackForce = KnockbackForce,
      StunTime = StunTime,
      AttackPosition = GlobalPosition
    };
    attack.AttackDamage = AttackDamage;
    hurtbox.TakeDamage(attack);
  }

}