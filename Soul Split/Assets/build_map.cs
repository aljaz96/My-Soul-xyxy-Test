using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class build_map : MonoBehaviour {

    // Use this for initialization
    GameObject camera;
    GameObject currentRoom;
    GameObject previousRoom;

    GameObject currentRoom_entrance;
    GameObject currentRoom_exit;
    Door currentRoom_door;

    GameObject previousRoom_entrance;
    GameObject previousRoom_exit;
    Door previousRoom_door;

    public int[,] roomArray;
    public int[,] roomNames;
    public int main_X;
    public int main_Y;
    int roomCounter = 1;

    /*
     * 1-Starting Room
     * 2-Normal Room
     * 3-Boos Room
     * 
     * 
     * 
     * 
     */

	void Start () {
        //previousRoom = GameObject.Find("StartingRoom");
        //setStartingRoomPosition();
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
        camera = GameObject.Find("Main Camera");
        MakeMap();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void MakeMap()
    {
        roomArray = new int[30, 30];
        roomNames = new int[30, 30];
        for (int i = 0; i < 30; i++)
        {
            for (int j = 0; j < 30; j++)
            {
                roomArray[i, j] = 0;
                roomNames[i, j] = 0;
            }
        }
        int x = main_X = Random.Range(10, 20);
        int y = main_Y = Random.Range(10, 20);
        roomArray[x, y] = 1;
        roomNames[x, y] = -1;
        previousRoom = Instantiate(Resources.Load("Prefabs/Rooms/StartingRoom", typeof(GameObject)) as GameObject, new Vector3(x * 50, y * 50, 0), Quaternion.identity);
        previousRoom.name = "StartingRoom";
        camera.transform.position = new Vector3(main_X * 50, main_Y * 50, -10);

        while (roomCounter != 15)
        {
            //  1 -> 10 = up, 11 -> 20 = down, 21 -> 30 = left, 31 -> 40 = right, 41 -> 45 = reset
            
            int decision = Random.Range(1, 45);
            if (decision >= 41 || x < 0 || x > 29 || y < 0 || y > 29)
            {
                previousRoom = GameObject.Find("StartingRoom");
                x = main_X;
                y = main_Y;
            }
            //IF NO ROOM UP, MAKE ROOM UPWARDS FROM CURRENT ROOM, ELSE MOVE UP
            else if (decision >= 1 && decision <= 10)
            {
                if (roomArray[x, y + 1] == 0 && y + 1 != 30)
                {
                    roomArray[x, y + 1] = 2;
                    roomNames[x, y + 1] = roomCounter;
                    y++;
                    currentRoom = Instantiate(Resources.Load("Prefabs/Rooms/Room", typeof(GameObject)) as GameObject, new Vector3(x * 50, y * 50, 0), Quaternion.identity);
                    currentRoom.name = "Room" + roomCounter;
                    roomCounter++;
                    //Instantiate(currentRoom, new Vector3(10, 0, 0), Quaternion.identity);
                    previousRoom_entrance = previousRoom.transform.Find("TopEntrance").gameObject;
                    previousRoom_exit = previousRoom.transform.Find("TopDoor").gameObject;
                    previousRoom_door = previousRoom_exit.GetComponent<Door>();
                    currentRoom_entrance = currentRoom.transform.Find("BottomEntrance").gameObject;
                    currentRoom_exit = currentRoom.transform.Find("BottomDoor").gameObject;
                    currentRoom_door = currentRoom_exit.GetComponent<Door>();
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
                else
                {
                    if (roomArray[x, y + 1] == 1) {
                        previousRoom = GameObject.Find("StartingRoom");
                    }
                    else if (roomArray[x, y + 1] == 2)
                    {
                        previousRoom = GameObject.Find("Room" + roomNames[x, y + 1]);
                    }
                    y++;                   
                }
            }
            //IF NO ROOM DOWN, MAKE ROOM DOWNWARDS FROM CURRENT ROOM, ELSE MOVE DOWN
            else if (decision >= 11 && decision <= 20)
            {
                if (roomArray[x, y - 1] == 0 && y - 1 != -1)
                {
                    roomArray[x, y - 1] = 2;
                    roomNames[x, y - 1] = roomCounter;
                    y--;
                    currentRoom = Instantiate(Resources.Load("Prefabs/Rooms/Room", typeof(GameObject)) as GameObject, new Vector3(x * 50, y * 50, 0), Quaternion.identity);
                    currentRoom.name = "Room" + roomCounter;
                    roomCounter++;
                    //Instantiate(currentRoom, new Vector3(10, 0, 0), Quaternion.identity);
                    previousRoom_entrance = previousRoom.transform.Find("BottomEntrance").gameObject;
                    previousRoom_exit = previousRoom.transform.Find("BottomDoor").gameObject;
                    previousRoom_door = previousRoom_exit.GetComponent<Door>();
                    currentRoom_entrance = currentRoom.transform.Find("TopEntrance").gameObject;
                    currentRoom_exit = currentRoom.transform.Find("TopDoor").gameObject;
                    currentRoom_door = currentRoom_exit.GetComponent<Door>();
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
                else
                {
                    if (roomArray[x, y - 1] == 1)
                    {
                        previousRoom = GameObject.Find("StartingRoom");
                    }
                    else if (roomArray[x, y - 1] == 2)
                    {
                        previousRoom = GameObject.Find("Room" + roomNames[x, y - 1]);
                    }
                    y--;
                }
            }
            //IF NO ROOM LEFT, MAKE ROOM LEFT FROM CURRENT ROOM, ELSE MOVE LEFT
            else if (decision >= 21 && decision <= 30)
            {
                if (roomArray[x - 1, y] == 0 && x - 1 != -1)
                {
                    roomArray[x - 1, y] = 2;
                    roomNames[x - 1, y] = roomCounter;
                    x--;
                    currentRoom = Instantiate(Resources.Load("Prefabs/Rooms/Room", typeof(GameObject)) as GameObject, new Vector3(x * 50, y * 50, 0), Quaternion.identity);
                    currentRoom.name = "Room" + roomCounter;
                    roomCounter++;
                    //Instantiate(currentRoom, new Vector3(10, 0, 0), Quaternion.identity);
                    previousRoom_entrance = previousRoom.transform.Find("LeftEntrance").gameObject;
                    previousRoom_exit = previousRoom.transform.Find("LeftDoor").gameObject;
                    previousRoom_door = previousRoom_exit.GetComponent<Door>();
                    currentRoom_entrance = currentRoom.transform.Find("RightEntrance").gameObject;
                    currentRoom_exit = currentRoom.transform.Find("RightDoor").gameObject;
                    currentRoom_door = currentRoom_exit.GetComponent<Door>();
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
                else
                {
                    if (roomArray[x - 1, y] == 1)
                    {
                        previousRoom = GameObject.Find("StartingRoom");
                    }
                    else if (roomArray[x - 1, y] == 2)
                    {
                        previousRoom = GameObject.Find("Room" + roomNames[x - 1, y]);
                    }
                    x--;
                }
            }
            //IF NO ROOM RIGHT, MAKE ROOM RIGHT FROM CURRENT ROOM, ELSE MORE RIGHT
            else if (decision >= 31 && decision <= 40)
            {
                if (roomArray[x + 1, y] == 0 && x + 1 != 30)
                {
                    roomArray[x + 1, y] = 2;
                    roomNames[x + 1, y] = roomCounter;
                    x++;
                    currentRoom = Instantiate(Resources.Load("Prefabs/Rooms/Room", typeof(GameObject)) as GameObject, new Vector3(x * 50, y * 50, 0), Quaternion.identity);
                    currentRoom.name = "Room" + roomCounter;
                    roomCounter++;
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

                    previousRoom = currentRoom;
                }
                else
                {
                    if (roomArray[x + 1, y] == 1)
                    {
                        previousRoom = GameObject.Find("StartingRoom");
                    }
                    else if (roomArray[x + 1, y] == 2)
                    {
                        previousRoom = GameObject.Find("Room" + roomNames[x + 1, y]);
                    }
                    x++;
                }
            }
        }

    }

    void setStartingRoomPosition()
    {
        previousRoom.transform.position = new Vector3(main_X * 50, main_Y * 50, 0);
        /*previousRoom_exit = previousRoom.transform.Find("RightDoor").gameObject;
        previousRoom_door = previousRoom_exit.GetComponent<Door>();
        previousRoom_door.getProperPosition();
        previousRoom_exit = previousRoom.transform.Find("LeftDoor").gameObject;
        previousRoom_door = previousRoom_exit.GetComponent<Door>();
        previousRoom_door.getProperPosition();
        previousRoom_exit = previousRoom.transform.Find("TopDoor").gameObject;
        previousRoom_door = previousRoom_exit.GetComponent<Door>();
        previousRoom_door.getProperPosition();
        previousRoom_exit = previousRoom.transform.Find("BottomDoor").gameObject;
        previousRoom_door = previousRoom_exit.GetComponent<Door>();
        previousRoom_door.getProperPosition();*/

    }
}
