namespace Item
{
    public class Weapon: Item
    {
        public float Damage;
        public Weapon(ElementalStatus s, float damage): base(s)
        {
            Damage = damage;
        }
    }
}