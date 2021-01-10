docker-compose -f dockers/infra.yml restart protocol-connect-sync

docker-compose -f applications.yml build
docker-compose -f applications.yml up