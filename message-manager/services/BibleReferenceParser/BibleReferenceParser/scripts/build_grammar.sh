#!/bin/bash
set -e

# Get the directory of this script, then go up one level
script_dir="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
project_dir="$(realpath "$script_dir/..")"
source_dir="$project_dir/src"
antlr4_jar="$(realpath "$project_dir/../ThirdParty/antlr4/antlr-4.9.2-complete.jar")"
classpath=".:$antlr4_jar:$CLASSPATH"
file_list="$source_dir/Grammar/*.g4"
output_dir="$project_dir/src/Grammar/Generated"
flags="-Dlanguage=CSharp -package BibleReferenceParser.Grammar.Generated -listener -no-visitor"

echo "Project directory: $project_dir"
echo "Source directory: $source_dir"
echo "Output directory: $output_dir"
echo "Classpath: $classpath"
echo "Files: $file_list"

java -cp "$classpath" org.antlr.v4.Tool -o "$output_dir" $flags $file_list
