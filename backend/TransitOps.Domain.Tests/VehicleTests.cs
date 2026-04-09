using TransitOps.Domain.Entities;
using TransitOps.Domain.Enums;

namespace TransitOps.Domain.Tests;

public class VehicleTests
{
    [Fact]
    public void MarkAsAssigned_WhenVehicleIsOutOfService_ThrowsInvalidOperationException()
    {
        var vehicle = CreateVehicle();
        vehicle.SetOutOfService();

        var action = () => vehicle.MarkAsAssigned();

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal("Out-of-service vehicles cannot be assigned or started.", exception.Message);
    }

    [Fact]
    public void StartOperation_WhenVehicleIsOutOfService_ThrowsInvalidOperationException()
    {
        var vehicle = CreateVehicle();
        vehicle.SetOutOfService();

        var action = () => vehicle.StartOperation();

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal("Out-of-service vehicles cannot be assigned or started.", exception.Message);
    }

    [Fact]
    public void StartOperation_WhenVehicleIsAvailable_SetsStatusToInOperation()
    {
        var vehicle = CreateVehicle();

        vehicle.StartOperation();

        Assert.Equal(VehicleStatus.InOperation, vehicle.Status);
    }

    private static Vehicle CreateVehicle()
    {
        return new Vehicle(Guid.NewGuid(), "BUS-01", 40);
    }
}
