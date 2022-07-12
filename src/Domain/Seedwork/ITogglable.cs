namespace Domain.Seedwork;

public interface ITogglable
{
    bool Enabled { get; }
    void Enable();
    void Disable();
}
