namespace SpaceInvadersMVP.PhysicsObject.Ship
{
    public class PlayerShip : GunShip
    {
        protected override bool DestructionBenefitsPlayer => false;
    }
}
