﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour 
{
	public void NewGame()
	{
		EventManager.TriggerEvent(EventManager.Events.NewGame);
	}

}
