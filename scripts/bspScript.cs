using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bsp : MonoBehaviour
{
    static public float MaxRatio;
    static public float RandomFloat;
    static public int RandomInt;
    static public int MaxHeight, MaxWidth;
    static public bool[,] BSPMaze;
    static public Vector3 StartMaze, EndMaze;
    public Transform PrefabBSP;
    public Collider ColliderBSP;
    [SerializeField] GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        MaxHeight = 50;
        MaxWidth = 50;
        MaxRatio = 0.3f;
        int StartX = 300;
        int StartZ = 0;
        int SplitCount = 6;
        Vector3 BlockSize = ColliderBSP.bounds.size;

        BSPMaze = new bool[MaxWidth + 2, MaxHeight + 2];
        for (int X = 0; X <= MaxWidth; X++)
        {
            for (int Z = 0; Z <= MaxHeight; Z++)
            {
                BSPMaze[X, Z] = true;
            }
        }

        Node BSPRoot = new Node(0, 0, MaxWidth, MaxHeight);
        BSPRoot.Split(SplitCount);
        BSPRoot.MapToField();

        Vector3 MostLeftChild = BSPRoot.GetMostLeftChild();
        Vector3 MostRightChild = BSPRoot.GetMostRightChild();
        Vector3 StartMazePoint = new Vector3(
            StartX + (MostLeftChild.x * BlockSize.x),
            MostLeftChild.y,
            StartZ + (MostLeftChild.z * BlockSize.z)
        );
        Vector3 EndMazePoint = new Vector3(
            StartX + (MostRightChild.x * BlockSize.x),
            MostLeftChild.y,
            StartZ + (MostRightChild.z * BlockSize.z)
        );

        StartMaze = StartMazePoint;

        for (int i = Mathf.FloorToInt(MostRightChild.z); i < MaxHeight; i++)
        {
            BSPMaze[Mathf.FloorToInt(MostRightChild.x), i] = false;
        }
        for (int i = Mathf.FloorToInt(MostRightChild.x); i <= MaxWidth; i++)
        {
            BSPMaze[i, MaxWidth - 1] = false;
        }


        //print map
        float MiddleY = BlockSize.y / 2;
        for (int X = 0; X <= MaxWidth; X++)
        {
            for (int Z = 0; Z <= MaxHeight; Z++)
            {
                if (BSPMaze[X, Z])
                {
                    //boundary
                    float CurrX = StartX + (X * BlockSize.x);
                    float CurrZ = StartZ + (Z * BlockSize.z);
                    Instantiate(PrefabBSP, new UnityEngine.Vector3(CurrX, MiddleY, CurrZ), UnityEngine.Quaternion.identity);
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public class Node
    {
        int X, Z;
        int Width, Height;
        Node LeftChild;
        Node RightChild;

        public Node(int X, int Z, int Width, int Height)
        {
            this.X = X;
            this.Z = Z;
            this.Width = Width;
            this.Height = Height;
            this.LeftChild = null;
            this.RightChild = null;
        }

        public float GetRatio()
        {
            return (X * 1f) / (Z * 1f);
        }

        public Vector3 GetCenter()
        { //return array
            int MiddleX = X + Mathf.FloorToInt(Width / 2f);
            int MiddleZ = Z + Mathf.FloorToInt(Height / 2f);
            Vector3 Result = new Vector3(MiddleX, 0, MiddleZ);
            return Result;
        }

        public List<Vector3> GetLineChild()
        {
            if (this.LeftChild == null) return null;
            Vector3 Left, Right;
            Left = this.LeftChild.GetCenter();
            Right = this.RightChild.GetCenter();
            List<Vector3> Result = new List<Vector3>();
            Result.Add(Left);
            Result.Add(Right);
            return Result;
        }

        public void SetChild(Node LeftChild, Node RightChild)
        {
            this.LeftChild = LeftChild;
            this.RightChild = RightChild;
        }

        public void Split(int Iteration)
        {
            Node Left, Right;
            int NewWidth;
            int NewHeight;
            do
            {
                NewWidth = Width;
                NewHeight = Height;
                // randomize vert / hori
                RandomFloat = UnityEngine.Random.Range(0f, 1f);
                RandomInt = Mathf.RoundToInt(RandomFloat);
                if (RandomInt == 0)
                { 
                    //vert
                    NewWidth = UnityEngine.Random.Range(1, Width);
                    Left = new Node(X, Z, NewWidth, Height);
                    Right = new Node(X + NewWidth, Z, Width - NewWidth, Height);
                }
                else
                { 
                    //hori
                    NewHeight = UnityEngine.Random.Range(1, Height);
                    Left = new Node(X, Z, Width, NewHeight);
                    Right = new Node(X, Z + NewHeight, Width, Height - NewHeight);
                }
            } while (!CheckRatio(Width, Height, NewWidth, NewHeight));
            this.LeftChild = Left;
            this.RightChild = Right;
            Iteration--;
            if (Iteration > 0)
            {
                this.LeftChild.Split(Iteration);
                this.RightChild.Split(Iteration);
            }
            return;
        }

        public void MapToField()
        {
            List<Vector3> Line = GetLineChild();
            if (Line != null)
            {
                int X1 = Mathf.RoundToInt(Line[0].x);
                int Z1 = Mathf.RoundToInt(Line[0].z);
                int X2 = Mathf.RoundToInt(Line[1].x);
                int Z2 = Mathf.RoundToInt(Line[1].z);
                for (int i = X1; i <= X2; i++)
                {
                    for (int k = Z1; k <= Z2; k++)
                    {
                        BSPMaze[i, k] = false;
                    }
                }
                this.RightChild.MapToField();
                this.LeftChild.MapToField();
            }
        }

        public bool CheckRatio(int Width, int Height, int NewWidth, int NewHeight)
        {
            if (Width == NewWidth)
            {
                if (
                    NewHeight * 1f / Width < MaxRatio || (Height - NewHeight) * 1f / Width < MaxRatio
                ) return false;
            }
            else
            {
                if (
                    NewWidth * 1f / Height < MaxRatio || (Width - NewWidth) * 1f / Height < MaxRatio
                ) return false;
            }
            return true;
        }

        public Vector3 GetMostLeftChild()
        {
            if (LeftChild != null) return LeftChild.GetMostLeftChild();
            return GetCenter();
        }

        public Vector3 GetMostRightChild()
        {
            if (RightChild != null) return RightChild.GetMostRightChild();
            return GetCenter();
        }
    }

    public void MovePlayerToStartMaze()
    {
        GameObject.Find("char").GetComponent<CharacterController>().enabled = false;
        GameObject.Find("char").transform.position = new Vector3(StartMaze.x, StartMaze.y, StartMaze.z);
        GameObject.Find("char").GetComponent<CharacterController>().enabled = true;
        //StartMaze;
    }
}