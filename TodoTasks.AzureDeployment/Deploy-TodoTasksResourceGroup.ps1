$resourceGroupName = "TodoTasks"
$location = "NorthEurope"

az group create -n $resourceGroupName -l $location
az group deployment create --template-file resources\security\roleAssignmentsDeploy.json --parameters resources\security\roleAssignmentsDeploy.parameters.json --resource-group $resourceGroupName
az group deployment create --template-file resources\web\appServicePlanDeploy.json --parameters resources\web\appServicePlanDeploy.parameters.json --resource-group $resourceGroupName # --query 'properties.outputs.planName.value'
az group deployment create --template-file resources\web\webAppDeploy.json --parameters resources\web\webAppDeploy.parameters.json --resource-group $resourceGroupName
az group deployment create --template-file resources\storage\todoDbDeploy.json --parameters resources\storage\todoDbDeploy.parameters.json --resource-group $resourceGroupName 
az group deployment create --template-file resources\security\keyVaultDeploy.json --parameters resources\security\keyVaultDeploy.parameters.json --resource-group $resourceGroupName