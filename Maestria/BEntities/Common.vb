Option Explicit On
Option Compare Text
Option Strict On
Option Infer On

Public Enum StatusType As Byte
    NoAction = 0
    Insert = 1
    Update = 2
    Delete = 3
    Dismiss = 4
End Enum

Public Enum Orientation As Byte
    Landscape = 5
    Portrait = 6
End Enum

Public Enum CardinalPoint As Byte
    North = 7
    South = 8
    East = 9
    West = 10
End Enum