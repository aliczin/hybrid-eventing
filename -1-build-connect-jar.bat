set BUILD_DIR=%~dp0\dockers\rabbitmq-kafka-sink

docker run -t -i -v %BUILD_DIR%:/data --workdir /data maven:3.6.3-jdk-11 mvn package
