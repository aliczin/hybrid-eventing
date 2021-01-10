echo ${PWD}/dockers/

export DOCKER_DIR=${PWD}/dockers

docker run --rm -it --name dcv -v "${DOCKER_DIR}/":"/input" pmsipilot/docker-compose-viz render -m image --force --output-file=infra-topology.png infra.yml