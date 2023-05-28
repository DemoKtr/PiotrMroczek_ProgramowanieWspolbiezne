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
        private readonly ConcurrentQueue<JObject> ballQueue = new ConcurrentQueue<JObject>();
        private readonly Mutex queueMutex = new Mutex();
        private readonly JArray fileDataArray;
        private Mutex fileMutex = new Mutex();
        public BallListLogger() {
            string tempPath = Path.GetTempPath();
            logFilePath = tempPath + "balls.json";
            if (File.Exists(logFilePath))
            {
                try
                {
                    string input = File.ReadAllText(logFilePath);
                    fileDataArray = JArray.Parse(input);
                    return;
                }
                catch (JsonReaderException)
                {
                    fileDataArray = new JArray();
                }
            }

            fileDataArray = new JArray();
            File.Create(logFilePath);
        }
        ~BallListLogger()
        {
            fileMutex.WaitOne();
            fileMutex.ReleaseMutex();
        }
        private async Task LogToFile()
        {
            while (ballQueue.TryDequeue(out JObject ball))
            {
                fileDataArray.Add(ball);
            }

          
            string output = JsonConvert.SerializeObject(fileDataArray);

            fileMutex.WaitOne();
            try
            {
                await File.WriteAllTextAsync(logFilePath, output);
            }
            finally
            {
                fileMutex.ReleaseMutex();
            }
        }
        public async void AddToLogQueue(IBall ball)
        {
            queueMutex.WaitOne();
            try
            {
                JObject itemToAdd = JObject.FromObject(ball);
                ballQueue.Enqueue(itemToAdd);


                if (loggingTask == null || loggingTask.IsCompleted)
                {
                    loggingTask = Task.Factory.StartNew(this.LogToFile);
                }
            }
            finally
            {
                queueMutex.ReleaseMutex();
            }
        }
        
    }
}

