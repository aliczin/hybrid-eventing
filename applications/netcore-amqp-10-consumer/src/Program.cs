using System;
using Amqp;
using Amqp.Framing;

namespace netcore_amqp_10_consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Раскоментируйте для изучения протокола ;-)
            //Trace.TraceLevel = TraceLevel.Frame;
            //Trace.TraceListener = (l, f, a) => Console.WriteLine(DateTime.Now.ToString("[hh:mm:ss.fff]") + " " + string.Format(f, a));

            Address address = new Address("amqp://rabbitmq:rabbitmq@localhost:5672");
            Connection connection = new Connection(address);
            Session session = new Session(connection);

            Attach recvAttach = new Attach()
            {
                Source = new Source()
                {
                    Address = "orders-amqp-10-consumer",
                    Durable = 1,
                },

                Target = new Target()
                {
                    Durable = 1,
                }
            };

            ReceiverLink receiver = 
                new ReceiverLink(session,"netcore_amqp_10_consumer", recvAttach, null);

            Console.WriteLine("Receiver connected to broker.");

            while (true) {
                Message message = receiver.Receive();
                if (message == null)
                {
                    Console.WriteLine("Client exiting.");
                    break;
                }
                Console.WriteLine("Received " 
                  + System.Text.Encoding.UTF8.GetString((byte[])message.Body)
                );
                receiver.Accept(message);
                //специально НЕ подтвержадаем - а только принимаем
                //будет расти NACK сообщения, хотя мы их будем получать
                System.Threading.Thread.Sleep(3000);
            }
            
            receiver.Close();
            session.Close();
            connection.Close();

        }
    }
}
