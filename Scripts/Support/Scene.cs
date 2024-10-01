[Tool]
[GlobalClass]
public partial class Scene : Node2D {

  [Export]
  private Vector2[] _playerSpawnPositions = [];
  public int PlayerSpawnIndex { get; set; } = 0;

  [Export]
  private CharacterBody2D Player { get; set; }

  public override void _Ready() {
    Player.GlobalPosition = _playerSpawnPositions[PlayerSpawnIndex];
  }

}