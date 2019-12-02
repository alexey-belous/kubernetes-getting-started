# Volumes

- emptydir
- hostPath
- nfs

## Persistent volume claim PVC

PVC - is a kind of request for persistent volume, that can be defined in the deployment to separate responsibility between developer of the service that requires a persistent storage and the admin, who knows details of storage configuration.

## Examples:
1. Local PV (useful for minikube) and PVM
```yaml
apiVersion: v1
kind: PersistentVolume
metadata:
  name: pv_name
  labels:
    type: local
spec:
  capacity:
    storage: 300Mi # ! MUST BE
  accessModes:
    - ReadWriteOnce
  hostPath:
    path: "mount/path/on/the/host" # !!! 
---
apiVersion: v1
kind: PersistentVolumeClaim
metadata:
  name: pvc_name
spec:
  accessModes:
    - ReadWriteOnce
  storageClassName: ""
  volumeName: pv_name
  resources:
    requests:
      storage: 300Mi # ! THE SAME AS HERE

```

2. Example of the deployment part with volume:
```yaml
...
    spec:
      containers:
        - name: container_name
          image: image
          resources:
            limits:
              memory: "128Mi"
              cpu: "500m"
          ports:
            - containerPort: 27017
          volumeMounts:
            - mountPath: /mount/path/inside/conainer
              name: pv_name
      volumes:
        - name: registration-storage-pv
          persistentVolumeClaim:
            claimName: pvc_name
```