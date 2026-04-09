using TransitOps.Domain.Enums;

namespace TransitOps.Domain.Entities;

public class Trip
{
    public Guid Id { get; private set; }

    public Guid RouteId { get; private set; }

    public Guid? AssignedVehicleId { get; private set; }

    public DateTime PlannedStartTime { get; private set; }

    public DateTime PlannedEndTime { get; private set; }

    public TripStatus Status { get; private set; }

    public Trip(Guid id, Guid routeId, DateTime plannedStartTime, DateTime plannedEndTime)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Trip id cannot be empty.", nameof(id));
        }

        if (routeId == Guid.Empty)
        {
            throw new ArgumentException("Route id cannot be empty.", nameof(routeId));
        }

        if (plannedEndTime <= plannedStartTime)
        {
            throw new ArgumentException("Planned end time must be later than planned start time.", nameof(plannedEndTime));
        }

        Id = id;
        RouteId = routeId;
        AssignedVehicleId = null;
        PlannedStartTime = plannedStartTime;
        PlannedEndTime = plannedEndTime;
        Status = TripStatus.Planned;
    }

    public void AssignVehicle(Guid vehicleId)
    {
        EnsureCanManageVehicleAssignment();

        if (vehicleId == Guid.Empty)
        {
            throw new ArgumentException("Vehicle id cannot be empty.", nameof(vehicleId));
        }

        AssignedVehicleId = vehicleId;
    }

    public void UnassignVehicle()
    {
        EnsureCanManageVehicleAssignment();
        AssignedVehicleId = null;
    }

    public void MarkReady()
    {
        if (Status != TripStatus.Planned)
        {
            throw new InvalidOperationException("Only planned trips can be marked as ready.");
        }

        if (AssignedVehicleId is null)
        {
            throw new InvalidOperationException("A trip must have an assigned vehicle before it can be marked as ready.");
        }

        Status = TripStatus.Ready;
    }

    public void Start()
    {
        if (Status != TripStatus.Ready)
        {
            throw new InvalidOperationException("Only ready trips can be started.");
        }

        Status = TripStatus.InProgress;
    }

    public void Complete()
    {
        if (Status != TripStatus.InProgress)
        {
            throw new InvalidOperationException("Only trips in progress can be completed.");
        }

        Status = TripStatus.Completed;
    }

    public void Cancel()
    {
        if (Status != TripStatus.Planned && Status != TripStatus.Ready)
        {
            throw new InvalidOperationException("Only planned or ready trips can be cancelled.");
        }

        AssignedVehicleId = null;
        Status = TripStatus.Cancelled;
    }

    private void EnsureCanManageVehicleAssignment()
    {
        if (Status != TripStatus.Planned && Status != TripStatus.Ready)
        {
            throw new InvalidOperationException("Vehicle assignment is only allowed while the trip is planned or ready.");
        }
    }
}
