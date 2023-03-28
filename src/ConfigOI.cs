using Menu.Remix.MixedUI;

namespace Mountaineer;

class ConfigOI : OptionInterface {
    public ConfigOI(Plugin plugin) {
        this.plugin = plugin;
        ClimbKey = config.Bind<KeyCode>("climb_key", KeyCode.C);
    }

    public override void Initialize()
    {
        base.Initialize();

        Tabs = new OpTab[1] { new OpTab(this) };
        OpTab t = Tabs[0];

        t.AddItems(
            //new OpLabel()
        );
    }

    private readonly Plugin plugin;
    public static Configurable<KeyCode> ClimbKey { get; private set; }
}
