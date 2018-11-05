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
    public int numberOfRooms = 16;
    int[,] roomArray;
    int[,] roomNames;
    int[,] roomDistance;
    int main_X;
    int main_Y;
    int distance = 0;
    public int limit = 220;
    int roomCounter = 1;
    //AstarPath astar;

    public int range = 10;
    /*
     * 1-Starting Room
     * 2-Normal Room
     * 3-Boos Room
     * 
     */

    void Start()
    {
        camera = GameObject.Find("Main Camera");
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
        int x = main_X = Random.Range(9, 40);
        int y = main_Y = Random.Range(9, 40);
        int room_X = 0;
        int room_Y = 0;
        roomArray[x, y] = 1;
        roomNames[x, y] = -1;
        roomDistance[x, y] = distance;
        //Make starting room
        previousRoom = Instantiate(Resources.Load("Prefabs/Rooms/StartingRoom2", typeof(GameObject)) as GameObject, new Vector3(room_X * range, room_Y * range, 0), Quaternion.identity);
        previousRoom.name = "StartingRoom";
        camera.transform.position = new Vector3(0* range, 0 * range, -10);
        //GameObject makePathFinding = Instantiate(Resources.Load("Prefabs/Room_components/A_", typeof(GameObject)) as GameObject, new Vector3(room_X * range, room_Y * range, 0), Quaternion.identity);
        //astar = makePathFinding.GetComponent<AstarPath>();
        

        while (roomCounter != numberOfRooms)
        {
            //  1 -> 50 = up, 51 -> 100 = down, 101 -> 150 = left, 151 -> 200 = right, 201 -> limit = reset

            int decision = Random.Range(1, limit);
            int roomNumber = Random.Range(1, 5);
            if (decision >= 201 || x < 0 || x > 49 || y < 0 || y > 49)
            {
                previousRoom = GameObject.Find("StartingRoom");
                distance = 0;
                x = main_X;
                y = main_Y;
                room_X = 0;
                room_Y = 0;
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
                    roomDistance[x, y + 1] = distance;
                    y++;
                    room_Y++;
                    currentRoom = Instantiate(Resources.Load("Prefabs/Rooms/Room" + roomNumber, typeof(GameObject)) as GameObject, new Vector3(room_X * range, room_Y * range, 0), Quaternion.identity);
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
                    room_Y++;
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
                    roomDistance[x, y - 1] = distance;
                    y--;
                    room_Y--;
                    currentRoom = Instantiate(Resources.Load("Prefabs/Rooms/Room" + roomNumber, typeof(GameObject)) as GameObject, new Vector3(room_X * range, room_Y * range, 0), Quaternion.identity);
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
                    room_Y--;
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
                    roomDistance[x - 1, y] = distance;
                    x--;
                    room_X--;
                    currentRoom = Instantiate(Resources.Load("Prefabs/Rooms/Room" + roomNumber, typeof(GameObject)) as GameObject, new Vector3(room_X * range, room_Y * range, 0), Quaternion.identity);
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
                    room_X--;
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
                    roomDistance[x + 1, y] = distance;
                    x++;
                    room_X++;
                    currentRoom = Instantiate(Resources.Load("Prefabs/Rooms/Room" + roomNumber, typeof(GameObject)) as GameObject, new Vector3(room_X * range, room_Y * range, 0), Quaternion.identity);
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
                    room_X++;
                }
            }
        }
        int max = 0;
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
                    bossPreRoomLocation.y++;
                    removeWall("Top", bossPreRoom);
                    removeWall("Bottom", currentRoom);
                    setBossRoom("Top", "Bottom");
                    break;
                }
            }
            else if (decision == 2)
            {
                if (roomArray[(int)bossPreRoomLocation.x, (int)bossPreRoomLocation.y - 1] == 0)
                {
                    bossPreRoomLocation.y--;
                    removeWall("Bottom", bossPreRoom);
                    removeWall("Top", currentRoom);
                    setBossRoom("Bottom", "Top");
                    break;
                }
            }
            else if (decision == 3)
            {
                if (roomArray[(int)bossPreRoomLocation.x - 1, (int)bossPreRoomLocation.y] == 0)
                {
                    bossPreRoomLocation.x--;
                    setBossRoom("Left", "Right");
                    break;
                }
            }
            else
            {
                if (roomArray[(int)bossPreRoomLocation.x + 1, (int)bossPreRoomLocation.y] == 0)
                {
                    bossPreRoomLocation.x++;
                    setBossRoom("Right", "Left");
                    break;
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
        bossRoom = Instantiate(Resources.Load("Prefabs/Rooms/Room2", typeof(GameObject)) as GameObject, new Vector3(bossPreRoomLocation.x * range, bossPreRoomLocation.y * range, 0), Quaternion.identity);
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
