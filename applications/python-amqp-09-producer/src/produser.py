import time
from kombu import Connection, Exchange, Queue

print(f'Start SIMPLE amqp-0-9 producer')

# для RMQ - хорошей практикой является когда отправиль и получатель
# явно определяют свою топологию связи точка обмена - очереди
order_exchange = Exchange('orders-send', 'fanout', durable=True)
kafka_queue = Queue('orders-for-ofssets', exchange=order_exchange, durable=True)
amqp10_queue = Queue('orders-amqp-10-consumer', exchange=order_exchange, durable=True)

with Connection('amqp://rabbitmq:rabbitmq@localhost:5672') as conn:
    while True:
        print(f'Send message LikeOrder - как будто Вася заказал на сайте 10 АйФонов')
        producer = conn.Producer(serializer='json')
        producer.publish({'client': 'Вася', 'count': 10, 'good': 'АйФончик'},
                      exchange=order_exchange,
                      declare=[kafka_queue, amqp10_queue])
        time.sleep(2)
        

        
