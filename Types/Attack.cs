namespace Morning_Play.Types;

partial struct Attack {
  public int AttackDamage { get; set; }
  public int KnockbackForce { get; set; }
  public int StunTime { get; set; }
  public Vector2 AttackPosition { get; set; }
}