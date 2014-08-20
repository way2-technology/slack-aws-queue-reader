using System;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AmazonSqs;
using SlackQueueReader.Handlers;

namespace SlackQueueReader {
    public class Program {
        public static void Main() {
            RunAsync().Wait();
        }

        private static async Task RunAsync() {
            var config = new Configurator.Configurator();
            var queue = new ObjectQueue(config.Get<string>("accessKey", true),
                config.Get<string>("secretKey", true),
                config.Get<string>("queueName", true));
            IMessageHandler[] handlers = {
                new SlackMessageHandler()
            };
            while (true) {
                await GetMessages(queue, handlers);
                Console.WriteLine("Esperando 10s...");
                Thread.Sleep(10*1000);
            }
        }

        private static async Task GetMessages(ObjectQueue queue, IMessageHandler[] handlers) {
            ObjectMessage<SlackMessage> msg;
            while ((msg = queue.Peek<SlackMessage>()) != null) {
                var slackMessage = msg.Object;
                foreach (var handler in handlers.Where(h => h.CanHandle(slackMessage))) {
                    handler.Handle(slackMessage);
                }
                queue.DeleteMessage(msg.ReceiptHandle);
            }
        }
    }
}