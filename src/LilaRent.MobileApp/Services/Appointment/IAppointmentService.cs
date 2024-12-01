using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LilaRent.MobileApp.Entities;

namespace LilaRent.MobileApp.Services;

    public interface IAppointmentService
    {
		Appointment GetAppointmentByAnnouncmentId(long id);
    }
