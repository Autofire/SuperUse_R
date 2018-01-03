#!/bin/sh

# This script is used to automatically add a type to the variable system.
# Call it with the name of the type you want to add support for.
#
# Be warned, this does no checking. It might break things if you
#  a) Call this for an already supported type
#  b) Call this for an invalid type
#
# This does make a backup of the editor scipt, but it is still wise to
# keep a backup of the project. (Or use a version control system.)

if [ "$#" -ne 1 ]; then
    echo "Illegal number of parameters"
	echo "usage: $0 typeName"
	exit 1
fi

typeName=$1
TypeName=`echo "${typeName:0:1}" | tr a-z A-Z`${typeName:1}

typeTemplate="_type_"
TypeTemplate="_Type_"

TEMPLATE="VariableTemplate.txt"
DEST="${TypeName}Variable.cs"

EDITOR_FILE="Editor/RefEditor.cs"
EDITOR_TMP_FILE="${EDITOR_FILE}.orig"
EDITOR_MARK="DO NOT REMOVE - Automatic insertion point"
ATTRIB_TEMPLATE="Editor/AttributeTemplate.txt"

SED=gsed
AWK=gawk


echo "Creating ${DEST}..."
$SED -E -e "s/${typeTemplate}/${typeName}/g" -e "s/${TypeTemplate}/${TypeName}/g" $TEMPLATE > $DEST

echo "Adding attributes to ${EDITOR_FILE}..."
attrib=`$SED -E -e "s/${typeTemplate}/${typeName}/g" -e "s/${TypeTemplate}/${TypeName}/g" $ATTRIB_TEMPLATE`
cp $EDITOR_FILE $EDITOR_TMP_FILE
$AWK -v searchStr="$EDITOR_MARK" -v key="${EDITOR_MARK}\n${attrib}" '{sub(searchStr,key)}1' $EDITOR_TMP_FILE > $EDITOR_FILE

echo "Operation complete; $typeName is now a supported variable"
