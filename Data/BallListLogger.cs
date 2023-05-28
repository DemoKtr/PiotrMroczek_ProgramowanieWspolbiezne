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
    internal class BallListLogger : IBallListLogger
    {
        private readonly string logFilePath;
        private Task? loggingTask;
        private ConcurrentQueue<IBall> ballQueue = new ConcurrentQueue<IBall>();
        private readonly Mutex queueMutex = new Mutex();

        public BallListLogger() {
            string tempPath = Path.GetTempPath();
            logFilePath = tempPath + "balls.json";

        }

        private async Task LogToFile()
        {
            JArray array;
            if (File.Exists(logFilePath))
            {
                try
                {
                    array = JArray.Parse(await File.ReadAllTextAsync(logFilePath));
                }
                catch (JsonReaderException)
                {
                    array = new JArray();
                }
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
            await File.WriteAllTextAsync(logFilePath, output);
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

            if (loggingTask != null && !loggingTask.IsCompleted)
                return;

            loggingTask = this.LogToFile();
        }
    }
}

