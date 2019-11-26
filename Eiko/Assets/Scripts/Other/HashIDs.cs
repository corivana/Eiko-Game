using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashIDs : MonoBehaviour
{
    public static int jogCond_Int = Animator.StringToHash("jogCond");
    public static int slashCond_Int = Animator.StringToHash("slashCond");
    public static int shootCond_Int = Animator.StringToHash("shootCond");
    public static int jumpCond_Int = Animator.StringToHash("jumpCond");

    public static int forwardCond_Int = Animator.StringToHash("forwardCond");
    public static int backwardCond_Int = Animator.StringToHash("backwardCond");
    public static int rightCond_Int = Animator.StringToHash("rightCond");
    public static int leftCond_Int = Animator.StringToHash("leftCond");

    public static int flyingCond_Int = Animator.StringToHash("flyingCond");
    public static int attackCond_Int = Animator.StringToHash("attackCond");
}
