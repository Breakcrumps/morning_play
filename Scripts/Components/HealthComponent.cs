using System.Threading.Tasks;

[GlobalClass]
public partial class HealthComponent : Node2D
{
  private int _health;

  [ExportGroup("Stats")]
    [Export] private int _maxHealth = 100;

  private CharacterBody2D Character => GetOwner<CharacterBody2D>();

  [ExportGroup("(EITHER!) (State for NPC)")]
    [Export] private StateMachine StateMachine { get; set; }
    [Export] private Controller Controller { get; set; }

  public override void _Ready()
  {
    _health = _maxHealth;
  }

  public async void TakeDamage(Attack attack)
  {
    _health -= attack.AttackDamage;

    await Knockback(attack.AttackPosition, attack.KnockbackForce, attack.StunTime);

    if (_health <= 0)
      GetParent().QueueFree();
  }

  private async Task Knockback(Vector2 attackPos, int knockBackForce, int stunTime)
  {
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

  private async Task Decelerate(int time, Vector2 initVelocity)
  {
    for (var i = time; i > 0; i--)
    {
      await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);

      var velocity = Character.Velocity;
      velocity.X -= initVelocity.X / time;
      velocity.Y -= initVelocity.Y / time;
      Character.Velocity = velocity;
    }
  }
}