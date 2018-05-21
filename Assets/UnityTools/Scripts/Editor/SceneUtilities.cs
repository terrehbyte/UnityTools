using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityTools
{
    public class SceneUtilities
    {
        private static Bounds GetGameObjectBounds(GameObject target)
        {
            // try with collider
            Collider gCol = target.GetComponent<Collider>();
            if (gCol == null)
            {
                // failing that, try with renderer
                Renderer gRender = target.GetComponent<Renderer>();
                if (gRender == null)
                {
                    return new Bounds(target.transform.position, Vector3.zero);
                }

                return gRender.bounds;
            }
            else
            {
                return gCol.bounds;
            }
        }

        [MenuItem("GameObject/Snap to Floor _END", true)]
        private static bool SnapToFloorValidate()
        {
            return Selection.gameObjects.Length > 0;
        }

        [MenuItem("GameObject/Snap to Floor _END", false, 0)]
        private static void SnapToFloor()
        {
            var selected = Selection.gameObjects;
            Debug.Assert(selected.Length > 0, "No objects to snap to floor!");

            foreach (var gObj in selected)
            {
                // determine bounds of snapped object
                Bounds bounds = GetGameObjectBounds(gObj);

                // TODO: snapping only functions with colliders for now, need to implement custom solution for renderers
                RaycastHit hit;
                Physics.Raycast(gObj.transform.position, Vector3.down, out hit, Mathf.Infinity, ~0);
                if (hit.collider != null)
                {
                    Undo.RecordObject(gObj.transform, "Snap to Floor");

                    float yOffset = gObj.transform.position.y - bounds.min.y;
                    gObj.transform.position = hit.point + (hit.normal * yOffset);
                    gObj.transform.up = hit.normal;
                }

                EditorUtility.SetDirty(gObj.transform);
            }
        }
    }
}