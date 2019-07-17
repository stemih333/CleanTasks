$resourceGroupName = "TodoTasksSecurity"
$location = "NorthEurope"

$subscription az account list --query [?isDefault].id -o tsv
az group create -n $resourceGroupName -l $location
