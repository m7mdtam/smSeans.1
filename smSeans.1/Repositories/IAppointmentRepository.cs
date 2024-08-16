using System;
using System.Threading.Tasks;

public interface IAppointmentRepository
{
    Task<Appointment> GetAppointmentByIdAsync(int id);
    Task<int> CreateAppointmentAsync(Appointment appointment);
    Task<bool>  UpdateAppointmentAsync(Appointment appointment);
    Task<bool> DeleteAppointmentAsync(int id);
}
