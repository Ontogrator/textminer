using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;

namespace TextMiner
{
    [RunInstaller(true)]
    public partial class TextMinerServiceInstaller : Installer
    {
        private ServiceInstaller _serviceInstaller;
        private ServiceProcessInstaller _processInstaller;

        public TextMinerServiceInstaller()
        {
            InitializeComponent();

            _processInstaller = new ServiceProcessInstaller();
            _serviceInstaller = new ServiceInstaller();

            // Service will run under system account
            _processInstaller.Account = ServiceAccount.LocalSystem;

            // Service will have Start Type of Manual
            _serviceInstaller.StartType = ServiceStartMode.Manual;

            _serviceInstaller.ServiceName = "Text Miner Service";

            Installers.Add(_serviceInstaller);
            Installers.Add(_processInstaller);
        }
    }
}