using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sai.Domain;
using Sai.Domain.Model.Services;

namespace QuartzSchedule
{
    public class Start : IJob
    {
        public Start()
        {
        }

        private static CommissionService commissionService = new CommissionService();
        private static ActivityService activityService = new ActivityService();
        void IJob.Execute(IJobExecutionContext context)
        {
            commissionService.CloseCommissionsInscriptions();
            commissionService.CourseBegins(); //Cuando comienza el dictado del curso, el estado cambia de manera automática a "En curso".
            commissionService.CourseEnds(); //Cuando finaliza el dictado del curso, el estado cambia de manera automática a "En proceso de Carga de Resultados".
            activityService.VerifyVigency(); //Cuando finaliza la fecha de dictamen, tiene que pasar a estado "Fuera de vigencia".
        }
    }
}
