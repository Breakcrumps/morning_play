using Godot;

namespace Morning_Play.NPC {

  partial class NPCIdle : State {

    [Export]
    private NPC NPC { get; set; }

    [Export]
    private int MovementSpeed { get; set; }
    private Vector2 MovementDirection { get; set; }
    private double WanderTime { get; set; }

    private void RandomiseWander() {
      MovementDirection = 
          new Vector2((float)GD.RandRange(-1.0, 1.0), (float)GD.RandRange(-1.0, 1.0));
      WanderTime = GD.RandRange(1, 3);
    }

    public override void Enter() {
      RandomiseWander();
    }
    public override void _Process(double delta) {
      if (WanderTime < 0) {
        RandomiseWander();
        return;
      }
      WanderTime -= delta;
    }
    public override void _PhysicsProcess(double delta) {
      NPC.Velocity = MovementDirection * MovementSpeed;
    }
    public override void Exit() {
      
    }

  }

}