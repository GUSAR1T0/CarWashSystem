using System;

namespace VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.CompanyProfile
{
    public class CarWashFullEntity
    {
        // Car Wash profile
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string Location { get; set; } = "";
        public decimal CoordinateX { get; set; }
        public decimal CoordinateY { get; set; }
        public string? Description { get; set; }
        public bool HasCafe { get; set; }
        public bool HasRestZone { get; set; }
        public bool HasParking { get; set; }
        public bool HasWC { get; set; }
        public bool HasCardPayment { get; set; }

        // Car Wash working hours
        public TimeSpan? MondayStartTime { get; set; }
        public TimeSpan? MondayStopTime { get; set; }
        public TimeSpan? TuesdayStartTime { get; set; }
        public TimeSpan? TuesdayStopTime { get; set; }
        public TimeSpan? WednesdayStartTime { get; set; }
        public TimeSpan? WednesdayStopTime { get; set; }
        public TimeSpan? ThursdayStartTime { get; set; }
        public TimeSpan? ThursdayStopTime { get; set; }
        public TimeSpan? FridayStartTime { get; set; }
        public TimeSpan? FridayStopTime { get; set; }
        public TimeSpan? SaturdayStartTime { get; set; }
        public TimeSpan? SaturdayStopTime { get; set; }
        public TimeSpan? SundayStartTime { get; set; }
        public TimeSpan? SundayStopTime { get; set; }
    }

    public class CarWashShortEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Location { get; set; } = "";
    }
}