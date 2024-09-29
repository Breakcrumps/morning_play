public partial class SceneTransition : Area2D {

  [Export]
  private PackedScene _scene;

  public override void _Ready() {
    BodyEntered += TransitionScene;
  }

  private void TransitionScene(Node2D body) {
    if (body is not Player)
      return;
    GetTree().ChangeSceneToPacked(_scene);
  }

}