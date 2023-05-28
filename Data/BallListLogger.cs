using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace Data
{
    internal class BallListLogger
    {
        public static string logFilePath = "%TEMP%/Balls/ballsLog.json";
        private Task? loggingTask;
        private ConcurrentQueue<IBall> ballQueue = new ConcurrentQueue<IBall>();
        private readonly Mutex queueMutex = new Mutex();
        private async Task LogToFile()
        {
            JArray array;
            if (File.Exists(logFilePath))
            {
                array = JArray.Parse(File.ReadAllText(logFilePath));
            }
            else
            {
                array = new JArray();
                File.Create(logFilePath);
            }
            IBall ball;
            while (ballQueue.TryDequeue(out ball))
            {
                JObject itemToAdd = JObject.FromObject(ball);
                array.Add(itemToAdd);
            }

            string output = JsonConvert.SerializeObject(array);
            File.WriteAllText(logFilePath, output);
        }
        public async void AddToLogQueue(IBall ball)
        {
            queueMutex.WaitOne();
            try
            {
                ballQueue.Enqueue(ball);
            }
            finally
            {
                queueMutex.ReleaseMutex();
            }

            if (loggingTask != null || loggingTask.IsCompleted.Equals("false"))
                return;

            loggingTask = Task.Run(this.LogToFile);
        }
    }
}

