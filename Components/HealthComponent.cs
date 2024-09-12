using Godot;

namespace Morning_Play.Components {

  partial class HealthComponent : Node2D {

    [Export]
    private int MaxHealth { get; set; }
    private int Health { get; set; }

    public override void _Ready() { Health = MaxHealth; }

    public void Damage(Attack attack) {
      Health -= attack.AttackDamage;
      if (Health <= 0) {
        GetParent().QueueFree();
      }
    }

  }

}