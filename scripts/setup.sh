#!/bin/bash
# setup a cosmos db account, database, and graph

# Generate a unique 10 character alphanumeric string to ensure unique resource names
unique_id=$(echo $(cat /proc/sys/kernel/random/uuid) | cut -c1-8)

# init variables for the demo
resource_group=explore-cosmosdb-gremlin
location=westus2
account_name="explorecosmos-$unique_id"
database_name=db
graph_name=graph

# Create the resource group for the demo
az group create -n $resource_group -l $location

# create the account (wait ~7 minutes)
az cosmosdb create -n $account_name -g $resource_group \
    --capabilities EnableGremlin \
    --kind GlobalDocumentDB

# Create the database
az cosmosdb gremlin database create -a $account_name -g $resource_group \
    -n $database_name --no-wait

# Create the graph

index_policy_file=indexing-policy-$unique_id.json

cat > $index_policy_file << EOF 
{
    "indexingMode": "consistent", 
    "includedPaths": [
        {"path": "/*"}
    ],
    "excludedPaths": [
        { "path": "/headquarters/employees/?"}
    ],
    "spatialIndexes": [
        {"path": "/*", "types": ["Point"]}
    ],
    "compositeIndexes":[
        [
            { "path":"/name", "order":"ascending" },
            { "path":"/age", "order":"descending" }
        ]
    ]
}
EOF

az cosmosdb gremlin graph create -a $account_name -g $resource_group \
    -d $database_name -n $graph_name -p '/zipcode' \
    --throughput 400 --idx @$index_policy_file

rm $index_policy_file