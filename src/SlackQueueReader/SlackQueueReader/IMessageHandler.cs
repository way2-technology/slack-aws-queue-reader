using System.Threading.Tasks;

namespace SlackQueueReader {
    public interface IMessageHandler {
        Task Handle(SlackMessage message);
        bool CanHandle(SlackMessage message);
    }
}