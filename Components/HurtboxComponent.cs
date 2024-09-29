using Morning_Play.Types;

namespace Morning_Play.Components;

internal partial class HurtboxComponent : Area2D {

  [Export]
  private HealthComponent HealthComponent { get; set; }

  public void TakeDamage(Attack attack) {
    HealthComponent.TakeDamage(attack);
  }

}