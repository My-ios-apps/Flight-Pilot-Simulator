using UnityEngine;
using System.Collections;

public static class GameConstant  {

    public static string sceneToLoad = "MainMenu";
	public static bool isTruckButtonClicked = false;
	public static int totalPoliceMen = 0;
	public static int policeCountOnPoint= 0;
	public static float PlayerHitPower= 25f;
	public static bool isCareerMode = true;
	public static bool isCityMode;

	public static int [] numberOfKills = {2,2,3,4,5,6};
	public static int totalKills = 0;
	public static int currentLevel = 5;
	public static int currentPlayer = 1;
	public static int currentPlane = 1;
	public static int count = 0;
	public static int total= 0 ;
	public static string gameFailReason="Plane Crashed";
	public static string [] objective = {
		"Level 1 TakeOff and Collect Atleast Five CheckPoints",
		"Level 2 TakeOff and Landing on the nearest Island with atleast 6 CheckPoints",
		"Level 3 TakeOff and Collect Stars from Brand New Air Balloons, try not to hit the side of Ballons",
		"Level 4 Collect Exactly 2 Moving Ballons from the air",
		"Level 5 Takeoff and Landing with the Virgina island, Collect atleast 8 Checkpoints",
		"Level 6 Let's takeoff the plane and collect 10 moving stars from the pools. Don't try to hit to the pool",
		"Level 7 It's time to trace and collect 4 Hot air balloon from the gaint sky",
		"Level 8 Let's TakeOff and Collect 10 moving stars from Brand New Air Balloons, try not to hit the side of Ballons",
		"Level 9 Let's takeoff the plane and collect 10 moving stars from the pools, land the plane safely",
		"Level 10 Let's start our final task in this mode, to pass this ultimate target you need to collect 6 Hot air balloon from the sky",
	};

	public static string [] cityObjective = {
		"Level 1 TakeOff and Collect Atleast Five CheckPoints",
		"Level 2 TakeOff and Collect Stars from Brand New Air Balloons, try not to hit the side of Ballons",
		"Level 3 Let's takeoff and Land the plane and collect 10 stars from the pools. Don't try to hit to the pool",
		"Level 4 Let's takeoff and Land the plane and collect 7 moving stars from the pools. Don't try to hit to the pool",
		"Level 5 Takeoff and Collect 12 Balloons from the city",
		"Level 6 It's time to trace and collect 4 moving Hot air balloon from the gaint sky",
		"Level 7 It's time to collect 6 hot air Balloons that were placed at the top of the building",
		"Level 8 Let's takeoff and Land the plane and collect 7 moving stars from the pools. Don't try to hit to the pool",
		"Level 9 It's time to trace and collect 6 moving Hot air balloon from the gaint sky",
		"Level 10 Let's start our final task in this mode, you have to collect balloon that were placed in a city and you have to land safely",
	};

	public static float [] timeForest = {120,210,210,360,210,210,360,360,360,360};
	public static float [] cityForest = {120,300,210,360,180,210,180,360,300,210};

	public static string  getObjective(){
		if (isCityMode) {
			return cityObjective [currentLevel - 1];
		} else {
			return objective [currentLevel - 1];
		}
	}

	public static float getTime(){
		if (isCityMode) {
			return cityForest [currentLevel - 1];
		} else {
			return timeForest [currentLevel - 1];
		}
	}


	public static string buycoins1000="buycoins1000"; // for 1$
	public static string buycoins2500= "buycoins2500"; // for 2$
	public static string buycoins5000= "buycoins5000"; // for 4$
	public static string buycoins10000 ="buycoins10000"; // for 7$
	public static string unlockallplanes= "unlockallplanes"; // for 10$
	public static string unlockalllevelsofisland="unlockalllevelsofisland"; // for 2$
	public static string unlockalllevelsofcity="unlockalllevelsofcity"; // for 2$

	//Impossible BMX Bicycle Freestyle Stunts Racing
}
