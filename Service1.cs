using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WcfHostService
{
    public partial class Service1 : ServiceBase
    {
        ServiceHost host;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
             host
           = new ServiceHost(typeof(WcfHostService.FileUploadService));
            host.Open();
        }

        protected override void OnStop()
        {
            host.Close();
        }

        internal void TestStartup(string[] args)
        {
            this.OnStart(args);
        }

        internal void TestStop()
        {
            this.OnStop();
        }
    }
}
