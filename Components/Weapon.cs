using Godot;

namespace Morning_Play.Components {

  partial class Weapon : Node2D {

    [Export]
    private int AttackStrength { get; set; } 
    [Export]
    private int KnockBack { get; set; }
    [Export]
    private Vector2 AttackPosition { get; set; }

    

  }

}