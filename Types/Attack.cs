namespace Morning_Play.Types;

struct Attack {
  public byte AttackDamage { get; set; }
  public byte KnockbackForce { get; set; }
  public byte StunTime { get; set; }
  public Vector2 AttackPosition { get; set; }
}