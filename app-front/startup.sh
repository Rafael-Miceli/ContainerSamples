file_content="{"

while IFS='=' read -r -d '' n v; do
    file_content="$file_content \"$n\" : \"$v\","
done < <(env -0)

file_content="${file_content::-1}"
file_content="$file_content }"
echo "$file_content" > "config.json"

nginx -g "daemon off;"