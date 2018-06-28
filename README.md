# PublishSuscribe
Deliver a message to multiple consumers by using RabbitMQ.
A producer is a user application that sends messages.
A queue is a buffer that stores messages.
A consumer is a user application that receives messages.
The core idea in the messaging model in RabbitMQ is that the producer never sends any messages directly to a queue. Actually, quite often the producer doesn't even know if a message will be delivered to any queue at all.

reference: https://www.rabbitmq.com/tutorials/tutorial-three-dotnet.html

