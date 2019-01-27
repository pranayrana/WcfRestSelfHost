using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WcfHostService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            try
            {
                if (Environment.UserInteractive)
                {
                    Service1 service1 = new Service1();
                    service1.TestStartup(args);
                    Console.WriteLine("Service Started at: http://localhost:8081/FileUploadService");
                    Console.ReadLine();
                    service1.TestStop();
                }
                else
                {

                    ServiceBase[] ServicesToRun;
                    ServicesToRun = new ServiceBase[]
                    {
                new Service1()
                    };
                    ServiceBase.Run(ServicesToRun);
                }
            }
            catch
            {

            }
        }
    }
}
