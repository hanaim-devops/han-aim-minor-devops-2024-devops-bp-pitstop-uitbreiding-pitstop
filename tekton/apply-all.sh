kubectl delete all --all

kubectl delete task build-and-deploy
kubectl apply -f ./task/git-clone.yaml
kubectl apply -f ./task/kaniko.yaml

kubectl delete taskrun build-and-deploy
kubectl apply -f ./task-run/git-clone-run.yaml

kubectl delete pipeline build-and-deploy
kubectl apply -f ./pipeline/build-and-deploy.yaml

#kubectl delete pipelinerun dotnet-aspnet-base-build-run
#kubectl delete pipelinerun dotnet-runtime-base-build-run
#kubectl delete pipelinerun dotnet-sdk-base-build-run
#kubectl delete pipelinerun customermanagementapi-build-run
#kubectl delete pipelinerun webapp-build-run
#kubectl delete pipelinerun workshopmanagementeventhandler-build-run
#kubectl delete pipelinerun timeservice-build-run
kubectl delete pipelinerun notificationservice-build-run
#kubectl apply -f ./pipeline-run/dotnet-aspnet-base-build-run.yaml
#kubectl apply -f ./pipeline-run/dotnet-runtime-base-build-run.yaml
#kubectl apply -f ./pipeline-run/dotnet-sdk-base-build-run.yaml
#kubectl apply -f ./pipeline-run/customermanagementapi-build-run.yaml
#kubectl apply -f ./pipeline-run/webapp-build-run.yaml
#kubectl apply -f ./pipeline-run/workshopmanagementeventhandler-build-run.yaml
#kubectl apply -f ./pipeline-run/timeservice-build-run.yaml
kubectl apply -f ./pipeline-run/notificationservice-build-run.yaml

#tkn pipelinerun logs dotnet-aspnet-base-build-run --follow
#tkn pipelinerun describe dotnet-aspnet-base-build-run

#tkn pipelinerun logs dotnet-runtime-base-build-run --follow
#tkn pipelinerun describe dotnet-runtime-base-build-run

#tkn pipelinerun logs dotnet-sdk-base-build-run --follow
#tkn pipelinerun describe dotnet-sdk-base-build-run

#tkn pipelinerun logs customermanagementapi-build-run --follow
#tkn pipelinerun describe customermanagementapi-build-run

#tkn pipelinerun logs webapp-build-run --follow
#tkn pipelinerun describe webapp-build-run

#tkn pipelinerun logs workshopmanagementeventhandler-build-run --follow
#tkn pipelinerun describe workshopmanagementeventhandler-build-run

#tkn pipelinerun logs timeservice-build-run --follow
#tkn pipelinerun describe timeservice-build-run

tkn pipelinerun logs notificationservice-build-run --follow
tkn pipelinerun describe notificationservice-build-run