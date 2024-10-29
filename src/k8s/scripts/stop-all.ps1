# Verwijder resources in de pitstop namespace
kubectl delete svc --all -n pitstop
kubectl delete deploy --all -n pitstop
kubectl delete virtualservice --all -n pitstop
kubectl delete destinationrule --all -n pitstop

kubectl delete svc --all -n monitoring
kubectl delete deploy --all -n monitoring
kubectl delete configmap --all -n monitoring
kubectl delete pvc --all -n monitoring
kubectl delete pv --all -n monitoring

kubectl delete namespace monitoring
