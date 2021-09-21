namespace Assets.Scripts.Interfaces
{
    public interface IDrawGizmo
    {
        bool DrawGizmo { get; set; }

        void OnDrawGizmos();
    }
}