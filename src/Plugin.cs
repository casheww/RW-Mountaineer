using BepInEx;
using UnityEngine;
using SlugBase.Features;
using static SlugBase.Features.FeatureTypes;

namespace Mountaineer;

[BepInPlugin("casheww.mountaineer", "The Mountaineer", "1.1.0")]
class Plugin : BaseUnityPlugin
{
    public void OnEnable()
    {
        On.RainWorld.OnModsInit += Extras.WrapInit(LoadResources);

    }
    
    // Load any resources, such as sprites or sounds
    private void LoadResources(RainWorld rainWorld)
    {
    }
}