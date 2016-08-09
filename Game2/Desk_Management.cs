using UnityEngine;
using System.Collections;

public class Desk_Management {
	static Desk_Slot[] desk_list;
	static int desk_list_length;

	public static bool Desk_Management_Init()
	{
		desk_list_length = 3;
		desk_list = new Desk_Slot[desk_list_length];

		for(int i=0;i<desk_list_length;i++)
		{
			desk_list[i] = new Desk_Slot(new Vector3(5*(i-1), 0.5f, 0));
		}

		return true;
	}

	public static int OrderDesk(string desk_name)
	{
		int num = -1;

		for (int i=0;i<desk_list_length;i++)
		{
			if(desk_list[i].GetInUse() == false)
			{
				num = i;
				break;
			}
		}

		if(num == -1)
			return 1;

		if(desk_list[num].UseDeskSlot(desk_name) == false)
			return -1;
		else
			return 0;
	}
}
