# Required Secrets for Tekton Pipeline

This document outlines the secrets required by the Tekton pipeline to build and push Docker images securely.

## 1. Docker Registry Credentials

The pipeline requires Docker registry credentials to authenticate with a Docker registry (e.g., Docker Hub, private registries) when pushing built images.

### Secret: `docker-registry-secret`

The `docker-registry-secret` contains the credentials for the Docker registry. This secret is referenced by the pipeline via a `ServiceAccount` to enable secure authentication for pushing images.

## Install Guide: Creating a `kubernetes.io/dockerconfigjson` Secret to Push Images to a Registry

Follow these simple steps to create a Kubernetes secret that allows your pods to push images to a Docker registry.

## Steps

### 1. Set Your Registry Credentials

Replace the placeholders with your actual registry URL, username, and password.

```bash
REGISTRY_URL="your-registry.com"
USERNAME="your-username"
PASSWORD="your-password or personal access token"
AUTH=$(echo -n "${USERNAME}:${PASSWORD}" | base64)

cat <<EOF > config.json
{
  "auths": {
    "${REGISTRY_URL}": {
      "auth": "${AUTH}"
    }
  }
}
EOF
```

### 2. Create the Kubernetes Secret

Use kubectl to create the secret.

```bash
kubectl create secret generic regcred \
  --type=kubernetes.io/dockerconfigjson \
  --from-file=.dockerconfigjson=config.json
```

Note: Replace your-registry.com, your-username, your-password, and your-image:tag with your actual registry details.