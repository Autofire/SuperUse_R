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
#
# Use the -t argument to specify a custom type name. If this is not used,
# then the target type with the first character uppercased is used instead.
#
# If the -n argument is used, the type is treated as if it is nullable.
# This has nothing to do with System.Nullable; use this if your type can have
# null assigned into it.


### CONFIG ###

TYPE_TEMPLATE="_type_"
TYPE_NAME_TEMPLATE="_TypeName_"
NULLABLE_TEMPLATE="_Nullable_"

VARIABLE_TEMPLATE_FILE="VariableTemplate.txt"

EDITOR_PATH="Editor"
EDITOR_FILE="${EDITOR_PATH}/RefEditor.cs"
EDITOR_BKUP_FILE="${EDITOR_FILE}.orig"
ATTRIB_TEMPLATE_FILE="${EDITOR_PATH}/AttributeTemplate.txt"
EDITOR_MARK="DO NOT REMOVE - Automatic insertion point"

REGISTER_PATH="RegisterComponents"
REGISTER_TEMPLATE_FILE="$REGISTER_PATH/RegisterTemplate.txt"

SED=sed
AWK=awk

# Search for "POST-INPUT CONFIG" for the other portion of config



### INPUT HANDLING ###

usage="usage: $0 [-n|--Nullable] [-t|--TypeName <typeName>] <type>"
type=
typeName=
nullable=

# Thanks to http://linuxcommand.org/lc3_wss0120.php on how to write this
while [ "$#" -gt 1 ]; do
    case $1 in
        -t | --TypeName )       shift
                                typeName=$1
                                ;;
        -n | --Nullable )       nullable="Nullable"
                                ;;
        --help )                echo -e $usage
                                exit
                                ;;
        * )                     echo -e $usage
                                exit 1
    esac
    shift
done

if [ "$1" = "--help" ]; then
    echo -e $usage
    exit
fi

if [ "$#" -ne 1 ]; then
    echo "Missing type parameter"
    echo -e $usage
    exit 1
fi

type=$1

if [ "$typeName" = "" ]; then
    typeName=`echo "${type:0:1}" | tr a-z A-Z`${type:1}
fi


### POST-INPUT CONFIG ###

DEST="${typeName}Variable.cs"
REGISTER_DEST="${REGISTER_PATH}/Register${typeName}.cs"


### PROCESSING ###

echo "Creating ${DEST}..."
$SED -E -e "s/${TYPE_TEMPLATE}/${type}/g" -e "s/${TYPE_NAME_TEMPLATE}/${typeName}/g" -e "s/${NULLABLE_TEMPLATE}/${nullable}/g" $VARIABLE_TEMPLATE_FILE > $DEST


echo "Adding attributes to ${EDITOR_FILE}..."
attrib=`$SED -E -e "s/${TYPE_TEMPLATE}/${type}/g" -e "s/${TYPE_NAME_TEMPLATE}/${typeName}/g" $ATTRIB_TEMPLATE_FILE`

cp $EDITOR_FILE $EDITOR_BKUP_FILE
$AWK -v searchStr="$EDITOR_MARK" -v key="${EDITOR_MARK}\n${attrib}" '{sub(searchStr,key)}1' $EDITOR_BKUP_FILE > $EDITOR_FILE


if [ "$nullable" != "" ]; then
	echo "Creating ${REGISTER_DEST}..."
	$SED -E -e "s/${TYPE_TEMPLATE}/${type}/g" -e "s/${TYPE_NAME_TEMPLATE}/${typeName}/g" -e "s/${NULLABLE_TEMPLATE}/${nullable}/g" $REGISTER_TEMPLATE_FILE > $REGISTER_DEST
fi


echo "Operation complete; $type is now a supported variable"
