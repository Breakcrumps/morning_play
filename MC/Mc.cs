using System;
using Godot;
using Morning_Play.Globals;

namespace Morning_Play.MC {

  partial class Mc : CharacterBody2D {
    
    [Export]
    private bool CanMove { get; set; } = true;
    [Export]
    private bool WeaponOut { get; set; } = false;

    private static float AccelerationTime => 5;
    private static float Speed { get; set; } = 0;
    private float MaxSpeed() {
      if (Controller.Run)
        return 100;
      return 75;
    }

    private McController Controller => GetNode<McController>("Controller");
    private AnimationPlayer Player => GetNode<AnimationPlayer>("AnimationPlayer");

    public override void _PhysicsProcess(double delta) {
      ManageWeapon();
      ApplyVelocity(Controller.Velocity);
      MoveAndSlide();
    }

    private void ApplyVelocity(Vector2 velocity) {

      if (!CanMove) {
        Velocity = Vector2.Zero;
        return;
      }

      if (velocity == Vector2.Zero) {
        IdleAnimation();
        Speed = 0;
      }
      else {
        WalkAnimation();
        if (Speed < MaxSpeed())
          Speed += MaxSpeed() / AccelerationTime;
        else
          Speed = MaxSpeed();
      }

      Velocity = velocity.Normalized() * Speed;
      
    }

    private void IdleAnimation() {
      if (WeaponOut) {
        Player.Play("Unsheethed_Idle");
        return;
      }
      Player.Play("Idle");
    }
    private void WalkAnimation() {
      if (WeaponOut) {
        Player.Play("Unsheethed_Walk");
        return;
      }
      Player.Play("Walk");
    }

    private void ManageWeapon() {
      if (Controller.Unsheethe) {
        Player.Play("Unsheethe");
        CanMove = false;
        return;
      }
    }

  }

}
