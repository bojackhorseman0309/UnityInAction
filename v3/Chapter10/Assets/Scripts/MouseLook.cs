using UnityEngine;

namespace UnityInAction
{
    public class MouseLook : MonoBehaviour
    {
        public enum RotationAxes
        {
            MouseXAndY = 0,
            MouseX = 1,
            MouseY = 2
        }

        public RotationAxes axes = RotationAxes.MouseXAndY;
        public float sensitivityHor = 9.0f;
        public float sensitivityVert = 9.0f;

        public float minimumVert = -45.0f;
        public float maximumVert = 45.0f;

        private float verticalRot;

        private void Start()
        {
            var body = GetComponent<Rigidbody>();
            if (body != null) body.freezeRotation = true;
        }

        private void Update()
        {
            if (axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
            }
            else if (axes == RotationAxes.MouseY)
            {
                verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVert;
                verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);

                var horizontalRot = transform.localEulerAngles.y;

                transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0);
            }
            else
            {
                verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVert;
                verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);

                var delta = Input.GetAxis("Mouse X") * sensitivityHor;
                var horizontalRot = transform.localEulerAngles.y + delta;

                transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0);
            }
        }
    }
}