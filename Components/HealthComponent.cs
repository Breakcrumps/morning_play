using System.Threading.Tasks;
using Morning_Play.ControlledCharacter;
using Morning_Play.NPC;
using Morning_Play.Types;

namespace Morning_Play.Components;

[GlobalClass]
partial class HealthComponent : Node2D {

  private CharacterBody2D Character => GetOwner<CharacterBody2D>();

  [ExportGroup("Stats")]
  [Export]
  private int _maxHealth;
  private int _health;
  
  [ExportGroup("State-NPC Control-Playable")]
  [Export]
  private StateMachine StateMachine { get; set; }
  [Export]
  private Controller Controller { get; set; }

  public override void _Ready() {
    _health = _maxHealth;
  }

  public async void TakeDamage(Attack attack) {

    _health -= attack.AttackDamage;

    await Knockback(attack.AttackPosition, attack.KnockbackForce, attack.StunTime);

    if (_health <= 0)
      GetParent().QueueFree();

  }

  private async Task Knockback(Vector2 attackPos, int knockBackForce, int stunTime) {

    var initVelocity = (GlobalPosition - attackPos).Normalized() * knockBackForce;
    Character.Velocity = initVelocity;

    if (StateMachine is not null)
      StateMachine.CanManageState = false;
    else
      Controller.CanControl = false;
    
    await Decelerate(stunTime, initVelocity);

    if (StateMachine is not null)
      StateMachine.CanManageState = true;
    else
      Controller.CanControl = true;

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