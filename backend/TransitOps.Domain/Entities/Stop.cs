using TransitOps.Domain.Enums;

namespace TransitOps.Domain.Entities;

public class Stop
{
    public Guid Id { get; private set; }

    public string Code { get; private set; }

    public string Name { get; private set; }

    public StopStatus Status { get; private set; }



    public Stop(Guid id, string code, string name)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Stop id cannot be empty.", nameof(id));
        }

        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException("Stop code cannot be empty.", nameof(code));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Stop name cannot be empty.", nameof(name));
        }

        Id = id;
        Code = code.Trim();
        Name = name.Trim();
        Status = StopStatus.Active;
    }

    public void Activate()
    {
        if (Status == StopStatus.Active)
        {
            return;
        }

        Status = StopStatus.Active;
    }

    public void Deactivate()
    {
        if (Status == StopStatus.Inactive)
        {
            return;
        }
        Status = StopStatus.Inactive;
    }
}
