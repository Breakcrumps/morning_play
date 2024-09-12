using Godot;

namespace Morning_Play.Types {

  partial struct Attack(int attackDamage, int knockBack, Vector2 attackPosition) {
    public readonly int AttackDamage => attackDamage;
    public readonly int KnockBack => knockBack;
    public readonly Vector2 AttackPosition => attackPosition;
  }

}