using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum COMMAND
{
    BROADCAST,
    BROADCAST_TO_CHILDREN,
    SEND_TO_TARGET,
    REQUEST_FROM_TARGET,
    CHECK_DATA,
    IGNORE
};

public enum COLOR_MODE
{
    LIGHT,
    DARK
}

public enum COLOR_SCHEME
{
    BACKGROUND,
    FOREGROUND,
    INFO,
    WARNING,
    SUCCESS
}

public enum DATA_TYPES
{
    EMAIL,
    USERNAME,
    PASSWORD,
    PRONOSTIC,
    UNDEF
}



public class Commands
{
}
