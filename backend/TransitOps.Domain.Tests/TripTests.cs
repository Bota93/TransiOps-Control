using TransitOps.Domain.Entities;
using TransitOps.Domain.Enums;

namespace TransitOps.Domain.Tests;

public class TripTests
{
    [Fact]
    public void MarkReady_Start_Complete_FollowsTheValidTripLifecycle()
    {
        var trip = CreatePlannedTrip();
        trip.AssignVehicle(Guid.NewGuid());

        trip.MarkReady();
        trip.Start();
        trip.Complete();

        Assert.Equal(TripStatus.Completed, trip.Status);
    }

    [Fact]
    public void Cancel_WhenTripIsPlanned_SetsStatusToCancelled()
    {
        var trip = CreatePlannedTrip();

        trip.Cancel();

        Assert.Equal(TripStatus.Cancelled, trip.Status);
    }

    [Fact]
    public void MarkReady_WhenTripIsAlreadyReady_ThrowsInvalidOperationException()
    {
        var trip = CreateReadyTrip();

        var action = () => trip.MarkReady();

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal("Only planned trips can be marked as ready.", exception.Message);
    }

    [Fact]
    public void Cancel_WhenTripIsReady_SetsStatusToCancelledAndReleasesVehicle()
    {
        var trip = CreateReadyTrip();

        trip.Cancel();

        Assert.Equal(TripStatus.Cancelled, trip.Status);
        Assert.Null(trip.AssignedVehicleId);
    }

    [Fact]
    public void Start_WhenTripIsPlanned_ThrowsInvalidOperationException()
    {
        var trip = CreatePlannedTrip();

        var action = () => trip.Start();

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal("Only ready trips can be started.", exception.Message);
    }

    [Fact]
    public void Complete_WhenTripIsReady_ThrowsInvalidOperationException()
    {
        var trip = CreateReadyTrip();

        var action = () => trip.Complete();

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal("Only trips in progress can be completed.", exception.Message);
    }

    [Fact]
    public void Cancel_WhenTripIsInProgress_ThrowsInvalidOperationException()
    {
        var trip = CreateInProgressTrip();

        var action = () => trip.Cancel();

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal("Only planned or ready trips can be cancelled.", exception.Message);
    }

    [Fact]
    public void Start_WhenTripIsAlreadyInProgress_ThrowsInvalidOperationException()
    {
        var trip = CreateInProgressTrip();

        var action = () => trip.Start();

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal("Only ready trips can be started.", exception.Message);
    }

    [Fact]
    public void Start_WhenTripIsCompleted_ThrowsInvalidOperationException()
    {
        var trip = CreateCompletedTrip();

        var action = () => trip.Start();

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal("Only ready trips can be started.", exception.Message);
    }

    [Fact]
    public void Cancel_WhenTripIsCompleted_ThrowsInvalidOperationException()
    {
        var trip = CreateCompletedTrip();

        var action = () => trip.Cancel();

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal("Only planned or ready trips can be cancelled.", exception.Message);
    }

    [Fact]
    public void Complete_WhenTripIsAlreadyCompleted_ThrowsInvalidOperationException()
    {
        var trip = CreateCompletedTrip();

        var action = () => trip.Complete();

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal("Only trips in progress can be completed.", exception.Message);
    }

    [Fact]
    public void Start_WhenTripIsCancelled_ThrowsInvalidOperationException()
    {
        var trip = CreateCancelledTrip();

        var action = () => trip.Start();

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal("Only ready trips can be started.", exception.Message);
    }

    [Fact]
    public void Complete_WhenTripIsCancelled_ThrowsInvalidOperationException()
    {
        var trip = CreateCancelledTrip();

        var action = () => trip.Complete();

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal("Only trips in progress can be completed.", exception.Message);
    }

    [Fact]
    public void Cancel_WhenTripIsAlreadyCancelled_ThrowsInvalidOperationException()
    {
        var trip = CreateCancelledTrip();

        var action = () => trip.Cancel();

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal("Only planned or ready trips can be cancelled.", exception.Message);
    }

    [Fact]
    public void MarkReady_WhenNoVehicleIsAssigned_ThrowsInvalidOperationException()
    {
        var trip = CreatePlannedTrip();

        var action = () => trip.MarkReady();

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal("A trip must have an assigned vehicle before it can be marked as ready.", exception.Message);
    }

    [Fact]
    public void AssignVehicle_WhenTripIsInProgress_ThrowsInvalidOperationException()
    {
        var trip = CreateInProgressTrip();

        var action = () => trip.AssignVehicle(Guid.NewGuid());

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal("Vehicle assignment is only allowed while the trip is planned or ready.", exception.Message);
    }

    [Fact]
    public void UnassignVehicle_WhenTripIsCompleted_ThrowsInvalidOperationException()
    {
        var trip = CreateCompletedTrip();

        var action = () => trip.UnassignVehicle();

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal("Vehicle assignment is only allowed while the trip is planned or ready.", exception.Message);
    }

    private static Trip CreatePlannedTrip()
    {
        return new Trip(
            Guid.NewGuid(),
            Guid.NewGuid(),
            new DateTime(2026, 4, 9, 8, 0, 0, DateTimeKind.Utc),
            new DateTime(2026, 4, 9, 9, 0, 0, DateTimeKind.Utc));
    }

    private static Trip CreateReadyTrip()
    {
        var trip = CreatePlannedTrip();
        trip.AssignVehicle(Guid.NewGuid());
        trip.MarkReady();
        return trip;
    }

    private static Trip CreateInProgressTrip()
    {
        var trip = CreateReadyTrip();
        trip.Start();
        return trip;
    }

    private static Trip CreateCompletedTrip()
    {
        var trip = CreateInProgressTrip();
        trip.Complete();
        return trip;
    }

    private static Trip CreateCancelledTrip()
    {
        var trip = CreateReadyTrip();
        trip.Cancel();
        return trip;
    }
}
