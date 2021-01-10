
set CURPATH=%~dp0
set DOCKER_DIR=%CURPATH%\dockers

docker run --rm -it --name dcv -v %DOCKER_DIR%\:/input pmsipilot/docker-compose-viz render -m image --force --output-file=infra-topology.png infra.yml
docker run --rm -it --name dcv -v %CURPATH%\:/input pmsipilot/docker-compose-viz render -m image --force --output-file=apps-topology.png applications.yml

copy /b/v/y dockers\infra-topology.png content\assets\infra-topology.png
copy /b/v/y apps-topology.png content\assets\apps-topology.png