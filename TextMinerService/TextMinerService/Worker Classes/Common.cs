using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Threading;
using System.Xml;
using System.Net;
using System.IO;
using TextMiner.PdfToText;
using TextMiner.TextCleaner;
using TextMiner.SentenceSplitter;
using System.Xml.Serialization;
using TextMiner.Properties.Settings;

namespace TextMiner.Worker
{
    abstract class Common
    {
        private int _pollingInterval;
        protected int _responseTimeout;
        public readonly int Timeout;
        private bool _alive = true;
        protected readonly ProcessType _processType;

        protected bool Alive
        {
            get
            {
                lock (this)
                {
                    return _alive;
                }
            }
        }

        public Common(ProcessType processType, int pollingInterval, int timeout, int responseTimeout)
        {
            _processType = processType;
            _pollingInterval = pollingInterval;
            _responseTimeout = responseTimeout;
            Timeout = timeout;
        }

        abstract protected bool DoWork();

        public virtual void Start()
        {
            EventLogWriter.WriteInformation("{0} worker thread ID {1} started:\nPolling Interval: {2}\nTimeout: {3}", _processType, Thread.CurrentThread.ManagedThreadId, _pollingInterval, Timeout);

            while (Alive)
            {
                if(!DoWork())
                    Sleep(this._pollingInterval);
            }

            EventLogWriter.WriteInformation("{0} worker thread ID {1} stopping.", _processType, Thread.CurrentThread.ManagedThreadId);
        }

        public virtual void Stop()
        {
            lock (this)
            {
                _alive = false;
            }
        }

        public void Sleep(int interval)
        {
            DateTime ticker = DateTime.Now.AddMilliseconds(interval);
            while (Alive && DateTime.Now < ticker)
            {
                Thread.Sleep(10);
            }
        }
    }
}
