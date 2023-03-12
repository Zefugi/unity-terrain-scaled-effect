# Terrain Scaled Effects for Unity
A simple example of how to scale effects and trigger events based on a terrain texture.

The example project uses URP, but the code (except the Vignette example effect) can be used in SRP or HDRP as well.

Usage:
- Dublicate and rename /Assets/Runtime/TerrainScaledExampleScript.cs
- Open your new file and rename the class to match the file.
- Insert your custom ligic into the Update method.
- Drop your new script onto an object that should control the effect.
- Assign the terrain you want to use.
- Set the terrain layer index to the index of your effect layer.

Tips:
- GetEffectWeight() returns a value between 0 and 1, where 0 is not on effect terrain and 1 is fully on effect terrain.
- If you use the Awake method, make sure to override and call base.Awake() or the code will halt and catch fire.
