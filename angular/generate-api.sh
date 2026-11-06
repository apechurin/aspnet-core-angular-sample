npx @openapitools/openapi-generator-cli generate \
  -i ./swagger.json \
  -g typescript-angular \
  -o ./src/app/domain/infrastructure/data-access \
  --additional-properties=ngVersion=17.3.0
