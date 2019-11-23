using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashIDs : MonoBehaviour
{
    public static int jogCond_Int = Animator.StringToHash("jogCond");
    public static int slashCond_Int = Animator.StringToHash("slashCond");

    public static int flyingCond_Int = Animator.StringToHash("flyingCond");
    public static int attackCond_Int = Animator.StringToHash("attackCond");
}
