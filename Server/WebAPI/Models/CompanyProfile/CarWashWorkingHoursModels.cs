using System;
using VXDesign.Store.CarWashSystem.Server.Core.Common;
using VXDesign.Store.CarWashSystem.Server.DataStorage.Entities.CompanyProfile;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Models.CompanyProfile
{
    public class CarWashWorkingDayModel
    {
        public string? StartTime { get; set; }
        public string? StopTime { get; set; }

        public CarWashWorkingDayModel()
        {
        }

        public CarWashWorkingDayModel(TimeSpan? startTime, TimeSpan? stopTime)
        {
            StartTime = startTime?.ToString(@"hh\:mm");
            StopTime = stopTime?.ToString(@"hh\:mm");
        }
    }

    public class CarWashWorkingHoursModel : IEntityToModelConvertible<CarWashFullEntity, CarWashWorkingHoursModel>
    {
        public CarWashWorkingDayModel? Monday { get; set; }
        public CarWashWorkingDayModel? Tuesday { get; set; }
        public CarWashWorkingDayModel? Wednesday { get; set; }
        public CarWashWorkingDayModel? Thursday { get; set; }
        public CarWashWorkingDayModel? Friday { get; set; }
        public CarWashWorkingDayModel? Saturday { get; set; }
        public CarWashWorkingDayModel? Sunday { get; set; }

        public CarWashWorkingHoursModel ToModel(CarWashFullEntity? entity)
        {
            if (entity == null) throw new Exception(ExceptionMessage.EmptyResponse);

            Monday = new CarWashWorkingDayModel(entity.MondayStartTime, entity.MondayStopTime);
            Tuesday = new CarWashWorkingDayModel(entity.TuesdayStartTime, entity.TuesdayStopTime);
            Wednesday = new CarWashWorkingDayModel(entity.WednesdayStartTime, entity.WednesdayStopTime);
            Thursday = new CarWashWorkingDayModel(entity.ThursdayStartTime, entity.ThursdayStopTime);
            Friday = new CarWashWorkingDayModel(entity.FridayStartTime, entity.FridayStopTime);
            Saturday = new CarWashWorkingDayModel(entity.SaturdayStartTime, entity.SaturdayStopTime);
            Sunday = new CarWashWorkingDayModel(entity.SundayStartTime, entity.SundayStopTime);
            return this;
        }
    }

    internal static class CarWashWorkingHoursExtensions
    {
        internal static bool TryToGet(this CarWashWorkingDayModel? model, out (TimeSpan? startTime, TimeSpan? stopTime)? workingDay)
        {
            if (model != null)
            {
                var startTimeResult = TimeSpan.TryParse(model.StartTime, out var startTime) ? startTime : (TimeSpan?) null;
                var stopTimeResult = TimeSpan.TryParse(model.StopTime, out var stopTime) ? stopTime : (TimeSpan?) null;
                workingDay = (startTimeResult, stopTimeResult);
                return true;
            }

            workingDay = null;
            return false;
        }

        public static bool IsCarWashOpen(CarWashFullEntity entity)
        {
            var now = DateTime.Now;

            bool IsCarWashOpenToday(TimeSpan? startTime, TimeSpan? stopTime)
            {
                TimeSpan? stopTimeValue;
                if (stopTime.HasValue) stopTimeValue = stopTime.Value != TimeSpan.Zero ? stopTime.Value : new TimeSpan(23, 59, 59);
                else stopTimeValue = null;
                return startTime <= now.TimeOfDay && stopTimeValue >= now.TimeOfDay;
            }

            return now.DayOfWeek switch
            {
                DayOfWeek.Monday => IsCarWashOpenToday(entity.MondayStartTime, entity.MondayStopTime),
                DayOfWeek.Tuesday => IsCarWashOpenToday(entity.TuesdayStartTime, entity.TuesdayStopTime),
                DayOfWeek.Wednesday => IsCarWashOpenToday(entity.WednesdayStartTime, entity.WednesdayStopTime),
                DayOfWeek.Thursday => IsCarWashOpenToday(entity.ThursdayStartTime, entity.ThursdayStopTime),
                DayOfWeek.Friday => IsCarWashOpenToday(entity.FridayStartTime, entity.FridayStopTime),
                DayOfWeek.Saturday => IsCarWashOpenToday(entity.SaturdayStartTime, entity.SaturdayStopTime),
                DayOfWeek.Sunday => IsCarWashOpenToday(entity.SundayStartTime, entity.SundayStopTime),
                _ => throw new Exception(ExceptionMessage.TimeSpanIsInvalid)
            };
        }
    }
}