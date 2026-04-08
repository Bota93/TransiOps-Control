namespace TransitOps.Domain.Entities;

public class RouteStop
{
    public Guid StopId { get; private set; }

    public int Sequence { get; private set; }

    public RouteStop(Guid stopId, int sequence)
    {
        if (stopId == Guid.Empty)
        {
            throw new ArgumentException("Stop id cannot be empty.", nameof(stopId));
        }

        if (sequence <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(sequence), "Sequence must be greater than zero.");
        }

        StopId = stopId;
        Sequence = sequence;
    }

    internal void UpdateSequence(int sequence)
    {
        if (sequence <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(sequence), "Sequence must be greater than zero.");
        }

        Sequence = sequence;
    }
}
