$resourceGroupName = "TodoTasks"
$location = "NorthEurope"

#az group create -n $resourceGroupName -l $location
#az group deployment create --template-file Resources\roleAssignmentsDeploy.json --parameters Resources\roleAssignmentsDeploy.parameters.json --resource-group $resourceGroupName
#az group deployment create --template-file Resources\appServicePlanDeploy.json --parameters Resources\appServicePlanDeploy.parameters.json --resource-group $resourceGroupName --query 'properties.outputs.planName.value'
az group deployment create --template-file Resources\webAppDeploy.json --parameters Resources\webAppDeploy.parameters.json --resource-group $resourceGroupName
# z group deployment create --template-file Resources\todoDbDeploy.json --parameters Resources\todoDbDeploy.parameters.json --resource-group $resourceGroupName