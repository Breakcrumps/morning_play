public partial class HurtboxComponent : Area2D {

  [Export]
  private HealthComponent HealthComponent { get; set; }

  public void TakeDamage(Attack attack) {
    HealthComponent.TakeDamage(attack);
  }

}