const { execSync } = require('child_process');
const fs = require('fs');
const path = require('path');

// Set variables
const specFile = './openapi.yaml';
const outputDir = './OpenApiGenerator';
const packageName = 'ProductInventoryApi';
const apiName = 'ProductInventory';

// Make sure the output directory exists
if (!fs.existsSync(outputDir)) {
  fs.mkdirSync(outputDir, { recursive: true });
}

// Check if OpenAPI generator CLI is installed
try {
  execSync('npx openapi-generator-cli version', { stdio: 'inherit' });
  console.log('OpenAPI Generator CLI is installed.');
} catch (error) {
  console.log('Installing OpenAPI Generator CLI...');
  execSync('npm install @openapitools/openapi-generator-cli', { stdio: 'inherit' });
}

console.log('Generating API code...');

// Build the command with all the parameters
const command = `npx openapi-generator-cli generate \
  -i ${specFile} \
  -g aspnetcore \
  -o ${outputDir} \
  --additional-properties=aspnetCoreVersion=8.0 \
  --additional-properties=buildTarget=library \
  --additional-properties=classModifier=abstract \
  --additional-properties=operationModifier=virtual \
  --additional-properties=packageName=${packageName} \
  --additional-properties=apiName=${apiName} \
  --additional-properties=returnICollection=true \
  --additional-properties=useDefaultRouting=true \
  --additional-properties=useSwashbuckle=true \
  --additional-properties=nullableReferenceTypes=true \
  --additional-properties=useDateTimeOffset=true \
  --additional-properties=centralizedPackageVersionManagement=enable \
  --additional-properties=useNewtonsoft=true`;

// Execute the command
try {
  execSync(command, { stdio: 'inherit' });
  console.log('API code generation completed!');
} catch (error) {
  console.error('Error generating API code:', error);
  process.exit(1);
}