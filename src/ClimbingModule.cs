
namespace Mountaineer;

class ClimbingModule {
    public Vector2 GetClimbVelocity(Player.InputPackage input) {
        Vector2 climbVel;

        climbVel = input.y switch {
            1  => new Vector2(0f,   3f),
            -1 => new Vector2(0f,  -3f),
            _  => new Vector2(0f, 0.9f)
        };

        climbVel.x = input.x;

        return climbVel;
    }
}
