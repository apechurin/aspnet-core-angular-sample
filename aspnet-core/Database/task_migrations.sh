#!/bin/bash

# Configuration constants
CONTEXT="TaskDbContext"
PROJECT="../Sample.Infrastructure"
STARTUP_PROJECT="../Sample.Host"

# Output colors
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

print_usage() {
  echo "Usage: $0 <command> [arguments]"
  echo ""
  echo "Commands:"
  echo "  add <migration_name>  - create an EF migration"
  echo "  script                - generate SQL script from migrations"
  echo "  bundle                - create a bundle (idempotent SQL script)"
  echo ""
  echo "Examples:"
  echo "  $0 add InitialCreate"
  echo "  $0 script"
  echo "  $0 bundle"
}

error_exit() {
  echo -e "${RED}Error: $1${NC}" >&2
  exit 1
}

success() {
  echo -e "${GREEN}$1${NC}"
}

info() {
  echo -e "${YELLOW}$1${NC}"
}

check_dotnet() {
  if ! command -v dotnet &>/dev/null; then
    error_exit "dotnet CLI not found in PATH"
  fi
}

# Create a migration
add_migration() {
  local migration_name="$1"

  if [ -z "$migration_name" ]; then
    error_exit "Migration name is required. Usage: $0 add <migration_name>"
  fi

  info "Creating migration '$migration_name'..."

  dotnet ef migrations add "$migration_name" \
    --context "$CONTEXT" \
    --project "$PROJECT" \
    --startup-project "$STARTUP_PROJECT" ||
    error_exit "Failed to create migration"

  success "Migration '$migration_name' created successfully"
}

# Generate SQL script
create_script() {
  info "Generating SQL script..."

  local timestamp=$(date +%Y%m%d_%H%M%S)
  local output_file="migrations_${timestamp}.sql"

  dotnet ef migrations script \
    --context "$CONTEXT" \
    --project "$PROJECT" \
    --startup-project "$STARTUP_PROJECT" \
    --idempotent \
    --output "$output_file" ||
    error_exit "Failed to generate SQL script"

  success "SQL script created: $output_file"
}

# Create a bundle (EF Core 6+)
create_bundle() {
  info "Creating bundle..."

  local timestamp=$(date +%Y%m%d_%H%M%S)
  local output_file="migrations_${timestamp}.efbundle"

  dotnet ef migrations bundle \
    --context "$CONTEXT" \
    --project "$PROJECT" \
    --startup-project "$STARTUP_PROJECT" \
    --force \
    --output "$output_file" ||
    error_exit "Failed to create bundle"

  success "Bundle created: $output_file"
}

# Main logic
main() {
  if [ $# -eq 0 ]; then
    print_usage
    exit 1
  fi

  check_dotnet

  local command="$1"
  shift

  case "$command" in
  add)
    add_migration "$1"
    ;;
  script)
    create_script
    ;;
  bundle)
    create_bundle
    ;;
  -h | --help | help)
    print_usage
    ;;
  *)
    error_exit "Unknown command: $command"
    ;;
  esac
}

main "$@"
