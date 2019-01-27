//By Joseph Giordano.

using UnityEngine;
using System.Collections;

//Attach this class to the GameObject you want the arrow to be pointing at.
public class OffScreenIndicator : MonoBehaviour
{
    public Texture2D icon; //The icon. Preferably an arrow pointing upwards.
    public float iconSize = 50f;

    [HideInInspector]
    public GUIStyle
        gooey; //GUIStyle to make the box around the icon invisible. Public so that everything has the default stats.

    Vector2 indRange;

    float scaleRes = Screen.width / 500; //The width of the screen divided by 500. Will make the GUI automatically

//scale with varying resolutions.
    public int cameraIndex;
    public Camera cam;
    bool visible = true; //Whether or not the object is visible in the camera.

    void Start()
    {
        cam = GameObject.FindWithTag($"cam{cameraIndex}").GetComponent<Camera>();
        
        visible = GetComponent<SpriteRenderer>().isVisible;

        indRange.x = cam.pixelRect.width - (cam.pixelRect.width / 6);
        indRange.y = cam.pixelRect.height - (cam.pixelRect.height / 7);
        indRange /= 2f;

        gooey.normal.textColor = new Vector4(0, 0, 0, 0); //Makes the box around the icon invisible.
    }

    void OnGUI()
    {
        if (!visible && cam.isActiveAndEnabled)
        {
            Vector3 dir = transform.position - cam.transform.position;
            dir = Vector3.Normalize(dir);
            dir.y *= -1f;

            Vector2 indPos = new Vector2(indRange.x * dir.x, indRange.y * dir.y);
            indPos = new Vector2((Screen.width / 2) + indPos.x,
                (Screen.height / 2) + indPos.y);

            Vector3 pdir = transform.position - cam.ScreenToWorldPoint(new Vector3(indPos.x, indPos.y,
                               transform.position.z));
            pdir = Vector3.Normalize(pdir);

            float angle = Mathf.Atan2(pdir.x, pdir.y) * Mathf.Rad2Deg;

            GUIUtility.RotateAroundPivot(angle,
                indPos); //Rotates the GUI. Only rotates GUI drawn after the rotate is called, not before.
            GUI.Box(new Rect(indPos.x, indPos.y, scaleRes * iconSize, scaleRes * iconSize), icon, gooey);
            GUIUtility.RotateAroundPivot(0,
                indPos); //Rotates GUI back to the default so that GUI drawn after is not rotated.
        }
    }

    void OnBecameInvisible()
    {
        visible = false;
    }

//Turns off the indicator if object is onscreen.
    void OnBecameVisible()
    {
        visible = true;
    }
}