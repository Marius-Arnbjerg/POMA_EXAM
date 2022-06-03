using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityFormalia : MonoBehaviour 
{
    // If needed, here you write where the script should be attached
    // If the code is copied from other sources, be sure to comment here with the location of the source code


    public float publicVariable;
    // When naming public variables, the first letter starts with lower case letter and each new word is capital letter.
    // Variable names should be easy to understand and no abbreviations
    // Only create public variables if other classes needs to comunicate with the variable or vice versa

    private float _privateVariable;
    // When naming private variables, the name starts with "_"

    [SerializeField] // If a variable is private but you need to see it in the inspector, [SerializeField] is needed above the variable
    private bool _privateVariableInInspector;

    [Header("Player Attributes")] // To make the inspector manageable be sure to create headers and tooltips
    [Tooltip("Player Health")]
    public float playerHealth;
    [Tooltip("Player Damage")]
    public int playerDamage;


    public void UnderstandingMethods()
    {
        // When creating a method/function, the name must be easy to understand, and relate to what the method is doing.
    }
}
