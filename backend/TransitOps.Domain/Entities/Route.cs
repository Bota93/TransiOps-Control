using TransitOps.Domain.Enums;

namespace TransitOps.Domain.Entities;

public class Route
{
    private readonly List<RouteStop> _stops = [];

    public Guid Id { get; private set; }

    public string Code { get; private set; }

    public string Name { get; private set; }

    public RouteStatus Status { get; private set; }

    public IReadOnlyCollection<RouteStop> Stops => _stops.AsReadOnly();

    public Route(Guid id, string code, string name)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Route id cannot be empty.", nameof(id));
        }

        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException("Route code cannot be empty.", nameof(code));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Route name cannot be empty.", nameof(name));
        }

        Id = id;
        Code = code.Trim();
        Name = name.Trim();
        Status = RouteStatus.Draft;
    }

    public void AddStop(Guid stopId)
    {
        EnsureIsDraft();

        if (stopId == Guid.Empty)
        {
            throw new ArgumentException("Stop id cannot be empty.", nameof(stopId));
        }

        if (_stops.Any(stop => stop.StopId == stopId))
        {
            throw new InvalidOperationException("A route cannot contain the same stop more than once.");
        }

        _stops.Add(new RouteStop(stopId, _stops.Count + 1));
    }

    public void RemoveStop(Guid stopId)
    {
        EnsureIsDraft();

        if (stopId == Guid.Empty)
        {
            throw new ArgumentException("Stop id cannot be empty.", nameof(stopId));
        }

        var routeStop = _stops.SingleOrDefault(stop => stop.StopId == stopId);

        if (routeStop is null)
        {
            throw new InvalidOperationException("The stop does not belong to this route.");
        }

        _stops.Remove(routeStop);
        ResequenceStops();
    }

    public void Activate()
    {
        if (Status != RouteStatus.Draft)
        {
            throw new InvalidOperationException("Only draft routes can be activated.");
        }

        if (_stops.Count < 2)
        {
            throw new InvalidOperationException("A route must contain at least two stops before it can be activated.");
        }

        Status = RouteStatus.Active;
    }

    public void Resume()
    {
        if (Status != RouteStatus.Suspended)
        {
            throw new InvalidOperationException("Only suspended routes can be resumed.");
        }

        Status = RouteStatus.Active;
    }

    public void Suspend()
    {
        if (Status != RouteStatus.Active)
        {
            throw new InvalidOperationException("Only active routes can be suspended.");
        }

        Status = RouteStatus.Suspended;
    }

    public void Retire()
    {
        if (Status == RouteStatus.Draft || Status == RouteStatus.Retired)
        {
            throw new InvalidOperationException("Only active or suspended routes can be retired.");
        }

        Status = RouteStatus.Retired;
    }

    private void EnsureIsDraft()
    {
        if (Status != RouteStatus.Draft)
        {
            throw new InvalidOperationException("Only draft routes can be modified.");
        }
    }

    private void ResequenceStops()
    {
        for (var index = 0; index < _stops.Count; index++)
        {
            _stops[index].UpdateSequence(index + 1);
        }
    }
}
