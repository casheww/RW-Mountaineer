global using UnityEngine;
using System;
using System.Runtime.CompilerServices;
using BepInEx;
using SlugBase.Features;
using static SlugBase.Features.FeatureTypes;

namespace Mountaineer;

[BepInPlugin("casheww.mountaineer", "The Mountaineer", "1.1.0")]
class Plugin : BaseUnityPlugin
{
    public static readonly PlayerFeature<bool> WallClimb = PlayerBool("wall_climb");
    private readonly ConditionalWeakTable<Player, ClimbingModule> climbingModules = new ();

    public void OnEnable() {
        On.RainWorld.OnModsInit += (orig, rw) => {
            try {
                orig(rw);
                MachineConnector.SetRegisteredOI(Info.Metadata.GUID, new ConfigOI(this));
            }
            catch (Exception e) {
                Logger.LogError(e);
            }
        };
        On.RainWorld.OnModsInit += Extras.WrapInit(LoadResources);

        On.Player.Update += Player_Update;
    }

    private void Player_Update(On.Player.orig_Update orig, Player self, bool eu) {
        orig(self, eu);

        if (!IsMe(WallClimb, self))
            return;

        if (Input.GetKey(ConfigOI.ClimbKey.Value) && self.bodyMode == Player.BodyModeIndex.WallClimb) {
            ClimbingModule cm = GetClimbingModule(self);
            Vector2 climbVel = cm.GetClimbVelocity(self.input[0]);

            foreach (BodyChunk bc in self.bodyChunks)
                bc.vel = climbVel;
        }
    }

    public static bool IsMe(PlayerFeature<bool> feature, Player player) =>
        feature.TryGet(player, out bool set) && set;

    private ClimbingModule GetClimbingModule(Player player) {
        ClimbingModule cm;

        if (!climbingModules.TryGetValue(player, out cm)) {
            cm = new ClimbingModule();
            climbingModules.Add(player, cm);
        }

        return cm;
    }
    
    // Load any resources, such as sprites or sounds
    private void LoadResources(RainWorld rainWorld) {}
}