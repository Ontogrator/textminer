using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using TextMiner.Properties.Settings;

namespace TextMiner.Worker
{
    class OntologySubset : Common
    {
        public OntologySubset(int pollingInterval, int timeout, int responseTimeout)
            : base(ProcessType.OntologySubsetWorker, pollingInterval, timeout, responseTimeout)
        {
        }

        protected override bool DoWork()
        {
            using (MySqlConnection conn = new MySqlConnection(Repository.Configuration.Database.ConnectionString))
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandTimeout = _responseTimeout;
                        cmd.CommandText = "UpdateOntologySubset";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    EventLogWriter.WriteError("Error occurred updating ontology subset:\n{0}", ex);
                }
            }
            return false;
        }
    }
}
