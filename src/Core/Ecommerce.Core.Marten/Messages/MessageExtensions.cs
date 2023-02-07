using Wolverine;

namespace Ecommerce.Core.Marten.Messages;

public static class MessageExtensions
{
    public static WolverineOptions AddPublishableMessage<TMessage>(
        this WolverineOptions opts,
        string queueName,
        bool useDurableInbox = false)
    where TMessage : class
    {
        if (string.IsNullOrWhiteSpace(queueName))
            throw new InvalidOperationException("Wolverine queue name cannot be null or empty");

        if (useDurableInbox)
            opts.PublishMessage<TMessage>()
                .ToLocalQueue(queueName)
                .UseDurableInbox();
        else
            opts.PublishMessage<TMessage>()
                .ToLocalQueue(queueName);

        return opts;
    }
}