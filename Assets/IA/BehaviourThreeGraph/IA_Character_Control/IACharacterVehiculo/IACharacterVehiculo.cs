using UnityEngine;

public class IACharacterVehiculo : IACharacterControl
{
    public enum UnitGame { Zombie, Soldier, Civil }
    public enum TipoAnimal { Pollo, Lobo, Perro }

    protected UnitGame _UnitGame;
    public TipoAnimal tipoAnimal;

    protected CalculateDiffuse _CalculateDiffuse;
    protected float speedRotation = 0;
    public float RangeWander;
    Vector3 positionWander;
    float FrameRate = 0;
    float Rate = 4;
    public HungerSystem HungerSystem { get; protected set; }

    public override void LoadComponent()
    {
        base.LoadComponent();
        positionWander = RandoWander(transform.position, RangeWander);
        _CalculateDiffuse = GetComponent<CalculateDiffuse>();
        HungerSystem = GetComponent<HungerSystem>();

        _UnitGame = tipoAnimal switch
        {
            TipoAnimal.Lobo => UnitGame.Zombie,
            TipoAnimal.Pollo => UnitGame.Civil,
            TipoAnimal.Perro => UnitGame.Soldier,
            _ => UnitGame.Civil
        };
    }

    public virtual void LookEnemy()
    {
        if (AIEye.ViewEnemy == null) return;
        Vector3 dir = (AIEye.ViewEnemy.transform.position - transform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(dir);
        rot.x = 0;
        rot.z = 0;
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 50);
    }

    public virtual void LookPosition(Vector3 position)
    {
        Vector3 dir = (position - transform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(dir);
        rot.x = 0;
        rot.z = 0;
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * speedRotation);
    }

    public virtual void LookRotationCollider()
    {
        if (_CalculateDiffuse.Collider)
        {
            speedRotation = _CalculateDiffuse.speedRotation;
            Vector3 posNormal = _CalculateDiffuse.hit.point + _CalculateDiffuse.hit.normal * 2;
            LookPosition(posNormal);
        }
    }

    public virtual void MoveToPosition(Vector3 pos)
    {
        agent.SetDestination(pos);
    }

    public virtual void MoveToEnemy()
    {
        if (AIEye.ViewEnemy == null) return;
        MoveToPosition(AIEye.ViewEnemy.transform.position);
    }

    public virtual void MoveToAllied()
    {
        if (AIEye.ViewAllie == null) return;
        MoveToPosition(AIEye.ViewAllie.transform.position);
    }

    public virtual void MoveToEvadEnemy()
    {
        if (AIEye.ViewEnemy == null) return;
        Vector3 dir = (transform.position - AIEye.ViewEnemy.transform.position).normalized;
        Vector3 newPosition = transform.position + dir * 5f;
        MoveToPosition(newPosition);
    }

    Vector3 RandoWander(Vector3 position, float range)
    {
        Vector3 randP = Random.insideUnitSphere * range;
        randP.y = transform.position.y;
        return position + randP;
    }

    public virtual void MoveToWander()
    {
        if (AIEye.ViewEnemy != null) return;

        float distance = (transform.position - positionWander).magnitude;

        if (distance < 2)
        {
            positionWander = RandoWander(transform.position, RangeWander);
        }

        if (FrameRate > Rate)
        {
            FrameRate = 0;
            positionWander = RandoWander(transform.position, RangeWander);
        }
        FrameRate += Time.deltaTime;

        MoveToPosition(positionWander);
    }
}
