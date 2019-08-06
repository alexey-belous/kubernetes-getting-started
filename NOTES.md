# Notes
Kube command for joining slave nodes to master node: 
```
kubeadm join 10.10.0.7:6443 --token wsclh0.9khpg2eyuplu0gsm \
    --discovery-token-ca-cert-hash sha256:aee723ec25e285300c21fd1e9bdc819f3d34b0622bc3b7e25e796cb8b3f1a6ee
```

## Useful commands
- `kubectl cluster-info [dump]` - cluster info
- `kubectl get nodes` - get all nodes
- `kubectl describe node` - diagnose specific node
- `kubectl get pods --all-namespaces`