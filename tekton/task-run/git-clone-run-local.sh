#!/bin/bash

# Set namespace
NAMESPACE="default"

# TaskRun name
TASKRUN_NAME="git-clone-taskrun"

# Get the Git repository root directory
GIT_ROOT=$(git rev-parse --show-toplevel)

# YAML file location (relative to the Git repository root)
YAML_PATH="$GIT_ROOT/tekton/task-run/git-clone-run.yaml"

# Check if the TaskRun already exists
existing_taskrun=$(kubectl get taskrun $TASKRUN_NAME -n $NAMESPACE --ignore-not-found)

if [ ! -z "$existing_taskrun" ]; then
    echo "Deleting existing TaskRun: $TASKRUN_NAME"
    kubectl delete taskrun $TASKRUN_NAME -n $NAMESPACE
fi

# Apply the YAML to run the task
echo "Applying the TaskRun YAML from $YAML_PATH"
kubectl apply -f $YAML_PATH

# Wait for the task to complete
echo "Waiting for TaskRun to complete..."
tkn taskrun logs $TASKRUN_NAME --follow -n $NAMESPACE

# Display the TaskRun status
echo "Fetching TaskRun status:"
tkn taskrun describe $TASKRUN_NAME -n $NAMESPACE
