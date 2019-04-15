public class DamageInfo
{
    public readonly float damage;
    public string ownerName;
    public string ownerTag;
    //Damage type

    public DamageInfo(float damage) { this.damage = damage; }
}

public interface IDamage
{
    DamageInfo GetDamageInfo();
}
