# Required Secrets for Tekton Pipeline

This document outlines the secrets required by the Tekton pipeline to build and push Docker images securely.

## 1. Docker Registry Credentials

The pipeline requires Docker registry credentials to authenticate with a Docker registry (e.g., Docker Hub, private registries) when pushing built images.

### Secret: `docker-registry-secret`

The `docker-registry-secret` contains the credentials for the Docker registry. This secret is referenced by the pipeline via a `ServiceAccount` to enable secure authentication for pushing images.

### How to Create the Docker Registry Secret

You can create the secret in Kubernetes by running the following command:

```bash
kubectl create secret docker-registry docker-registry-secret \
  --docker-username=<your-username> \
  --docker-password=<your-password> \
  --docker-email=<your-email@example.com> \
  --docker-server=<registry-url>
```

Parameters:
`<your-username>`: Your Docker registry username (e.g., Docker Hub username).
`<your-password>`: Your Docker registry password or access token.
`<your-email@example.com>`: The email associated with your Docker account.
`<registry-url>`: The URL of the Docker registry

## 2. Access to the Secret via ServiceAccount

To ensure that the pipeline can use the docker-registry-secret, a ServiceAccount must be created.

The pipeline will reference this ServiceAccount when running.

### How to Apply the ServiceAccount

Go to `/serviceaccount/pipeline-serviceaccount.yaml`, and apply it:

```bash
kubectl apply -f pipeline-serviceaccount.yaml
```
