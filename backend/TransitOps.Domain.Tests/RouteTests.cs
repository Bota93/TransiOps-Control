using TransitOps.Domain.Entities;
using TransitOps.Domain.Enums;

namespace TransitOps.Domain.Tests;

public class RouteTests
{
    [Fact]
    public void AddStop_WhenRouteIsDraft_AddsStopsInSequenceOrder()
    {
        var route = CreateDraftRoute();
        var firstStopId = Guid.NewGuid();
        var secondStopId = Guid.NewGuid();

        route.AddStop(firstStopId);
        route.AddStop(secondStopId);

        Assert.Collection(
            route.Stops.OrderBy(stop => stop.Sequence),
            firstStop =>
            {
                Assert.Equal(firstStopId, firstStop.StopId);
                Assert.Equal(1, firstStop.Sequence);
            },
            secondStop =>
            {
                Assert.Equal(secondStopId, secondStop.StopId);
                Assert.Equal(2, secondStop.Sequence);
            });
    }

    [Fact]
    public void AddStop_WhenStopAlreadyExistsInRoute_ThrowsInvalidOperationException()
    {
        var route = CreateDraftRoute();
        var stopId = Guid.NewGuid();
        route.AddStop(stopId);

        var action = () => route.AddStop(stopId);

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal("A route cannot contain the same stop more than once.", exception.Message);
    }

    [Fact]
    public void RemoveStop_WhenRouteIsDraft_ResequencesRemainingStops()
    {
        var route = CreateDraftRoute();
        var firstStopId = Guid.NewGuid();
        var secondStopId = Guid.NewGuid();
        var thirdStopId = Guid.NewGuid();

        route.AddStop(firstStopId);
        route.AddStop(secondStopId);
        route.AddStop(thirdStopId);

        route.RemoveStop(secondStopId);

        Assert.Collection(
            route.Stops.OrderBy(stop => stop.Sequence),
            firstStop =>
            {
                Assert.Equal(firstStopId, firstStop.StopId);
                Assert.Equal(1, firstStop.Sequence);
            },
            secondStop =>
            {
                Assert.Equal(thirdStopId, secondStop.StopId);
                Assert.Equal(2, secondStop.Sequence);
            });
    }

    [Fact]
    public void AddStop_WhenRouteIsNotDraft_ThrowsInvalidOperationException()
    {
        var route = CreateActiveRoute();

        var action = () => route.AddStop(Guid.NewGuid());

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal("Only draft routes can be modified.", exception.Message);
    }

    [Fact]
    public void RemoveStop_WhenRouteIsNotDraft_ThrowsInvalidOperationException()
    {
        var route = CreateActiveRoute();
        var existingStopId = route.Stops.First().StopId;

        var action = () => route.RemoveStop(existingStopId);

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal("Only draft routes can be modified.", exception.Message);
    }

    [Fact]
    public void Activate_WhenDraftRouteHasAtLeastTwoStops_SetsStatusToActive()
    {
        var route = CreateDraftRouteWithTwoStops();

        route.Activate();

        Assert.Equal(RouteStatus.Active, route.Status);
    }

    [Fact]
    public void Activate_WhenRouteIsAlreadyActive_ThrowsInvalidOperationException()
    {
        var route = CreateActiveRoute();

        var action = () => route.Activate();

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal("Only draft routes can be activated.", exception.Message);
    }

    [Fact]
    public void Activate_WhenDraftRouteHasFewerThanTwoStops_ThrowsInvalidOperationException()
    {
        var route = CreateDraftRoute();
        route.AddStop(Guid.NewGuid());

        var action = () => route.Activate();

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal("A route must contain at least two stops before it can be activated.", exception.Message);
    }

    [Fact]
    public void Suspend_WhenRouteIsActive_SetsStatusToSuspended()
    {
        var route = CreateActiveRoute();

        route.Suspend();

        Assert.Equal(RouteStatus.Suspended, route.Status);
    }

    [Fact]
    public void Suspend_WhenRouteIsAlreadySuspended_ThrowsInvalidOperationException()
    {
        var route = CreateActiveRoute();
        route.Suspend();

        var action = () => route.Suspend();

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal("Only active routes can be suspended.", exception.Message);
    }

    [Fact]
    public void Resume_WhenRouteIsSuspended_SetsStatusToActive()
    {
        var route = CreateActiveRoute();
        route.Suspend();

        route.Resume();

        Assert.Equal(RouteStatus.Active, route.Status);
    }

    [Fact]
    public void Resume_WhenRouteIsAlreadyActive_ThrowsInvalidOperationException()
    {
        var route = CreateActiveRoute();

        var action = () => route.Resume();

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal("Only suspended routes can be resumed.", exception.Message);
    }

    [Fact]
    public void Retire_WhenRouteIsActive_SetsStatusToRetired()
    {
        var route = CreateActiveRoute();

        route.Retire();

        Assert.Equal(RouteStatus.Retired, route.Status);
    }

    [Fact]
    public void Retire_WhenRouteIsAlreadyRetired_ThrowsInvalidOperationException()
    {
        var route = CreateActiveRoute();
        route.Retire();

        var action = () => route.Retire();

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal("Only active or suspended routes can be retired.", exception.Message);
    }

    [Fact]
    public void Resume_WhenRouteIsNotSuspended_ThrowsInvalidOperationException()
    {
        var route = CreateDraftRouteWithTwoStops();

        var action = () => route.Resume();

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal("Only suspended routes can be resumed.", exception.Message);
    }

    [Fact]
    public void Suspend_WhenRouteIsNotActive_ThrowsInvalidOperationException()
    {
        var route = CreateDraftRouteWithTwoStops();

        var action = () => route.Suspend();

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal("Only active routes can be suspended.", exception.Message);
    }

    [Fact]
    public void Retire_WhenRouteIsDraft_ThrowsInvalidOperationException()
    {
        var route = CreateDraftRoute();

        var action = () => route.Retire();

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal("Only active or suspended routes can be retired.", exception.Message);
    }

    private static Route CreateDraftRoute()
    {
        return new Route(Guid.NewGuid(), "R-100", "Airport Shuttle");
    }

    private static Route CreateDraftRouteWithTwoStops()
    {
        var route = CreateDraftRoute();
        route.AddStop(Guid.NewGuid());
        route.AddStop(Guid.NewGuid());
        return route;
    }

    private static Route CreateActiveRoute()
    {
        var route = CreateDraftRouteWithTwoStops();
        route.Activate();
        return route;
    }
}
