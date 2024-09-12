using Godot;
using Morning_Play.Types;

namespace Morning_Play.Components {

  [GlobalClass]
  partial class Weapon : Area2D {

    [Export]
    private int AttackStrength { get; set; } = 10;
    [Export]
    private int Knockback { get; set; } = 10;
    [Export]
    private Vector2 AttackPosition { get; set; }

    public override void _Ready() {
      AreaEntered += DealDamage;
    }

    private void DealDamage(Area2D area) {
      var hurtbox = (HurtboxComponent)area;
      Attack attack = new(AttackStrength, Knockback, AttackPosition);
      hurtbox.TakeDamage(attack);
    }

  }

}