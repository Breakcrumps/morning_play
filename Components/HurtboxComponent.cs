using Godot;
using Morning_Play.Types;

namespace Morning_Play.Components;

partial class HurtboxComponent : Area2D {

  [Export]
  private HealthComponent HealthComponent;

  public void TakeDamage(Attack attack) {
    HealthComponent.TakeDamage(attack);
  }

}