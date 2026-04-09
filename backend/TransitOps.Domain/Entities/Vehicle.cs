using TransitOps.Domain.Enums;

namespace TransitOps.Domain.Entities;

public class Vehicle
{
    public Guid Id { get; private set; }

    public string Code { get; private set; }

    public int Capacity { get; private set; }

    public VehicleStatus Status { get; private set; }

    public Vehicle(Guid id, string code, int capacity)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Vehicle id cannot be empty.", nameof(id));
        }

        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException("Vehicle code cannot be empty.", nameof(code));
        }

        if (capacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity), "Vehicle capacity must be greater than zero.");
        }

        Id = id;
        Code = code.Trim();
        Capacity = capacity;
        Status = VehicleStatus.Available;
    }

    public void SetAvailable()
    {
        if (Status == VehicleStatus.Available)
        {
            return;
        }
        Status = VehicleStatus.Available;
    }

    public void MarkAsAssigned()
    {
        if (Status == VehicleStatus.Assigned)
        {
            return;
        }
        EnsureNotOutOfService();
        Status = VehicleStatus.Assigned;
    }

    public void StartOperation()
    {
        if (Status == VehicleStatus.InOperation)
        {
            return;
        }
        EnsureNotOutOfService();
        Status = VehicleStatus.InOperation;
    }

    public void SetOutOfService()
    {
        if (Status == VehicleStatus.OutOfService)
        {
            return;
        }
        Status = VehicleStatus.OutOfService;
    }

    private void EnsureNotOutOfService()
    {
        if (Status == VehicleStatus.OutOfService)
        {
            throw new InvalidOperationException("Out-of-service vehicles cannot be assigned or started.");
        }
    }
}
