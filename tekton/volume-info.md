# Setting Up Volumes with Local Path Provisioner

This guide explains how I set up volumes using Local Path Provisioner in Kubernetes.

## Step 1: Apply the Local Path Provisioner

First, I applied the Local Path Provisioner to enable dynamic provisioning of persistent volumes.

```bash
kubectl apply -f https://raw.githubusercontent.com/rancher/local-path-provisioner/master/deploy/local-path-storage.yaml
```

## Step 2: Configure the StorageClass

Next, I created a StorageClass that uses the Local Path Provisioner. Here's the configuration for the StorageClass:

```yaml
apiVersion: storage.k8s.io/v1
kind: StorageClass
metadata:
  name: local-path
provisioner: rancher.io/local-path
volumeBindingMode: WaitForFirstConsumer
reclaimPolicy: Delete
```

## Step 3: Define Workspaces in Tekton Pipeline

In the Tekton Pipeline, I defined workspaces using a volume claim template. Below is the configuration:

```yaml
workspaces:
  - name: output
    volumeClaimTemplate:
      spec:
        storageClassName: local-path
        accessModes: ["ReadWriteOnce"]
        resources:
          requests:
            storage: 1Gi
  - name: dockerconfig
    secret:
      secretName: regcred
      items:
      - key: .dockerconfigjson
        path: config.json
podTemplate:
  securityContext:
    fsGroup: 65532
```

This setup ensures that the volumes are properly created and bound when needed by the pipeline, without the need for manual PersistentVolume creation.
