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
        internal float blinkDelay
        {
            get 
                => Time.time + Random.Range(2f, 7.5f);
        }
        internal const float blinkCooldownClosed = 0.22f;
        internal const float blinkCooldown = 0.10f;

        public BlinkState currentBlinkState = BlinkState.Idle;
        internal BlinkState lastBlinkState = BlinkState.Idle;
        public enum BlinkState
        {
            Idle,
            IdleHalf,
            BlinkClosed,
            BlinkHalf
        }

        public void OnEnable()
        {
            rig = gameObject.GetComponent<VRRig>();

            faceRenderer = rig.headMesh.transform.Find("gorillaface").GetComponent<Renderer>();
            faceRenderer.material.mainTexture = Plugin.Instance.faceSheet;
            faceRenderer.material.mainTextureScale = new Vector2(1f / 3f, 1f);
            faceRenderer.material.mainTextureOffset = Vector2.zero;
            blinkTime = blinkDelay;
        }

        internal void LateUpdate()
        {
            if (rig == null || faceRenderer == null)
                return;

            if (Time.time >= blinkTime)
            {
                switch (currentBlinkState)
                {
                    case BlinkState.Idle:
                        currentBlinkState = BlinkState.IdleHalf;
                        offsetX = 1f / 3f;
                        blinkTime = Time.time + blinkCooldown * 0.7f;
                        break;
                    case BlinkState.IdleHalf:
                        currentBlinkState = BlinkState.BlinkClosed;
                        offsetX = 1f / 3f * 2;
                        blinkTime = Time.time + blinkCooldownClosed;
                        break;
                    case BlinkState.BlinkClosed:
                        currentBlinkState = BlinkState.BlinkHalf;
                        offsetX = 1f / 3f;
                        blinkTime = Time.time + blinkCooldown;
                        break;
                    case BlinkState.BlinkHalf:
                        currentBlinkState = BlinkState.Idle;
                        offsetX = 0f;
                        blinkTime = blinkDelay;
                        break;
                }
            }

            if (lastBlinkState != currentBlinkState)
            {
                lastBlinkState = currentBlinkState;
                mainTextureOffset = new Vector2(offsetX, offsetY);
                faceRenderer.material.mainTextureOffset = mainTextureOffset;
            }
        }
    }
}
