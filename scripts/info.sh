#!/bin/bash
# get the info needed to make a connection

resource_group_name=explore-cosmosdb-gremlin 
database_name=db
graph_name=graph

account_name=$(az cosmosdb list -g $resource_group_name -o tsv --query '[].name' | grep explorecosmos-)
gremlin_endpoint="wss://${account_name}.gremlin.cosmos.azure.com:443/"
auth_key=$(az cosmosdb keys list --name $account_name --resource-group $resource_group_name --type keys -o tsv --query 'primaryMasterKey')


cat > conn_info.json << EOF 
{
    "accountName": "${account_name}",
    "databaseName": "${database_name}",
    "graphName": "${database_name}",
    "gremlinEndpoint": "${gremlin_endpoint}",
    "authenticationKey": "${auth_key}"
}
EOF

echo "JSON for the environment you will run the sample:"
echo

cat conn_info.json