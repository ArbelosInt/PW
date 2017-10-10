using UnityEngine;
using System.Collections;

/// ScoringSystem
/// Keeps track of game progress and current score

public class ScoringSystem : MonoBehaviour
{
	//===================================
	//===================================
	//		MODULE VARIABLES
	//===================================
	//===================================

	// enable 'expressive' displays
	private float scoreReductionFactor = 0f;
	
	// hunt stats
	private int huntCount = 0;
	private int huntSuccessCount = 0;
	private int[] huntStatsSelectedPuma;
	private float[] huntStatsPumaHealth;
	
	// meat consumption
	private float meatLimitForLevel;
	private float meatTotalEaten;
	public float meatEatenOverdrive = 1f;

	// health points
	private float[] healthPoints;
	private float[] maxHealthPoints;
	private bool[] carKilledFlags;
	
	// energy usage
	private float expensePerMeterChasing;
	private float expensePerMeterStalking;

	// amounts for last kill
	private float lastKillMeatEaten;
	private float lastKillCaloriesEaten;
	private string lastKillDeerType;
	private float[] lastKillExpenses;

	// kill scoreboard
	private int[] bucksKilled;
	private int[] doesKilled;
	private int[] fawnsKilled;

	// energy scoreboard
	private float[] buckExpenses;
	private float[] buckCalories;
	private float[] doeExpenses;
	private float[] doeCalories;
	private float[] fawnExpenses;
	private float[] fawnCalories;

	// EXTERNAL MODULES
	private GuiManager guiManager;
	private LevelManager levelManager;

	//===================================
	//===================================
	//		INITIALIZATION
	//===================================
	//===================================

    void Start()
    {
		// connect to external modules
		guiManager = GetComponent<GuiManager>();
		levelManager = GetComponent<LevelManager>();

		InitScoringSystem();
	}

    public void InitScoringSystem()
    {
		// hunt stats
		huntCount = 0;
		huntSuccessCount = 0;
		huntStatsSelectedPuma = new int[500];
		huntStatsPumaHealth = new float[500];
		
/*	
		huntStatsSelectedPuma[huntCount] = 2;
		huntStatsPumaHealth[huntCount] = 0.6f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 2;
		huntStatsPumaHealth[huntCount] = 0.7f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 2;
		huntStatsPumaHealth[huntCount] = 0.75f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 2;
		huntStatsPumaHealth[huntCount] = 0.8f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 2;
		huntStatsPumaHealth[huntCount] = 0.4f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 2;
		huntStatsPumaHealth[huntCount] = 0f;
		huntCount++;
		
		
		huntStatsSelectedPuma[huntCount] = 4;
		huntStatsPumaHealth[huntCount] = 0.6f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 4;
		huntStatsPumaHealth[huntCount] = 0.4f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 4;
		huntStatsPumaHealth[huntCount] = 0.3f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 4;
		huntStatsPumaHealth[huntCount] = 0.3f;
		huntCount++;

		huntStatsSelectedPuma[huntCount] = 6;
		huntStatsPumaHealth[huntCount] = 0f;
		huntCount++;
		
		
		huntStatsSelectedPuma[huntCount] = 1;
		huntStatsPumaHealth[huntCount] = 0.3f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 1;
		huntStatsPumaHealth[huntCount] = 0.4f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 1;
		huntStatsPumaHealth[huntCount] = 0.5f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 1;
		huntStatsPumaHealth[huntCount] = 0.8f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 1;
		huntStatsPumaHealth[huntCount] = 0.9f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 1;
		huntStatsPumaHealth[huntCount] = 1f;
		huntCount++;
		
		
		huntStatsSelectedPuma[huntCount] = 6;
		huntStatsPumaHealth[huntCount] = 0f;
		huntCount++;
		
		
		huntStatsSelectedPuma[huntCount] = 5;
		huntStatsPumaHealth[huntCount] = 0.3f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 5;
		huntStatsPumaHealth[huntCount] = 0.4f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 5;
		huntStatsPumaHealth[huntCount] = 0.5f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 5;
		huntStatsPumaHealth[huntCount] = 0.8f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 5;
		huntStatsPumaHealth[huntCount] = 0.9f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 5;
		huntStatsPumaHealth[huntCount] = 0.7f;
		huntCount++;
	
		
		
		huntStatsSelectedPuma[huntCount] = 6;
		huntStatsPumaHealth[huntCount] = 0f;
		huntCount++;
		
		
		huntStatsSelectedPuma[huntCount] = 0;
		huntStatsPumaHealth[huntCount] = 0.3f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 0;
		huntStatsPumaHealth[huntCount] = 0.4f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 0;
		huntStatsPumaHealth[huntCount] = 0.5f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 0;
		huntStatsPumaHealth[huntCount] = 0.8f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 0;
		huntStatsPumaHealth[huntCount] = 0.9f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 0;
		huntStatsPumaHealth[huntCount] = 0.7f;
		huntCount++;


		huntStatsSelectedPuma[huntCount] = 6;
		huntStatsPumaHealth[huntCount] = 0;
		huntCount++;

		
		huntStatsSelectedPuma[huntCount] = 0;
		huntStatsPumaHealth[huntCount] = 0.3f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 0;
		huntStatsPumaHealth[huntCount] = 0.4f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 0;
		huntStatsPumaHealth[huntCount] = 0.5f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 0;
		huntStatsPumaHealth[huntCount] = 0.8f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 0;
		huntStatsPumaHealth[huntCount] = 0.9f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 0;
		huntStatsPumaHealth[huntCount] = 0.7f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 0;
		huntStatsPumaHealth[huntCount] = 0.3f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 0;
		huntStatsPumaHealth[huntCount] = 0.4f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 0;
		huntStatsPumaHealth[huntCount] = 0.5f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 0;
		huntStatsPumaHealth[huntCount] = 0.8f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 0;
		huntStatsPumaHealth[huntCount] = 0.9f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 0;
		huntStatsPumaHealth[huntCount] = 0.7f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 0;
		huntStatsPumaHealth[huntCount] = 0.5f;
		huntCount++;
		huntStatsSelectedPuma[huntCount] = 0;
		huntStatsPumaHealth[huntCount] = 0.7f;
		huntCount++;
*/	
	
		

		// meat consumption
		meatLimitForLevel = 1000f;
		meatTotalEaten = 0f;



		// health points
		float defaultMaxHealth = 225000f;

		//healthPoints = new float[] {defaultMaxHealth * 0.9f, defaultMaxHealth * 0.9f, defaultMaxHealth * 0.9f, defaultMaxHealth * 0.9f, defaultMaxHealth * 0.9f, defaultMaxHealth * 0.9f};
		//healthPoints = new float[] {defaultMaxHealth * 0.7f, defaultMaxHealth * 0.7f, defaultMaxHealth * 0.7f, defaultMaxHealth * 0.7f, defaultMaxHealth * 0.7f, defaultMaxHealth * 0.7f};
		//healthPoints = new float[] {defaultMaxHealth * 0.5f, defaultMaxHealth * 0.5f, defaultMaxHealth * 0.5f, defaultMaxHealth * 0.5f, defaultMaxHealth * 0.5f, defaultMaxHealth * 0.5f};
		//healthPoints = new float[] {defaultMaxHealth * 0.3f, defaultMaxHealth * 0.3f, defaultMaxHealth * 0.3f, defaultMaxHealth * 0.3f, defaultMaxHealth * 0.3f, defaultMaxHealth * 0.3f};
		//healthPoints = new float[] {defaultMaxHealth * 0.1f, defaultMaxHealth * 0.1f, defaultMaxHealth * 0.1f, defaultMaxHealth * 0.1f, defaultMaxHealth * 0.1f, defaultMaxHealth * 0.1f};
		//healthPoints = new float[] {defaultMaxHealth * 0.1f, defaultMaxHealth * 0.01f, defaultMaxHealth * 0.01f, defaultMaxHealth * 0f, defaultMaxHealth * 0f, defaultMaxHealth * 0f};
		//healthPoints = new float[] {defaultMaxHealth * 0.01f, defaultMaxHealth * 0f, defaultMaxHealth * 0f, defaultMaxHealth * 0f, defaultMaxHealth * 0f, defaultMaxHealth * 0f};

		healthPoints = new float[] {defaultMaxHealth * 0.5f, defaultMaxHealth * 0.5f, defaultMaxHealth * 0.5f, defaultMaxHealth * 0.5f, defaultMaxHealth * 0.5f, defaultMaxHealth * 0.5f};

		maxHealthPoints = new float[] {defaultMaxHealth, defaultMaxHealth, defaultMaxHealth, defaultMaxHealth, defaultMaxHealth, defaultMaxHealth};
		carKilledFlags = new bool[] {false, false, false, false, false, false};

		
		
		// energy usage
		expensePerMeterChasing = 110f;
		expensePerMeterStalking = 40f;

		// amounts for last kill
		lastKillMeatEaten = 0f;
		lastKillCaloriesEaten = 0f;
		lastKillDeerType = "None";
		lastKillExpenses = new float[] {0f, 0f, 0f, 0f, 0f, 0f};

		// kill scoreboard
		bucksKilled = new int[] {0, 0, 0, 0, 0, 0};
		doesKilled = new int[] {0, 0, 0, 0, 0, 0};
		fawnsKilled = new int[] {0, 0, 0, 0, 0, 0};
		//bucksKilled = new int[] {2, 5, 0, 0, 1, 0};
		//doesKilled = new int[] {3, 0, 2, 2, 0, 1};
		//fawnsKilled = new int[] {1, 0, 3, 0, 5, 2};

		// energy scoreboard - totals for energy spent and calories eaten; NOTE: index 6 is total for all
		buckExpenses = new float[] {0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f};
		buckCalories = new float[] {0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f};
		doeExpenses = new float[] {0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f};
		doeCalories = new float[] {0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f};
		fawnExpenses = new float[] {0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f};
		fawnCalories = new float[] {0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f};
		//buckExpenses = new float[] {5f, 4f, 6f, 4f, 6f, 3f, 5f};
		//buckCalories = new float[] {4f, 5f, 3f, 6f, 4f, 5f, 3f};
		//doeExpenses = new float[] {4f, 6f, 3f, 4f, 6f, 4f, 5f};
		//doeCalories = new float[] {4f, 5f, 4f, 6f, 4f, 5f, 3f};
		//fawnExpenses = new float[] {5f, 4f, 6f, 3f, 5f, 4f, 5f};
		//fawnCalories = new float[] {4f, 5f, 3f, 5f, 4f, 6f, 3f};
	}
	
	//===========================================
	//===========================================
	//	SCORE UPDATES
	//
	// 	This set of functions is called by the 
	// 	level manager when game events happen
	//	that result in changes in game score 
	//===========================================
	//===========================================

	//------------------------------------------
	// PumaHasWalked(), PumaHasRun()
	// 
	// Called for every movement of the puma
	// when under player control; these are the 
	// main source of puma energy 'expenses'
	//------------------------------------------

	public void PumaHasWalked(int selectedPuma, float distance)
	{
		//distance *= 5f;  // TEMP !!!!!!
		//distance /= 100f;  // TEMP !!!!!!
	
	
		if (selectedPuma == -1)
			return;
		if (distance < 0)
			distance *= -1f;
			
		float newExpense = distance * expensePerMeterStalking;
		lastKillExpenses[selectedPuma] += newExpense;
		healthPoints[selectedPuma] -= newExpense;
		if (healthPoints[selectedPuma] < 0)
			healthPoints[selectedPuma] = 0;
	}
	
	public void PumaHasRun(int selectedPuma, float distance)
	{
		if (selectedPuma == -1)
			return;

		if (distance < 0)
			distance *= -1f;
			
		float newExpense = distance * expensePerMeterChasing;
		lastKillExpenses[selectedPuma] += newExpense;
		healthPoints[selectedPuma] -= newExpense;
		if (healthPoints[selectedPuma] < 0)
			healthPoints[selectedPuma] = 0;
	}

	
	//--------------------------------------------
	// DeerCaught()
	// 
	// Called whenever a puma catches its prey;
	// this is the source of calories for 'health'
	//--------------------------------------------

	public void DeerCaught(int selectedPuma, string deerType)
	{
		float meatEaten;
		float caloriesEaten;
	
		switch (deerType) {
		case "Buck":
			meatEaten = Random.Range(35f, 43f);
			caloriesEaten = meatEaten * Random.Range(900f, 1100f);
			bucksKilled[selectedPuma] += 1;
			buckExpenses[selectedPuma] += lastKillExpenses[selectedPuma];
			buckCalories[selectedPuma] += caloriesEaten;
			buckExpenses[6] += lastKillExpenses[selectedPuma];  // population total
			buckCalories[6] += caloriesEaten;  					// population total
			break;
		
		case "Doe":
			meatEaten = Random.Range(25f, 32f);
			caloriesEaten = meatEaten * Random.Range(900f, 1100f);
			doesKilled[selectedPuma] += 1;
			doeExpenses[selectedPuma] += lastKillExpenses[selectedPuma];
			doeCalories[selectedPuma] += caloriesEaten;
			doeExpenses[6] += lastKillExpenses[selectedPuma];   // population total
			doeCalories[6] += caloriesEaten;  					// population total
			break;
		
		case "Fawn":
			meatEaten = Random.Range(17f, 23f);
			caloriesEaten = meatEaten * Random.Range(900f, 1100f);
			fawnsKilled[selectedPuma] += 1;
			fawnExpenses[selectedPuma] += lastKillExpenses[selectedPuma];
			fawnCalories[selectedPuma] += caloriesEaten;
			fawnExpenses[6] += lastKillExpenses[selectedPuma];  // population total
			fawnCalories[6] += caloriesEaten;  					// population total
			break;
			
		default:
			System.Console.WriteLine("ScoringSystem.DeerCaught got bad deerType: " + deerType);
			return;
		}
		
		meatEaten *= meatEatenOverdrive * (1f / levelManager.difficultyLevel);
		caloriesEaten *= meatEatenOverdrive * (1f / levelManager.difficultyLevel);

		lastKillMeatEaten = meatEaten;
		lastKillCaloriesEaten = caloriesEaten;
		lastKillDeerType = deerType;
		
		meatTotalEaten += meatEaten;
		healthPoints[selectedPuma] += caloriesEaten;
		if (healthPoints[selectedPuma] > maxHealthPoints[selectedPuma])
			maxHealthPoints[selectedPuma] = healthPoints[selectedPuma];
			
		// remember state in 'hunting stats'	
		huntStatsSelectedPuma[huntCount] = selectedPuma;
		huntStatsPumaHealth[huntCount] = GetPumaHealth(selectedPuma);
		huntCount++;
		
		if ((caloriesEaten - lastKillExpenses[selectedPuma]) > 0f)
			huntSuccessCount++;
		else
			huntSuccessCount = 0;
	}

	
	//------------------------------------------
	// PumaBadHunt()
	// 
	// Called when puma exits hunt after prey
	// gets too far or after hitting tree
	//------------------------------------------

	public void PumaBadHunt(int selectedPuma)
	{
		// remember failure in 'hunting stats'	
		huntStatsSelectedPuma[huntCount] = selectedPuma;
		huntStatsPumaHealth[huntCount] = GetPumaHealth(selectedPuma);
		huntCount++;
	}


	//------------------------------------------
	// PumaHasDied()
	// 
	// Called when puma dies from
	// collision with vehicle
	//------------------------------------------

	public void PumaHasDied(int selectedPuma, bool carKilledFlag)
	{
		lastKillMeatEaten = 0f;
		lastKillCaloriesEaten = 0f;
		lastKillDeerType = "None";
		lastKillExpenses[selectedPuma] = 0f;
		healthPoints[selectedPuma] = 0f;
		carKilledFlags[selectedPuma] = carKilledFlag;
		
		// remember death in 'hunting stats'	
		huntStatsSelectedPuma[huntCount] = selectedPuma;
		huntStatsPumaHealth[huntCount] = 0f;
		huntCount++;
	}

	//------------------------------------------
	// LevelHasChanged()
	// 
	// Called when level changes
	//------------------------------------------

	public void LevelHasChanged()
	{
		// remember level change in 'hunting stats'	
		huntStatsSelectedPuma[huntCount] = 6;
		huntStatsPumaHealth[huntCount] = 0f;
		huntCount++;
	}

	//------------------------------------------
	// ClearLastKillInfo()
	// 
	// Called when leaving the Feeding Display,
	// which is the point where the accumulated
	// expenditures and other data are cleared
	//------------------------------------------

	public void ClearLastKillInfo(int selectedPuma)
	{
		lastKillMeatEaten = 0f;
		lastKillCaloriesEaten = 0f;
		lastKillDeerType = "None";
		lastKillExpenses[selectedPuma] = 0f;
	}

	//===========================================
	//===========================================
	//	GET THE SCORE
	//
	// 	This set of functions is called by the 
	// 	GUI to display the score for the game
	//===========================================
	//===========================================

	
	public void SetScoreReduction(float reductionFactor)
	{
		scoreReductionFactor = reductionFactor;
	}
		
	
	public int GetHuntCount()
	{
		return huntCount;
	}
	
	
	public int GetHuntStatsSelectedPuma(int huntNum)
	{
		return huntStatsSelectedPuma[huntNum];
	}
	
	
	public float GetHuntStatsPumaHealth(int huntNum)
	{
		return huntStatsPumaHealth[huntNum];
	}
	
	
	public int GetHuntSuccessCount()
	{
		return huntSuccessCount;
	}
	
	
	public void SetHuntSuccessCount(int count)
	{
		huntSuccessCount = count;
	}
	
		
	public float GetMeatLevel()
	{
		return meatTotalEaten / meatLimitForLevel;
	}
		
	public float GetPumaHealth(int pumaNum)
	{
		if (pumaNum == -1)
			return -1f;

		float effectiveHealthPoints = healthPoints[pumaNum];

		if (pumaNum == guiManager.selectedPuma && scoreReductionFactor > 0f) {
			effectiveHealthPoints -= lastKillCaloriesEaten * scoreReductionFactor;
			effectiveHealthPoints += lastKillExpenses[guiManager.selectedPuma] * scoreReductionFactor;
		}

		float health = effectiveHealthPoints / maxHealthPoints[pumaNum];
		
		if (health > 0.995f)
			health = 1f;
		
		return (health);
	}

	public float GetPopulationHealth()
	{
		float health = 0f;
		float pumaHealth = 0f;
		int selectedPuma = guiManager.selectedPuma;

		pumaHealth += healthPoints[0];
		if (selectedPuma == 0 && scoreReductionFactor > 0f) {
			pumaHealth -= lastKillCaloriesEaten * scoreReductionFactor;
			pumaHealth += lastKillExpenses[selectedPuma] * scoreReductionFactor;
		}
		pumaHealth /= maxHealthPoints[0]; 
		health += pumaHealth;


		pumaHealth += healthPoints[1];
		if (selectedPuma == 1 && scoreReductionFactor > 0f) {
			pumaHealth -= lastKillCaloriesEaten * scoreReductionFactor;
			pumaHealth += lastKillExpenses[selectedPuma] * scoreReductionFactor;
		}
		pumaHealth /= maxHealthPoints[1]; 
		health += pumaHealth;


		pumaHealth += healthPoints[2];
		if (selectedPuma == 2 && scoreReductionFactor > 0f) {
			pumaHealth -= lastKillCaloriesEaten * scoreReductionFactor;
			pumaHealth += lastKillExpenses[selectedPuma] * scoreReductionFactor;
		}
		pumaHealth /= maxHealthPoints[2]; 
		health += pumaHealth;


		pumaHealth += healthPoints[3];
		if (selectedPuma == 3 && scoreReductionFactor > 0f) {
			pumaHealth -= lastKillCaloriesEaten * scoreReductionFactor;
			pumaHealth += lastKillExpenses[selectedPuma] * scoreReductionFactor;
		}
		pumaHealth /= maxHealthPoints[3]; 
		health += pumaHealth;


		pumaHealth += healthPoints[4];
		if (selectedPuma == 4 && scoreReductionFactor > 0f) {
			pumaHealth -= lastKillCaloriesEaten * scoreReductionFactor;
			pumaHealth += lastKillExpenses[selectedPuma] * scoreReductionFactor;
		}
		pumaHealth /= maxHealthPoints[4]; 
		health += pumaHealth;


		pumaHealth += healthPoints[5];
		if (selectedPuma == 5 && scoreReductionFactor > 0f) {
			pumaHealth -= lastKillCaloriesEaten * scoreReductionFactor;
			pumaHealth += lastKillExpenses[selectedPuma] * scoreReductionFactor;
		}
		pumaHealth /= maxHealthPoints[5]; 
		health += pumaHealth;
		
		
		return (health / 6f);
	}

	public bool WasKilledByCar(int pumaNum)
	{
		return carKilledFlags[pumaNum];
	}

	public float GetLastKillMeatEaten()
	{
		return lastKillMeatEaten;
	}
		
	public float GetLastKillCaloriesEaten()
	{
		return lastKillCaloriesEaten;
	}
	
	public string GetLastKillDeerType()
	{
		return lastKillDeerType;
	}
	
	public float GetLastKillExpense(int selectedPuma)
	{
		return lastKillExpenses[selectedPuma];
	}
	
	public int GetBucksKilled(int selectedPuma)
	{
		return bucksKilled[selectedPuma];
	}

	public int GetDoesKilled(int selectedPuma)
	{
		return doesKilled[selectedPuma];
	}

	public int GetFawnsKilled(int selectedPuma)
	{
		return fawnsKilled[selectedPuma];
	}

	public float GetBuckExpenses(int pumaNum)
	{
		return buckExpenses[pumaNum];
	}
	
	public float GetDoeExpenses(int pumaNum)
	{
		return doeExpenses[pumaNum];
	}
	
	public float GetFawnExpenses(int pumaNum)
	{
		return fawnExpenses[pumaNum];
	}
	
	public float GetBuckCalories(int pumaNum)
	{
		return buckCalories[pumaNum];
	}
	
	public float GetDoeCalories(int pumaNum)
	{
		return doeCalories[pumaNum];
	}
	
	public float GetFawnCalories(int pumaNum)
	{
		return fawnCalories[pumaNum];
	}
}

















