using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using TextMiner.Properties.Settings;
using Tamir.SharpSsh.jsch;

namespace TextMiner
{
    public partial class TextMinerService : ServiceBase
    {
        /// <summary>
        /// Our SSH session should it be required
        /// </summary>
        private JSch _jsch = null;
        private Session _sshSession = null;

        public TextMinerService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            bool ontWorkerInstantiated = false;

            if (Repository.Configuration.Processes == null)
                return;

            if (Repository.Configuration.Database.SSH.Enabled)
            {
                try
                {
                    //Create a new SSH session
                    _jsch = new JSch();
                    _sshSession = _jsch.getSession(
                        Repository.Configuration.Database.SSH.UserID, 
                        Repository.Configuration.Database.SSH.Host,
                        Repository.Configuration.Database.SSH.Port);

                    _sshSession.setHost(Repository.Configuration.Database.SSH.Host);
                    _sshSession.setPassword(Repository.Configuration.Database.SSH.Password);

                    UserInfo ui = new JschUserInfo();
                    _sshSession.setUserInfo(ui);
                    
                    // Connect
                    _sshSession.connect();

                    //Set port forwarding on the opened session
                    _sshSession.setPortForwardingL(
                        Repository.Configuration.Database.SSH.LocalPort,
                        Repository.Configuration.Database.SSH.ForwardingHost,
                        Repository.Configuration.Database.SSH.RemotePort);

                    if (!_sshSession.isConnected())
                        throw new Exception("SSH Session did not connect.");
                }
                catch (Exception ex)
                {
                    EventLogWriter.WriteError("Could not start due to SSH Error:\n{0}", ex);
                    return;
                }
            }

            foreach (TextMinerServiceSettingsProcess process in Repository.Configuration.Processes)
            {
                if (!process.Enabled)
                    continue;

                switch (process.Type)
                {
                    case ProcessType.OntologySubsetWorker:
                        {
                            // Only one thread of this type allowed
                            if (ontWorkerInstantiated == true)
                                continue;

                            process.Worker = new Worker.OntologySubset(process.PollingInterval, process.Timeout, process.ResponseTimeout);
                            ontWorkerInstantiated = true;
                            break;
                        }
                    case ProcessType.PubMed:
                        {
                            process.Worker = new Worker.PubMed(process.PollingInterval, process.Timeout, process.PostPollingInterval, process.ResponseTimeout, process.OntogratorTab);
                            break;
                        }
                    case ProcessType.Pubget:
                        {
                            process.Worker = new Worker.Pubget(process.PollingInterval, process.Timeout, process.PostPollingInterval, process.ResponseTimeout, process.OntogratorTab);
                            break;
                        }
                    case ProcessType.ClinicalTrialsGov:
                        {
                            process.Worker = new Worker.ClinicalTrialsGov(process.PollingInterval, process.Timeout, process.PostPollingInterval, process.ResponseTimeout, process.OntogratorTab);
                            break;
                        }
                    default:
                        {
                            continue;
                        }
                }

                process.Thread = new Thread(new ThreadStart(process.Worker.Start));
                process.Thread.Start();
            }
        }

        protected override void OnStop()
        {
            if (Repository.Configuration.Processes != null)
            {
                // Ask each of the workers to stop
                foreach (TextMinerServiceSettingsProcess process in Repository.Configuration.Processes)
                {
                    if (process.Enabled && process.Worker != null)
                        process.Worker.Stop();
                }

                // Now wait for them to finish up
                foreach (TextMinerServiceSettingsProcess process in Repository.Configuration.Processes)
                {
                    if (process.Enabled && process.Thread != null)
                    {
                        try
                        {
                            process.Thread.Join(process.Timeout);
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            // Last resort
                            process.Thread.Abort();
                        }
                    }
                }
            }

            if (_sshSession != null)
            {
                try
                {
                    _sshSession.disconnect();
                    _sshSession = null;
                    _jsch = null;
                }
                catch (Exception ex)
                {
                    EventLogWriter.WriteError("Error occurred disconnecting SSH connection:\n{0}", ex);
                }
            }
        }
    }
}
