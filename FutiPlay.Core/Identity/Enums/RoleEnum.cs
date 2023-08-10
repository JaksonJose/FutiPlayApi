

using Ardalis.SmartEnum;

namespace FutiPlay.Core.Identity.Enums
{
    public sealed class RoleEnum : SmartEnum<RoleEnum>
    {
        public static readonly RoleEnum SysAdmin = new(nameof(SysAdmin), 0);
        public static readonly RoleEnum NormalUser = new(nameof(NormalUser), 1);
        public static readonly RoleEnum TournamentOwner = new(nameof(TournamentOwner), 2);
        public static readonly RoleEnum TeamOwner = new(nameof(TeamOwner), 3);
        public static readonly RoleEnum Coach = new(nameof(Coach), 4);
        public static readonly RoleEnum Player = new(nameof(Player), 5);
        public static readonly RoleEnum Referee = new(nameof(Referee), 6);

        public RoleEnum(string name, int value) : base(name, value)
        {
        }

        public RoleEnum() : this("None", 0)
        {
        }
    }
}
