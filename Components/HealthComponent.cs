using System.Threading.Tasks;
using Godot;
using Morning_Play.NPC;
using Morning_Play.Types;

namespace Morning_Play.Components {

  [GlobalClass]
  partial class HealthComponent : Node2D {

    [Export]
    private int MaxHealth { get; set; }
    private int Health { get; set; }
    [Export]
    private CharacterBody2D Character { get; set; }
    [Export]
    private StateMachine StateMachine { get; set; }

    public override void _Ready() {
      Health = MaxHealth;
    }

    public void TakeDamage(Attack attack) {

      Health -= attack.AttackDamage;

      if (Health <= 0) {
        GetParent().QueueFree();
        return;
      }

      Knockback(attack.AttackPosition, attack.KnockbackForce, attack.StunTime);

    }

    private async void Knockback(Vector2 attackPos, int knockBackForce, int stunTime) {

      var initVelocity = (GlobalPosition - attackPos).Normalized() * knockBackForce;
      Character.Velocity = initVelocity;

      StateMachine.CanManageState = false;
      
      await Decelerate(stunTime, initVelocity);

      StateMachine.CanManageState = true;

    }

    private async Task Decelerate(int time, Vector2 initVelocity) {

      for (int i = time; i > 0; i--) {

        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);

        var velocity = Character.Velocity;
        velocity.X -= initVelocity.X / time;
        velocity.Y -= initVelocity.Y / time;
        Character.Velocity = velocity;

      }

    }

  }

}