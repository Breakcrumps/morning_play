using Godot;

namespace Morning_Play.Components {

  partial class HitboxComponent : Area2D {

	[Export]
	private HealthComponent HealthComponent { get; set; }

	private void Damage(Attack attack) {
	  HealthComponent.Damage(attack);
	}

  }

}
