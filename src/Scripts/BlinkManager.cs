using Photon.Pun;
using UnityEngine;

namespace DevBlinkMod.Scripts
{
    public class BlinkManager : MonoBehaviour
    {
        public VRRig rig;
        public Renderer faceRenderer;

        internal float offsetX = 0;
        internal float offsetY = 0f;
        internal Vector2 mainTextureOffset;

        internal float blinkTime;
        internal float blinkCooldown = 0.075f;

        public BlinkState blink = BlinkState.Neutral;

        public enum BlinkState
        {
            Neutral,
            BeginStart,
            MiddleStart,
            EndStart,
            MiddleEnd,
            BeginEnd
        }

        internal float SetRandomBlinkTime() { return Time.time + Random.Range(0.5f, 6.5f); }

        public void Start()
        {
            if (gameObject.GetComponent<VRRig>() == null) Destroy(this);
            rig = gameObject.GetComponent<VRRig>();

            faceRenderer = rig.headMesh.transform.Find("gorillaface").GetComponent<Renderer>();
            faceRenderer.material.mainTexture = Plugin.Instance.faceSheet;
            faceRenderer.material.mainTextureScale = new Vector2(0.25f, 1f);
            faceRenderer.material.mainTextureOffset = Vector2.zero;

            blinkTime = SetRandomBlinkTime();
        }

        internal void LateUpdate()
        {
            if (rig == null || faceRenderer == null)
            {
                return;
            }

            if (rig.isOfflineVRRig)
            {
                faceRenderer.forceRenderingOff = PhotonNetwork.InRoom;
            }

            if (Time.time >= blinkTime)
            {
                switch (blink)
                {
                    case BlinkState.Neutral:
                        blink = BlinkState.BeginStart;
                        offsetX = 0.25f;
                        blinkTime = Time.time + blinkCooldown;
                        break;
                    case BlinkState.BeginStart:
                        blink = BlinkState.MiddleStart;
                        offsetX = 0.5f;
                        blinkTime = Time.time + blinkCooldown;
                        break;
                    case BlinkState.MiddleStart:
                        blink = BlinkState.EndStart;
                        offsetX = 0.75f;
                        blinkTime = Time.time + blinkCooldown;
                        break;
                    case BlinkState.EndStart:
                        blink = BlinkState.MiddleEnd;
                        offsetX = 0.5f;
                        blinkTime = Time.time + blinkCooldown;
                        break;
                    case BlinkState.MiddleEnd:
                        blink = BlinkState.BeginEnd;
                        offsetX = 0.25f;
                        blinkTime = Time.time + blinkCooldown;
                        break;
                    case BlinkState.BeginEnd:
                        blink = BlinkState.Neutral;
                        offsetX = 0f;
                        blinkTime = SetRandomBlinkTime();
                        break;
                }
            }

            mainTextureOffset = new Vector2(offsetX, offsetY);
            faceRenderer.material.mainTextureOffset = mainTextureOffset;
        }
    }
}
