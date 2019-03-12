#!/bin/bash
file_content="{"

echo "getting environment variables"

while IFS='=' read -r -d '' n v; do
    file_content="$file_content \"$n\" : \"$v\","
done < <(env -0)

echo "environment variables got"

file_content="${file_content::-1}"
file_content="$file_content }"
echo "$file_content" > "config.json"

nginx -g "daemon off;"