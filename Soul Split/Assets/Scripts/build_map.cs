using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class build_map : MonoBehaviour
{

    // Use this for initialization
    GameObject camera;
    GameObject currentRoom;
    GameObject previousRoom;
    GameObject bossPreRoom;
    GameObject bossRoom;

    GameObject currentRoom_entrance;
    GameObject currentRoom_exit;
    Door currentRoom_door;

    GameObject previousRoom_entrance;
    GameObject previousRoom_exit;
    Door previousRoom_door;
    Vector2 bossPreRoomLocation;
    public int min_gen_room;
    public int max_gen_room;
    public int numberOfRooms = 16;
    public int[,] roomArray;
    int[,] roomNames;
    int[,] roomDistance;
    int main_X;
    int main_Y;
    int distance = 0;
    public int limit = 220;
    int roomCounter = 1;
    //AstarPath astar;
    List<int> roomsX;
    List<int> roomsY;
    public string path = "Prefabs/Rooms/Stage2/";

    public int range = 40;
    /* 0-Empty Space
     * 1-Starting Room
     * 2-Normal Room
     * 3-Boss Room
     * 5-Treasure Room
     */

    void Start()
    {
        camera = GameObject.Find("Main Camera");
        roomsX = new List<int>();
        roomsY = new List<int>();
        MakeMap();
        //astar.Scan();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MakeMap()
    {
        roomArray = new int[50, 50];
        roomNames = new int[50, 50];
        roomDistance = new int[50, 50];
        for (int i = 0; i < 50; i++)
        {
            for (int j = 0; j < 50; j++)
            {
                roomArray[i, j] = 0;
                roomNames[i, j] = 0;
                roomDistance[i, j] = -1;
            }
        }
        int x = main_X = Random.Range(15, 35);
        int y = main_Y = Random.Range(15, 35);
        roomArray[x, y] = 1;
        roomNames[x, y] = -1;
        roomDistance[x, y] = distance;
        //Make starting room
        previousRoom = Instantiate(Resources.Load(path + "StartingRoom", typeof(GameObject)) as GameObject, new Vector3(x * range, y * range, 0), Quaternion.identity);
        previousRoom.name = "StartingRoom";
        camera.transform.position = new Vector3(x * range, y * range, -10);


        while (roomCounter != numberOfRooms)
        {
            //  1 -> 50 = up, 51 -> 100 = down, 101 -> 150 = left, 151 -> 200 = right, 201 -> limit = reset

            int decision = Random.Range(1, limit);
            int roomNumber = Random.Range(min_gen_room, max_gen_room + 1);
            if (decision >= 201 || x < 0 || x > 49 || y < 0 || y > 49)
            {
                previousRoom = GameObject.Find("StartingRoom");
                distance = 0;
                x = main_X;
                y = main_Y;
                if (limit > 200)
                {
                    limit--;
                }
            }
            //IF NO ROOM UP, MAKE ROOM UPWARDS FROM CURRENT ROOM, ELSE MOVE UP
            else if (decision >= 1 && decision <= 50)
            {
                if (roomArray[x, y + 1] == 0 && y + 1 != 50)
                {
                    distance++;
                    roomArray[x, y + 1] = 2;
                    roomNames[x, y + 1] = roomCounter;
                    roomsX.Add(x);
                    roomsY.Add(y+1);
                    roomDistance[x, y + 1] = distance;
                    y++;
                    currentRoom = Instantiate(Resources.Load(path + "Room" + roomNumber, typeof(GameObject)) as GameObject, new Vector3(x * range, y * range, 0), Quaternion.identity);
                    currentRoom.name = "Room" + roomCounter;
                    roomCounter++;
                    removeWall("Top", previousRoom);
                    removeWall("Bottom", currentRoom);
                    //Instantiate(currentRoom, new Vector3(10, 0, 0), Quaternion.identity);

                    setDoors("Top", "Bottom");
                }
                else
                {
                    if (roomArray[x, y + 1] == 1)
                    {
                        previousRoom = GameObject.Find("StartingRoom");
                        distance = 0;
                    }
                    else if (roomArray[x, y + 1] == 2)
                    {
                        previousRoom = GameObject.Find("Room" + roomNames[x, y + 1]);
                        distance++;
                    }
                    y++;
                }
            }
            //IF NO ROOM DOWN, MAKE ROOM DOWNWARDS FROM CURRENT ROOM, ELSE MOVE DOWN
            else if (decision >= 51 && decision <= 100)
            {
                if (roomArray[x, y - 1] == 0 && y - 1 != -1)
                {
                    distance++;
                    roomArray[x, y - 1] = 2;
                    roomNames[x, y - 1] = roomCounter;
                    roomsX.Add(x);
                    roomsY.Add(y-1);
                    roomDistance[x, y - 1] = distance;
                    y--;
                    currentRoom = Instantiate(Resources.Load(path + "Room" + roomNumber, typeof(GameObject)) as GameObject, new Vector3(x * range, y * range, 0), Quaternion.identity);
                    currentRoom.name = "Room" + roomCounter;
                    roomCounter++;
                    removeWall("Bottom", previousRoom);
                    removeWall("Top", currentRoom);
                    //Instantiate(currentRoom, new Vector3(10, 0, 0), Quaternion.identity);
                    setDoors("Bottom", "Top");
                }
                else
                {
                    if (roomArray[x, y - 1] == 1)
                    {
                        previousRoom = GameObject.Find("StartingRoom");
                        distance = 0;
                    }
                    else if (roomArray[x, y - 1] == 2)
                    {
                        previousRoom = GameObject.Find("Room" + roomNames[x, y - 1]);
                        distance++;
                    }
                    y--;
                }
            }
            //IF NO ROOM LEFT, MAKE ROOM LEFT FROM CURRENT ROOM, ELSE MOVE LEFT
            else if (decision >= 101 && decision <= 150)
            {
                if (roomArray[x - 1, y] == 0 && x - 1 != -1)
                {
                    distance++;
                    roomArray[x - 1, y] = 2;
                    roomNames[x - 1, y] = roomCounter;
                    roomsX.Add(x-1);
                    roomsY.Add(y);
                    roomDistance[x - 1, y] = distance;
                    x--;
                    currentRoom = Instantiate(Resources.Load(path + "Room" + roomNumber, typeof(GameObject)) as GameObject, new Vector3(x * range, y * range, 0), Quaternion.identity);
                    currentRoom.name = "Room" + roomCounter;
                    roomCounter++;
                    //Instantiate(currentRoom, new Vector3(10, 0, 0), Quaternion.identity);
                    setDoors("Left", "Right");
                }
                else
                {
                    if (roomArray[x - 1, y] == 1)
                    {
                        previousRoom = GameObject.Find("StartingRoom");
                        distance = 0;
                    }
                    else if (roomArray[x - 1, y] == 2)
                    {
                        previousRoom = GameObject.Find("Room" + roomNames[x - 1, y]);
                        distance++;
                    }
                    x--;
                }
            }
            //IF NO ROOM RIGHT, MAKE ROOM RIGHT FROM CURRENT ROOM, ELSE MORE RIGHT
            else if (decision >= 151 && decision <= 200)
            {
                if (roomArray[x + 1, y] == 0 && x + 1 != 50)
                {
                    distance++;
                    roomArray[x + 1, y] = 2;
                    roomNames[x + 1, y] = roomCounter;
                    roomsX.Add(x+1);
                    roomsY.Add(y);
                    roomDistance[x + 1, y] = distance;
                    x++;
                    currentRoom = Instantiate(Resources.Load(path + "Room" + roomNumber, typeof(GameObject)) as GameObject, new Vector3(x * range, y * range, 0), Quaternion.identity);
                    currentRoom.name = "Room" + roomCounter;
                    roomCounter++;
                    //Instantiate(currentRoom, new Vector3(10, 0, 0), Quaternion.identity);
                    setDoors("Right", "Left");
                }
                else
                {
                    if (roomArray[x + 1, y] == 1)
                    {
                        previousRoom = GameObject.Find("StartingRoom");
                        distance = 0;
                    }
                    else if (roomArray[x + 1, y] == 2)
                    {
                        previousRoom = GameObject.Find("Room" + roomNames[x + 1, y]);
                        distance++;
                    }
                    x++;
                }
            }
        }
        int max = 0;
        //Make boss room
        bossPreRoomLocation = new Vector2();
        for (int i = 0; i < 50; i++)
        {
            for (int j = 0; j < 50; j++)
            {
                if (roomDistance[i, j] > max)
                {
                    max = roomDistance[i, j];
                    bossPreRoomLocation.x = i;
                    bossPreRoomLocation.y = j;
                }
            }
        }
        bossPreRoom = GameObject.Find("Room" + roomNames[(int)bossPreRoomLocation.x, (int)bossPreRoomLocation.y]);
        while (true)
        {
            int decision = Random.Range(1, 4);
            if (decision == 1)
            {
                if (roomArray[(int)bossPreRoomLocation.x, (int)bossPreRoomLocation.y + 1] == 0)
                {
                    roomArray[(int)bossPreRoomLocation.x, (int)bossPreRoomLocation.y + 1] = 3;
                    roomNames[(int)bossPreRoomLocation.x, (int)bossPreRoomLocation.y + 1] = -3;
                    bossPreRoomLocation.y++;
                    removeWall("Top", bossPreRoom);
                    setBossRoom("Top", "Bottom");
                    break;
                }
            }
            else if (decision == 2)
            {
                if (roomArray[(int)bossPreRoomLocation.x, (int)bossPreRoomLocation.y - 1] == 0)
                {
                    roomArray[(int)bossPreRoomLocation.x, (int)bossPreRoomLocation.y - 1] = 3;
                    roomNames[(int)bossPreRoomLocation.x, (int)bossPreRoomLocation.y + 1] = -3;
                    bossPreRoomLocation.y--;
                    removeWall("Bottom", bossPreRoom);
                    setBossRoom("Bottom", "Top");
                    break;
                }
            }
            else if (decision == 3)
            {
                if (roomArray[(int)bossPreRoomLocation.x - 1, (int)bossPreRoomLocation.y] == 0)
                {
                    roomArray[(int)bossPreRoomLocation.x - 1, (int)bossPreRoomLocation.y] = 3;
                    roomNames[(int)bossPreRoomLocation.x, (int)bossPreRoomLocation.y + 1] = -3;
                    bossPreRoomLocation.x--;
                    setBossRoom("Left", "Right");
                    break;
                }
            }
            else
            {
                if (roomArray[(int)bossPreRoomLocation.x + 1, (int)bossPreRoomLocation.y] == 0)
                {
                    roomArray[(int)bossPreRoomLocation.x + 1, (int)bossPreRoomLocation.y] = 3;
                    roomNames[(int)bossPreRoomLocation.x, (int)bossPreRoomLocation.y + 1] = -3;
                    bossPreRoomLocation.x++;
                    setBossRoom("Right", "Left");
                    break;
                }
            }
        }
        //Make treasure room
        
        bool treasure = false;
        
        while (!treasure)
        {
            int cx = roomsX[Random.Range(0, roomsX.Count)];
            int cy = roomsY[Random.Range(0, roomsY.Count)];
            for (int i = 0; i < 5; i++)
            {
                int rx = Random.Range(-1, 3);
                int ry = 0;
                if (rx == 0 || rx == 2)
                {
                    rx = 0;
                    ry = Random.Range(0, 2);
                    if (ry == 0)
                    {
                        ry = -1;
                    }
                }
                if (roomArray[cx + rx, cy + ry] == 0)
                {
                    roomArray[cx + rx, cy + ry] = 5;
                    roomNames[cx + rx, cy + ry] = -5;
                    roomDistance[cx + rx, cy + ry] = -1;
                    currentRoom = Instantiate(Resources.Load(path + "TreasureRoom", typeof(GameObject)) as GameObject, new Vector3((cx + rx) * range, (cy + ry) * range, 0), Quaternion.identity);
                    currentRoom.name = "TreasureRoom";
                    previousRoom = GameObject.Find("Room" + roomNames[cx, cy]).gameObject;
                    if (rx == -1)
                    {
                        setDoors("Left", "Right");
                        treasure = true;
                        break;
                    }
                    else if (rx == 1)
                    {
                        setDoors("Right", "Left");
                        treasure = true;
                        break;
                    }
                    else if (ry == -1)
                    {
                        removeWall("Bottom", previousRoom);  //correct
                        removeWall("Top", currentRoom);
                        setDoors("Bottom", "Top");
                        treasure = true;
                        break;
                    }
                    else if (ry == 1)
                    {
                        removeWall("Top", previousRoom);   //correct
                        removeWall("Bottom", currentRoom);
                        setDoors("Top", "Bottom");
                        treasure = true;
                        break;
                    }
                }                        
            }            
        }
    }

    void removeWall(string side, GameObject room)
    {
        if (side == "Bottom" || side == "Top")
        {
            GameObject wall = room.transform.Find("No" + side + "Door").gameObject;
            wall.SetActive(false);
        }
    }

    void setDoors(string previous, string current)
    {
        previousRoom_entrance = previousRoom.transform.Find(previous + "Entrance").gameObject;
        previousRoom_exit = previousRoom.transform.Find(previous + "Door").gameObject;
        previousRoom_door = previousRoom_exit.GetComponent<Door>();
        previousRoom_exit.tag = "ExitDoor";
        currentRoom_entrance = currentRoom.transform.Find(current + "Entrance").gameObject;
        currentRoom_exit = currentRoom.transform.Find(current + "Door").gameObject;
        currentRoom_door = currentRoom_exit.GetComponent<Door>();
        currentRoom_exit.tag = "EnterDoor";
        currentRoom_door.exitPoint = previousRoom_entrance;
        previousRoom_door.exitPoint = currentRoom_entrance;

        currentRoom_exit.SetActive(true);
        currentRoom_entrance.SetActive(true);
        previousRoom_exit.SetActive(true);
        previousRoom_entrance.SetActive(true);
        currentRoom_door.getProperPosition();
        previousRoom_door.getProperPosition();
        previousRoom = currentRoom;
    }

    void setBossRoom(string previous, string current)
    {
        int roomNumber = 6;//= Random.Range(min_gen_room, max_gen_room+1);
        bossRoom = Instantiate(Resources.Load(path + "Bossroom", typeof(GameObject)) as GameObject, new Vector3(bossPreRoomLocation.x * range, bossPreRoomLocation.y * range, 0), Quaternion.identity);
        bossRoom.name = "BossRoom";
        previousRoom_entrance = bossPreRoom.transform.Find(previous + "Entrance").gameObject;
        previousRoom_exit = bossPreRoom.transform.Find(previous + "Door").gameObject;
        previousRoom_door = previousRoom_exit.GetComponent<Door>();
        previousRoom_exit.tag = "ExitDoor";
        currentRoom_entrance = bossRoom.transform.Find(current + "Entrance").gameObject;
        currentRoom_exit = bossRoom.transform.Find(current + "Door").gameObject;
        currentRoom_door = currentRoom_exit.GetComponent<Door>();
        currentRoom_exit.tag = "EnterDoor";
        currentRoom_door.exitPoint = previousRoom_entrance;
        previousRoom_door.exitPoint = currentRoom_entrance;
        currentRoom_exit.SetActive(true);
        currentRoom_entrance.SetActive(true);
        previousRoom_exit.SetActive(true);
        previousRoom_entrance.SetActive(true);
        currentRoom_door.getProperPosition();
        previousRoom_door.getProperPosition();
        removeWall(current, bossRoom);
    }

    void setTreasureRoom(string previous, string current)
    {
        previousRoom_entrance = bossPreRoom.transform.Find(previous + "Entrance").gameObject;
        previousRoom_exit = bossPreRoom.transform.Find(previous + "Door").gameObject;
        previousRoom_door = previousRoom_exit.GetComponent<Door>();
        previousRoom_exit.tag = "ExitDoor";
        currentRoom_entrance = bossRoom.transform.Find(current + "Entrance").gameObject;
        currentRoom_exit = bossRoom.transform.Find(current + "Door").gameObject;
        currentRoom_door = currentRoom_exit.GetComponent<Door>();
        currentRoom_exit.tag = "EnterDoor";
        currentRoom_door.exitPoint = previousRoom_entrance;
        previousRoom_door.exitPoint = currentRoom_entrance;
        currentRoom_exit.SetActive(true);
        currentRoom_entrance.SetActive(true);
        previousRoom_exit.SetActive(true);
        previousRoom_entrance.SetActive(true);
        currentRoom_door.getProperPosition();
        previousRoom_door.getProperPosition();
        removeWall(current, currentRoom);
    }

    //previousRoom = GameObject.Find("StartingRoom");
    /*
    currentRoom = Instantiate(Resources.Load("Prefabs/Rooms/Room", typeof(GameObject)) as GameObject, new Vector3(50, 0, 0), Quaternion.identity);
    currentRoom.name = "Room1";       
    //Instantiate(currentRoom, new Vector3(10, 0, 0), Quaternion.identity);
    previousRoom_entrance = previousRoom.transform.Find("RightEntrance").gameObject;
    previousRoom_exit = previousRoom.transform.Find("RightDoor").gameObject;
    previousRoom_door = previousRoom_exit.GetComponent<Door>();
    currentRoom_entrance = currentRoom.transform.Find("LeftEntrance").gameObject;
    currentRoom_exit = currentRoom.transform.Find("LeftDoor").gameObject;
    currentRoom_door = currentRoom_exit.GetComponent<Door>();
    currentRoom_door.exitPoint = previousRoom_entrance;
    previousRoom_door.exitPoint = currentRoom_entrance;

    currentRoom_exit.SetActive(true);
    currentRoom_entrance.SetActive(true);
    previousRoom_exit.SetActive(true);
    previousRoom_entrance.SetActive(true);
    currentRoom_door.getProperPosition();
    previousRoom_door.getProperPosition();
    */

}
