using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#if UNITY_EDITOR
using UnityEditor;
//#endif

public class LightFlicker : MonoBehaviour
{
    public enum FlickerMode
    {
        Random,
        AnimationCurve,
        LERP
    }

    [SerializeField] private GameObject JohnLemon;

    public Light flickeringLight;
    public Renderer flickeringRenderer;
    public FlickerMode flickerMode;
    public float lightIntensityMin = 0f;
    public float lightIntensityMax = 100f;
    public float flickerDuration = 0.075f;
    public AnimationCurve intensityCurve;

    Material m_FlickeringMaterial;
    Color m_EmissionColor;
    float m_Timer;
    float m_FlickerLightIntensity;
    
    static readonly int k_EmissionColorID = Shader.PropertyToID (k_EmissiveColorName);
    
    const string k_EmissiveColorName = "_EmissionColor";
    const string k_EmissionName = "_Emission";
    const float k_LightIntensityToEmission = 2f / 3f;

    float minDist = 0f;
    float maxDist = 20f;

    void Start()
    {
        m_FlickeringMaterial = flickeringRenderer.material;
        m_FlickeringMaterial.EnableKeyword(k_EmissionName);
        m_EmissionColor = m_FlickeringMaterial.GetColor(k_EmissionColorID);
    }

    void Update()
    {
        m_Timer += Time.deltaTime;

        if (flickerMode == FlickerMode.Random)
        {
            if (m_Timer >= flickerDuration)
            {
                ChangeRandomFlickerLightIntensity ();
            }
        }
        else if(flickerMode == FlickerMode.AnimationCurve)
        {
            ChangeAnimatedFlickerLightIntensity ();
        }
        else if(flickerMode == FlickerMode.LERP)
        {
            ChangeLERPFlickerLightIntensity();
        }
            
        flickeringLight.intensity = m_FlickerLightIntensity;
        m_FlickeringMaterial.SetColor (k_EmissionColorID, m_EmissionColor * m_FlickerLightIntensity * k_LightIntensityToEmission);
    }

    void ChangeLERPFlickerLightIntensity()
    {
        float dist = DistToPlayer()/20;
        //lerp function: a = lightIntensity max, b = lightIntensityMin, f = distance to player
        m_FlickerLightIntensity = (1 - dist) * lightIntensityMin + dist * lightIntensityMax;
        //Debug.Log(m_FlickerLightIntensity);
        //(1-f) * a + f * b
    }

    void ChangeRandomFlickerLightIntensity ()
    {

        m_FlickerLightIntensity = Random.Range(lightIntensityMin, lightIntensityMax);
        m_Timer = 0f;
    }

    void ChangeAnimatedFlickerLightIntensity ()
    {
        m_FlickerLightIntensity = intensityCurve.Evaluate (m_Timer);

        if (m_Timer >= intensityCurve[intensityCurve.length - 1].time)
            m_Timer = intensityCurve[0].time;
    }

    float DistToPlayer()
    {
        return Mathf.Clamp(Vector3.Distance(JohnLemon.transform.position, transform.position), minDist, maxDist);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(LightFlicker))]
public class LightFlickerEditor : Editor
{
    SerializedProperty m_ScriptProp;
    SerializedProperty m_FlickeringLightProp;
    SerializedProperty m_FlickeringRendererProp;
    SerializedProperty m_FlickerModeProp;
    SerializedProperty m_LightIntensityMinProp;
    SerializedProperty m_LightIntensityMaxProp;
    SerializedProperty m_FlickerDurationProp;
    SerializedProperty m_IntensityCurveProp;
    SerializedProperty JohnLemon;


    void OnEnable ()
    {
        m_ScriptProp = serializedObject.FindProperty ("m_Script");
        m_FlickeringLightProp = serializedObject.FindProperty ("flickeringLight");
        m_FlickeringRendererProp = serializedObject.FindProperty ("flickeringRenderer");
        m_FlickerModeProp = serializedObject.FindProperty ("flickerMode");
        m_LightIntensityMinProp = serializedObject.FindProperty ("lightIntensityMin");
        m_LightIntensityMaxProp = serializedObject.FindProperty ("lightIntensityMax");
        m_FlickerDurationProp = serializedObject.FindProperty ("flickerDuration");
        m_IntensityCurveProp = serializedObject.FindProperty ("intensityCurve");
        JohnLemon = serializedObject.FindProperty("JohnLemon");
    }

    public override void OnInspectorGUI ()
    {
        serializedObject.Update ();

        GUI.enabled = false;
        EditorGUILayout.PropertyField (m_ScriptProp);
        GUI.enabled = true;
        
        EditorGUILayout.PropertyField (m_FlickeringLightProp);
        EditorGUILayout.PropertyField (m_FlickeringRendererProp);
        EditorGUILayout.PropertyField (m_FlickerModeProp);

        if (m_FlickerModeProp.enumValueIndex == 0)
        {
            EditorGUILayout.PropertyField (m_LightIntensityMinProp);
            EditorGUILayout.PropertyField (m_LightIntensityMaxProp);
            EditorGUILayout.PropertyField (m_FlickerDurationProp);

        }
        else if (m_FlickerModeProp.enumValueIndex == 1)
        {
            EditorGUILayout.PropertyField (m_IntensityCurveProp);
        }
        else if (m_FlickerModeProp.enumValueIndex == 2)
        {
            EditorGUILayout.PropertyField(JohnLemon);
            EditorGUILayout.PropertyField(m_LightIntensityMinProp);
            EditorGUILayout.PropertyField(m_LightIntensityMaxProp);
        }

        serializedObject.ApplyModifiedProperties ();
    }

    /*public Light flickeringLight;
    public Renderer flickeringRenderer;
    public FlickerMode flickerMode;
    public float lightIntensityMin = 1.25f;
    public float lightIntensityMax = 2.25f;
    public float flickerDuration = 0.075f;
    public AnimationCurve intensityCurve;*/
}
#endif