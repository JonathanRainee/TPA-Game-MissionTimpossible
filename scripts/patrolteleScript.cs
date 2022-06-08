using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class patroltele : MonoBehaviour
{
    static int MinX, MaxX, MinZ, MaxZ;
    static float GridSize = 1f;
    static float MaxSteep = 1.5f;
    static float EndX, EndZ;
    static float StopAstarChasing, StopAstarPatroling;
    static float ShootingRange;
    static float ChasingRange;
    public Transform prefab;
    static public Transform target;

    private healthsodier hs;

    // points for patrol
    public GameObject parentPatrolPoints;

    private Animator anim;
    public GameObject Player;
    float MoveSpeed;
    float RotateSpeed;
    [SerializeField] private LayerMask ground;
    private bool isGrounded;
    private float gravity = -9.8f;
    private float groundDistance;
    private Vector3 velocity;
    private CharacterController controller;

    // A STAR
    List<Vector3> Path;
    bool IsChasing;
    bool IsShooting;
    bool IsPatroling;
    float AStarCoolDown;
    float LastAStar;
    float PointDistance;
    float speed;
    public List<Vector3> PatrolPoints;
    int PatrolIndex;
    float PointVisitRange;

    // HP system
    float hp;
    float maxHp;
    //UIEnemyHealth uIEnemyHealth;

    [SerializeField] GameObject PistolAmmo;
    [SerializeField] GameObject RifleAmmo;

    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        target = GameObject.Find("char").transform;
        MinX = 0;
        MaxX = 500;
        MinZ = 0;
        MaxZ = 500;
        MoveSpeed = 3f;
        RotateSpeed = 10f;
        ShootingRange = 25;
        ChasingRange = 35;
        StopAstarChasing = 10;
        StopAstarPatroling = 0.5f;
        IsChasing = false;
        IsShooting = false;
        IsPatroling = false;
        AStarCoolDown = 2f;
        LastAStar = 1;
        PointDistance = 3f;
        PointVisitRange = 12f;
        Path = new List<Vector3>();

        maxHp = 100;
        hp = maxHp;
        Field = new Node[MaxX + 2, MaxZ + 2];
        //uIEnemyHealth = GetComponentInChildren<UIEnemyHealth>();

        // initialize point to patrol
        parentPatrolPoints = GameObject.Find("ppsoldiertp");
        PatrolIndex = -1;
        foreach (Transform PatrolPoint in parentPatrolPoints.transform)
        {
            PatrolPoints.Add(PatrolPoint.position);
            Debug.Log("posistion ngew " + PatrolPoint.position);
        }
        hs = gameObject.GetComponent<healthsodier>();
    }

    void Update()
    {
        

        float distX = Player.transform.position.x - transform.position.x;
        float distZ = Player.transform.position.z - transform.position.z;
        float dist = Mathf.Sqrt(distX * distX + distZ * distZ);
        // UnityEngine.Debug.Log("dist " + dist);
        if (dist <= ShootingRange)
        {
            if (hs.currhealth > 0)
            {
                Shooting();
            }
        }
        else if (dist <= ChasingRange)
        {
            Chasing();
        }
        else
        {
            Patroling();
        }

        // gravity
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, ground); // checkk apakah colide disekitarnya
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 2f; // biar ga melayang
        }
        velocity.y += gravity * Time.deltaTime;
        // controller.Move(velocity * Time.deltaTime);





    }

    public static float getHeuristic(float X, float Z)
    {
        X += (GridSize / 2);
        Z += (GridSize / 2);
        float GCost = Mathf.Sqrt(Mathf.Pow((X - EndX), 2) + Mathf.Pow((Z - EndZ), 2) * 1f);
        // float HCost = Mathf.Sqrt(Mathf.Pow((X-target.position.x), 2) + Mathf.Pow((Z-target.position.z), 2) * 1f);
        // return GCost + HCost;
        return GCost;
    }
    public class Node
    {
        public int PrevX, PrevZ;
        public int NextX, NextZ;
        public int X, Z;
        public bool Visited;
        public float Heuristic;
        public Node() { }
        public Node(int X, int Z)
        {
            this.X = X;
            this.Z = Z;
            this.PrevX = -1; // default value
            this.PrevZ = -1;
            this.NextX = -1;
            this.NextZ = -1;
            this.Visited = false;
            this.Heuristic = getHeuristic(X, Z);
        }
        public void SetPrev(int X, int Z)
        {
            this.PrevX = X;
            this.PrevZ = Z;
        }
        public void SetNext(int X, int Z)
        {
            this.NextX = X;
            this.NextZ = Z;
        }
        public void MarkVisit()
        {
            this.Visited = true;
        }
    }
    public Node[,] Field;
    public Node GetField(int X, int Z)
    {
        if (Field[X, Z] == null)
        {
            Field[X, Z] = new Node(X, Z);
        }
        return Field[X, Z];
    }
    public bool CheckPos(int CurrentX, int CurrentZ, int PrevX, int PrevZ)
    {
        Vector3 Current = new Vector3(CurrentX, 0, CurrentZ);
        Vector3 Prev = new Vector3(PrevX, 0, PrevZ);
        if (
            CurrentX >= MinX && CurrentX <= MaxX && CurrentZ >= MinZ && CurrentZ <= MaxZ // in of bounds
            && GetField(CurrentX, CurrentZ).Visited == false // not visited
            && astarMapper.ValidField[CurrentX, CurrentZ] // not collidingKC
            && Mathf.Abs(Terrain.activeTerrain.SampleHeight(Current) - Terrain.activeTerrain.SampleHeight(Prev)) < MaxSteep // not too steep
        ) return true;
        return false;
    }
    public float getDistance(float X1, float Z1, float X2, float Z2)
    {
        return Mathf.Sqrt((Mathf.Pow((X1 - X2), 2f) + Mathf.Pow((Z1 - Z2), 2f)));
    }
    public List<Vector3> pathfind(int StartX, int StartZ, int X2, int Z2, float StopAstarRange)
    {
        List<Vector3> Result = new List<Vector3>();
        EndX = X2;
        EndZ = Z2;
        Field = new Node[MaxX + 2, MaxZ + 2];
        bool found = false;
        int LastX = -1, LastZ = -1;

        // add first grid to the queue
        GetField(StartX, StartZ).MarkVisit();
        List<Node> Queue = new List<Node>();
        Node TempNode = new Node(StartX, StartZ);
        Queue.Add(TempNode);

        //  loop while queue not empty and not found
        while (Queue.Count > 0 && !found)
        {

            // get node with least heuristic
            float LeastHeuristic = 10000000f;
            int LeastIndex = -1;
            for (int i = 0; i < Queue.Count; i++)
            {
                if (Queue[i].Heuristic < LeastHeuristic)
                {
                    LeastIndex = i;
                    LeastHeuristic = Queue[i].Heuristic;
                }
            }

            // check the current node
            Node Check = Queue[LeastIndex];
            // UnityEngine.Debug.Log("visiting " + Check.X + " " + Check.Z);
            Queue.RemoveAt(LeastIndex);
            if (getDistance(Check.X, Check.Z, EndX, EndZ) <= StopAstarRange)
            {
                found = true;
                // UnityEngine.Debug.Log("found " + Check.X + " " + Check.Z);
                LastX = Check.X;
                LastZ = Check.Z;
                break;
            }

            for (int i = -1; i <= 1; i++)
            {
                for (int k = -1; k <= 1; k++)
                {
                    if (i == 0 && k == 0) continue;
                    int TempX = Check.X + i;
                    int TempZ = Check.Z + k;
                    if (CheckPos(TempX, TempZ, Check.X, Check.Z))
                    {
                        int CountSide = 0;
                        if (k == -1 || k == 1) CountSide++;
                        if (i == -1 || i == 1) CountSide++;
                        if (CountSide == 2)
                        {
                            if (!(CheckPos(TempX - i, TempZ, Check.X, Check.Z) && CheckPos(TempX, TempZ - k, Check.X, Check.Z))) continue;
                        }
                        GetField(TempX, TempZ).MarkVisit();
                        GetField(TempX, TempZ).SetPrev(Check.X, Check.Z);
                        TempNode = new Node(TempX, TempZ);
                        Queue.Add(TempNode);
                    }
                    else
                    {
                        // UnityEngine.Debug.Log("Invalid arouund");
                    }
                }
            }
        }


        if (found)
        {
            int CurrentX = LastX;
            int CurrentZ = LastZ;
            int PrevX = -1, PrevZ = -1, NextX, NextZ;
            while (GetField(CurrentX, CurrentZ).PrevX > 0)
            {
                // UnityEngine.Debug.Log("back tracking " + CurrentX + " " + CurrentZ);
                PrevX = GetField(CurrentX, CurrentZ).PrevX;
                PrevZ = GetField(CurrentX, CurrentZ).PrevZ;
                GetField(PrevX, PrevZ).SetNext(CurrentX, CurrentZ);
                CurrentX = PrevX;
                CurrentZ = PrevZ;
            }

            CurrentX = StartX;
            CurrentZ = StartZ;

            while (GetField(CurrentX, CurrentZ).NextX > 0 && GetField(CurrentX, CurrentZ).NextZ > 0)
            {
                // Instantiate(prefab, new Vector3(CurrentX, Terrain.activeTerrain.SampleHeight(new Vector3(CurrentX,1f, CurrentZ)), CurrentZ), Quaternion.identity);
                Result.Add(new Vector3(CurrentX, 0, CurrentZ));
                NextX = GetField(CurrentX, CurrentZ).NextX;
                NextZ = GetField(CurrentX, CurrentZ).NextZ;
                CurrentX = NextX;
                CurrentZ = NextZ;
            }
        }
        else
        {
            UnityEngine.Debug.Log("Not found ngew");
        }
        return Result;
    }
    void FaceToPosition(Vector3 Pos)
    {
        var lookTo = Pos - transform.position;
        lookTo.y = 0;
        var rotation = Quaternion.LookRotation(lookTo);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * RotateSpeed);
    }
    void faceToPlayer()
    {
        FaceToPosition(Player.transform.position);
    }
    void Shooting()
    {
        faceToPlayer();
        IsShooting = true;
        IsChasing = false;
        IsPatroling = false;
        anim.SetFloat("Shooting", 1f);
        PatrolWeapon weapon = GetComponentInParent<PatrolWeapon>();
        weapon.ShootToPlayer();
    }
    void Chasing()
    {
        if (!IsChasing)
        {
            IsShooting = false;
            IsChasing = true;
            IsPatroling = false;
            anim.SetFloat("Shooting", 1f);
            Path = pathfind(
                Mathf.FloorToInt(transform.position.x),
                Mathf.FloorToInt(transform.position.z),
                Mathf.FloorToInt(Player.transform.position.x),
                Mathf.FloorToInt(Player.transform.position.z),
                StopAstarChasing
            );
        }
        if (Time.time > LastAStar + AStarCoolDown)
        {
            // update pathfinder
            LastAStar = Time.time;
            Path = pathfind(
                Mathf.FloorToInt(transform.position.x),
                Mathf.FloorToInt(transform.position.z),
                Mathf.FloorToInt(Player.transform.position.x),
                Mathf.FloorToInt(Player.transform.position.z),
                StopAstarChasing
            );
        }
        Vector3 NextPoint;
        float height;
        bool Repeat = false;
        do
        {
            if (Path.Count <= 0) return;
            Repeat = false;
            NextPoint = Path[0];
            height = Terrain.activeTerrain.SampleHeight(NextPoint);
            NextPoint.y = height;
            if (getDistance(
                NextPoint.x,
                NextPoint.z,
                transform.position.x,
                transform.position.z
            ) < PointDistance)
            {
                Repeat = true;
                Path.RemoveAt(0);
            }
        } while (Repeat);
        FaceToPosition(NextPoint);
        speed = MoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, NextPoint, speed);
        // UnityEngine.Debug.Log("moving to" + Path[0].x + " " + NextPoint.y + " " + Path[0].y);
    }
    void Patroling()
    {
        if (PatrolIndex == -1)
        {
            // first patrol
            Debug.Log("first patrol");
            PatrolIndex = Random.Range(0, PatrolPoints.Count - 1);
            Debug.Log(PatrolPoints[PatrolIndex].x + " and " + PatrolPoints[PatrolIndex].z);
        }
        if (!IsPatroling)
        {
            IsShooting = false;
            IsChasing = false;
            IsPatroling = true;
            Path = pathfind(
                Mathf.FloorToInt(transform.position.x),
                Mathf.FloorToInt(transform.position.z),
                Mathf.FloorToInt(PatrolPoints[PatrolIndex].x),
                Mathf.FloorToInt(PatrolPoints[PatrolIndex].z),
                StopAstarPatroling
            );
        }
        else
        {
            Vector3 NextPoint;
            float height;
            bool Repeat = false;
            do
            {
                if (Path.Count <= 0) return;
                Repeat = false;
                NextPoint = Path[0];
                height = Terrain.activeTerrain.SampleHeight(NextPoint);
                NextPoint.y = height;
                if (getDistance(
                    NextPoint.x,
                    NextPoint.z,
                    transform.position.x,
                    transform.position.z
                ) < PointDistance)
                {
                    Repeat = true;
                    Path.RemoveAt(0);
                }
            } while (Repeat);
            FaceToPosition(NextPoint);
            speed = MoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, NextPoint, speed);
            // UnityEngine.Debug.Log("Patroling to" + Path[0].x + " " + NextPoint3.y + " " + Path[0].y);

        }
        if (getDistance(
            PatrolPoints[PatrolIndex].x,
            PatrolPoints[PatrolIndex].z,
            transform.position.x,
            transform.position.z
        ) < PointVisitRange)
        {
            // switch destination
            PatrolIndex = Random.Range(0, PatrolPoints.Count - 1);
            Path = pathfind(
                Mathf.FloorToInt(transform.position.x),
                Mathf.FloorToInt(transform.position.z),
                Mathf.FloorToInt(PatrolPoints[PatrolIndex].x),
                Mathf.FloorToInt(PatrolPoints[PatrolIndex].z),
                StopAstarPatroling
            );
        }
    }

    public void TakeDamage(float damageReceived)
    {
        Debug.Log("take damage of " + damageReceived);
        hp -= damageReceived;
        if (hp < 0) Die();
        //GetComponentInChildren<UIEnemyHealth>().SetHealthPercentage(hp, maxHp);
    }

    void Die()
    {
        //GameStateScript.AddPatrolKill();
        int rand = Random.Range(1, 5);
        if (rand == 3) Instantiate(RifleAmmo, transform.position, Quaternion.identity);
        if (rand == 4) Instantiate(PistolAmmo, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

}