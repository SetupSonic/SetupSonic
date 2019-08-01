
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SetupSonic
{
    public class SonicProfile
    {

        public string Profile { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string CharName { get; set; }
        public string Realm { get; set; }
        public bool Ladder { get; set; }
        public bool Expansion { get; set; }
        public bool Hardcore { get; set; }
        public int RespecLevel { get => 24; }
        public string BeforeRespecBuild { get => "start"; }
        public string AfterRespectBuild { get => Expansion ? "xblizzard" : "blizzard";  }
        public bool IsRemovable { get => Profile != "all"; }
    }
}
