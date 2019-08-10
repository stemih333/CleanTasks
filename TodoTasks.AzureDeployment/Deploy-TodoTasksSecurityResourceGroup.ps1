$resourceGroupName = "TodoTasksSecurity"
$location = "NorthEurope"

#$subscription az account list --query [?isDefault].id -o tsv
az group create -n $resourceGroupName -l $location
#az group deployment create --template-file resources/security/roleAssignmentsDeploy.json --parameters resources/security/roleAssignmentsDeploy.parameters.json --resource-group $resourceGroupName
az group deployment create --template-file resources/storage/storageDeploy.json --resource-group $resourceGroupName
#az group deployment create --template-file resources/security/keyVaultDeploy.json --resource-group $resourceGroupName --parameters resources/security/keyVaultDeploy.parameters.json
