#!/bin/sh

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
EDITOR_MARK="Automatic insertion point"
ATTRIB_TEMPLATE="Editor/AttributeTemplate.txt"

SED=gsed
AWK=gawk


echo "Creating ${DEST}..."
$SED -E -e "s/${typeTemplate}/${typeName}/g" -e "s/${TypeTemplate}/${TypeName}/g" $TEMPLATE > $DEST

echo "Adding attributes to ${EDITOR_FILE}..."
attrib=`$SED -E -e "s/${typeTemplate}/${typeName}/g" -e "s/${TypeTemplate}/${TypeName}/g" $ATTRIB_TEMPLATE`
cp $EDITOR_FILE ${EDITOR_FILE}.orig
$AWK -v searchStr="$EDITOR_MARK" -v key="${EDITOR_MARK}\n${attrib}" '{sub(searchStr,key)}1' ${EDITOR_FILE}.orig > $EDITOR_FILE

echo "Operation complete; $typeName is now a supported variable"
