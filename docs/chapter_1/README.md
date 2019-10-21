# Theory

## Kubernetes terms
- Pods - sets of containers that represents a one logical unit (unit of service). **Usually only one service**
- Deployment - microservice - all containers (pods) to make the "job done".

## Commands:
- `kubectl apply -f deployment.yml` - apply a deployment (or any other config file)
- `kubectl expose deployment minikube-registration-ms --type=NodePort` - creates a service to expose a Pod
- `kubectl attach pod-name -c container` - attach to a process
- `kubectl exec -it pod-name -c container command(bash)` - attach to a process
- `kubectl label type-of-resource resource-name label=value` - label a specific resource
- `minikube service minikube-registration-ms --url` - get url of the service

## Dashboard
See link: https://github.com/kubernetes/dashboard