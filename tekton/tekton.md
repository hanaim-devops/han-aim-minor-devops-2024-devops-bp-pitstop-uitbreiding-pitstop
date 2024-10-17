# Required Secrets for Tekton Pipeline

This document outlines the secrets required by the Tekton pipeline to build and push Docker images securely.

## 1. Docker Registry Credentials

The pipeline requires Docker registry credentials to authenticate with a Docker registry (e.g., Docker Hub, private registries) when pushing built images.

### Secret: `docker-registry-secret`

The `docker-registry-secret` contains the credentials for the Docker registry. This secret is referenced by the pipeline via a `ServiceAccount` to enable secure authentication for pushing images.

## Install Guide: Creating a `kubernetes.io/dockerconfigjson` Secret to Push Images to a Registry

Create a Kubernetes secret that allows your pods to push images to a Docker registry.

### Create the Kubernetes Secret

Use kubectl to create the secret.

```bash
kubectl create secret docker-registry regcred \
  --docker-username=your-username \
  --docker-password=your-password \
  --docker-email=docker-email \
  --docker-server=your-registry.com
```

Note: Replace your-registry.com, your-username, your-password, and docker-email